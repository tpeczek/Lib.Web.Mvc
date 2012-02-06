using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid Navigator search action.
    /// </summary>
    public class JqGridNavigatorSearchActionOptions : JqGridNavigatorActionOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the function for event which is raised every time after the search dialog is shown.
        /// </summary>
        public string AfterShowSearch { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised every time before the search dialog is shown.
        /// </summary>
        public string BeforeShowSearch { get; set; }

        /// <summary>
        /// Gets or sets the caption for the dialog.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if search action dialog should be closed after searching.
        /// </summary>
        public bool CloseAfterSearch { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if search action dialog should be closed after reseting.
        /// </summary>
        public bool CloseAfterReset { get; set; }

        /// <summary>
        /// Gets or sets the text for search button.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if advanced searching is enabled.
        /// </summary>
        public bool AdvancedSearching { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if advanced searching with a possibilities to define a complex condfitions is enabled.
        /// </summary>
        public bool AdvancedSearchingWithGroups { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if added row (in advanced searching) should be copied from previous row.
        /// </summary>
        public bool CloneSearchRowOnAdd { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised once when the dialog is created.
        /// </summary>
        public string OnInitializeSearch { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the entry filter should be destroyed unbinding all the events and then constructed again.
        /// </summary>
        public bool RecreateFilter { get; set; }

        /// <summary>
        /// Gets or sets the text for reset button.
        /// </summary>
        public string ResetText { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the query which is generated when the user defines the conditions for the search should be shown.
        /// </summary>
        public bool ShowQuery { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNavigatorSearchActionOptions class.
        /// </summary>
        public JqGridNavigatorSearchActionOptions()
            : base()
        {
            Width = 450;
            AfterShowSearch = null;
            BeforeShowSearch = null;
            Caption = null;
            CloseAfterSearch = false;
            CloseAfterReset = false;
            SearchText = null;
            AdvancedSearching = false;
            AdvancedSearchingWithGroups = false;
            CloneSearchRowOnAdd = true;
            OnInitializeSearch = null;
            RecreateFilter = false;
            ResetText = null;
            ShowQuery = false;
        }
        #endregion
    }
}
