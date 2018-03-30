using System;

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
        /// Combines equal and not equal
        /// </summary>
        EqualOrNotEqual = Eq | Ne,
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
        Nc = 8192,
        /// <summary>
        /// Is null
        /// </summary>
        Nu = 16384,
        /// <summary>
        /// Is not null
        /// </summary>
        Nn = 32768,
        /// <summary>
        /// Combines equal, not equal, less, less or equal, greater, greater or equal, is null, is not null.
        /// </summary>
        NoTextOperators = Eq | Ne | Lt | Le | Gt | Ge | Nu | Nn,
        /// <summary>
        /// Combines equal, not equal, begins with, does not begin with, ends with, does not end with, contains and does not contain, is null, is not null
        /// </summary>
        TextOperators = Eq | Ne | Bw | Bn | Ew | En | Cn | Nc | Nu | Nn,
        /// <summary>
        /// Combines is null, is not null
        /// </summary>
        NullOperators = Nu | Nn,
        /// <summary>
        /// Use JqGrid default value
        /// </summary>
        Default = 65536
    }
}
