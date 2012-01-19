using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid Inline Navigator.
    /// </summary>
    public class JqGridInlineNavigatorOptions : JqGridNavigatorOptionsBase
    {
        #region Properties
        /// <summary>
        /// Gets or set the value which defines if save action is enabled (default true).
        /// </summary>
        public bool Save { get; set; }

        /// <summary>
        /// Gets or sets the icon (form UI theme images) for save action.
        /// </summary>
        public string SaveIcon { get; set; }

        /// <summary>
        /// Gets or sets the text for save action.
        /// </summary>
        public string SaveText { get; set; }

        /// <summary>
        /// Gets or sets the tooltip for save action.
        /// </summary>
        public string SaveToolTip { get; set; }

        /// <summary>
        /// Gets or set the value which defines if cancel action is enabled (default true).
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets or sets the icon (form UI theme images) for cancel action.
        /// </summary>
        public string CancelIcon { get; set; }

        /// <summary>
        /// Gets or sets the text for cancel action.
        /// </summary>
        public string CancelText { get; set; }

        /// <summary>
        /// Gets or sets the tooltip for cancel action.
        /// </summary>
        public string CancelToolTip { get; set; }

        /// <summary>
        /// Gets or sets the options for edit, save and cancel actions
        /// </summary>
        public JqGridInlineNavigatorActionOptions ActionOptions { get; set; }

        /// <summary>
        /// Gets or sets the options for add action.
        /// </summary>
        public JqGridInlineNavigatorAddActionOptions AddActionOptions { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridInlineNavigatorOptions class.
        /// </summary>
        public JqGridInlineNavigatorOptions()
            : base()
        {
            Save = true;
            SaveIcon = JqGridNavigatorDefaults.SaveIcon;
            SaveText = String.Empty;
            SaveToolTip = JqGridNavigatorDefaults.SaveToolTip;
            Cancel = true;
            CancelIcon = JqGridNavigatorDefaults.CancelIcon;
            CancelText = String.Empty;
            CancelToolTip = JqGridNavigatorDefaults.CancelToolTip;
            ActionOptions = null;
            AddActionOptions = null;
        }
        #endregion
    }
}
