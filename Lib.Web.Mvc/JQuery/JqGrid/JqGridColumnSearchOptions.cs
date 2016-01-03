namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid searchable column
    /// </summary>
    public class JqGridColumnSearchOptions: JqGridColumnElementOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the value which defines if Clear ("X") button is available at the end of search field in jqGrid filter toolbar.
        /// </summary>
        public bool ClearSearch { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if hidden column can be searched.
        /// </summary>
        public bool SearchHidden { get; set; }

        /// <summary>
        /// Gets or sets the available search operators for the column.
        /// </summary>
        public JqGridSearchOperators SearchOperators { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnSearchOptions class.
        /// </summary>
        public JqGridColumnSearchOptions()
        {
            BuildSelect = null;
            ClearSearch = true;
            DataEvents = null;
            DataInit = null;
            DataUrl = null;
            DefaultValue = null;
            HtmlAttributes = null;
            SearchHidden = false;
            SearchOperators = (JqGridSearchOperators)32768;
        }
        #endregion
    }
}
