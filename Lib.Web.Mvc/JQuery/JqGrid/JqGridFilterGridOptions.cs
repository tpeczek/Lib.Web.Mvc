using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid filter grid.
    /// </summary>
    public class JqGridFilterGridOptions : JqGridFilterOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the type of the filter grid.
        /// </summary>
        public JqGridFilterGridTypes? Type { get; set; }

        /// <summary>
        /// Gets or sets the name of CSS class which will be applied to the buttons.
        /// </summary>
        public string ButtonsClass { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if clear button is enabled.
        /// </summary>
        public bool? ClearEnabled { get; set; }

        /// <summary>
        /// Gets or sets the text for clear button.
        /// </summary>
        public string ClearText { get; set; }

        /// <summary>
        /// Gets or sets the name of CSS class which will be applied to the form.
        /// </summary>
        public string FormClass { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if after a search every column to which search is applied is marked as searchable.
        /// </summary>
        public bool? MarkSearched { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if search button is enabled.
        /// </summary>
        public bool? SearchEnabled { get; set; }

        /// <summary>
        /// Gets or sets the text for search button.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets or sets the name of CSS class which will be applied to the table.
        /// </summary>
        public string TableClass { get; set; }

        /// <summary>
        /// Gets or sets a separate url for searching values.
        /// </summary>
        public string Url { get; set; }
        #endregion
    }
}
