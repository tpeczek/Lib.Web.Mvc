using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents additional column options for jqGrid form editing.
    /// </summary>
    public class JqGridColumnFormOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the column position of the element (with the label) in the form (one-based).
        /// </summary>
        public int? ColumnPosition { get; set; }

        /// <summary>
        /// Gets or sets the text or HTML content to appear before the input element.
        /// </summary>
        public string ElementPrefix { get; set; }

        /// <summary>
        /// Gets or sets the text or HTML content to appear after the input element.
        /// </summary>
        public string ElementSuffix { get; set; }

        /// <summary>
        /// Gets or sets the text which will replace the name from ColumnNames as label in the form.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the row position of the element (with the label) in the form (one-based).
        /// </summary>
        public int? RowPosition { get; set; }
        #endregion
    }
}
