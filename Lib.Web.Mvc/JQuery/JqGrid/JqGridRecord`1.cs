using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Globalization;

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
        internal override List<object> GetValuesAsList()
        {
            List<object> values = new List<object>();

            IEnumerable<ModelMetadata> modelMetadata = ModelMetadataProviders.Current.GetMetadataForProperties(Value, typeof(TModel));
            foreach (ModelMetadata propertyMetadata in modelMetadata.Where(p => p.ShowForDisplay && !p.IsComplexType))
            {

                object formattedValue = propertyMetadata.Model;
                if (propertyMetadata.Model == null)
                    formattedValue = propertyMetadata.NullDisplayText;
                else if (!String.IsNullOrEmpty(propertyMetadata.DisplayFormatString))
                    formattedValue = String.Format(CultureInfo.CurrentCulture, propertyMetadata.DisplayFormatString, propertyMetadata.Model);

                values.Add(formattedValue);
            }

            return values;
        }

        internal override Dictionary<string, object> GetValuesAsDictionary()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            IEnumerable<ModelMetadata> modelMetadata = ModelMetadataProviders.Current.GetMetadataForProperties(Value, typeof(TModel));
            foreach (ModelMetadata propertyMetadata in modelMetadata.Where(p => p.ShowForDisplay && !p.IsComplexType))
            {

                object formattedValue = propertyMetadata.Model;
                if (propertyMetadata.Model == null)
                    formattedValue = propertyMetadata.NullDisplayText;
                else if (!String.IsNullOrEmpty(propertyMetadata.DisplayFormatString))
                    formattedValue = String.Format(CultureInfo.CurrentCulture, propertyMetadata.DisplayFormatString, propertyMetadata.Model);

                values.Add(propertyMetadata.PropertyName, formattedValue);
            }

            return values;
        }
        #endregion
    }
}
