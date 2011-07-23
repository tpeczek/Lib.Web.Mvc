using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Defines available types of grouping summary for jqGrid column.
    /// </summary>
    public enum JqGridColumnSummaryTypes
    {
        /// <summary>
        /// The sum function.
        /// </summary>
        Sum,
        /// <summary>
        /// The count function.
        /// </summary>
        Count,
        /// <summary>
        /// The average function.
        /// </summary>
        Avg,
        /// <summary>
        /// The minimum function.
        /// </summary>
        Min,
        /// <summary>
        /// The maximum function.
        /// </summary>
        Max,
        /// <summary>
        /// The custom function.
        /// </summary>
        Custom
    }
}
