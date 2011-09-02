using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid editable column
    /// </summary>
    public class JqGridColumnEditOptions : JqGridColumnElementOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of function which is used to create custom edit element
        /// </summary>
        public string CustomElementFunction { get; set; }
        
        /// <summary>
        /// Gets or sets the name of function which should return the value from the custom element after the editing.
        /// </summary>
        public string CustomValueFunction { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if null value should be send to server if the field is empty.
        /// </summary>
        public bool NullIfEmpty { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnEditOptions class.
        /// </summary>
        public JqGridColumnEditOptions()
        {
            BuildSelect = null;
            DataEvents = null;
            DataInit = null;
            DataUrl = null;
            DefaultValue = null;
            HtmlAttributes = null;
            CustomElementFunction = null;
            CustomValueFunction = null;
            NullIfEmpty = false;
        }
        #endregion
    }
}
