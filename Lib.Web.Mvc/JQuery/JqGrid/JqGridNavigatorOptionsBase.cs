using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents base options for jqGrid navigators.
    /// </summary>
    public abstract class JqGridNavigatorOptionsBase
    {
        #region Properties
        /// <summary>
        /// Gets or set the value which defines if add action is enabled (default true).
        /// </summary>
        public bool Add { get; set; }

        /// <summary>
        /// Gets or sets the icon (form UI theme images) for add action.
        /// </summary>
        public string AddIcon { get; set; }

        /// <summary>
        /// Gets or sets the text for add action.
        /// </summary>
        public string AddText { get; set; }

        /// <summary>
        /// Gets or sets the tooltip for add action.
        /// </summary>
        public string AddToolTip { get; set; }

        /// <summary>
        /// Gets or set the value which defines if edit action is enabled (default true).
        /// </summary>
        public bool Edit { get; set; }

        /// <summary>
        /// Gets or sets the icon (form UI theme images) for edit action.
        /// </summary>
        public string EditIcon { get; set; }

        /// <summary>
        /// Gets or sets the text for edit action.
        /// </summary>
        public string EditText { get; set; }

        /// <summary>
        /// Gets or sets the tooltip for edit action.
        /// </summary>
        public string EditToolTip { get; set; }

        /// <summary>
        /// Gets or sets the position of the Navigator buttons in the pager.
        /// </summary>
        public JqGridAlignments Position { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNavigatorOptionsBase class.
        /// </summary>
        public JqGridNavigatorOptionsBase()
        {
            Add = true;
            AddIcon = JqGridNavigatorDefaults.AddIcon;
            AddText = String.Empty;
            AddToolTip = JqGridNavigatorDefaults.AddToolTip;
            Edit = true;
            EditIcon = JqGridNavigatorDefaults.EditIcon;
            EditText = String.Empty;
            EditToolTip = JqGridNavigatorDefaults.EditToolTip;
            Position = JqGridAlignments.Left;
        }
        #endregion
    }
}
