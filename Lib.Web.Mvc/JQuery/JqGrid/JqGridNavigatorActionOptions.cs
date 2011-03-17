using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid Navigator action.
    /// </summary>
    public class JqGridNavigatorActionOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the value indicating if add action dialog should be cleared after submiting.
        /// </summary>
        public bool? ClearAfterAdd { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if add action dialog should be closed after submiting.
        /// </summary>
        public bool? CloseAfterAdd { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if edit action dialog should be closed after submiting.
        /// </summary>
        public bool? CloseAfterEdit { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if modal window can be closed with ESC key.
        /// </summary>
        public bool? CloseOnEscape { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if dialog is dragable (requires jqDnR plugin or dragable widget from jQuery UI).
        /// </summary>
        public bool? Dragable { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if jqModel plugin should be used for creating dialogs otherwise jqGrid uses its internal function.
        /// </summary>
        public bool? UseJqModal { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if dialog is modal (requires jqModal plugin).
        /// </summary>
        public bool? Modal { get; set; }

        /// <summary>
        /// Gets or sets the type or request to make when data is sent to the server.
        /// </summary>
        public JqGridMethodTypes? MethodType { get; set; }

        /// <summary>
        /// Gets or sets the name of the function for event which is raised just before closing the form.
        /// </summary>
        public string OnClose { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if grid should be reloaded after submiting.
        /// </summary>
        public bool? ReloadAfterSubmit { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if dialog is resizable (requires jqDnR plugin or resizable widget from jQuery UI).
        /// </summary>
        public bool? Resizable { get; set; }
        
        /// <summary>
        /// Gets or sets the url for action requests (default null)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the width of confirmation dialog in pixels (default 'auto').
        /// </summary>
        public int? Width { get; set; }
        #endregion
    }
}
