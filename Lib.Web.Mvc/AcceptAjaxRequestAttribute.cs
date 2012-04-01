using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// Represents an attribute that specifies if action method will respond to AJAX request or not.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AcceptAjaxRequestAttribute : ActionMethodSelectorAttribute
    {
        #region Properties
        /// <summary>
        /// Gets value defining if action method will respond to AJAX request or not.
        /// </summary>
        public bool Accept { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the AcceptAjaxRequestAttribute class.
        /// </summary>
        /// <param name="accept">Defines if action method will respond to AJAX request or not.</param>
        public AcceptAjaxRequestAttribute(bool accept)
        {
            Accept = accept;
        }
        #endregion

        #region ActionMethodSelectorAttribute Members
        /// <summary>
        /// Determines whether a request is a valid request.
        /// </summary>
        /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP content, request context, and route data.</param>
        /// <param name="methodInfo">Encapsulates information about a method, such as its type, return type, and arguments.</param>
        /// <returns>True if the request is valid; otherwise, false.</returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");

            bool isAjaxRequest = (controllerContext.HttpContext.Request["X-Requested-With"] == "XMLHttpRequest") || ((controllerContext.HttpContext.Request.Headers != null) && (controllerContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest"));

            return Accept == isAjaxRequest;
        }
        #endregion
    }
}