using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the mapping properties for the column.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class JqGridColumnMappingAttribute : Attribute, IMetadataAware
    {
        #region Properties
        /// <summary>
        /// Gets or sets the JSON mapping for the column in the incoming JSON string.
        /// </summary>
        public string JsonMapping { get; set; }

        /// <summary>
        /// Defines if this column value should be used as unique row id (in case there is no id from the server). 
        /// </summary>
        public bool Key { get; set; }

        /// <summary>
        /// Gets or sets the XML mapping for the column in the incomming XML file.
        /// </summary>
        public string XmlMapping { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnMappingAttribute class.
        /// </summary>
        public JqGridColumnMappingAttribute()
        {
            JsonMapping = null;
            Key = false;
            XmlMapping = null;
        }
        #endregion

        #region IMetadataAware
        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.SetColumnJsonMapping(JsonMapping);
            metadata.SetColumnKey(Key);
            metadata.SetColumnXmlMapping(XmlMapping);
        }
        #endregion
    }
}
