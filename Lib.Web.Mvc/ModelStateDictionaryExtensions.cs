using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// Provides extension methods for ModelStateDictionary
    /// </summary>
    public static class ModelStateDictionaryExtensions
    {
        /// <summary>
        /// Returns model state errors in list
        /// </summary>
        /// <param name="modelState">Model state</param>
        /// <returns>Model state errors</returns>
        public static List<string> GetModelErrors(this ModelStateDictionary modelState)
        {
            List<string> errors = new List<string>();
            if (!modelState.IsValid)
            {
                foreach (ModelState state in modelState.Values)
                {
                    foreach (ModelError error in state.Errors)
                        errors.Add(error.ErrorMessage);
                }
            }
            return errors;
        }
    }
}
