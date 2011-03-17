using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for predefined formatter.
    /// </summary>
    public class JqGridColumnFormatterOptions
    {
        /// <summary>
        /// Gets or sets the additional parameter that can be added after the IdName property for showlink
        /// </summary>
        public string AddParam { get; set; }

        /// <summary>
        /// Gets or sets the link for showlink
        /// </summary>
        public string BaseLinkUrl { get; set; }

        /// <summary>
        /// Gets or set how many decimal places we should there be for a number.
        /// </summary>
        public int? DecimalPlaces { get; set; }

        /// <summary>
        /// Gets or sets the separator for the decimals.
        /// </summary>
        public string DecimalSeparator { get; set; }

        /// <summary>
        /// Gets or sets the default value if nothing in the data
        /// </summary>
        public string DefaulValue { get; set; }

        /// <summary>
        /// Gets or sets  if the checkbox value can be changed.
        /// </summary>
        public bool? Disabled { get; set; }

        /// <summary>
        /// Gets or sets the first parameter that is added after the ShowAction for showlink
        /// </summary>
        public string IdName { get; set; }

        /// <summary>
        /// Gets or sets the text which is puted before the number.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the additional value which is added after the BaseLinkUrl for showlink.
        /// </summary>
        public string ShowAction { get; set; }

        /// <summary>
        /// Gets or sets the source format of the date that should be converted
        /// </summary>
        public string SourceFormat { get; set; }

        /// <summary>
        /// Gets or sets the text that is added after the number
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// Gets or sets the target property for link or additional attribute for showlink
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the target format of the date that should be converted
        /// </summary>
        public string TargetFormat { get; set; }

        /// <summary>
        /// Gets or sets the separator for the thousands.
        /// </summary>
        public string ThousandsSeparator { get; set; }  
    }
}
