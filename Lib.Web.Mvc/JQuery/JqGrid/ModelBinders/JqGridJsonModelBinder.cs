using System;
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
				throw new ArgumentNullException(nameof(controllerContext));
			if (bindingContext == null)
				throw new ArgumentNullException(nameof(bindingContext));

			var serializer = new JavaScriptSerializer();
			serializer.RegisterConverters(new JavaScriptConverter[] { new Serialization.JqGridScriptConverter() });

			string jsonString;
			if (controllerContext.RequestContext.HttpContext.Request.ContentType.Contains("application/json"))
			{
				controllerContext.HttpContext.Request.InputStream.Seek(0, SeekOrigin.Begin);
				using (var jsonReader = new StreamReader(controllerContext.HttpContext.Request.InputStream))
					jsonString = jsonReader.ReadToEnd();
			}
			else
				jsonString = controllerContext.HttpContext.Request[bindingContext.ModelName];

			if (string.IsNullOrEmpty(jsonString))
				return null;
			return serializer.Deserialize(jsonString, bindingContext.ModelType);
		}
	}
}
