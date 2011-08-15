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
    /// Specifies the searching options for column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class JqGridColumnSearchableAttribute : Attribute, IMetadataAware
    {
        #region Properties
        private string DataUrl
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
        /// Gets or sets the function which will build the select element in case where the server response can not build it (requires DataUrl property to be set).
        /// </summary>
        public string BuildSelect
        {
            get { return SearchOptions.BuildSelect; }
            set { SearchOptions.BuildSelect = value; }
        }

        /// <summary>
        /// Gets or sets if the value should be validated with custom function.
        /// </summary>
        public bool CustomValidation
        {
            get { return SearchRules.Custom; }
            set { SearchRules.Custom = value; }
        }

        /// <summary>
        /// Gets or sets the name of custom validation function
        /// </summary>
        public string CustomValidationFunction
        {
            get { return SearchRules.CustomFunction; }
            set { SearchRules.CustomFunction = value; }
        }

        /// <summary>
        /// When overriden in delivered class, provides a list of events to apply to the element.
        /// </summary>
        protected virtual IList<JqGridColumnDataEvent> DataEvents
        {
            get { return null; }
        }

        /// <summary>
        /// Gets or sets the function which will be called once when the element is created.
        /// </summary>
        public string DataInit
        {
            get { return SearchOptions.DataInit; }
            set { SearchOptions.DataInit = value; }
        }

        /// <summary>
        /// Gets or sets if the value should be valid ISO date.
        /// </summary>
        public bool DateValidation
        {
            get { return SearchRules.Date; }
            set { SearchRules.Date = value; }
        }

        /// <summary>
        /// Gets or sets the default value in the search input element.
        /// </summary>
        public string DefaultValue
        {
            get { return SearchOptions.DefaultValue; }
            set { SearchOptions.DefaultValue = value; }
        }

        /// <summary>
        /// Gets or sets if the value should be valid email
        /// </summary>
        public bool EmailValidation
        {
            get { return SearchRules.Email; }
            set { SearchRules.Email = value; }
        }

        /// <summary>
        /// When overriden in delivered class, provides a dictionary where keys should be valid attributes for the element.
        /// </summary>
        protected virtual IDictionary<string, object> HtmlAttributes
        {
            get { return null; }
        }

        /// <summary>
        /// Gets or sets if the value is required while searching.
        /// </summary>
        public bool RequiredValidation
        {
            get { return SearchRules.Required; }
            set { SearchRules.Required = value; }
        }

        /// <summary>
        /// Gets the value defining if this column can be searched.
        /// </summary>
        public bool Searchable { get; private set; }

        /// <summary>
        /// Gets or sets the value which defines if hidden column can be searched.
        /// </summary>
        public bool SearchHidden
        {
            get { return SearchOptions.SearchHidden; }
            set { SearchOptions.SearchHidden = value; }
        }

        /// <summary>
        /// Gets or sets the available search operators for the column (default JqGridSearchOperators.Eq).
        /// </summary>
        public JqGridSearchOperators SearchOperators
        {
            get { return SearchOptions.SearchOperators; }
            set { SearchOptions.SearchOperators = value; }
        }

        internal JqGridColumnSearchOptions SearchOptions { get; private set; }

        internal JqGridColumnRules SearchRules { get; private set; }

        /// <summary>
        /// Gets or sets the type of the search field (default JqGridColumnSearchTypes.Text).
        /// </summary>
        public JqGridColumnSearchTypes SearchType { get; set; }

        /// <summary>
        /// Gets or sets if the value should be valid time (hh:mm format and optional am/pm at the end).
        /// </summary>
        public bool TimeValidation
        {
            get { return SearchRules.Time; }
            set { SearchRules.Time = value; }
        }

        /// <summary>
        /// Gets or sets if the value should be valid url.
        /// </summary>
        public bool UrlValidation
        {
            get { return SearchRules.Url; }
            set { SearchRules.Url = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnSearchableAttribute class.
        /// </summary>
        /// <param name="searchable">If this column can be searched</param>
        public JqGridColumnSearchableAttribute(bool searchable)
        {
            Searchable = searchable;
            SearchOptions = new JqGridColumnSearchOptions();
            SearchRules = new JqGridColumnRules();
            SearchType = JqGridColumnSearchTypes.Text;
        }

        /// <summary>
        /// Initializes a new instance of the JqGridColumnSearchableAttribute class.
        /// </summary>
        /// <param name="searchable">If this column can be searched</param>
        /// <param name="dataUrlRouteName">Route name for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select)</param>
        public JqGridColumnSearchableAttribute(bool searchable, string dataUrlRouteName)
            : this(searchable)
        {
            if (String.IsNullOrWhiteSpace(dataUrlRouteName))
                throw new ArgumentNullException("dataUrlRouteName");
            

            DataUrlRouteName = dataUrlRouteName;
            DataUrlRouteData = new RouteValueDictionary();
        }

        /// <summary>
        /// Initializes a new instance of the JqGridColumnSearchableAttribute class.
        /// </summary>
        /// <param name="searchable">If this column can be searched</param>
        /// <param name="dataUrlAction">Action for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select)</param>
        /// <param name="dataUrlController">Controller for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select)</param>
        public JqGridColumnSearchableAttribute(bool searchable, string dataUrlAction, string dataUrlController) :
            this(searchable, dataUrlAction, dataUrlController, null)
        { }

        /// <summary>
        /// Initializes a new instance of the JqGridColumnSearchableAttribute class.
        /// </summary>
        /// <param name="searchable">If this column can be searched</param>
        /// <param name="dataUrlAction">Action for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select)</param>
        /// <param name="dataUrlController">Controller for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select)</param>
        /// <param name="dataUrlAreaName">Area for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select)</param>
        public JqGridColumnSearchableAttribute(bool searchable, string dataUrlAction, string dataUrlController, string dataUrlAreaName)
            : this(searchable)
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
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            SearchOptions.DataEvents = DataEvents;
            SearchOptions.DataUrl = DataUrl;
            SearchOptions.HtmlAttributes = HtmlAttributes;

            if ((metadata.ModelType == typeof(Int16)) || (metadata.ModelType == typeof(Int32)) || (metadata.ModelType == typeof(Int64)) || (metadata.ModelType == typeof(UInt16)) || (metadata.ModelType == typeof(UInt32)) || (metadata.ModelType == typeof(UInt32)))
                SearchRules.Integer = true;
            else if ((metadata.ModelType == typeof(Decimal)) || (metadata.ModelType == typeof(Double)) || (metadata.ModelType == typeof(Single)))
                SearchRules.Number = true;

            metadata.SetColumnSearchable(Searchable);
            metadata.SetColumnSearchOptions(SearchOptions);
            metadata.SetColumnSearchRules(SearchRules);
            metadata.SetColumnSearchType(SearchType);
        }
        #endregion
    }
}
