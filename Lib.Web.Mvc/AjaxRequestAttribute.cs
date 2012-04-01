using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// Represents an attribute that is used to restrict an action method so that the method handles only AJAX requests.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AjaxRequestAttribute : ActionMethodSelectorAttribute
    {
        #region Fields
        private static readonly AcceptAjaxRequestAttribute _innerAttribute = new AcceptAjaxRequestAttribute(true);
        #endregion

        #region ActionMethodSelectorAttribute Members
        /// <summary>
        /// Determines whether a request is a valid AJAX request.
        /// </summary>
        /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP content, request context, and route data.</param>
        /// <param name="methodInfo">Encapsulates information about a method, such as its type, return type, and arguments.</param>
        /// <returns>True if the request is valid; otherwise, false.</returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return _innerAttribute.IsValidForRequest(controllerContext, methodInfo);
        }
        #endregion
    }
}