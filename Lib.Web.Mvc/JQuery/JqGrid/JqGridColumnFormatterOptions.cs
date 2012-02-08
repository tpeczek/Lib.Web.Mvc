using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for predefined formatter.
    /// </summary>
    public class JqGridColumnFormatterOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the decimal places.
        /// </summary>
        public int DecimalPlaces { get; set; }

        /// <summary>
        /// Gets or sets the decimal separator.
        /// </summary>
        public string DecimalSeparator { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if checkbox is disabled.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets the currency prefix.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the currency suffix.
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// Gets or sets the date source format.
        /// </summary>
        public string SourceFormat { get; set; }

        /// <summary>
        /// Gets or sets the date output format.
        /// </summary>
        public string OutputFormat { get; set; }

        /// <summary>
        /// Gets or sets the link for showlink formatter.
        /// </summary>
        public string BaseLinkUrl { get; set; }

        /// <summary>
        /// Gets or sets the additional value which is added after the BaseLinkUrl.
        /// </summary>
        public string ShowAction { get; set; }

        /// <summary>
        /// Gets or sets the additional parameter which can be added after the IdName property.
        /// </summary>
        public string AddParam { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the first parameter that is added after the ShowAction.
        /// </summary>
        public string IdName { get; set; }

        /// <summary>
        /// Gets or sets the thousands separator.
        /// </summary>
        public string ThousandsSeparator { get; set; }

        internal bool EditButton { get; set; }
        
        internal bool DeleteButton { get; set; }
        
        internal bool UseFormEditing { get; set; }
        
        internal JqGridInlineNavigatorActionOptions InlineEditingOptions { get; set; }
        
        internal JqGridNavigatorEditActionOptions FormEditingOptions { get; set; }
        
        internal JqGridNavigatorDeleteActionOptions DeleteOptions { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes new instance of JqGridColumnFormatterOptions class.
        /// </summary>
        public JqGridColumnFormatterOptions()
        {
            DecimalPlaces = 0;
            DecimalSeparator = String.Empty;
            DefaultValue = String.Empty;
            Disabled = true;
            ThousandsSeparator = String.Empty;
            Prefix = String.Empty;
            Suffix = String.Empty;
            SourceFormat = String.Empty;
            OutputFormat = String.Empty;
            BaseLinkUrl = String.Empty;
            ShowAction = String.Empty;
            AddParam = String.Empty;
            Target = String.Empty;
            IdName = String.Empty;
            EditButton = true;
            DeleteButton = true;
            UseFormEditing = false;
            InlineEditingOptions = null;
            FormEditingOptions = null;
            DeleteOptions = null;
        }

        /// <summary>
        /// Initializes new instance of JqGridColumnFormatterOptions class.
        /// </summary>
        /// <param name="formatter">Predefined formatter</param>
        public JqGridColumnFormatterOptions(string formatter)
            : this()
        {
            switch (formatter)
            {
                case JqGridColumnPredefinedFormatters.Integer:
                    DefaultValue = JqGridOptionsDefaults.IntegerFormatterDefaultValue;
                    ThousandsSeparator = JqGridOptionsDefaults.FormatterThousandsSeparator;
                    break;
                case JqGridColumnPredefinedFormatters.Number:
                    DecimalPlaces = JqGridOptionsDefaults.FormatterDecimalPlaces;
                    DecimalSeparator = JqGridOptionsDefaults.FormatterDecimalSeparator;
                    DefaultValue = JqGridOptionsDefaults.NumberFormatterDefaultValue;
                    ThousandsSeparator = JqGridOptionsDefaults.FormatterThousandsSeparator;
                    break;
                case JqGridColumnPredefinedFormatters.Currency:
                    DecimalPlaces = JqGridOptionsDefaults.FormatterDecimalPlaces;
                    DecimalSeparator = JqGridOptionsDefaults.FormatterDecimalSeparator;
                    DefaultValue = JqGridOptionsDefaults.CurrencyFormatterDefaultValue;
                    ThousandsSeparator = JqGridOptionsDefaults.FormatterThousandsSeparator;
                    break;
                case JqGridColumnPredefinedFormatters.Date:
                    SourceFormat = JqGridOptionsDefaults.FormatterSourceFormat;
                    OutputFormat = JqGridOptionsDefaults.FormatterOutputFormat;
                    break;
                case JqGridColumnPredefinedFormatters.ShowLink:
                    IdName = JqGridOptionsDefaults.FormatterIdName;
                    break;
            }
        }
        #endregion

        #region Methods
        internal bool IsDefault(string formatter)
        {
            switch (formatter)
            {
                case JqGridColumnPredefinedFormatters.Integer:
                    return ((DefaultValue == JqGridOptionsDefaults.IntegerFormatterDefaultValue) && (ThousandsSeparator == JqGridOptionsDefaults.FormatterThousandsSeparator));
                case JqGridColumnPredefinedFormatters.Number:
                    return ((DecimalPlaces == JqGridOptionsDefaults.FormatterDecimalPlaces) && (DecimalSeparator == JqGridOptionsDefaults.FormatterDecimalSeparator) && (DefaultValue == JqGridOptionsDefaults.NumberFormatterDefaultValue) && (ThousandsSeparator == JqGridOptionsDefaults.FormatterThousandsSeparator));
                case JqGridColumnPredefinedFormatters.Currency:
                    return ((DecimalPlaces == JqGridOptionsDefaults.FormatterDecimalPlaces) && (DecimalSeparator == JqGridOptionsDefaults.FormatterDecimalSeparator) && (DefaultValue == JqGridOptionsDefaults.CurrencyFormatterDefaultValue) && String.IsNullOrWhiteSpace(Prefix) && String.IsNullOrWhiteSpace(Suffix) && (ThousandsSeparator == JqGridOptionsDefaults.FormatterThousandsSeparator));
                case JqGridColumnPredefinedFormatters.Date:
                    return ((SourceFormat == JqGridOptionsDefaults.FormatterSourceFormat) && (OutputFormat == JqGridOptionsDefaults.FormatterOutputFormat));
                case JqGridColumnPredefinedFormatters.Link:
                    return String.IsNullOrWhiteSpace(Target);
                case JqGridColumnPredefinedFormatters.ShowLink:
                    return (String.IsNullOrWhiteSpace(BaseLinkUrl) && String.IsNullOrWhiteSpace(ShowAction) && String.IsNullOrWhiteSpace(AddParam) && String.IsNullOrWhiteSpace(Target) && (IdName == JqGridOptionsDefaults.FormatterIdName));
                case JqGridColumnPredefinedFormatters.CheckBox:
                    return Disabled;
                case JqGridColumnPredefinedFormatters.Actions:
                    return (EditButton && DeleteButton && !UseFormEditing && (InlineEditingOptions == null) && (FormEditingOptions == null) && (DeleteOptions == null));
                default:
                    return true;
            }
        }
        #endregion
    }
}
