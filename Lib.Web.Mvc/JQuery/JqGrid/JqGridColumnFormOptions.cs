namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents additional column options for jqGrid form editing.
    /// </summary>
    public class JqGridColumnFormOptions
    {
        #region Fields
        private string _elementPrefix;
        private string _elementSuffix;
        private string _label;

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the column position of the element (with the label) in the form (one-based).
        /// </summary>
        public int? ColumnPosition { get; set; }

        internal bool IsElementPrefixSetted { get; private set; }
        /// <summary>
        /// Gets or sets the text or HTML content to appear before the input element.
        /// </summary>
        public string ElementPrefix
        {
            get => _elementPrefix;
            set
            {
                _elementPrefix = value;
                IsElementPrefixSetted = true;
            }
        }

        internal bool IsElementSuffixSetted { get; private set; }
        /// <summary>
        /// Gets or sets the text or HTML content to appear after the input element.
        /// </summary>
        public string ElementSuffix
        {
            get => _elementSuffix;
            set
            {
                _elementSuffix = value;
                IsElementSuffixSetted = true;
            }
        }

        internal bool IsLabelSetted { get; private set; }
        /// <summary>
        /// Gets or sets the text which will replace the name from ColumnNames as label in the form.
        /// </summary>
        public string Label
        {
            get => _label;
            set
            {
                _label = value;
                IsLabelSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the row position of the element (with the label) in the form (one-based).
        /// </summary>
        public int? RowPosition { get; set; }
        #endregion
    }
}
