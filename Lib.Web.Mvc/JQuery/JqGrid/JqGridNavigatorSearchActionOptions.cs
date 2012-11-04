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
        /// Gets or sets the function for event which is raised every time the filter is redrawed .
        /// </summary>
        public string AfterRedraw { get; set; }

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
        /// Gets or sets the value indicating if the SearchRules should be validated.
        /// </summary>
        public bool ErrorCheck { get; set; }

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
        /// Gets or sets the function for event which is raised when the reset button is activated.
        /// </summary>
        public string OnReset { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised when the search button is activated.
        /// </summary>
        public string OnSearch { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the entry filter should be destroyed unbinding all the events and then constructed again.
        /// </summary>
        public bool RecreateFilter { get; set; }

        /// <summary>
        /// Gets or sets the text for reset button.
        /// </summary>
        public string ResetText { get; set; }

        /// <summary>
        /// Gets or sets the available search operators for the searching (if not defined on column).
        /// </summary>
        public JqGridSearchOperators? SearchOperators { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the dialog should appear automatically when the grid is constructed for first time.
        /// </summary>
        public bool ShowOnLoad { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the query which is generated when the user defines the conditions for the search should be shown.
        /// </summary>
        public bool ShowQuery { get; set; }

        /// <summary>
        /// Gets or sets the filters templates for advanced searching and avanced searching with groups.
        /// </summary>
        public IDictionary<string, JqGridRequestSearchingFilters> Templates { get; set; }

        /// <summary>
        /// Gets or sets the valid DOM id of the element into which the filter is inserted as child.
        /// </summary>
        public string Layer { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNavigatorSearchActionOptions class.
        /// </summary>
        public JqGridNavigatorSearchActionOptions()
            : base()
        {
            Width = 450;
            AfterRedraw = null;
            AfterShowSearch = null;
            BeforeShowSearch = null;
            Caption = null;
            CloseAfterSearch = false;
            CloseAfterReset = false;
            ErrorCheck = true;
            SearchText = null;
            AdvancedSearching = false;
            AdvancedSearchingWithGroups = false;
            CloneSearchRowOnAdd = true;
            OnInitializeSearch = null;
            OnReset = null;
            OnSearch = null;
            RecreateFilter = false;
            ResetText = null;
            SearchOperators = null;
            ShowOnLoad = false;
            ShowQuery = false;
            Templates = null;
            Layer = null;
        }
        #endregion
    }
}
