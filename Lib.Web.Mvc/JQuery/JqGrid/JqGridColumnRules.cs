namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents rules for jqGrid editable or searchable column
    /// </summary>
    public class JqGridColumnRules
    {
        #region Fields
        private string _customFunction;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets if the value should be validated with custom function.
        /// </summary>
        public bool? Custom { get; set; }

        internal bool IsCustomFunctionSetted { get; set; }
        /// <summary>
        /// Gets or sets the name of custom validation function
        /// </summary>
        public string CustomFunction
        {
            get => _customFunction;
            set
            {
                _customFunction = value;
                IsCustomFunctionSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets if the value should be valid date based on DateFormat property (if not set than ISO date is used).
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
        public bool Integer { get; set; }

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
        public bool Number { get; set; }

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

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnRules class.
        /// </summary>
        public JqGridColumnRules()
        {
        }
        #endregion
    }
}
