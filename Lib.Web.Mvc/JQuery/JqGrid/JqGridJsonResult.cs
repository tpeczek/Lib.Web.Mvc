using System;
using System.Web.Mvc;
using System.Web;
using System.Web.Script.Serialization;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Represents a class that is used to send object from Lib.Web.Mvc.JQuery.JqGrid namespace as JSON-formatted content to the response, converted the way jqGrid expects it.
    /// </summary>
    public class JqGridJsonResult : JsonResult
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridJsonResult class.
        /// </summary>
        public JqGridJsonResult()
            : base()
        { }
        #endregion

        #region JsonResult Members
        /// <summary>
        /// When called by the action invoker, writes the JSON formatted result to the response.
        /// </summary>
        /// <param name="context">The context that the result is executed in.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
                response.ContentType = ContentType;
            else
                response.ContentType = "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.RegisterConverters(new JavaScriptConverter[] { new Lib.Web.Mvc.JQuery.JqGrid.Serialization.JqGridScriptConverter() });


                response.Write(serializer.Serialize(Data));
            }
        }
        #endregion
    }
}
