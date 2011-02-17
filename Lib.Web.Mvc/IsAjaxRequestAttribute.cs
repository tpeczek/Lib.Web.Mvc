using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// Action filter which checks if request is Ajax
    /// </summary>
    public class IsAjaxRequestAttribute : FilterAttribute, IActionFilter
    {
        #region Fields
        private string _actionParameterName;
        #endregion

        #region Constructor
        public IsAjaxRequestAttribute(string actionParameterName)
        {
            _actionParameterName = actionParameterName;
        }
        #endregion

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
            filterContext.ActionParameters[_actionParameterName] = filterContext.HttpContext.Request.IsAjaxRequest();
        }
        #endregion
    }
}
