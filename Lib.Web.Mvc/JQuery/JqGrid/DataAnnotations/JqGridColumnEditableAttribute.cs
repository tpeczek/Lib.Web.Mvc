using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the editing options for column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class JqGridColumnEditableAttribute : Attribute
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
        /// Gets or sets if the value should be validated with custom function.
        /// </summary>
        public bool? CustomValidation
        {
            get { return EditRules.Custom; }
            set { EditRules.Custom = value; }
        }

        /// <summary>
        /// Gets or sets the name of custom validation function
        /// </summary>
        public string CustomValidationFunction
        {
            get { return EditRules.CustomFunction; }
            set { EditRules.CustomFunction = value; }
        }

        internal string DataUrl
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(DataUrlRouteName) || DataUrlRouteData != null)
                {
                    VirtualPathData dataUrlPathData = RouteTable.Routes.GetVirtualPathForArea(HttpContext.Current != null ? HttpContext.Current.Request.RequestContext : null, DataUrlRouteName, DataUrlRouteData);

                    if (dataUrlPathData == null)
                        throw new InvalidOperationException("The DataUrl could not be resolved.");

                    return dataUrlPathData.VirtualPath;
                }
                return null;
            }
        }

        private RouteValueDictionary DataUrlRouteData { get; set; }

        private string DataUrlRouteName { get; set; }

        /// <summary>
        /// Gets or sets if the value should be valid ISO date.
        /// </summary>
        public bool? DateValidation
        {
            get { return EditRules.Date; }
            set { EditRules.Date = value; }
        }

        /// <summary>
        /// Gets the value defining if this column can be edited.
        /// </summary>
        public bool Editable { get; private set; }

        /// <summary>
        /// Gets or sets the value which defines if hidden column can be edited in form editing.
        /// </summary>
        public bool? EditHidden
        {
            get { return EditRules.EditHidden; }
            set { EditRules.EditHidden = value; }
        }

        internal JqGridColumnEditOptions EditOptions { get; private set; }

        internal JqGridColumnEditRules EditRules { get; private set; }

        /// <summary>
        /// Gets or sets the type of the editable field (default JqGridColumnEditTypes.Text).
        /// </summary>
        public JqGridColumnEditTypes EditType { get; set; }

        /// <summary>
        /// Gets or sets if the value should be valid email
        /// </summary>
        public bool? EmailValidation
        {
            get { return EditRules.Email; }
            set { EditRules.Email = value; }
        }

        /// <summary>
        /// Gets or sets the column position of the element (with the label) in form editing (one-based).
        /// </summary>
        public int? FormColumnPosition
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
        public int? RowPosition
        {
            get { return FormOptions.RowPosition.HasValue ? FormOptions.RowPosition.Value : 1; }
            set { FormOptions.RowPosition = value; }
        }

        internal JqGridColumnFormOptions FormOptions { get; private set; }

        /// <summary>
        /// Gets or sets value which defines if multiselect is enabled for select edit element.
        /// </summary>
        public bool? MultipleSelect
        {
            get { return EditOptions.MultipleSelect; }
            set { EditOptions.MultipleSelect = value; }
        }

        /// <summary>
        /// Get or sets the source for element of type image.
        /// </summary>
        public string Source
        {
            get { return EditOptions.Source; }
            set { EditOptions.Source = value; }
        }

        /// <summary>
        /// Gets or sets if the value should be valid time (hh:mm format and optional am/pm at the end).
        /// </summary>
        public bool? TimeValidation
        {
            get { return EditRules.Time; }
            set { EditRules.Time = value; }
        }

        /// <summary>
        /// Gets or sets if the value should be valid url.
        /// </summary>
        public bool? UrlValidation
        {
            get { return EditRules.Url; }
            set { EditRules.Url = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnEditableAttribute class.
        /// </summary>
        /// <param name="editable">If this column can be edited</param>
        public JqGridColumnEditableAttribute(bool editable)
        {
            Editable = editable;
            EditOptions = new JqGridColumnEditOptions();
            EditRules = new JqGridColumnEditRules();
            EditType = JqGridColumnEditTypes.Text;
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
    }
}
