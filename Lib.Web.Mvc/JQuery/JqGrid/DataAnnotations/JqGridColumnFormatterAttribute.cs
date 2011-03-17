using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the custom formatter for column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class JqGridColumnFormatterAttribute: Attribute
    {
        #region Properties
        /// <summary>
        /// Gets the predefined formatter type ('' delimited string) or custom JavaScript formatting function name.
        /// </summary>
        public string Formatter { get; private set; }

        /// <summary>
        /// Gets or sets the options for predefined formatter (every predefined formatter uses only a subset of all options), which are overwriting the defaults from the language file.
        /// </summary>
        internal JqGridColumnFormatterOptions FormatterOptions { get; private set; }

        /// <summary>
        /// Gets or sets the custom function to "unformat" a value of the cell when used in editing or client-side sorting
        /// </summary>
        public string UnFormatter { get; set; }

        /// <summary>
        /// Predefined formatter option, which gets or sets the additional parameter that can be added after the IdName property for showlink
        /// </summary>
        public string AddParam
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.AddParam = value;
            }

            get
            {
                if (FormatterOptions == null)
                    return null;
                else
                    return FormatterOptions.AddParam;
            }
        }

        /// <summary>
        /// Predefined formatter option, which gets or sets the link for showlink
        /// </summary>
        public string BaseLinkUrl
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.BaseLinkUrl = value;
            }

            get
            {
                if (FormatterOptions == null)
                    return null;
                else
                    return FormatterOptions.BaseLinkUrl;
            }
        }

        /// <summary>
        /// Predefined formatter option, which gets or set how many decimal places we should there be for a number.
        /// </summary>
        public int DecimalPlaces
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.DecimalPlaces = value;
            }

            get
            {
                if (FormatterOptions == null || !FormatterOptions.DecimalPlaces.HasValue)
                    return 0;
                else
                    return FormatterOptions.DecimalPlaces.Value;
            }
        }

        /// <summary>
        /// Predefined formatter option, which gets or sets the separator for the decimals.
        /// </summary>
        public string DecimalSeparator
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.DecimalSeparator = value;
            }

            get
            {
                if (FormatterOptions == null)
                    return null;
                else
                    return FormatterOptions.DecimalSeparator;
            }
        }

        /// <summary>
        /// Predefined formatter option, which gets or sets the default value if nothing in the data
        /// </summary>
        public string DefaulValue
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.DefaulValue = value;
            }

            get
            {
                if (FormatterOptions == null)
                    return null;
                else
                    return FormatterOptions.DefaulValue;
            }
        }

        /// <summary>
        /// Predefined formatter option, which gets or sets  if the checkbox value can be changed.
        /// </summary>
        public bool Disabled
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.Disabled = value;
            }

            get
            {
                if (FormatterOptions == null || !FormatterOptions.Disabled.HasValue)
                    return true;
                else
                    return FormatterOptions.Disabled.Value;
            }
        }

        /// <summary>
        /// Predefined formatter option, which gets or sets the first parameter that is added after the ShowAction for showlink
        /// </summary>
        public string IdName
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.IdName = value;
            }

            get
            {
                if (FormatterOptions == null)
                    return null;
                else
                    return FormatterOptions.IdName;
            }
        }

        /// <summary>
        /// Predefined formatter option, which gets or sets the text which is puted before the number.
        /// </summary>
        public string Prefix
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.Prefix = value;
            }

            get
            {
                if (FormatterOptions == null)
                    return null;
                else
                    return FormatterOptions.Prefix;
            }
        }

        /// <summary>
        /// Predefined formatter option, which gets or sets the additional value which is added after the BaseLinkUrl for showlink.
        /// </summary>
        public string ShowAction
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.ShowAction = value;
            }

            get
            {
                if (FormatterOptions == null)
                    return null;
                else
                    return FormatterOptions.ShowAction;
            }
        }

        /// <summary>
        /// Predefined formatter option, which gets or sets the source format of the date that should be converted
        /// </summary>
        public string SourceFormat
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.SourceFormat = value;
            }

            get
            {
                if (FormatterOptions == null)
                    return null;
                else
                    return FormatterOptions.SourceFormat;
            }
        }

        /// <summary>
        /// Predefined formatter option, which gets or sets the text that is added after the number
        /// </summary>
        public string Suffix
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.Suffix = value;
            }

            get
            {
                if (FormatterOptions == null)
                    return null;
                else
                    return FormatterOptions.Suffix;
            }
        }

        /// <summary>
        /// Predefined formatter option, which gets or sets the target property for link or additional attribute for showlink
        /// </summary>
        public string Target
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.Target = value;
            }

            get
            {
                if (FormatterOptions == null)
                    return null;
                else
                    return FormatterOptions.Target;
            }
        }

        /// <summary>
        /// Predefined formatter option, which gets or sets the target format of the date that should be converted
        /// </summary>
        public string TargetFormat
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.TargetFormat = value;
            }

            get
            {
                if (FormatterOptions == null)
                    return null;
                else
                    return FormatterOptions.TargetFormat;
            }
        }

        /// <summary>
        /// Predefined formatter option, which gets or sets the separator for the thousands.
        /// </summary>
        public string ThousandsSeparator
        {
            set
            {
                if (FormatterOptions == null)
                    FormatterOptions = new JqGridColumnFormatterOptions();

                FormatterOptions.ThousandsSeparator = value;
            }

            get
            {
                if (FormatterOptions == null)
                    return null;
                else
                    return FormatterOptions.ThousandsSeparator;
            }
        }
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
        }
        #endregion
    }
}
