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
        public bool ClearAfterAdd { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if add action dialog should be closed after submiting.
        /// </summary>
        public bool CloseAfterAdd { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if edit action dialog should be closed after submiting.
        /// </summary>
        public bool CloseAfterEdit { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if modal window can be closed with ESC key.
        /// </summary>
        public bool CloseOnEscape { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if dialog is dragable (requires jqDnR plugin or dragable widget from jQuery UI).
        /// </summary>
        public bool Dragable { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if jqModel plugin should be used for creating dialogs otherwise jqGrid uses its internal function.
        /// </summary>
        public bool UseJqModal { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if dialog is modal (requires jqModal plugin).
        /// </summary>
        public bool Modal { get; set; }

        /// <summary>
        /// Gets or sets the type or request to make when data is sent to the server.
        /// </summary>
        public JqGridMethodTypes MethodType { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after the data for the new row is loaded from the grid (if navigator buttons are enabled in edit mode).
        /// </summary>
        public string AfterClickPgButtons { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised fires immediately after all actions and events are completed and the row is inserted or updated in the grid.
        /// </summary>
        public string AfterComplete { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after showing the form with the new data.
        /// </summary>
        public string AfterShowForm { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after response has been received from server.
        /// </summary>
        public string AfterSubmit { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before checking the values (if checking is defined in colModel via editrules option).
        /// </summary>
        public string BeforeCheckValues { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before initializing the new form data.
        /// </summary>
        public string BeforeInitData { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before showing the form with the new data.
        /// </summary>
        public string BeforeShowForm { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before the data is submitted to the server.
        /// </summary>
        public string BeforeSubmit { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised immediately after the previous or next button is clicked, before leaving the current row (if navigator buttons are enabled in edit mode).
        /// </summary>
        public string OnClickPgButtons { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after the submit button is clicked and the postdata is constructed.
        /// </summary>
        public string OnClickSubmit { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised only once when creating the form for editing and adding.
        /// </summary>
        public string OnInitializeForm { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised just before closing the form.
        /// </summary>
        public string OnClose { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised when error occurs from the ajax call and can be used for better formatting of the error messages.
        /// </summary>
        public string ErrorTextFormat { get; set; }

        /// <summary>
        /// Gets or sets the function for event which can serialize the data passed to the ajax request from the form.
        /// </summary>
        public string SerializeEditData { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if grid should be reloaded after submiting.
        /// </summary>
        public bool ReloadAfterSubmit { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if dialog is resizable (requires jqDnR plugin or resizable widget from jQuery UI).
        /// </summary>
        public bool Resizable { get; set; }
        
        /// <summary>
        /// Gets or sets the url for action requests (default null)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the width of confirmation dialog in pixels (default 'auto').
        /// </summary>
        public int Width { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNavigatorActionOptions class.
        /// </summary>
        public JqGridNavigatorActionOptions()
        {
            ClearAfterAdd = true;
            CloseAfterAdd = false;
            CloseAfterEdit = false;
            CloseOnEscape = false;
            Dragable = true;
            MethodType = JqGridMethodTypes.Post;
            Modal = false;
            AfterShowForm = null;
            AfterSubmit = null;
            BeforeShowForm = null;
            OnClose = null;
            OnInitializeForm = null;
            ReloadAfterSubmit = true;
            Resizable = true;
            Url = null;
            UseJqModal = true;
            Width = 300;
        }
        #endregion
    }
}
