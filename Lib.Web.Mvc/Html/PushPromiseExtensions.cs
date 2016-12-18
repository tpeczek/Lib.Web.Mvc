using System;
using System.Web;
using System.Web.Mvc;

namespace Lib.Web.Mvc.Html
{
    /// <summary>
    /// Provides support for HTTP/2 Server Push during rendering.
    /// </summary>
    public static class PushPromiseExtensions
    {
        #region Constants
        private const string _linkElement = "link";
        private const string _scriptElement = "script";

        private const string _hrefAttribute = "href";
        private const string _relationAttribute = "rel";
        private const string _sourceAttribute = "src";
        private const string _typeAttribute = "type";
        #endregion

        #region Methods
        /// <summary>
        /// Provides HTTP/2 Server Push support for link elements.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="contentPath">The virtual path of resource.</param>
        /// <param name="relation">The relation (default is stylesheet).</param>
        /// <returns></returns>
        public static IHtmlString PushPromiseLink(this HtmlHelper htmlHelper, string contentPath, string relation = "stylesheet")
        {
            PushPromise(htmlHelper.ViewContext.HttpContext, contentPath);

            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder linkTagBuilder = new TagBuilder(_linkElement);
            linkTagBuilder.Attributes.Add(_relationAttribute, relation);
            linkTagBuilder.Attributes.Add(_hrefAttribute, urlHelper.Content(contentPath));

            return new HtmlString(linkTagBuilder.ToString());
        }

        /// <summary>
        /// Provides HTTP/2 Server Push support for script elements.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="contentPath">The virtual path of resource.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IHtmlString PushPromiseScript(this HtmlHelper htmlHelper, string contentPath, string type = "")
        {
            PushPromise(htmlHelper.ViewContext.HttpContext, contentPath);

            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder scriptTagBuilder = new TagBuilder(_scriptElement);
            scriptTagBuilder.Attributes.Add(_sourceAttribute, urlHelper.Content(contentPath));

            if (!String.IsNullOrWhiteSpace(type))
            {
                scriptTagBuilder.Attributes.Add(_typeAttribute, type);
            }
            
            return new HtmlString(scriptTagBuilder.ToString());
        }

        private static void PushPromise(HttpContextBase httpContext, string contentPath)
        {
            if (httpContext.Request.IsSecureConnection)
            {
                httpContext.Response.PushPromise(contentPath);
            }
        }
        #endregion
    }
}
