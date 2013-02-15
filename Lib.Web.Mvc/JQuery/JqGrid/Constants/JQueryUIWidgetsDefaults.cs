using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid.Constants
{
    /// <summary>
    /// Contains default options values for jQuery UI widgets integrations. 
    /// </summary>
    public static class JQueryUIWidgetsDefaults
    {
        /// <summary>
        /// The icon class (form UI theme icons) for Spinner down button.
        /// </summary>
        public const string SpinnerDownIcon = "ui-icon-triangle-1-s";

        /// <summary>
        /// The icon class (form UI theme icons) for Spinner up button.
        /// </summary>
        public const string SpinnerUpIcon = "ui-icon-triangle-1-n";

        /// <summary>
        /// The format for parsed and displayed dates in Datepicker.
        /// </summary>
        public const string DatepickerDateFormat = "m/d/yy";

        /// <summary>
        /// The cutoff year for determining the century for a date in Datepicker.
        /// </summary>
        public const string DatepickerShortYearCutoff = "+10";

        /// <summary>
        /// The range of years displayed in the Datepicker year dropdown.
        /// </summary>
        public const string DatepickerYearRange = "c-10:c+10";
    }
}
