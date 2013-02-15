using Lib.Web.Mvc.JQuery.JqGrid.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the custom formatter for column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class JqGridColumnFormatterAttribute : Attribute, IMetadataAware
    {
        #region Properties
        /// <summary>
        /// Gets the predefined formatter type ('' delimited string) or custom JavaScript formatting function name.
        /// </summary>
        public string Formatter { get; private set; }

        /// <summary>
        /// Gets or sets the options for predefined formatter (every predefined formatter uses only a subset of all options), which are overwriting the defaults from the language file.
        /// </summary>
        private JqGridColumnFormatterOptions Options { get; set; }

        /// <summary>
        /// Gets or sets the custom function to "unformat" a value of the cell when used in editing or client-side sorting
        /// </summary>
        public string UnFormatter { get; set; }

        /// <summary>
        /// Gets or sets the decimal places.
        /// </summary>
        public int DecimalPlaces
        {
            get { return Options.DecimalPlaces; }
            set { Options.DecimalPlaces = value; }
        }

        /// <summary>
        /// Gets or sets the decimal separator.
        /// </summary>
        public string DecimalSeparator
        {
            get { return Options.DecimalSeparator; }
            set { Options.DecimalSeparator = value; }
        }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        public string DefaultValue
        {
            get { return Options.DefaultValue; }
            set { Options.DefaultValue = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating if checkbox is disabled.
        /// </summary>
        public bool Disabled
        {
            get { return Options.Disabled; }
            set { Options.Disabled = value; }
        }

        /// <summary>
        /// Gets or sets the currency prefix.
        /// </summary>
        public string Prefix
        {
            get { return Options.Prefix; }
            set { Options.Prefix = value; }
        }

        /// <summary>
        /// Gets or sets the currency suffix.
        /// </summary>
        public string Suffix
        {
            get { return Options.Suffix; }
            set { Options.Suffix = value; }
        }

        /// <summary>
        /// Gets or sets the date source format.
        /// </summary>
        public string SourceFormat
        {
            get { return Options.SourceFormat; }
            set { Options.SourceFormat = value; }
        }

        /// <summary>
        /// Gets or sets the date output format.
        /// </summary>
        public string OutputFormat
        {
            get { return Options.OutputFormat; }
            set { Options.OutputFormat = value; }
        }

        /// <summary>
        /// Gets or sets the link for showlink formatter.
        /// </summary>
        public string BaseLinkUrl
        {
            get { return Options.BaseLinkUrl; }
            set { Options.BaseLinkUrl = value; }
        }

        /// <summary>
        /// Gets or sets the additional value which is added after the BaseLinkUrl.
        /// </summary>
        public string ShowAction
        {
            get { return Options.ShowAction; }
            set { Options.ShowAction = value; }
        }

        /// <summary>
        /// Gets or sets the additional parameter which can be added after the IdName property.
        /// </summary>
        public string AddParam
        {
            get { return Options.AddParam; }
            set { Options.AddParam = value; }
        }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public string Target
        {
            get { return Options.Target; }
            set { Options.Target = value; }
        }

        /// <summary>
        /// Gets or sets the first parameter that is added after the ShowAction.
        /// </summary>
        public string IdName
        {
            get { return Options.IdName; }
            set { Options.IdName = value; }
        }

        /// <summary>
        /// Gets or sets the thousands separator.
        /// </summary>
        public string ThousandsSeparator
        {
            get { return Options.ThousandsSeparator; }
            set { Options.ThousandsSeparator = value; }
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
        /// <param name="formatter">The predefined formatter type ('' delimited string) or custom JavaScript formatting function name.</param>
        public JqGridColumnFormatterAttribute(string formatter)
        {
            if (String.IsNullOrWhiteSpace(formatter))
                throw new ArgumentNullException("formatter");
            Formatter = formatter;
            Options = new JqGridColumnFormatterOptions(formatter);
            PrimaryIcon = String.Empty;
            SecondaryIcon = String.Empty;
            Label = String.Empty;
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
                metadata.SetColumnFormatter(GetJQueryUIButtonFormatter());
            else
            {
                metadata.SetColumnFormatter(Formatter);
                metadata.SetColumnFormatterOptions(Options);
                metadata.SetColumnUnFormatter(UnFormatter);
            }
        }
        #endregion

        #region Methods
        internal string GetJQueryUIButtonFormatter()
        {
            StringBuilder formatterBuilder = new StringBuilder(80);
            formatterBuilder.Append("function(cellValue, options, rowObject) { setTimeout(function() { $('#' + options.rowId + '_JQueryUIButton').attr('data-cell-value', cellValue).button({ ");

            if (!String.IsNullOrEmpty(Label))
                formatterBuilder.AppendFormat("label: '{0}', ", Label);

            if (!String.IsNullOrWhiteSpace(PrimaryIcon) || !String.IsNullOrWhiteSpace(SecondaryIcon))
            {
                formatterBuilder.Append("icons: { ");

                if (!String.IsNullOrWhiteSpace(PrimaryIcon))
                    formatterBuilder.AppendFormat("primary: '{0}', ", PrimaryIcon);

                if (!String.IsNullOrWhiteSpace(SecondaryIcon))
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

            if (String.IsNullOrWhiteSpace(OnClick))
                formatterBuilder.Append(";");
            else
                formatterBuilder.AppendFormat(".click({0});", OnClick);
            formatterBuilder.Append(" }, 0); return '<button id=\"' + options.rowId + '_JQueryUIButton\" />'; }");

            return formatterBuilder.ToString();
        }
        #endregion
    }
}
