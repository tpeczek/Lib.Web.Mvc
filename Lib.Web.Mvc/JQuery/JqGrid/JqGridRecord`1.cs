using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Globalization;
using System.Data.Linq;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents record for jqGrid.
    /// </summary>
    /// <typeparam name="TModel">Type of model for this grid</typeparam>
    public class JqGridRecord<TModel>: JqGridRecord
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridRecord class.
        /// </summary>
        /// <param name="id">The record identifier</param>
        /// <param name="value">The value for record</param>
        public JqGridRecord(string id, TModel value)
            : base(id, value)
        { }
        #endregion

        #region Methods
        protected internal override List<object> GetValuesAsList()
        {
            List<object> values = new List<object>();

            IEnumerable<ModelMetadata> modelMetadata = ModelMetadataProviders.Current.GetMetadataForProperties(Value, typeof(TModel)).OrderBy(m => m.Order);
            foreach (ModelMetadata propertyMetadata in modelMetadata.Where(p => p.IsValidForColumn()))
                values.Add(GetFormattedValue(propertyMetadata));

            return values;
        }

        protected internal override Dictionary<string, object> GetValuesAsDictionary()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            IEnumerable<ModelMetadata> modelMetadata = ModelMetadataProviders.Current.GetMetadataForProperties(Value, typeof(TModel));
            foreach (ModelMetadata propertyMetadata in modelMetadata.Where(p => p.IsValidForColumn()))
                values.Add(propertyMetadata.PropertyName, GetFormattedValue(propertyMetadata));

            return values;
        }

        private static object GetFormattedValue(ModelMetadata propertyMetadata)
        {
            object formattedValue = propertyMetadata.Model;
                
            if (propertyMetadata.Model == null)
                formattedValue = propertyMetadata.NullDisplayText;
            else
            {
                if (propertyMetadata.IsComplexType)
                {
                    if (propertyMetadata.ModelType == typeof(Binary))
                        formattedValue = Convert.ToBase64String((propertyMetadata.Model as Binary).ToArray());
                    else if (propertyMetadata.ModelType == typeof(byte[]))
                        formattedValue = Convert.ToBase64String(propertyMetadata.Model as byte[]);
                }
                else if (!String.IsNullOrEmpty(propertyMetadata.DisplayFormatString))
                    formattedValue = String.Format(CultureInfo.CurrentCulture, propertyMetadata.DisplayFormatString, propertyMetadata.Model);
            }
            return formattedValue;
        }
        #endregion
    }
}
