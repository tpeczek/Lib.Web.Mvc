using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid.Constants
{
    /// <summary>
    /// Contains default values for jqGrid filter toolbar properties
    /// </summary>
    public static class JqGridFilterToolbarDefaults
    {
        /// <summary>
        /// The tooltip for the operation button.
        /// </summary>
        public const string OperandToolTip = "Click to select search operation";

        /// <summary>
        /// The short texts for search operators which are dispalyed to the user when a operation button is clicked.
        /// </summary>
        public static readonly Dictionary<JqGridSearchOperators, string> Operands = new Dictionary<JqGridSearchOperators, string>()
        {
            { JqGridSearchOperators.Eq, "==" },
            { JqGridSearchOperators.Ne, "!" },
            { JqGridSearchOperators.Lt, "<" },
            { JqGridSearchOperators.Le, "<=" },
            { JqGridSearchOperators.Gt, ">" },
            { JqGridSearchOperators.Ge, ">=" },
            { JqGridSearchOperators.Bw, "^" },
            { JqGridSearchOperators.Bn, "!^" },
            { JqGridSearchOperators.In, "=" },
            { JqGridSearchOperators.Ni, "!=" },
            { JqGridSearchOperators.Ew, "|" },
            { JqGridSearchOperators.En, "!@" },
            { JqGridSearchOperators.Cn, "~" },
            { JqGridSearchOperators.Nc, "!~" },
            { JqGridSearchOperators.Nu, "#" },
            { JqGridSearchOperators.Nn, "!#" }
        };
    }
}
