using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.Validation
{
    public class MinLengthValidator : DataAnnotationsModelValidator<MinLengthAttribute>
    {
        #region Fields
        int _minLength;
        string _errorMessage;
        #endregion

        #region Constructor
        public MinLengthValidator(ModelMetadata metadata, ControllerContext context, MinLengthAttribute attribute)
            : base(metadata, context, attribute) 
        {
            _minLength = attribute.MinLength;
            _errorMessage = attribute.ErrorMessage;
        }
        #endregion

        #region Methods
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            var validatioRule = new ModelClientValidationRule
            {
                ErrorMessage = _errorMessage,
                ValidationType = "minlength"
            };
            validatioRule.ValidationParameters.Add("length", _minLength);

            return new[] { validatioRule };
        }
        #endregion
    }
}
