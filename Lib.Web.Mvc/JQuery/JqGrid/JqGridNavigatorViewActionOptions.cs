using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid Navigator view action.
    /// </summary>
    public class JqGridNavigatorViewActionOptions : JqGridNavigatorFormActionOptions, IJqGridNavigatorPageableFormActionOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the options for keyboard navigation.
        /// </summary>
        public JqGridFormKeyboardNavigation NavigationKeys { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the pager buttons should appear on the form.
        /// </summary>
        public bool ViewPagerButtons { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the form should be recreated every time the dialog is activeted with the new options from colModel (if they are changed).
        /// </summary>
        public bool RecreateForm { get; set; }

        /// <summary>
        /// Gets or sets the value which defines how much width is needed for the labels.
        /// </summary>
        public string LabelsWidth { get; set; }

        /// <summary>
        /// Gets or sets the icon for the close button.
        /// </summary>
        public JqGridFormButtonIcon CloseButtonIcon { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNavigatorViewActionOptions class.
        /// </summary>
        public JqGridNavigatorViewActionOptions()
        {
            Width = 0;
            NavigationKeys = new JqGridFormKeyboardNavigation();
            ViewPagerButtons = true;
            RecreateForm = false;
            LabelsWidth = "30%";
            CloseButtonIcon = JqGridFormButtonIcon.CloseIcon;
        }
        #endregion
    }
}
