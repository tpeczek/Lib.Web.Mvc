using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// jqGrid cell editing submit modes
    /// </summary>
    public enum JqGridCellEditingSubmitModes
    {
        /// <summary>
        /// The change is immediately saved to the server.
        /// </summary>
        Remote,
        /// <summary>
        /// No ajax request is made and the content of the changed cell can be obtained via the JavaScript jqGrid method getChangedCells 
        /// </summary>
        ClientArray
    }
}
