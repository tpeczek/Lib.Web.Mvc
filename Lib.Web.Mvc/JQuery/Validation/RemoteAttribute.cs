using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Lib.Web.Mvc.JQuery.Validation
{
    public class RemoteValidatingEventArgs : EventArgs
    {
        #region Properties
        public string Value;
        public bool Valid;
        public string ErrorMessage;
        #endregion
    }

    public delegate void RemoteValidatingEventHandler(object sender, RemoteValidatingEventArgs e);

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class RemoteAttribute : ValidationAttribute
    {
        #region Properties
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public Type ValidatorType { get; set; }
        public string ValidationMethodName { get; set; }
        #endregion

        #region Constructor
        public RemoteAttribute(string actionName, string controllerName, Type validatorType, string validationMethodName)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            ValidatorType = validatorType;
            ValidationMethodName = validationMethodName;
        }
        #endregion

        #region Methods
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            MethodInfo validationMethodInfo = ValidatorType.GetMethod(ValidationMethodName);
            object validatorObject = ValidatorType.GetConstructor(Type.EmptyTypes).Invoke(null);
            if (validationMethodInfo.ReturnType == typeof(string))
            {
                ErrorMessage = (string)validationMethodInfo.Invoke(validatorObject, new[] { value });
                return String.IsNullOrEmpty(ErrorMessage);
            }
            else if (validationMethodInfo.ReturnType == typeof(bool))
                return (bool)validationMethodInfo.Invoke(validatorObject, new[] { value });
            else
                return false;
        }
        #endregion
    }
}
