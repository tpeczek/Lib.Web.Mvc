using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Defines available modes for jqGrid dynamic scrolling
    /// </summary>
    public enum JqGridDynamicScrollingModes
    {
        /// <summary>
        /// Dynamic scrolling disabled.
        /// </summary>
        Disabled,
        /// <summary>
        /// Dynamic scrolling enabled, the grid will hold all items requested.
        /// </summary>
        HoldAllRows,
        /// <summary>
        /// Dynamic scrolling enabled, the grid will hold only visible rows.
        /// </summary>
        HoldVisibleRows
    }
}
