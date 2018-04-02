﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the label in the header for the column.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class JqGridColumnLabelAttribute : Attribute, IMetadataAware
    {
        #region Properties
        /// <summary>
        /// Gets or sets the content of the label (if empty string the content will not be changed).
        /// </summary>
        public string Label
        {
            get => LabelOptions.Label;
            set => LabelOptions.Label = value;
        }

        /// <summary>
        /// Gets or sets the additional class for the label.
        /// </summary>
        public string Class
        {
            get => LabelOptions.Class;
            set => LabelOptions.Class = value;
        }

        /// <summary>
        /// When overriden in delivered class, provides a dictionary where keys should be CSS styles for the label (will be applied only if Class is null or empty string).
        /// </summary>
        protected virtual IDictionary<string, object> CssStyles => null;

        /// <summary>
        /// When overriden in delivered class, provides a dictionary where keys should be valid attributes for the label.
        /// </summary>
        protected virtual IDictionary<string, object> HtmlAttributes => null;

        private JqGridColumnLabelOptions LabelOptions { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnLabelAttribute class.
        /// </summary>
        public JqGridColumnLabelAttribute()
        {
            LabelOptions = new JqGridColumnLabelOptions();
        }
        #endregion

        #region IMetadataAware
        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            LabelOptions.CssStyles = CssStyles;
            LabelOptions.HtmlAttributes = HtmlAttributes;

            metadata.SetColumnLabelOptions(LabelOptions);
        }
        #endregion
    }
}
