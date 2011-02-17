using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.Validation
{
    public class EqualToValidator : DataAnnotationsModelValidator<EqualToAttribute>
    {
        #region Fields
        string _sourcePropertyName;
        string _destinationPropertyName;
        string _errorMessage;
        #endregion 

        #region Constructor
        public EqualToValidator(ModelMetadata metadata, ControllerContext context, EqualToAttribute attribute)
            : base(metadata, context, attribute) 
        {
            _sourcePropertyName = attribute.SourcePropertyName;
            _destinationPropertyName = attribute.DestinationPropertyName;
            _errorMessage = attribute.ErrorMessage;
        }
        #endregion

        #region Methods
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            var validatioRule = new ModelClientValidationRule
            {
                ErrorMessage = _errorMessage,
                ValidationType = "equalTo"
            };
            validatioRule.ValidationParameters.Add("other", _sourcePropertyName);

            return new[] { validatioRule };

        }
        #endregion
    }
}
