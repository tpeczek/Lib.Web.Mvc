using Lib.Web.Mvc.JQuery.JqGrid.Constants;
using Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Base class which represents options for jqGrid editable or searchable column element
    /// </summary>
    public abstract class JqGridColumnElementOptions
    {
        #region Fields
        private static char[] _invalidDateFormatTokens = new char[] { 'N', 'S', 'w', 'W', 't', 'L', 'o' };
        private const string _ignoredDateFormatTokensRegexExpression = "[aABgGhHisueIOPTZcr]";
        private static Regex _dateFormatTokensRegex = new Regex("d|j|l|z|F|m|n|Y|U", RegexOptions.Compiled);
        private static Dictionary<string, string> _dateFormatTokensReplecements = new Dictionary<string, string> { { "d", "dd" }, { "j", "d" }, { "l", "DD" }, { "z", "o" }, { "F", "MM" }, { "m", "mm" }, { "n", "m" }, { "Y", "yy" }, { "U", "@" } };
        private static DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private readonly static IDictionary<JqGridCompatibilityModes, string> _jqueryUIDatepickerDaysNamesLocalizationOptions = new Dictionary<JqGridCompatibilityModes, string>
        {
            { JqGridCompatibilityModes.JqGrid, "dayNames: $.jgrid.formatter.date.dayNames.slice(7), dayNamesMin: $.jgrid.formatter.date.dayNames.slice(0, 7), dayNamesShort: $.jgrid.formatter.date.dayNames.slice(0, 7), " },
            { JqGridCompatibilityModes.GuriddoJqGrid, "dayNames: $.jgrid.getRegional($({0})[0], 'formatter.date.dayNames').slice(7), dayNamesMin: $.jgrid.getRegional($({0})[0], 'formatter.date.dayNames').slice(0, 7), dayNamesShort: $.jgrid.getRegional($({0})[0], 'formatter.date.dayNames').slice(0, 7), " },
            { JqGridCompatibilityModes.FreeJqGrid, "dayNames: $({0}).jqGrid('getGridRes', 'formatter.date.dayNames').slice(7), dayNamesMin: $({0}).jqGrid('getGridRes', 'formatter.date.dayNames').slice(0, 7), dayNamesShort: $({0}).jqGrid('getGridRes', 'formatter.date.dayNames').slice(0, 7), " }
        };

        private readonly static IDictionary<JqGridCompatibilityModes, string> _jqueryUIDatepickerMonthsNamesLocalizationOptions = new Dictionary<JqGridCompatibilityModes, string>
        {
            { JqGridCompatibilityModes.JqGrid, "monthNames: $.jgrid.formatter.date.monthNames.slice(12), monthNamesShort: $.jgrid.formatter.date.monthNames.slice(0, 12), " },
            { JqGridCompatibilityModes.GuriddoJqGrid, "monthNames: $.jgrid.getRegional($({0})[0], 'formatter.date.monthNames').slice(12), monthNamesShort: $.jgrid.getRegional($({0})[0], 'formatter.date.monthNames').slice(0, 12), " },
            { JqGridCompatibilityModes.FreeJqGrid, "monthNames: $({0}).jqGrid('getGridRes', 'formatter.date.monthNames').slice(12), monthNamesShort: $({0}).jqGrid('getGridRes', 'formatter.date.monthNames').slice(0, 12), " }
        };
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the text to display after each date field (jQuery UI Datepicker widget).
        /// </summary>
        public string AppendText { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the first item will automatically be focused when the menu is shown (jQuery UI Autocomplete widget).
        /// </summary>
        public bool AutoFocus { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the input field should be automatically resize to accommodate dates in the current format (jQuery UI Datepicker widget).
        /// </summary>
        public bool AutoSize { get; set; }

        /// <summary>
        /// Gets or sets the function which will build the select element in case where the server response can not build it (requires DataUrl property to be set).
        /// </summary>
        public string BuildSelect { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the month should be rendered as a dropdown instead of text (jQuery UI Datepicker widget).
        /// </summary>
        public bool ChangeMonth { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the year should be rendered as a dropdown instead of text (jQuery UI Datepicker widget).
        /// </summary>
        public bool ChangeYear { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the input field should constrained to characters allowed by the current format (jQuery UI Datepicker widget).
        /// </summary>
        public bool ConstrainInput { get; set; }

        /// <summary>
        /// Gets or sets the culture to use for parsing and formatting the value when Globalize plugin is included (jQuery UI Spinner widget).
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets the list of events to apply to the element (jQuery UI widgets events can also be applied this way).
        /// </summary>
        public IList<JqGridColumnDataEvent> DataEvents { get; set; }

        /// <summary>
        /// Gets or sets the function which will be called once when the element is created. This property is ignored in case of jQuery UI widgets.
        /// </summary>
        public string DataInit { get; set; }

        internal Func<JqGridCompatibilityModes, string, string> JQueryUIWidgetDataInitRenderer { get; private set; }

        /// <summary>
        /// Gets or sets the URL to get the AJAX data for the select element (if type is JqGridColumnSearchTypes/JqGridColumnEditTypes.Select) or jQuery UI Autocomplete widget (if type is JqGridColumnSearchTypes/JqGridColumnEditTypes.Autocomplete).
        /// </summary>
        public string DataUrl { get; set; }

        /// <summary>
        /// Gets or sets the format for parsed and displayed dates (jQuery UI Datepicker widget).
        /// </summary>
        /// <remarks>If the value for this property is not provided, but there is a JqGridColumnFormatterAttribute with JqGridColumnPredefinedFormatters.Date formatter the helper will try to provide the value based on JqGridColumnFormatterAttribute.OutputFormat.</remarks> 
        public string DatePickerDateFormat { get; set; }

        /// <summary>
        /// Gets or sets the default value in the search input element.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the delay in milliseconds between when a keystroke occurs and when a search is performed (jQuery UI Autocomplete widget).
        /// </summary>
        public int Delay { get; set; }

        /// <summary>
        /// Gets or sets the first day of the week: Sunday is 0, Monday is 1, etc. (jQuery UI Datepicker widget).
        /// </summary>
        public int FirstDay { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the current day link moves to the currently selected date instead of today. (jQuery UI Datepicker widget).
        /// </summary>
        public bool GotoCurrent { get; set; }

        /// <summary>
        /// Gets or sets a dictionary where keys should be valid attributes for the element.
        /// </summary>
        public IDictionary<string, object> HtmlAttributes { get; set; }

        /// <summary>
        /// Gets or sets value controlling the number of steps taken when holding down a spin button (jQuery UI Spinner widget).
        /// </summary>
        public bool Incremental { get; set; }

        /// <summary>
        /// Gets or sets maximum allowed value (jQuery UI Spinner widget).
        /// </summary>
        public int? Max { get; set; }

        /// <summary>
        /// Gets or sets minimum allowed value (jQuery UI Spinner widget).
        /// </summary>
        public int? Min { get; set; }

        /// <summary>
        /// Gets or sets the maximum selectable date. (jQuery UI Datepicker widget).
        /// </summary>
        /// <remarks>This string must be in the format defined by the DateFormat property, or a relative date. Relative dates must contain value and period pairs; valid periods are "y" for years, "m" for months, "w" for weeks, and "d" for days. For example, "+1m +7d" represents one month and seven days from today.</remarks> 
        public string MaxDate { get; set; }

        /// <summary>
        /// Gets or sets the minimum selectable date. (jQuery UI Datepicker widget).
        /// </summary>
        /// <remarks>This string must be in the format defined by the DateFormat property, or a relative date. Relative dates must contain value and period pairs; valid periods are "y" for years, "m" for months, "w" for weeks, and "d" for days. For example, "+1m +7d" represents one month and seven days from today.</remarks> 
        public string MinDate { get; set; }

        /// <summary>
        /// Gets or sets the minimum number of characters a user must type before a search is performed (jQuery UI Autocomplete widget).
        /// </summary>
        public int MinLength { get; set; }

        /// <summary>
        /// Gets or sets the format of numbers passed to Globalize plugin if it is included (jQuery UI Spinner widget).
        /// </summary>
        public string NumberFormat { get; set; }

        /// <summary>
        /// Gets or sets the number of months to show at once (jQuery UI Datepicker widget).
        /// </summary>
        public int NumberOfMonths { get; set; }

        /// <summary>
        /// Gets or sets the number of steps to take when paging via the pageUp/pageDown JavaScript methods (jQuery UI Spinner widget).
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether days in other months shown before or after the current month are selectable, this only applies if the ShowOtherMonths is set to true. (jQuery UI Datepicker widget).
        /// </summary>
        public bool SelectOtherMonths { get; set; }

        /// <summary>
        /// Gets or sets the cutoff year for determining the century for a date (jQuery UI Datepicker widget).
        /// </summary>
        /// <remarks>This string must be a relative number of years from the current year, e.g., "+3" or "-5".</remarks> 
        public string ShortYearCutoff { get; set; }

        /// <summary>
        /// Gets or sets the value which defines position to display the current month in, if the ShowOtherMonths is set to true (jQuery UI Datepicker widget).
        /// </summary>
        public int ShowCurrentAtPos { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether to show the month after the year in the header (jQuery UI Datepicker widget).
        /// </summary>
        public bool ShowMonthAfterYear { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether to display dates in other months (non-selectable) at the start or end of the current month, to make these days selectable set the SelectOtherMonths to true (jQuery UI Datepicker widget).
        /// </summary>
        public bool ShowOtherMonths { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether to add column with the week of the year (jQuery UI Datepicker widget).
        /// </summary>
        public bool ShowWeek { get; set; }

        /// <summary>
        /// Gets or sets the value which defines how many months to move when clicking the previous/next links (jQuery UI Datepicker widget).
        /// </summary>
        public int StepMonths { get; set; }

        /// <summary>
        /// Gets or sets the icon class (form UI theme icons) for jQuery UI Spinner widget down button.
        /// </summary>
        public string SpinnerDownIcon { get; set; }

        /// <summary>
        /// Gets or sets the icon class (form UI theme icons) for jQuery UI Spinner widget up button.
        /// </summary>
        public string SpinnerUpIcon { get; set; }

        /// <summary>
        /// Gets or sets the size of the step to take when spinning via buttons or via the stepUp/stepDown JavaScript methods (jQuery UI Spinner widget).
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// Gets or sets the set of value:label pairs for select element (takes precedence over ValueDictionary property).
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the dictionary which will be serialized into set of value:label pairs for select element.
        /// </summary>
        public IDictionary<string, string> ValueDictionary { get; set; }

        /// <summary>
        /// Gets or sets the range of years displayed in the year dropdown (jQuery UI Datepicker widget).
        /// </summary>
        /// <remarks>This string must be either relative to today's year ("-nn:+nn"), relative to the currently selected year ("c-nn:c+nn"), absolute ("nnnn:nnnn"), or combinations of these formats ("nnnn:-nn"). Note that this option only affects what appears in the dropdown, to restrict which dates may be selected use the MinDate and/or MaxDate.</remarks> 
        public string YearRange { get; set; }

        /// <summary>
        /// Gets or sets the additional text to display after the year in the month headers (jQuery UI Datepicker widget).
        /// </summary>
        public string YearSuffix { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnElementOptions class.
        /// </summary>
        public JqGridColumnElementOptions()
        {
            AppendText = null;
            AutoFocus = false;
            AutoSize = false;
            ChangeMonth = false;
            ChangeYear = false;
            ConstrainInput = true;
            Culture = null;
            DataInit = null;
            DatePickerDateFormat = JQueryUIWidgetsDefaults.DatepickerDateFormat;
            Delay = 300;
            FirstDay = 0;
            GotoCurrent = false;
            Incremental = true;
            JQueryUIWidgetDataInitRenderer = null;
            Max = null;
            Min = null;
            MinLength = 1;
            NumberFormat = null;
            NumberOfMonths = 1;
            Page = 10;
            SelectOtherMonths = false;
            ShortYearCutoff = JQueryUIWidgetsDefaults.DatepickerShortYearCutoff;
            ShowCurrentAtPos = 0;
            ShowMonthAfterYear = false;
            ShowOtherMonths = false;
            ShowWeek = false;
            SpinnerDownIcon = JQueryUIWidgetsDefaults.SpinnerDownIcon;
            SpinnerUpIcon = JQueryUIWidgetsDefaults.SpinnerUpIcon;
            Step = 1;
            StepMonths = 1;
            YearRange = JQueryUIWidgetsDefaults.DatepickerYearRange;
            YearSuffix = String.Empty;
        }
        #endregion

        #region Methods
        private string JQueryUIAutocompleteDataInitRenderer(JqGridCompatibilityModes compatibilityMode, string gridSelector, string dataUrl)
        {
            StringBuilder dataInitBuilder = new StringBuilder(100 + DataUrl.Length);
            dataInitBuilder.AppendFormat("function(element) {{ setTimeout(function() {{ $(element).autocomplete({{ source: '{0}', ", dataUrl);

            if (AutoFocus)
                dataInitBuilder.Append("autoFocus: true, ");

            if (Delay != 300)
                dataInitBuilder.AppendFormat("delay: {0}, ", Delay);

            if (MinLength != 1)
                dataInitBuilder.AppendFormat("minLength: {0}, ", MinLength);

            dataInitBuilder.Remove(dataInitBuilder.Length - 2, 2);
            dataInitBuilder.Append(" }); }, 0); }");

            return dataInitBuilder.ToString();
        }

        internal void ConfigureJQueryUIAutocomplete()
        {
            string dataUrl = DataUrl;
            JQueryUIWidgetDataInitRenderer = (JqGridCompatibilityModes compatibilityMode, string gridSelector) => { return JQueryUIAutocompleteDataInitRenderer(compatibilityMode, gridSelector, dataUrl); };

            DataUrl = String.Empty;
            DataInit = null;
        }

        private string JQueryUIDatepickerDataInitRenderer(JqGridCompatibilityModes compatibilityMode, string gridSelector, ModelMetadata propertyMetadata)
        {
            StringBuilder dataInitBuilder = new StringBuilder(80);
            dataInitBuilder.Append("function(element) { setTimeout(function() { $(element).datepicker({ ");

            if (!String.IsNullOrEmpty(AppendText))
                dataInitBuilder.AppendFormat("appendText: '{0}', ", AppendText);

            if (AutoSize)
                dataInitBuilder.Append("autoSize: true, ");

            if (ChangeMonth)
                dataInitBuilder.Append("changeMonth: true, ");

            if (ChangeYear)
                dataInitBuilder.Append("changeYear: true, ");

            if (!ConstrainInput)
                dataInitBuilder.Append("constrainInput: true, ");

            if (DatePickerDateFormat == JQueryUIWidgetsDefaults.DatepickerDateFormat)
            {
                IEnumerable<object> customAttributes = propertyMetadata.ContainerType.GetProperty(propertyMetadata.PropertyName).GetCustomAttributes(true).AsEnumerable();
                JqGridColumnFormatterAttribute formatterAttribute = customAttributes.OfType<JqGridColumnFormatterAttribute>().FirstOrDefault();

                if (formatterAttribute != null && formatterAttribute.Formatter == JqGridColumnPredefinedFormatters.Date && formatterAttribute.OutputFormat != JqGridOptionsDefaults.FormatterOutputFormat && formatterAttribute.OutputFormat.IndexOfAny(_invalidDateFormatTokens) == -1)
                {
                    DatePickerDateFormat = Regex.Replace(formatterAttribute.OutputFormat, _ignoredDateFormatTokensRegexExpression, String.Empty);
                    DatePickerDateFormat = _dateFormatTokensRegex.Replace(DatePickerDateFormat, match => { return _dateFormatTokensReplecements[match.Value]; });
                }
            }
            dataInitBuilder.AppendFormat("dateFormat: '{0}', ", DatePickerDateFormat);
            dataInitBuilder.AppendFormat(_jqueryUIDatepickerDaysNamesLocalizationOptions[compatibilityMode], gridSelector);

            if (FirstDay != 0)
                dataInitBuilder.AppendFormat("firstDay: {0}, ", FirstDay);

            if (GotoCurrent)
                dataInitBuilder.Append("gotoCurrent: true, ");

            if (!String.IsNullOrEmpty(MaxDate))
                dataInitBuilder.AppendFormat("maxDate: '{0}', ", MaxDate);

            if (!String.IsNullOrEmpty(MinDate))
                dataInitBuilder.AppendFormat("minDate: '{0}', ", MinDate);

            dataInitBuilder.AppendFormat(_jqueryUIDatepickerMonthsNamesLocalizationOptions[compatibilityMode], gridSelector);

            if (NumberOfMonths != 1)
                dataInitBuilder.AppendFormat("numberOfMonths: {0}, ", NumberOfMonths);

            if (SelectOtherMonths)
                dataInitBuilder.Append("selectOtherMonths: true, ");

            if (ShortYearCutoff != JQueryUIWidgetsDefaults.DatepickerShortYearCutoff)
                dataInitBuilder.AppendFormat("shortYearCutoff: '{0}', ", ShortYearCutoff);

            if (ShowCurrentAtPos != 0)
                dataInitBuilder.AppendFormat("showCurrentAtPos: {0}, ", ShowCurrentAtPos);

            if (ShowMonthAfterYear)
                dataInitBuilder.Append("showMonthAfterYear: true, ");

            if (ShowOtherMonths)
                dataInitBuilder.Append("showOtherMonths: true, ");

            if (ShowWeek)
                dataInitBuilder.Append("showWeek: true, ");

            if (StepMonths != 1)
                dataInitBuilder.AppendFormat("stepMonths: {0}, ", StepMonths);

            if (YearRange != JQueryUIWidgetsDefaults.DatepickerYearRange)
                dataInitBuilder.AppendFormat("yearRange: '{0}', ", YearRange);

            if (!String.IsNullOrEmpty(YearSuffix))
                dataInitBuilder.AppendFormat("yearSuffix: '{0}', ", YearSuffix);

            dataInitBuilder.Remove(dataInitBuilder.Length - 2, 2);
            dataInitBuilder.Append(" }); }, 0); }");

            return dataInitBuilder.ToString();
        }

        internal void ConfigureJQueryUIDatepicker(ModelMetadata propertyMetadata)
        {
            JQueryUIWidgetDataInitRenderer = (JqGridCompatibilityModes compatibilityMode, string gridSelector) => { return JQueryUIDatepickerDataInitRenderer(compatibilityMode, gridSelector, propertyMetadata); };

            DataInit = null;
        }

        private string JQueryUISpinnerDataInitRenderer(JqGridCompatibilityModes compatibilityMode, string gridSelector)
        {
            StringBuilder dataInitBuilder = new StringBuilder(80);
            dataInitBuilder.Append("function(element) { setTimeout(function() { $(element).spinner({ ");

            if (!String.IsNullOrEmpty(Culture))
                dataInitBuilder.AppendFormat("culture: '{0}', ", Culture);

            if (SpinnerDownIcon != JQueryUIWidgetsDefaults.SpinnerDownIcon || SpinnerUpIcon != JQueryUIWidgetsDefaults.SpinnerUpIcon)
            {
                dataInitBuilder.Append("icons: { ");

                if (SpinnerDownIcon != JQueryUIWidgetsDefaults.SpinnerDownIcon)
                    dataInitBuilder.AppendFormat("down: '{0}', ", SpinnerDownIcon);

                if (SpinnerUpIcon != JQueryUIWidgetsDefaults.SpinnerUpIcon)
                    dataInitBuilder.AppendFormat("up: '{0}', ", SpinnerUpIcon);

                dataInitBuilder.Remove(dataInitBuilder.Length - 2, 2);
                dataInitBuilder.Append(" }, ");
            }

            if (!Incremental)
                dataInitBuilder.Append("incremental: false, ");

            if (Max.HasValue)
                dataInitBuilder.AppendFormat("max: {0}, ", Max.Value);

            if (Min.HasValue)
                dataInitBuilder.AppendFormat("min: {0}, ", Min.Value);

            if (!String.IsNullOrEmpty(NumberFormat))
                dataInitBuilder.AppendFormat("numberFormat: '{0}', ", NumberFormat);

            if (Page != 10)
                dataInitBuilder.AppendFormat("page: {0}, ", Page);

            if (Step != 1)
                dataInitBuilder.AppendFormat("step: {0}, ", Step);

            dataInitBuilder.Remove(dataInitBuilder.Length - 2, 2);
            if (dataInitBuilder[dataInitBuilder.Length - 1] == '(')
                dataInitBuilder.Append("); }, 0); }");
            else
                dataInitBuilder.Append(" }); }, 0); }");

            return dataInitBuilder.ToString();
        }

        internal void ConfigureJQueryUISpinner()
        {
            JQueryUIWidgetDataInitRenderer = JQueryUISpinnerDataInitRenderer;

            DataInit = null;
        }
        #endregion
    }
}
