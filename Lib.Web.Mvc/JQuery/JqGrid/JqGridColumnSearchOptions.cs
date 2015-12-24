namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid searchable column
    /// </summary>
    public class JqGridColumnSearchOptions: JqGridColumnElementOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the value which defines if hidden column can be searched.
        /// </summary>
        public bool SearchHidden { get; set; }

        /// <summary>
        /// Gets or sets the available search operators for the column.
        /// </summary>
        public JqGridSearchOperators SearchOperators { get; set; }

				/// <summary>
				/// Available search operators for the column with the ability to specify their order.
				/// </summary>
				public JqGridSearchOperators[] SearchOrderedOperators { get; set; }
				
				/// <summary>
				/// When set to false the X icon at end of search field which is responsible to clear
				/// the search data is disabled. the default value is true.
				/// </summary>
				public bool ClearSearch { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnSearchOptions class.
        /// </summary>
        public JqGridColumnSearchOptions()
        {
            BuildSelect = null;
            DataEvents = null;
            DataInit = null;
            DataUrl = null;
            DefaultValue = null;
            HtmlAttributes = null;
            SearchHidden = false;
						ClearSearch = true;
            SearchOperators = (JqGridSearchOperators)32768;
        }
        #endregion
    }
}
