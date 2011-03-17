using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.IO;

namespace Lib.Web.Mvc.JQuery.JqGrid.ModelBinders
{
    internal class JqGridJsonModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");
            if (bindingContext == null)
                throw new ArgumentNullException("bindingContext");

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new Lib.Web.Mvc.JQuery.JqGrid.Serialization.JqGridScriptConverter() });
            
            string jsonString = String.Empty;
            if (controllerContext.RequestContext.HttpContext.Request.ContentType.Contains("application/json"))
            {
                controllerContext.HttpContext.Request.InputStream.Seek(0, SeekOrigin.Begin);
                using (StreamReader jsonReader = new StreamReader(controllerContext.HttpContext.Request.InputStream))
                    jsonString = jsonReader.ReadToEnd();
            }
            else
                jsonString = controllerContext.HttpContext.Request[bindingContext.ModelName];

            if (String.IsNullOrEmpty(jsonString))
                    return null;
                else
                    return serializer.Deserialize(jsonString, bindingContext.ModelType);
        }
    }
}
