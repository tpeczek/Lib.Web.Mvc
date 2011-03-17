using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Abstract class which represents options for jqGrid filter toolba and grid.
    /// </summary>
    public abstract class JqGridFilterOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the function for event which is raised after clearing entered values.
        /// </summary>
        public string AfterClear { get; set; }

        /// <summary>
        /// Gets or sets the name of the function for event which is raised after searching.
        /// </summary>
        public string AfterSearch { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if searching is performed automatically.
        /// </summary>
        public bool? AutoSearch { get; set; }

        /// <summary>
        /// Gets or sets the name of the function for event which is raised before clearing entered values.
        /// </summary>
        public string BeforeClear { get; set; }

        /// <summary>
        /// Gets or sets the name of the function for event which is raised before searching.
        /// </summary>
        public string BeforeSearch { get; set; }
        #endregion
    }
}
