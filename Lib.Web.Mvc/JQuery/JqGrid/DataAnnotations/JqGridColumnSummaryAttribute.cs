using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the grouping summary for column.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class JqGridColumnSummaryAttribute: Attribute, IMetadataAware
    {
        #region Properties
        /// <summary>
        /// Gets the grouping summary type.
        /// </summary>
        public JqGridColumnSummaryTypes Type { get; private set; }

        /// <summary>
        /// Gets or sets the grouping summary template.
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Gets or sets the grouping summary function for custom type.
        /// </summary>
        public string Function { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnGroupingSummaryAttribute class.
        /// </summary>
        /// <param name="type">Type of summary</param>
        public JqGridColumnSummaryAttribute(JqGridColumnSummaryTypes type)
        {
            this.Type = type;
            this.Template = "{0}";
        }
        #endregion

        #region IMetadataAware
        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.SetColumnSummaryType(this.Type);
            metadata.SetColumnSummaryTemplate(this.Template);
            metadata.SetColumnSummaryFunction(this.Function);
        }
        #endregion
    }
}
