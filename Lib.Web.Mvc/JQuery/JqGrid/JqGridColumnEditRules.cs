using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents rules for jqGrid editable column
    /// </summary>
    public class JqGridColumnEditRules
    {
        #region Properties
        /// <summary>
        /// Gets or sets if the value should be validated with custom function.
        /// </summary>
        public bool? Custom { get; set; }

        /// <summary>
        /// Gets or sets the name of custom validation function
        /// </summary>
        public string CustomFunction { get; set; }

        /// <summary>
        /// Gets or sets if the value should be valid ISO date.
        /// </summary>
        public bool? Date { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if hidden column can be edited in form editing.
        /// </summary>
        public bool? EditHidden { get; set; }

        /// <summary>
        /// Gets or sets if the value should be valid email
        /// </summary>
        public bool? Email { get; set; }

        /// <summary>
        /// Gets or sets if the value should be valid integer.
        /// </summary>
        public bool? Integer { get; set; }

        /// <summary>
        /// Gets or set the maximum value.
        /// </summary>
        public double? MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        public double? MinValue { get; set; }

        /// <summary>
        /// Gets or sets if the value should be valid number
        /// </summary>
        public bool? Number { get; set; }

        /// <summary>
        /// Gets or sets if the value is required.
        /// </summary>
        public bool? Required { get; set; }

        /// <summary>
        /// Gets or sets if the value should be valid time (hh:mm format and optional am/pm at the end).
        /// </summary>
        public bool? Time { get; set; }

        /// <summary>
        /// Gets or sets if the value should be valid url.
        /// </summary>
        public bool? Url { get; set; }
        #endregion
    }
}
