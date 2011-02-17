using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// Model binder which allows binding JSON data to objects
    /// </summary>
    public class JsonModelBinder : IModelBinder
    {
        #region IModelBinder Members
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");
            if (bindingContext == null)
                throw new ArgumentNullException("bindingContext");

            var serializer = new DataContractJsonSerializer(bindingContext.ModelType);
            return serializer.ReadObject(controllerContext.HttpContext.Request.InputStream);
        }
        #endregion
    }
}
