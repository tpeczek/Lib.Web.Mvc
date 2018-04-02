﻿namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Defines the type of the column for appropriate sorting when datatype is local.
    /// </summary>
    public enum JqGridColumnSortTypes
    {
        /// <summary>
        /// Use JqGrid default value
        /// </summary>
        Default,
        /// <summary>
        /// Sorting as integers.
        /// </summary>
        Integer,
        /// <summary>
        /// Sorting as decimal numbers.
        /// </summary>
        Float,
        /// <summary>
        /// Sorting as date.
        /// </summary>
        Date,
        /// <summary>
        /// Sorting as text.
        /// </summary>
        Text,
        /// <summary>
        /// Sorting using custom function (this option is not supported in configuration import/export).
        /// </summary>
        Function
    }
}
