using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// Action filter which disables response caching
    /// </summary>
    public class NoCacheAttribute : FilterAttribute, IActionFilter
    {
        #region IActionFilter Members
        /// <summary>
        /// Called after the action method executes
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            return;
        }

        /// <summary>
        /// Called before an action method executes
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
        }
        #endregion
    }
}
