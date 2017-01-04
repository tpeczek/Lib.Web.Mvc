using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;
using Lib.Web.Mvc.Http;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// Action filter providing server push promise support
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class PushPromiseAttribute : FilterAttribute, IActionFilter
    {
        #region Constants
        private const int _defaultCacheDigestMaxAge = 31536000;
        #endregion

        #region Fields
        private PushPromiseTable _pushPromiseTable;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets value indicating if cookie based cache-digest implementation should be used.
        /// </summary>
        public bool UseCookieBasedCacheDigest { get; set; }

        /// <summary>
        /// Gets or sets probability of hashes collision for cookie based cache-digest implementation.
        /// </summary>
        public uint CacheDigestProbability { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if validators are to be included for cookie based cache-digest implementation.
        /// </summary>
        public bool CacheDigestValidators { get; set; }

        /// <summary>
        /// Gets or sets max age for cache-digest cookie (in seconds).
        /// </summary>
        public int CacheDigestMaxAge { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes new instance of PushPromiseAttribute
        /// </summary>
        /// <param name="pushPromiseTable">The push promises table.</param>
        public PushPromiseAttribute(PushPromiseTable pushPromiseTable)
        {
            if (pushPromiseTable == null)
            {
                throw new ArgumentNullException(nameof(pushPromiseTable));
            }

            _pushPromiseTable = pushPromiseTable;

            UseCookieBasedCacheDigest = false;
            CacheDigestProbability = CacheDigestValue.DefaultProbability;
            CacheDigestValidators = false;
            CacheDigestMaxAge = _defaultCacheDigestMaxAge;
        }
        #endregion

        #region IActionFilter Members
        /// <summary>
        /// Called after the action method executes.
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        { }

        /// <summary>
        /// Called before an action method executes.
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            // HTTP/2 is currently supported only over HTTPS
            if (filterContext.HttpContext.Request.IsSecureConnection)
            {
                CacheDigestHeaderValue cacheDigest = GetCacheDigest(filterContext.HttpContext.Request);

                IDictionary<string, string> cacheDigestUrls = PushPromises(filterContext, cacheDigest);

                SetCacheDigest(filterContext.HttpContext, cacheDigestUrls);
            }
        }
        #endregion

        #region Methods
        private CacheDigestHeaderValue GetCacheDigest(HttpRequestBase request)
        {
            CacheDigestHeaderValue cacheDigest = null;

            if (UseCookieBasedCacheDigest)
            {
                cacheDigest = (request.Cookies[RequestHeaders.CacheDigest] != null) ? new CacheDigestHeaderValue(CacheDigestValue.FromBase64String(request.Cookies[RequestHeaders.CacheDigest].Value), validators: CacheDigestValidators) : null;
            }
            else
            {
                cacheDigest = (request.Headers[RequestHeaders.CacheDigest] != null) ? new CacheDigestHeaderValue(request.Headers[RequestHeaders.CacheDigest]) : null;
            }

            return cacheDigest;
        }

        private void SetCacheDigest(HttpContextBase httpContext, IDictionary<string, string> cacheDigestUrls)
        {
            if (UseCookieBasedCacheDigest)
            {
                HttpCookie cacheDigestCookie = new HttpCookie(RequestHeaders.CacheDigest)
                {
                    Value = (cacheDigestUrls != null) ? CacheDigestValue.FromUrls(cacheDigestUrls, CacheDigestProbability, CacheDigestValidators).ToBase64String() : httpContext.Request.Cookies[RequestHeaders.CacheDigest].Value,
                    Expires = DateTime.Now.AddSeconds(CacheDigestMaxAge)
                };
                httpContext.Response.Cookies.Set(cacheDigestCookie);
            }
        }

        private IDictionary<string, string> PushPromises(ActionExecutingContext filterContext, CacheDigestHeaderValue cacheDigest)
        {
            bool newCacheDigestValueNeeded = false;
            IDictionary<string, string> cacheDigestUrls = new Dictionary<string, string>();

            IEnumerable<PushPromiseTable.Entry> pushPromiseEntries = _pushPromiseTable.GetPushPromiseEntries(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);
            foreach (PushPromiseTable.Entry pushPromiseEntry in pushPromiseEntries)
            {
                pushPromiseEntry.AbsoluteUrl = pushPromiseEntry.AbsoluteUrl ?? GetAbsolutePushPromiseContentUrl(filterContext.RequestContext, pushPromiseEntry.ContentPath);

                if (!(cacheDigest?.QueryDigest(pushPromiseEntry.AbsoluteUrl, pushPromiseEntry.EntityTag) ?? false))
                {
                    filterContext.HttpContext.Response.PushPromise(pushPromiseEntry.ContentPath);
                    newCacheDigestValueNeeded = newCacheDigestValueNeeded || pushPromiseEntry.TrackInCacheDigest;
                }

                if (UseCookieBasedCacheDigest && pushPromiseEntry.TrackInCacheDigest)
                {
                    cacheDigestUrls.Add(pushPromiseEntry.AbsoluteUrl, pushPromiseEntry.EntityTag);
                }
            }

            return newCacheDigestValueNeeded ? cacheDigestUrls : null;
        }

        private string GetAbsolutePushPromiseContentUrl(RequestContext requestContext, string pushPromiseContentPath)
        {
            UrlHelper urlHelper = new UrlHelper(requestContext);

            return (new Uri(requestContext.HttpContext.Request.Url, urlHelper.Content(pushPromiseContentPath))).AbsoluteUri;
        }
        #endregion
    }
}
