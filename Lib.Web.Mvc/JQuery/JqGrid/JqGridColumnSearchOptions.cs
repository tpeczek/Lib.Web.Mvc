using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid searchable column
    /// </summary>
    public class JqGridColumnSearchOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the function which will build the select element in case where the server response can not build it (requires DataUrl property to be set).
        /// </summary>
        public string BuildSelect { get; set; }

        /// <summary>
        /// Gets or sets the list of events to apply to the element.
        /// </summary>
        public IList<JqGridColumnDataEvent> DataEvents { get; set; }

        /// <summary>
        /// Gets or sets the function which will be called once when the element is created.
        /// </summary>
        public string DataInit { get; set; }

        /// <summary>
        /// Gets or sets the URL to get the AJAX data for the select element (if SearchType is JqGridColumnSearchTypes.Select).
        /// </summary>
        public string DataUrl { get; set; }

        /// <summary>
        /// Gets or sets the default value in the search input element.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets a dictionary where keys should be valid attributes for the element.
        /// </summary>
        public IDictionary<string, object> HtmlAttributes { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if hidden column can be searched.
        /// </summary>
        public bool SearchHidden { get; set; }

        /// <summary>
        /// Gets or sets the available search operators for the column.
        /// </summary>
        public JqGridSearchOperators SearchOperators { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnSearchOptions class.
        /// </summary>
        public JqGridColumnSearchOptions()
        {
            BuildSelect = null;
            DataEvents = null;
            DataInit = null;
            DataUrl = null;
            DefaultValue = null;
            HtmlAttributes = null;
            SearchHidden = false;
            SearchOperators = (JqGridSearchOperators)16383;
        }
        #endregion
    }
}
