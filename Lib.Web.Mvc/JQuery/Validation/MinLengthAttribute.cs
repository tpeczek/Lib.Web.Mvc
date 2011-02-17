using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Lib.Web.Mvc.JQuery.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MinLengthAttribute : ValidationAttribute
    {
        #region Properties
        public int MinLength { get; set; }
        #endregion

        #region Constructor
        public MinLengthAttribute(int minLength)
        {
            MinLength = minLength;
        }
        #endregion

        #region Methods
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            if ((value as String).Length < MinLength)
                return false;
             
            return true;
        }
        #endregion
    }
}
