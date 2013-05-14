using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web;
using System.Reflection;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Base class which specifies the options for jqGrid editable or searchable column element
    /// </summary>
    public abstract class JqGridColumnElementAttribute : Attribute, IMetadataAware
    {
        #region Properties
        internal string DataUrl
        {
            get
            {
                if ((HttpContext.Current != null) && (!String.IsNullOrWhiteSpace(DataUrlRouteName) || DataUrlRouteData != null))
                {
                    RouteValueDictionary dataUrlRouteValueDictionary = DataUrlRouteData;
                    if (DataUrlRouteValues != null)
                    {
                        dataUrlRouteValueDictionary = new RouteValueDictionary(DataUrlRouteValues);
                        if (DataUrlRouteData != null)
                        {
                            foreach (string key in DataUrlRouteData.Keys)
                            {
                                if (dataUrlRouteValueDictionary.ContainsKey(key))
                                    dataUrlRouteValueDictionary[key] = DataUrlRouteData[key];
                                else
                                    dataUrlRouteValueDictionary.Add(key, DataUrlRouteData[key]);
                            }
                        }
                    }
                    VirtualPathData dataUrlPathData = RouteTable.Routes.GetVirtualPathForArea(HttpContext.Current.Request != null ? HttpContext.Current.Request.RequestContext : null, DataUrlRouteName, dataUrlRouteValueDictionary);

                    if (dataUrlPathData == null)
                        throw new InvalidOperationException("The DataUrl could not be resolved.");

                    return dataUrlPathData.VirtualPath;
                }
                return null;
            }
        }

        internal RouteValueDictionary DataUrlRouteData { get; set; }

        internal string DataUrlRouteName { get; set; }

        /// <summary>
        /// When overriden in delivered class, provides additional route values for the select element AJAX request.
        /// </summary>
        protected virtual object DataUrlRouteValues
        {
            get { return null; }
        }

        /// <summary>
        /// Gets or sets the text to display after each date field (jQuery UI Datepicker widget).
        /// </summary>
        public string AppendText
        {
            get { return Options.AppendText; }
            set { Options.AppendText = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating if the first item will automatically be focused when the menu is shown (jQuery UI Autocomplete widget).
        /// </summary>
        public bool AutoFocus
        {
            get { return Options.AutoFocus; }
            set { Options.AutoFocus = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating if the input field should be automatically resize to accommodate dates in the current format (jQuery UI Datepicker widget).
        /// </summary>
        public bool AutoSize
        {
            get { return Options.AutoSize; }
            set { Options.AutoSize = value; }
        }

        /// <summary>
        /// Gets or sets the function which will build the select element in case where the server response can not build it (requires DataUrl property to be set).
        /// </summary>
        public string BuildSelect
        {
            get { return Options.BuildSelect; }
            set { Options.BuildSelect = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating if the month should be rendered as a dropdown instead of text (jQuery UI Datepicker widget).
        /// </summary>
        public bool ChangeMonth
        {
            get { return Options.ChangeMonth; }
            set { Options.ChangeMonth = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating if the year should be rendered as a dropdown instead of text (jQuery UI Datepicker widget).
        /// </summary>
        public bool ChangeYear
        {
            get { return Options.ChangeYear; }
            set { Options.ChangeYear = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating if the input field should constrained to characters allowed by the current format (jQuery UI Datepicker widget).
        /// </summary>
        public bool ConstrainInput
        {
            get { return Options.ConstrainInput; }
            set { Options.ConstrainInput = value; }
        }

        /// <summary>
        /// Gets or sets the culture to use for parsing and formatting the value when Globalize plugin is included (jQuery UI Spinner widget).
        /// </summary>
        public string Culture
        {
            get { return Options.Culture; }
            set { Options.Culture = value; }
        }

        /// <summary>
        /// Gets or sets if the value should be validated with custom function.
        /// </summary>
        public bool CustomValidation
        {
            get { return Rules.Custom; }
            set { Rules.Custom = value; }
        }

        /// <summary>
        /// Gets or sets the name of custom validation function
        /// </summary>
        public string CustomValidationFunction
        {
            get { return Rules.CustomFunction; }
            set { Rules.CustomFunction = value; }
        }

        /// <summary>
        /// When overriden in delivered class, provides a list of events to apply to the element.
        /// </summary>
        protected virtual IList<JqGridColumnDataEvent> DataEvents
        {
            get { return null; }
        }

        /// <summary>
        /// Gets or sets the function which will be called once when the element is created. This property is ignored in case of jQuery UI widgets.
        /// </summary>
        public string DataInit
        {
            get { return Options.DataInit; }
            set { Options.DataInit = value; }
        }

        /// <summary>
        /// Gets or sets the format for parsed and displayed dates (jQuery UI Datepicker widget).
        /// </summary>
        /// <remarks>If the value for this property is not provided, but there is a JqGridColumnFormatterAttribute with JqGridColumnPredefinedFormatters.Date formatter the helper will try to provide the value based on JqGridColumnFormatterAttribute.OutputFormat.</remarks> 
        public string DatepickerDateFormat
        {
            get { return Options.DatePickerDateFormat; }
            set { Options.DatePickerDateFormat = value; }
        }

        /// <summary>
        /// Gets or sets if the value should be valid date.
        /// </summary>
        public bool DateValidation
        {
            get { return Rules.Date; }
            set { Rules.Date = value; }
        }

        /// <summary>
        /// Gets or sets the default value in the search input element.
        /// </summary>
        public string DefaultValue
        {
            get { return Options.DefaultValue; }
            set { Options.DefaultValue = value; }
        }

        /// <summary>
        /// Gets or sets the delay in milliseconds between when a keystroke occurs and when a search is performed (jQuery UI Autocomplete widget).
        /// </summary>
        public int Delay
        {
            get { return Options.Delay; }
            set { Options.Delay = value; }
        }

        /// <summary>
        /// Gets or sets if the value should be valid email
        /// </summary>
        public bool EmailValidation
        {
            get { return Rules.Email; }
            set { Rules.Email = value; }
        }

        /// <summary>
        /// Gets or sets the first day of the week: Sunday is 0, Monday is 1, etc. (jQuery UI Datepicker widget).
        /// </summary>
        public int FirstDay
        {
            get { return Options.FirstDay; }
            set { Options.FirstDay = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating if the current day link moves to the currently selected date instead of today. (jQuery UI Datepicker widget).
        /// </summary>
        public bool GotoCurrent
        {
            get { return Options.GotoCurrent; }
            set { Options.GotoCurrent = value; }
        }

        /// <summary>
        /// When overriden in delivered class, provides a dictionary where keys should be valid attributes for the element.
        /// </summary>
        protected virtual IDictionary<string, object> HtmlAttributes
        {
            get { return null; }
        }

        /// <summary>
        /// Gets or sets value controlling the number of steps taken when holding down a spin button (jQuery UI Spinner widget).
        /// </summary>
        public bool Incremental
        {
            get { return Options.Incremental; }
            set { Options.Incremental = value; }
        }

        /// <summary>
        /// Gets or sets maximum allowed value (jQuery UI Spinner widget).
        /// </summary>
        public int Max
        {
            get { return Options.Max.HasValue ? Options.Max.Value : Int32.MaxValue; }
            set { Options.Max = value; }
        }

        /// <summary>
        /// Gets or sets minimum allowed value (jQuery UI Spinner widget).
        /// </summary>
        public int Min
        {
            get { return Options.Min.HasValue ? Options.Min.Value : Int32.MinValue; }
            set { Options.Min = value; }
        }

        /// <summary>
        /// Gets or sets the maximum selectable date. (jQuery UI Datepicker widget).
        /// </summary>
        /// <remarks>This string must be in the format defined by the DateFormat property, or a relative date. Relative dates must contain value and period pairs; valid periods are "y" for years, "m" for months, "w" for weeks, and "d" for days. For example, "+1m +7d" represents one month and seven days from today.</remarks> 
        public string MaxDate
        {
            get { return Options.MaxDate; }
            set { Options.MaxDate = value; }
        }

        /// <summary>
        /// Gets or sets the minimum selectable date. (jQuery UI Datepicker widget).
        /// </summary>
        /// <remarks>This string must be in the format defined by the DateFormat property, or a relative date. Relative dates must contain value and period pairs; valid periods are "y" for years, "m" for months, "w" for weeks, and "d" for days. For example, "+1m +7d" represents one month and seven days from today.</remarks> 
        public string MinDate
        {
            get { return Options.MinDate; }
            set { Options.MinDate = value; }
        }

        /// <summary>
        /// Gets or sets the minimum number of characters a user must type before a search is performed (jQuery UI Autocomplete widget).
        /// </summary>
        public int MinLength
        {
            get { return Options.MinLength; }
            set { Options.MinLength = value; }
        }

        /// <summary>
        /// Gets or sets the format of numbers passed to Globalize plugin if it is included (jQuery UI Spinner widget).
        /// </summary>
        public string NumberFormat
        {
            get { return Options.NumberFormat; }
            set { Options.NumberFormat = value; }
        }

        /// <summary>
        /// Gets or sets the number of months to show at once (jQuery UI Datepicker widget).
        /// </summary>
        public int NumberOfMonths
        {
            get { return Options.NumberOfMonths; }
            set { Options.NumberOfMonths = value; }
        }

        internal JqGridColumnElementOptions Options { get; set; }

        /// <summary>
        /// Gets or sets the number of steps to take when paging via the pageUp/pageDown JavaScript methods (jQuery UI Spinner widget).
        /// </summary>
        public int Page
        {
            get { return Options.Page; }
            set { Options.Page = value; }
        }

        internal JqGridColumnRules Rules { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether days in other months shown before or after the current month are selectable, this only applies if the ShowOtherMonths is set to true. (jQuery UI Datepicker widget).
        /// </summary>
        public bool SelectOtherMonths
        {
            get { return Options.SelectOtherMonths; }
            set { Options.SelectOtherMonths = value; }
        }

        /// <summary>
        /// Gets or sets the cutoff year for determining the century for a date (jQuery UI Datepicker widget).
        /// </summary>
        /// <remarks>This string must be a relative number of years from the current year, e.g., "+3" or "-5".</remarks> 
        public string ShortYearCutoff
        {
            get { return Options.ShortYearCutoff; }
            set { Options.ShortYearCutoff = value; }
        }

        /// <summary>
        /// Gets or sets the value which defines position to display the current month in, if the ShowOtherMonths is set to true (jQuery UI Datepicker widget).
        /// </summary>
        public int ShowCurrentAtPos
        {
            get { return Options.ShowCurrentAtPos; }
            set { Options.ShowCurrentAtPos = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating whether to show the month after the year in the header (jQuery UI Datepicker widget).
        /// </summary>
        public bool ShowMonthAfterYear
        {
            get { return Options.ShowMonthAfterYear; }
            set { Options.ShowMonthAfterYear = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating whether to display dates in other months (non-selectable) at the start or end of the current month, to make these days selectable set the SelectOtherMonths to true (jQuery UI Datepicker widget).
        /// </summary>
        public bool ShowOtherMonths
        {
            get { return Options.ShowOtherMonths; }
            set { Options.ShowOtherMonths = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating whether to add column with the week of the year (jQuery UI Datepicker widget).
        /// </summary>
        public bool ShowWeek
        {
            get { return Options.ShowWeek; }
            set { Options.ShowWeek = value; }
        }

        /// <summary>
        /// Gets or sets the icon class (form UI theme icons) for jQuery UI Spinner widget down button.
        /// </summary>
        public string SpinnerDownIcon
        {
            get { return Options.SpinnerDownIcon; }
            set { Options.SpinnerDownIcon = value; }
        }

        /// <summary>
        /// Gets or sets the icon class (form UI theme icons) for jQuery UI Spinner widget up button.
        /// </summary>
        public string SpinnerUpIcon
        {
            get { return Options.SpinnerUpIcon; }
            set { Options.SpinnerUpIcon = value; }
        }

        /// <summary>
        /// Gets or sets the size of the step to take when spinning via buttons or via the stepUp/stepDown JavaScript methods (jQuery UI Spinner widget).
        /// </summary>
        public int Step
        {
            get { return Options.Step; }
            set { Options.Step = value; }
        }

        /// <summary>
        /// Gets or sets the value which defines how many months to move when clicking the previous/next links (jQuery UI Datepicker widget).
        /// </summary>
        public int StepMonths
        {
            get { return Options.StepMonths; }
            set { Options.StepMonths = value; }
        }

        /// <summary>
        /// Gets or sets if the value should be valid time (hh:mm format and optional am/pm at the end).
        /// </summary>
        public bool TimeValidation
        {
            get { return Rules.Time; }
            set { Rules.Time = value; }
        }

        /// <summary>
        /// Gets or sets if the value should be valid url.
        /// </summary>
        public bool UrlValidation
        {
            get { return Rules.Url; }
            set { Rules.Url = value; }
        }

        /// <summary>
        /// Gets or sets the set of value:label pairs for select element.
        /// </summary>
        public virtual string Value{ get; set; }

        internal Type ValueProviderType { get; set; }

        internal string ValueProviderMethodName { get; set; }

        internal IDictionary<string, string> ValueDictionary
        {
            get
            {
                if (ValueProviderType != null && !String.IsNullOrWhiteSpace(ValueProviderMethodName))
                {
                    MethodInfo valueProviderMethodInfo = ValueProviderType.GetMethod(ValueProviderMethodName);
                    if (valueProviderMethodInfo == null)
                        throw new InvalidOperationException("The method specified by ValueProviderType and ValueProviderMethodName could not be found.");

                    ConstructorInfo valueProviderConstructorInfo = ValueProviderType.GetConstructor(Type.EmptyTypes);
                    if (valueProviderConstructorInfo == null)
                        throw new InvalidOperationException("The type specified by ValueProviderType does not have parameterless constructor.");

                    object valueProvider = valueProviderConstructorInfo.Invoke(null);
                    if (typeof(IDictionary<string, string>).IsAssignableFrom(valueProviderMethodInfo.ReturnType))
                        return (IDictionary<string, string>)valueProviderMethodInfo.Invoke(valueProvider, null);
                    else
                        throw new InvalidOperationException("The method specified by ValueProviderType and ValueProviderMethodName does not return IDictionary<string, string>.");
                }
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the range of years displayed in the year dropdown (jQuery UI Datepicker widget).
        /// </summary>
        /// <remarks>This string must be either relative to today's year ("-nn:+nn"), relative to the currently selected year ("c-nn:c+nn"), absolute ("nnnn:nnnn"), or combinations of these formats ("nnnn:-nn"). Note that this option only affects what appears in the dropdown, to restrict which dates may be selected use the MinDate and/or MaxDate.</remarks> 
        public string YearRange
        {
            get { return Options.YearRange; }
            set { Options.YearRange = value; }
        }

        /// <summary>
        /// Gets or sets the additional text to display after the year in the month headers (jQuery UI Datepicker widget).
        /// </summary>
        public string YearSuffix
        {
            get { return Options.YearSuffix; }
            set { Options.YearSuffix = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnElementAttribute class.
        /// </summary>
        public JqGridColumnElementAttribute()
        {
            Rules = new JqGridColumnRules();
        }
        #endregion

        #region IMetadataAware
        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            Options.Value = Value;

            if ((metadata.ModelType == typeof(Int16)) || (metadata.ModelType == typeof(Int32)) || (metadata.ModelType == typeof(Int64)) || (metadata.ModelType == typeof(UInt16)) || (metadata.ModelType == typeof(UInt32)) || (metadata.ModelType == typeof(UInt32)))
                Rules.Integer = true;
            else if ((metadata.ModelType == typeof(Decimal)) || (metadata.ModelType == typeof(Double)) || (metadata.ModelType == typeof(Single)))
                Rules.Number = true;

            InternalOnMetadataCreated(metadata);
        }

        /// <summary>
        /// Provides metadata to the model metadata creation process in derivered class.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        protected abstract void InternalOnMetadataCreated(ModelMetadata metadata);
        #endregion
    }
}
