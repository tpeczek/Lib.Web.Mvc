using System;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the grouping summary for column.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class JqGridColumnSummaryAttribute : Attribute, IMetadataAware
    {
        #region Fields
        private string _template;

        #endregion

        #region Properties
        /// <summary>
        /// Gets the grouping summary type.
        /// </summary>
        public JqGridColumnSummaryTypes Type { get; private set; }

        internal bool IsTemplateSetted { get; private set; }
        /// <summary>
        /// Gets or sets the grouping summary template.
        /// </summary>
        public string Template
        {
            get => _template;
            set
            {
                _template = value;
                IsTemplateSetted = true;
            }
        }

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
            Type = type;
        }
        #endregion

        #region IMetadataAware
        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.SetColumnSummaryType(Type);
            metadata.SetColumnSummaryTemplate(new SettedString(IsTemplateSetted, Template));
            metadata.SetColumnSummaryFunction(Function);
        }
        #endregion
    }
}
