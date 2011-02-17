using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc.DataAnnotations
{
    public class ValidationGroupValidator : DataAnnotationsModelValidator<ValidationGroupAttribute>
    {
        #region Fields
        string _groupName;
        #endregion

        #region Constructor
        public ValidationGroupValidator(ModelMetadata metadata, ControllerContext context, ValidationGroupAttribute attribute)
            : base(metadata, context, attribute) 
        {
            _groupName = attribute.GroupName;
        }
        #endregion

        #region Methods
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            //Add informations about validation group to metadata
            var validatioRule = new ModelClientValidationRule
            {
                ErrorMessage = String.Empty,
                ValidationType = "validationGroup"
            };
            validatioRule.ValidationParameters.Add("groupName", _groupName);

            return new[] { validatioRule };
        }
        #endregion
    }
}
