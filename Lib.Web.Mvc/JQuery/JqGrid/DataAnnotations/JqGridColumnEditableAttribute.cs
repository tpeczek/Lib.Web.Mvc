using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the editing options for column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class JqGridColumnEditableAttribute : JqGridColumnElementAttribute
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of function which is used to create custom edit element
        /// </summary>
        public string CustomElementFunction
        {
            get { return EditOptions.CustomElementFunction; }
            set { EditOptions.CustomElementFunction = value; }
        }

        /// <summary>
        /// Gets or sets the name of function which should return the value from the custom element after the editing.
        /// </summary>
        public string CustomElementValueFunction
        {
            get { return EditOptions.CustomValueFunction; }
            set { EditOptions.CustomValueFunction = value; }
        }

        /// <summary>
        /// Gets or sets the expected date format for this column in case of date validation (default ISO date). 
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// Gets the value defining if this column can be edited.
        /// </summary>
        public bool Editable { get; private set; }

        /// <summary>
        /// Gets or sets the value which defines if hidden column can be edited in form editing.
        /// </summary>
        public bool EditHidden
        {
            get { return Rules.EditHidden; }
            set { Rules.EditHidden = value; }
        }

        internal JqGridColumnEditOptions EditOptions
        {
            get { return (base.Options as JqGridColumnEditOptions); }
            set { base.Options = value; }
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
            get { return FormOptions.ColumnPosition.HasValue ? FormOptions.ColumnPosition.Value : 1; }
            set { FormOptions.ColumnPosition = value; }
        }

        /// <summary>
        /// Gets or sets the text or HTML content to appear before the input element in form editing.
        /// </summary>
        public string FormElementPrefix
        {
            get { return FormOptions.ElementPrefix; }
            set { FormOptions.ElementPrefix = value; }
        }

        /// <summary>
        /// Gets or sets the text or HTML content to appear after the input element in form editing.
        /// </summary>
        public string FormElementSuffix
        {
            get { return FormOptions.ElementSuffix; }
            set { FormOptions.ElementSuffix = value; }
        }

        /// <summary>
        /// Gets or sets the text which will replace the name from ColumnNames as label in form editing.
        /// </summary>
        public string FormLabel
        {
            get { return FormOptions.Label; }
            set { FormOptions.Label = value; }
        }

        /// <summary>
        /// Gets or sets the row position of the element (with the label) in form editing (one-based).
        /// </summary>
        public int FormRowPosition
        {
            get { return FormOptions.RowPosition.HasValue ? FormOptions.RowPosition.Value : 1; }
            set { FormOptions.RowPosition = value; }
        }

        internal JqGridColumnFormOptions FormOptions { get; private set; }

        /// <summary>
        /// Gets or sets the value which defines if null value should be send to server if the field is empty.
        /// </summary>
        public bool NullIfEmpty
        {
            get { return EditOptions.NullIfEmpty; }
            set { EditOptions.NullIfEmpty = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnEditableAttribute class.
        /// </summary>
        /// <param name="editable">If this column can be edited</param>
        public JqGridColumnEditableAttribute(bool editable)
            : base()
        {
            DateFormat = JqGridOptionsDefaults.DateFormat;
            Editable = editable;
            EditOptions = new JqGridColumnEditOptions();
            EditType = JqGridColumnEditTypes.Text;
            FormOptions = new JqGridColumnFormOptions();
        }

        /// <summary>
        /// Initializes a new instance of the JqGridColumnEditableAttribute class.
        /// </summary>
        /// <param name="editable">If this column can be edited</param>
        /// <param name="dataUrlRouteName">Route name for the URL to get the AJAX data for the select element (if is JqGridColumnEditTypes.Select)</param>
        public JqGridColumnEditableAttribute(bool editable, string dataUrlRouteName)
            : this(editable)
        {
            if (String.IsNullOrWhiteSpace(dataUrlRouteName))
                throw new ArgumentNullException("dataUrlRouteName");
            

            DataUrlRouteName = dataUrlRouteName;
            DataUrlRouteData = new RouteValueDictionary();
        }

        /// <summary>
        /// Initializes a new instance of the JqGridColumnEditableAttribute class.
        /// </summary>
        /// <param name="editable">If this column can be edited</param>
        /// <param name="dataUrlAction">Action for the URL to get the AJAX data for the select element (if is JqGridColumnEditTypes.Select)</param>
        /// <param name="dataUrlController">Controller for the URL to get the AJAX data for the select element (if is JqGridColumnEditTypes.Select)</param>
        public JqGridColumnEditableAttribute(bool editable, string dataUrlAction, string dataUrlController) :
            this(editable, dataUrlAction, dataUrlController, null)
        { }

        /// <summary>
        /// Initializes a new instance of the JqGridColumnEditableAttribute class.
        /// </summary>
        /// <param name="editable">If this column can be edited</param>
        /// <param name="dataUrlAction">Action for the URL to get the AJAX data for the select element (if is JqGridColumnEditTypes.Select)</param>
        /// <param name="dataUrlController">Controller for the URL to get the AJAX data for the select element (if is JqGridColumnEditTypes.Select)</param>
        /// <param name="dataUrlAreaName">Area for the URL to get the AJAX data for the select element (if is JqGridColumnEditTypes.Select)</param>
        public JqGridColumnEditableAttribute(bool editable, string dataUrlAction, string dataUrlController, string dataUrlAreaName)
            : this(editable)
        {
            if (String.IsNullOrWhiteSpace(dataUrlAction))
                throw new ArgumentNullException("dataUrlAction");
            
            if (String.IsNullOrWhiteSpace(dataUrlController))
                throw new ArgumentNullException("dataUrlController");

            DataUrlRouteData = new RouteValueDictionary();
            DataUrlRouteData["controller"] = dataUrlController;
            DataUrlRouteData["action"] = dataUrlAction;

            if (!String.IsNullOrWhiteSpace(dataUrlAreaName))
                DataUrlRouteData["area"] = dataUrlAreaName;
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
            EditOptions.DataUrl = DataUrl;
            EditOptions.HtmlAttributes = HtmlAttributes;

            metadata.SetColumnDateFormat(DateFormat);
            metadata.SetColumnEditable(Editable);
            metadata.SetColumnEditOptions(EditOptions);
            metadata.SetColumnEditRules(Rules);
            metadata.SetColumnEditType(EditType);
            metadata.SetColumnFormOptions(FormOptions);
        }
        #endregion
    }
}
