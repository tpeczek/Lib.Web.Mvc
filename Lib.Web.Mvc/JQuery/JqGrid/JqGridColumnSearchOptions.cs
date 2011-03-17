using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid searchable column
    /// </summary>
    public class JqGridColumnSearchOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the URL to get the AJAX data for the select element (if SearchType is JqGridColumnSearchTypes.Select)
        /// </summary>
        public string DataUrl { get; set; }

        /// <summary>
        /// Gets or sets the default value in the search input element.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if hidden column can be searched.
        /// </summary>
        public bool? SearchHidden { get; set; }

        /// <summary>
        /// Gets or sets the available search operators for the column.
        /// </summary>
        public JqGridSearchOperators? SearchOperators { get; set; }
        #endregion
    }
}
