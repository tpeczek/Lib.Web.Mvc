using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents record for jqGrid.
    /// </summary>
    public class JqGridRecord
    {
        #region Properties
        /// <summary>
        /// Gets the record cells values.
        /// </summary>
        public List<object> Values { get; private set; }

        /// <summary>
        /// Gets the record value.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Gets the record identifier.
        /// </summary>
        public string Id { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridRecord class.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <param name="values">The record cells values.</param>
        public JqGridRecord(string id, List<object> values)
        {
            Id = id;
            Values = values;
            Value = null;
        }

        /// <summary>
        /// Initializes a new instance of the JqGridRecord class.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <param name="value">The record values</param>
        public JqGridRecord(string id, object value)
        {
            Id = id;
            Value = value;
            Values = GetValuesAsList();
        }
        #endregion

        #region Methods
        protected internal virtual List<object> GetValuesAsList()
        {
            List<object> values = new List<object>();

            if (Value != null)
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(Value))
                    values.Add(property.GetValue(Value));
            }

            return values;
        }

        protected internal virtual Dictionary<string, object> GetValuesAsDictionary()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            if (Value != null)
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(Value))
                    values.Add(property.Name, property.GetValue(Value));
            }

            return values;
        }
        #endregion
    }
}
