using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents TreeGrid record for jqGrid in nested set model.
    /// </summary>
    /// <typeparam name="TModel">Type of model for this grid</typeparam>
    public class JqGridNestedSetTreeRecord<TModel> : JqGridNestedSetTreeRecord
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNestedSetTreeRecord class.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <param name="value">The record value.</param>
        /// <param name="level">The level of the record in the hierarchy.</param>
        /// <param name="leftField">The rowid of the record to the left.</param>
        /// <param name="rightField">The rowid of the record to the right.</param>
        public JqGridNestedSetTreeRecord(string id, TModel value, int level, int leftField, int rightField)
            : base(id, value, level, leftField, rightField)
        { }
        #endregion
    }
}
