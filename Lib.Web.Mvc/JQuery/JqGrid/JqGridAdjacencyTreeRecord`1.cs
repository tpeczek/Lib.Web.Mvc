using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents TreeGrid record for jqGrid in adjacency model.
    /// </summary>
    /// <typeparam name="TModel">Type of model for this grid</typeparam>
    public class JqGridAdjacencyTreeRecord<TModel> : JqGridAdjacencyTreeRecord
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridAdjacencyTreeRecord class.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <param name="value">The record value.</param>
        /// <param name="level">The level of the record in the hierarchy.</param>
        /// <param name="parentId">The id of parent of this record.</param>
        public JqGridAdjacencyTreeRecord(string id, TModel value, int level, string parentId)
            : base(id, value, level, parentId)
        { }
        #endregion
    }
}
