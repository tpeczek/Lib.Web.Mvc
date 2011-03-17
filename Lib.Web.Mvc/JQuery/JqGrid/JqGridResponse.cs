using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents response for jqGrid.
    /// </summary>
    public class JqGridResponse
    {
        #region Properties
        /// <summary>
        /// Gets or sets the index (zero based) of page to return
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets total pages count
        /// </summary>
        public int TotalPagesCount { get; set; }

        /// <summary>
        /// Gets or sets total records count
        /// </summary>
        public int TotalRecordsCount { get; set; }

        /// <summary>
        /// Gets the records list
        /// </summary>
        public List<JqGridRecord> Records { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridResponse class.
        /// </summary>
        public JqGridResponse()
        {
            Records = new List<JqGridRecord>();
        }
        #endregion
    }
}
