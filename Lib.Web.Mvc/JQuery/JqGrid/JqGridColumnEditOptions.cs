using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid editable column
    /// </summary>
    public class JqGridColumnEditOptions
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
        /// Gets or sets the URL to get the AJAX data for the select element (if is JqGridColumnEditTypes.Select)
        /// </summary>
        public string DataUrl { get; set; }

        /// <summary>
        /// Gets or sets the maxlength attribute for the input element
        /// </summary>
        public int? MaximumLength { get; set; }

        /// <summary>
        /// Gets or sets value which defines if multiselect is enabled for select edit element.
        /// </summary>
        public bool? MultipleSelect { get; set; }
        
        /// <summary>
        /// Get or sets the source for element of type image.
        /// </summary>
        public string Source { get; set; }
        #endregion
    }
}
