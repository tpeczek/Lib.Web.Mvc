using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.Validation
{
    public class EmailValidator : DataAnnotationsModelValidator<EmailAttribute>
    {
        #region Fields
        string _errorMessage;
        #endregion

        #region Constructor
        public EmailValidator(ModelMetadata metadata, ControllerContext context, EmailAttribute attribute)
            : base(metadata, context, attribute) 
        {
            _errorMessage = attribute.ErrorMessage;
        }
        #endregion

        #region Methods
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] {
                new ModelClientValidationRule
                {
                    ErrorMessage = _errorMessage,
                    ValidationType = "email"
                }
            };
        }
        #endregion
    }
}
