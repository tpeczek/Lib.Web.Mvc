using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid Navigator separator.
    /// </summary>
    public class JqGridNavigatorSeparatorOptions : JqGridNavigatorControlOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the class for the separator.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets the content for the separator.
        /// </summary>
        public string Content { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNavigatorSeparatorOptions class.
        /// </summary>
        public JqGridNavigatorSeparatorOptions()
            : base()
        {
            Class = JqGridNavigatorDefaults.SeparatorClass;
            Content = String.Empty;
        }
        #endregion
    }
}
