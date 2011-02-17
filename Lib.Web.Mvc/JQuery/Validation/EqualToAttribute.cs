using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Lib.Web.Mvc.JQuery.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class EqualToAttribute : ValidationAttribute
    {
        #region Properties
        public string SourcePropertyName { get; set; }
        public string DestinationPropertyName { get; set; }
        public Type ObjectType { get; set; }

        #endregion

        #region Constructor
        public EqualToAttribute(string sourcePropertyName, string destinationPropertyName, Type objectType)
        {
            SourcePropertyName = sourcePropertyName;
            DestinationPropertyName = destinationPropertyName;
            ObjectType = objectType;
        }
        #endregion

        #region Methods
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            if (value.GetType() != ObjectType)
                return true;

            object sourcePropertyValue = ObjectType.GetProperty(SourcePropertyName).GetValue(value, null);
            object destinationPropertyValue = ObjectType.GetProperty(DestinationPropertyName).GetValue(value, null);

            if (!sourcePropertyValue.Equals(destinationPropertyValue))
                return false;
             
            return true;
        }
        #endregion
    }
}
