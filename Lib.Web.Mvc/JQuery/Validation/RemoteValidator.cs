using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Lib.Web.Mvc.JQuery.Validation
{
    public class RemoteValidator : DataAnnotationsModelValidator<RemoteAttribute>
    {
        #region Fields
        string _actionName;
        string _controllerName;
        string _errorMessage;
        #endregion

        #region Constructor
        public RemoteValidator(ModelMetadata metadata, ControllerContext context, RemoteAttribute attribute)
            : base(metadata, context, attribute) 
        {
            _actionName = attribute.ActionName;
            _controllerName = attribute.ControllerName;
            _errorMessage = attribute.ErrorMessage;
        }
        #endregion

        #region Methods
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            var validatioRule = new ModelClientValidationRule
            {
                ErrorMessage = _errorMessage,
                ValidationType = "remote"
            };
            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            validatioRule.ValidationParameters.Add("options", url.Action(_actionName, _controllerName));

            return new[] { validatioRule };

        }
        #endregion
    }
}
