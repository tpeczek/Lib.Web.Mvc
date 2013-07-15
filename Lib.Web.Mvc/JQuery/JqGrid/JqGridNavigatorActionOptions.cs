using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid Navigator action.
    /// </summary>
    public abstract class JqGridNavigatorActionOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the initial top position of modal dialog.
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Gets or sets the initial left position of modal dialog.
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if dialog is modal (requires jqModal plugin).
        /// </summary>
        public bool Modal { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if dialog is dragable (requires jqDnR plugin or dragable widget from jQuery UI).
        /// </summary>
        public bool Dragable { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if dialog is resizable (requires jqDnR plugin or resizable widget from jQuery UI).
        /// </summary>
        public bool Resizable { get; set; }

        /// <summary>
        /// Gets or sets the width of confirmation dialog in pixels (default 'auto').
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the width of the scrolling content.
        /// </summary>
        public int? DataWidth { get; set; }

        /// <summary>
        /// Gets or sets the entry height of confirmation dialog.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the height of the scrolling content (i.e between the modal header and modal footer).
        /// </summary>
        public int? DataHeight { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if jqModel plugin should be used for creating dialogs otherwise jqGrid uses its internal function.
        /// </summary>
        public bool UseJqModal { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if modal window can be closed with ESC key.
        /// </summary>
        public bool CloseOnEscape { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised just before closing the form.
        /// </summary>
        public string OnClose { get; set; }

        /// <summary>
        /// Gets or sets the value controling overlay in the grid.
        /// </summary>
        public int Overlay { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNavigatorActionOptions class.
        /// </summary>
        public JqGridNavigatorActionOptions()
        {
            Top = 0;
            Left = 0;
            Modal = false;
            Dragable = true;
            Resizable = true;
            UseJqModal = true;
            CloseOnEscape = false;
            Overlay = 30;

            OnClose = null;
        }
        #endregion
    }
}
