using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Defines available operators for search fields for jqGrid
    /// </summary>
    [Flags]
    public enum JqGridSearchOperators
    {
        /// <summary>
        /// Equal
        /// </summary>
        Eq = 1,
        /// <summary>
        /// Not equal
        /// </summary>
        Ne = 2,
        /// <summary>
        /// Less
        /// </summary>
        Lt = 4,
        /// <summary>
        /// Less or equal
        /// </summary>
        Le = 8,
        /// <summary>
        /// Greater
        /// </summary>
        Gt = 16,
        /// <summary>
        /// Greater or equal
        /// </summary>
        Ge = 32,
        /// <summary>
        /// Begins with
        /// </summary>
        Bw = 64,
        /// <summary>
        /// Does not begin with
        /// </summary>
        Bn = 128,
        /// <summary>
        /// Is in
        /// </summary>
        In = 256,
        /// <summary>
        /// Is not in
        /// </summary>
        Ni = 512,
        /// <summary>
        /// Ends with
        /// </summary>
        Ew = 1024,
        /// <summary>
        /// Does not end with
        /// </summary>
        En = 2048,
        /// <summary>
        /// Contains
        /// </summary>
        Cn = 4096,
        /// <summary>
        /// Does not contain
        /// </summary>
        Nc = 8192
    }
}
