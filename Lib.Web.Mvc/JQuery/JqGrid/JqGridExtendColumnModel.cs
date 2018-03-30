using System.Collections.Generic;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Options for extend column model
    /// </summary>
    public class JqGridExtendColumnModel
    {
        #region Fields

        private object _editOptionsPostData;
        private IDictionary<string, object> _editOptionsHtmlAttributes;
        private IDictionary<string, string> _editOptionsValueDictionary;
        private IDictionary<string, object> _labelOptionsCssStyles;
        private IDictionary<string, object> _labelOptionsHtmlAttributes;
        private IDictionary<string, string> _searchOptionsValueDictionary;
        private IDictionary<string, object> _searchOptionsHtmlAttributes;

        #endregion

        internal bool IsEditOptionsPostDataSetted { get; private set; }
        /// <summary>
        /// Gets or sets the additional data which will be added to the AJAX request to get the data for the select element (if EditType is JqGridColumnEditTypes.Select).
        /// </summary>
        public object EditOptionsPostData
        {
            get => _editOptionsPostData;
            set
            {
                _editOptionsPostData = value;
                IsEditOptionsPostDataSetted = true;
            }
        }

        internal bool IsEditOptionsHtmlAttributesSetted { get; private set; }
        /// <summary>
        /// Gets or sets a dictionary where keys should be valid attributes for the element in edit mode.
        /// </summary>
        public IDictionary<string, object> EditOptionsHtmlAttributes
        {
            get => _editOptionsHtmlAttributes;
            set
            {
                _editOptionsHtmlAttributes = value;
                IsEditOptionsHtmlAttributesSetted = true;
            }
        }

        internal bool IsEditOptionsValueDictionarySetted { get; private set; }
        /// <summary>
        /// Gets or sets the dictionary which will be serialized into set of value:label pairs for select element in edit mode.
        /// </summary>
        public IDictionary<string, string> EditOptionsValueDictionary
        {
            get => _editOptionsValueDictionary;
            set
            {
                _editOptionsValueDictionary = value;
                IsEditOptionsValueDictionarySetted = true;
            }
        }

        internal bool IsLabelOptionsCssStylesSetted { get; private set; }
        /// <summary>
        /// Gets or sets a dictionary where keys should be CSS styles for the label (will be applied only if Class is null or empty string).
        /// </summary>
        public IDictionary<string, object> LabelOptionsCssStyles
        {
            get => _labelOptionsCssStyles;
            set
            {
                _labelOptionsCssStyles = value;
                IsLabelOptionsCssStylesSetted = true;
            }
        }

        internal bool IsLabelOptionsHtmlAttributesSetted { get; private set; }
        /// <summary>
        ///Gets or sets a dictionary where keys should be valid attributes for the label.
        /// </summary>
        public IDictionary<string, object> LabelOptionsHtmlAttributes
        {
            get => _labelOptionsHtmlAttributes;
            set
            {
                _labelOptionsHtmlAttributes = value;
                IsLabelOptionsHtmlAttributesSetted = true;
            }
        }

        internal bool IsSearchOptionsValueDictionarySetted { get; private set; }
        /// <summary>
        /// Gets or sets the dictionary which will be serialized into set of value:label pairs for select element in search mode.
        /// </summary>
        public IDictionary<string, string> SearchOptionsValueDictionary
        {
            get => _searchOptionsValueDictionary;
            set
            {
                _searchOptionsValueDictionary = value;
                IsSearchOptionsValueDictionarySetted = true;
            }
        }

        internal bool IsSearchOptionsHtmlAttributesSetted { get; private set; }
        /// <summary>
        /// Gets or sets a dictionary where keys should be valid attributes for the element in search mode.
        /// </summary>
        public IDictionary<string, object> SearchOptionsHtmlAttributes
        {
            get => _searchOptionsHtmlAttributes;
            set
            {
                _searchOptionsHtmlAttributes = value;
                IsSearchOptionsHtmlAttributesSetted = true;
            }
        }
    }
}