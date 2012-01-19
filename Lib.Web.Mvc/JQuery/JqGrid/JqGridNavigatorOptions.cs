using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid Navigator.
    /// </summary>
    public class JqGridNavigatorOptions : JqGridNavigatorOptionsBase
    {
        #region Properties
        /// <summary>
        /// Gets or sets the pager to which the Navigator should be attached to (jqGrid can have only one Navigator).
        /// </summary>
        public JqGridNavigatorPagers Pager { get; set; }

        /// <summary>
        /// Gets or sets the caption for warning which appears when user try to edit, delete or view a row without selecting it.
        /// </summary>
        public string AlertCaption { get; set; }

        /// <summary>
        /// Gets or sets the text for warning which appears when user try to edit, delete or view a row without selecting it.
        /// </summary>
        public string AlertText { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if all the actions from the bottom pager should be coppied to the top pager.
        /// </summary>
        public bool CloneToTop { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if the warning dialog can be closed with ESC key.
        /// </summary>
        public bool CloseOnEscape { get; set; }
        
        /// <summary>
        /// Gets or set the value which defines if delete action is enabled (default true).
        /// </summary>
        public bool Delete { get; set; }

        /// <summary>
        /// Gets or sets the icon (form UI theme images) for delete action.
        /// </summary>
        public string DeleteIcon { get; set; }

        /// <summary>
        /// Gets or sets the text for delete action.
        /// </summary>
        public string DeleteText { get; set; }

        /// <summary>
        /// Gets or sets the tooltip for delete action.
        /// </summary>
        public string DeleteToolTip { get; set; }

        /// <summary>
        /// Gets or set the value which defines if refresh action is enabled (default true).
        /// </summary>
        public bool Refresh { get; set; }

        /// <summary>
        /// Gets or sets the icon (form UI theme images) for refresh action.
        /// </summary>
        public string RefreshIcon { get; set; }

        /// <summary>
        /// Gets or sets the text for refresh action.
        /// </summary>
        public string RefreshText { get; set; }

        /// <summary>
        /// Gets or sets the tooltip for refresh action.
        /// </summary>
        public string RefreshToolTip { get; set; }

        /// <summary>
        /// Gets or sets the mode for refresh action.
        /// </summary>
        public JqGridRefreshModes RefreshMode { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after the refresh button is clicked.
        /// </summary>
        public string AfterRefresh { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before the refresh button is clicked.
        /// </summary>
        public string BeforeRefresh { get; set; } 

        /// <summary>
        /// Gets or set the value which defines if search action is enabled (default true).
        /// </summary>
        public bool Search { get; set; }

        /// <summary>
        /// Gets or sets the icon (form UI theme images) for search action.
        /// </summary>
        public string SearchIcon { get; set; }

        /// <summary>
        /// Gets or sets the text for search action.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets or sets the tooltip for search action.
        /// </summary>
        public string SearchToolTip { get; set; }

        /// <summary>
        /// Gets or set the value which defines if view action is enabled (default true).
        /// </summary>
        public bool View { get; set; }

        /// <summary>
        /// Gets or sets the icon (form UI theme images) for view action.
        /// </summary>
        public string ViewIcon { get; set; }

        /// <summary>
        /// Gets or sets the text for view action.
        /// </summary>
        public string ViewText { get; set; }

        /// <summary>
        /// Gets or sets the tooltip for view action.
        /// </summary>
        public string ViewToolTip { get; set; }

        /// <summary>
        /// Gets or sets the custom function to replace the build in add function.
        /// </summary>
        public string AddFunction { get; set; }

        /// <summary>
        /// Gets or sets the custom function to replace the build in edit function.
        /// </summary>
        public string EditFunction { get; set; }

        /// <summary>
        /// Gets or sets the custom function to replace the build in delete function.
        /// </summary>
        public string DeleteFunction { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNavigatorOptions class.
        /// </summary>
        public JqGridNavigatorOptions()
            : base()
        {
            Pager = JqGridNavigatorPagers.Standard;
            AlertCaption = JqGridNavigatorDefaults.AlertCaption;
            AlertText = JqGridNavigatorDefaults.AlertText;
            CloneToTop = false;
            CloseOnEscape = true;
            Delete = true;
            DeleteIcon = JqGridNavigatorDefaults.DeleteIcon;
            DeleteText = String.Empty;
            DeleteToolTip = JqGridNavigatorDefaults.DeleteToolTip;
            Refresh = true;
            RefreshIcon = JqGridNavigatorDefaults.RefreshIcon;
            RefreshText = String.Empty;
            RefreshToolTip = JqGridNavigatorDefaults.RefreshToolTip;
            RefreshMode = JqGridRefreshModes.FirstPage;
            AfterRefresh = null;
            BeforeRefresh = null;
            Search = true;
            SearchIcon = JqGridNavigatorDefaults.SearchIcon;
            SearchText = String.Empty;
            SearchToolTip = JqGridNavigatorDefaults.SearchToolTip;
            View = false;
            ViewIcon = JqGridNavigatorDefaults.ViewIcon;
            ViewText = String.Empty;
            ViewToolTip = JqGridNavigatorDefaults.ViewToolTip;
            AddFunction = null;
            EditFunction = null;
            DeleteFunction = null;
        }
        #endregion
    }
}
