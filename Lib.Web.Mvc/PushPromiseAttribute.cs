using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// Action filter providing server push promise support
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class PushPromiseAttribute : FilterAttribute, IActionFilter
    {
        #region Fields
        private PushPromiseTable _pushPromiseTable;
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

            // HTTP/2 is supported only over HTTPS
            if (filterContext.HttpContext.Request.IsSecureConnection)
            {
                IEnumerable<string> pushPromiseContentPaths = _pushPromiseTable.GetPushPromiseContentPaths(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);
                foreach (string pushPromiseContentPath in pushPromiseContentPaths)
                {
                    filterContext.HttpContext.Response.PushPromise(pushPromiseContentPath);
                }
            }
        }
        #endregion
    }
}
