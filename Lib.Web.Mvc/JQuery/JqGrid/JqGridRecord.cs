using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents record for jqGrid.
    /// </summary>
    public class JqGridRecord
    {
        #region Properties
        /// <summary>
        /// Gets or sets the list of values for cells
        /// </summary>
        public List<object> Values { get; private set; }

        /// <summary>
        /// Gets or set the record identifier
        /// </summary>
        public string Id { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridRecord class.
        /// </summary>
        /// <param name="id">The record identifier</param>
        /// <param name="values">The list of values for cells</param>
        public JqGridRecord(string id, List<object> values)
        {
            Id = id;
            Values = values;
        }
        #endregion
    }
}
