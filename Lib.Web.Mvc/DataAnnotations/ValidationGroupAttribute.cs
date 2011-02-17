using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Lib.Web.Mvc.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ValidationGroupAttribute : ValidationAttribute
    {
        #region Properties
        /// <summary>
        /// Name of the validation group
        /// </summary>
        public string GroupName { get; set; }
        #endregion

        #region Constructor
        public ValidationGroupAttribute(string groupName)
        {
            GroupName = groupName;
        }
        #endregion

        #region Methods
        public override bool IsValid(object value)
        {
            //No validation logic, always return true
            return true;
        }
        #endregion
    }
}
