using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid filter toolbar.
    /// </summary>
    public class JqGridFilterToolbarOptions : JqGridFilterOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the default search operator for all fields (it will be overriden by JqGridColumnSearchOptions.SearchOperators).
        /// </summary>
        public JqGridSearchOperators? DefaultSearchOperator { get; set; }

        /// <summary>
        /// Gets or sets the grouping operator for filters.
        /// </summary>
        public JqGridSearchGroupingOperators? GroupingOperator { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if the user needs to press Enter key in text input to trigger search or if searching should triggered after typing any character.
        /// </summary>
        public bool? SearchOnEnter { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if filters should be posted as JSON string (the same as in advanced searching) or as key:value pairs.
        /// </summary>
        public bool? StringResult { get; set; }
        #endregion
    }
}
