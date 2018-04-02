﻿using Lib.Web.Mvc.JQuery.JqGrid.Constants;
using System;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the custom formatter for column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class JqGridColumnFormatterAttribute : Attribute, IMetadataAware
    {

        #region Fields
        private string _unFormatter;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the predefined formatter type ('' delimited string) or custom JavaScript formatting function name.
        /// </summary>
        public string Formatter { get; }

        /// <summary>
        /// Gets or sets the options for predefined formatter (every predefined formatter uses only a subset of all options), which are overwriting the defaults from the language file.
        /// </summary>
        private JqGridColumnFormatterOptions Options { get; }

        internal bool IsUnFormatterSetted { get; private set; }
        /// <summary>
        /// Gets or sets the custom function to "unformat" a value of the cell when used in editing or client-side sorting
        /// </summary>
        public string UnFormatter
        {
            get => _unFormatter;
            set
            {
                _unFormatter = value;
                IsUnFormatterSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the decimal places.
        /// </summary>
        public int DecimalPlaces
        {
            get => Options.DecimalPlaces ?? JqGridOptionsDefaults.FormatterDecimalPlaces;
            set => Options.DecimalPlaces = value;
        }

        /// <summary>
        /// Gets or sets the decimal separator.
        /// </summary>
        public string DecimalSeparator
        {
            get => Options.DecimalSeparator;
            set => Options.DecimalSeparator = value;
        }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        public string DefaultValue
        {
            get => Options.DefaultValue;
            set => Options.DefaultValue = value;
        }

        /// <summary>
        /// Gets or sets the value indicating if checkbox is disabled.
        /// </summary>
        public bool Disabled
        {
            get => Options.Disabled ?? false;
            set => Options.Disabled = value;
        }

        /// <summary>
        /// Gets or sets the currency prefix.
        /// </summary>
        public string Prefix
        {
            get => Options.Prefix;
            set => Options.Prefix = value;
        }

        /// <summary>
        /// Gets or sets the currency suffix.
        /// </summary>
        public string Suffix
        {
            get => Options.Suffix;
            set => Options.Suffix = value;
        }

        /// <summary>
        /// Gets or sets the date source format.
        /// </summary>
        public string SourceFormat
        {
            get => Options.SourceFormat;
            set => Options.SourceFormat = value;
        }

        /// <summary>
        /// Gets or sets the date output format.
        /// </summary>
        public string OutputFormat
        {
            get => Options.OutputFormat;
            set => Options.OutputFormat = value;
        }

        /// <summary>
        /// Gets or sets the link for showlink formatter.
        /// </summary>
        public string BaseLinkUrl
        {
            get => Options.BaseLinkUrl;
            set => Options.BaseLinkUrl = value;
        }

        /// <summary>
        /// Gets or sets the additional value which is added after the BaseLinkUrl.
        /// </summary>
        public string ShowAction
        {
            get => Options.ShowAction;
            set => Options.ShowAction = value;
        }

        /// <summary>
        /// Gets or sets the additional parameter which can be added after the IdName property.
        /// </summary>
        public string AddParam
        {
            get => Options.AddParam;
            set => Options.AddParam = value;
        }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public string Target
        {
            get => Options.Target;
            set => Options.Target = value;
        }

        /// <summary>
        /// Gets or sets the first parameter that is added after the ShowAction.
        /// </summary>
        public string IdName
        {
            get => Options.IdName;
            set => Options.IdName = value;
        }

        /// <summary>
        /// Gets or sets the thousands separator.
        /// </summary>
        public string ThousandsSeparator
        {
            get => Options.ThousandsSeparator;
            set => Options.ThousandsSeparator = value;
        }

        /// <summary>
        /// Gets or sets the primary icon class (form UI theme icons) for jQuery UI Button widget.
        /// </summary>
        public string PrimaryIcon { get; set; }

        /// <summary>
        /// Gets or sets the secondary icon class (form UI theme icons) for jQuery UI Button widget.
        /// </summary>
        public string SecondaryIcon { get; set; }

        /// <summary>
        /// Gets or sets the text to show in the button for jQuery UI Button widget.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the value whether to show the label in jQuery UI Button widget.
        /// </summary>
        public bool Text { get; set; }

        /// <summary>
        /// Gets or sets the click handler (JavaScript) for jQuery UI Button widget.
        /// </summary>
        public string OnClick { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnFormatterAttribute class.
        /// </summary>
        /// <param name="formatter">The predefined formatter type ('' delimited string) or custom JavaScript formatting function name. <c>null</c> for not using default formatter if setted in JqGrid</param>
        public JqGridColumnFormatterAttribute(string formatter)
        {
            Formatter = formatter;
            Options = new JqGridColumnFormatterOptions();
            PrimaryIcon = string.Empty;
            SecondaryIcon = string.Empty;
            Label = string.Empty;
            Text = true;
        }
        #endregion

        #region IMetadataAware
        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            if (Formatter == JqGridColumnPredefinedFormatters.JQueryUIButton)
                metadata.SetColumnFormatter(new SettedString(true, GetJQueryUIButtonFormatter()));
            else
            {
                metadata.SetColumnFormatter(new SettedString(true, Formatter));
                metadata.SetColumnFormatterOptions(Options);
                metadata.SetColumnUnFormatter(new SettedString(IsUnFormatterSetted, UnFormatter));
            }
        }
        #endregion

        #region Methods
        internal string GetJQueryUIButtonFormatter()
        {
            var formatterBuilder = new StringBuilder(80);
            formatterBuilder.Append("function(cellValue, options, rowObject) { setTimeout(function() { $('#' + options.rowId + '_JQueryUIButton').attr('data-cell-value', cellValue).button({ ");

            if (!string.IsNullOrEmpty(Label))
                formatterBuilder.AppendFormat("label: '{0}', ", Label);

            if (!string.IsNullOrWhiteSpace(PrimaryIcon) || !string.IsNullOrWhiteSpace(SecondaryIcon))
            {
                formatterBuilder.Append("icons: { ");

                if (!string.IsNullOrWhiteSpace(PrimaryIcon))
                    formatterBuilder.AppendFormat("primary: '{0}', ", PrimaryIcon);

                if (!string.IsNullOrWhiteSpace(SecondaryIcon))
                    formatterBuilder.AppendFormat("secondary: '{0}', ", SecondaryIcon);

                formatterBuilder.Remove(formatterBuilder.Length - 2, 2);
                formatterBuilder.Append(" }, ");
            }

            if (!Text)
                formatterBuilder.Append("text: false, ");

            formatterBuilder.Remove(formatterBuilder.Length - 2, 2);
            if (formatterBuilder[formatterBuilder.Length - 1] == '(')
                formatterBuilder.Append(")");
            else
                formatterBuilder.Append(" })");

            if (string.IsNullOrWhiteSpace(OnClick))
                formatterBuilder.Append(";");
            else
                formatterBuilder.AppendFormat(".click({0});", OnClick);
            formatterBuilder.Append(" }, 0); return '<button id=\"' + options.rowId + '_JQueryUIButton\" />'; }");

            return formatterBuilder.ToString();
        }
        #endregion
    }
}
