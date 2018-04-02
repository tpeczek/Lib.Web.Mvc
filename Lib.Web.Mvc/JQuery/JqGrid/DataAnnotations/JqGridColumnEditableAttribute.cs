using System;
using System.Web.Routing;
using System.Web.Mvc;
using JetBrains.Annotations;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <inheritdoc />
    /// <summary>
    /// Specifies the editing options for column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class JqGridColumnEditableAttribute : JqGridColumnElementAttribute
    {
        #region Fields
        private string _dateFormat;


        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the name of function which is used to create custom edit element
        /// </summary>
        public string CustomElementFunction
        {
            get => EditOptions.CustomElementFunction;
            set => EditOptions.CustomElementFunction = value;
        }

        /// <summary>
        /// Gets or sets the name of function which should return the value from the custom element after the editing.
        /// </summary>
        public string CustomElementValueFunction
        {
            get => EditOptions.CustomValueFunction;
            set => EditOptions.CustomValueFunction = value;
        }

        internal bool IsDateFormatSetted { get; set; }
        /// <summary>
        /// Gets or sets the expected date format for this column in case of date validation (default ISO date). 
        /// </summary>
        public string DateFormat
        {
            get => _dateFormat;
            set
            {
                _dateFormat = value;
                IsDateFormatSetted = true;
            }
        }

        /// <summary>
        /// Gets the value defining if this column can be edited.
        /// </summary>
        public bool Editable { get; }

        /// <summary>
        /// Gets or sets the value which defines if hidden column can be edited in form editing.
        /// </summary>
        public bool EditHidden
        {
            get => Rules.EditHidden ?? false;
            set => Rules.EditHidden = value;
        }

        internal JqGridColumnEditOptions EditOptions
        {
            get => Options as JqGridColumnEditOptions;
            set => Options = value;
        }

        /// <summary>
        /// Gets or sets the type of the editable field (default JqGridColumnEditTypes.Text).
        /// </summary>
        public JqGridColumnEditTypes EditType { get; set; }

        /// <summary>
        /// Gets or sets the column position of the element (with the label) in form editing (one-based).
        /// </summary>
        public int FormColumnPosition
        {
            get => FormOptions.ColumnPosition ?? 1;
            set => FormOptions.ColumnPosition = value;
        }

        /// <summary>
        /// Gets or sets the text or HTML content to appear before the input element in form editing.
        /// </summary>
        public string FormElementPrefix
        {
            get => FormOptions.ElementPrefix;
            set => FormOptions.ElementPrefix = value;
        }

        /// <summary>
        /// Gets or sets the text or HTML content to appear after the input element in form editing.
        /// </summary>
        public string FormElementSuffix
        {
            get => FormOptions.ElementSuffix;
            set => FormOptions.ElementSuffix = value;
        }

        /// <summary>
        /// Gets or sets the text which will replace the name from ColumnNames as label in form editing.
        /// </summary>
        public string FormLabel
        {
            get => FormOptions.Label;
            set => FormOptions.Label = value;
        }

        /// <summary>
        /// Gets or sets the row position of the element (with the label) in form editing (one-based).
        /// </summary>
        public int FormRowPosition
        {
            get => FormOptions.RowPosition ?? 1;
            set => FormOptions.RowPosition = value;
        }

        internal JqGridColumnFormOptions FormOptions { get; }

        /// <summary>
        /// Gets or sets the value which defines if null value should be send to server if the field is empty.
        /// </summary>
        public bool NullIfEmpty
        {
            get => EditOptions.NullIfEmpty ?? false;
            set => EditOptions.NullIfEmpty = value;
        }

        internal bool IsPostDataDefault { get; private set; }
        /// <summary>
        /// When overriden in delivered class, provides additional data which will be added to the AJAX request to get the data for the select element (if EditType is JqGridColumnEditTypes.Select).
        /// </summary>
        /// <remarks>Do not call base.PostData when overriden.</remarks>
        protected virtual object PostData
        {
            get
            {
                IsPostDataDefault = true;
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the JavaScript which will dynamically generate the additional data which will be added to the AJAX request to get the data for the select element (if EditType is JqGridColumnEditTypes.Select). This property takes precedence over PostData.
        /// </summary>
        public string PostDataScript
        {
            get => EditOptions.PostDataScript;
            set => EditOptions.PostDataScript = value;
        }

        /// <summary>
        /// Gets or sets the child property name if this element performs parent role in selects cascade.
        /// </summary>
        public string ChildName
        {
            get => EditOptions.ChildName;
            set => EditOptions.ChildName = value;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnEditableAttribute class.
        /// </summary>
        /// <param name="editable">If this column can be edited</param>
        public JqGridColumnEditableAttribute(bool editable = true)
        {
            //DateFormat = JqGridOptionsDefaults.DateFormat;
            Editable = editable;
            EditOptions = new JqGridColumnEditOptions();
            EditType = JqGridColumnEditTypes.Default;
            FormOptions = new JqGridColumnFormOptions();
        }

        /// <summary>
        /// Initializes a new instance of the JqGridColumnEditableAttribute class and set column as editable force.
        /// </summary>
        /// <param name="dataUrlRouteName">Route name for the URL to get the AJAX data for the select element (if EditType is JqGridColumnEditTypes.Select) or jQuery UI Autocomplete widget (if EditType is JqGridColumnEditTypes.Autocomplete).</param>
        public JqGridColumnEditableAttribute(string dataUrlRouteName)
            : this()
        {
            if (string.IsNullOrWhiteSpace(dataUrlRouteName))
                throw new ArgumentNullException(nameof(dataUrlRouteName));


            DataUrlRouteName = dataUrlRouteName;
            DataUrlRouteData = new RouteValueDictionary();
        }

        /// <summary>
        /// Initializes a new instance of the JqGridColumnEditableAttribute class and set column as editable force.
        /// </summary>
        /// <param name="dataUrlAction">Action for the URL to get the AJAX data for the select element (if EditType is JqGridColumnEditTypes.Select) or jQuery UI Autocomplete widget (if EditType is JqGridColumnEditTypes.Autocomplete).</param>
        /// <param name="dataUrlController">Controller for the URL to get the AJAX data for the select element (if EditType is JqGridColumnEditTypes.Select) or jQuery UI Autocomplete widget (if EditType is JqGridColumnEditTypes.Autocomplete).</param>
        public JqGridColumnEditableAttribute([AspMvcAction] string dataUrlAction, [AspMvcController] string dataUrlController) :
            this(dataUrlAction, dataUrlController, null)
        { }

        /// <summary>
        /// Initializes a new instance of the JqGridColumnEditableAttribute class and set column as editable force.
        /// </summary>
        /// <param name="dataUrlAction">Action for the URL to get the AJAX data for the select element (if EditType is JqGridColumnEditTypes.Select) or jQuery UI Autocomplete widget (if EditType is JqGridColumnEditTypes.Autocomplete).</param>
        /// <param name="dataUrlController">Controller for the URL to get the AJAX data for the select element (if EditType is JqGridColumnEditTypes.Select) or jQuery UI Autocomplete widget (if EditType is JqGridColumnEditTypes.Autocomplete).</param>
        /// <param name="dataUrlAreaName">Area for the URL to get the AJAX data for the select element (if EditType is JqGridColumnEditTypes.Select) or jQuery UI Autocomplete widget (if EditType is JqGridColumnEditTypes.Autocomplete).</param>
        public JqGridColumnEditableAttribute([AspMvcAction] string dataUrlAction, [AspMvcController] string dataUrlController, [AspMvcArea] string dataUrlAreaName)
            : this()
        {
            if (string.IsNullOrWhiteSpace(dataUrlAction))
                throw new ArgumentNullException(nameof(dataUrlAction));

            if (string.IsNullOrWhiteSpace(dataUrlController))
                throw new ArgumentNullException(nameof(dataUrlController));

            DataUrlRouteData = new RouteValueDictionary
            {
                ["controller"] = dataUrlController,
                ["action"] = dataUrlAction
            };

            if (dataUrlAreaName != null)
                DataUrlRouteData["area"] = dataUrlAreaName;
        }

        /// <summary>
        /// Initializes a new instance of the JqGridColumnEditableAttribute class and set column as editable force.
        /// </summary>
        /// <param name="valueProviderType">The type of class which contains a method which will provide data for select element (if is JqGridColumnEditTypes.Select). This class must have public parameterless constructor.</param>
        /// <param name="valueProviderMethodName">The name of method which will provide data for select element (if is JqGridColumnEditTypes.Select). This method must return an object which implements IDictionary&lt;string, string&gt;.</param>
        public JqGridColumnEditableAttribute([NotNull] Type valueProviderType, string valueProviderMethodName)
            : this()
        {
            if (string.IsNullOrWhiteSpace(valueProviderMethodName))
                throw new ArgumentNullException(nameof(valueProviderMethodName));

            ValueProviderType = valueProviderType ?? throw new ArgumentNullException(nameof(valueProviderType));
            ValueProviderMethodName = valueProviderMethodName;
        }
        #endregion

        #region IMetadataAware
        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        protected override void InternalOnMetadataCreated(ModelMetadata metadata)
        {
            EditOptions.DataEvents = DataEvents;
            if (!string.IsNullOrWhiteSpace(DataUrlRouteName) || DataUrlRouteData != null)
                EditOptions.DataUrl = DataUrl;
            EditOptions.HtmlAttributes = HtmlAttributes;
            object testOverridePostData = PostData;
            if (!IsPostDataDefault)
                EditOptions.PostData = PostData;
            EditOptions.ValueDictionary = ValueDictionary;

            if (EditType == JqGridColumnEditTypes.JQueryUIAutocomplete)
            {
                EditType = JqGridColumnEditTypes.Text;
                EditOptions.ConfigureJQueryUIAutocomplete();
            }
            else if (EditType == JqGridColumnEditTypes.JQueryUIDatepicker)
            {
                EditType = JqGridColumnEditTypes.Text;
                EditOptions.ConfigureJQueryUIDatepicker(metadata);
            }
            else if (EditType == JqGridColumnEditTypes.JQueryUISpinner)
            {
                EditType = JqGridColumnEditTypes.Text;
                EditOptions.ConfigureJQueryUISpinner();
            }
            else if (EditType == JqGridColumnEditTypes.SelectsCascadeParent)
            {
                EditType = JqGridColumnEditTypes.Select;
                EditOptions.ConfigureSelectsCascadeParent();
            }

            metadata.SetColumnDateFormat(new SettedString(IsDateFormatSetted, DateFormat));
            metadata.SetColumnEditable(Editable);
            metadata.SetColumnEditOptions(EditOptions);
            metadata.SetColumnEditRules(Rules);
            metadata.SetColumnEditType(EditType);
            metadata.SetColumnFormOptions(FormOptions);
        }
        #endregion
    }
}
