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
    public sealed class JqGridColumnSearchableAttribute : Attribute
    {
        #region Properties
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
        /// Gets or sets the default value in the search input element.
        /// </summary>
        public string DefaultValue
        {
            get { return SearchOptions.DefaultValue; }
            set { SearchOptions.DefaultValue = value; }
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
            get { return SearchOptions.SearchHidden.HasValue ? SearchOptions.SearchHidden.Value : false; }
            set { SearchOptions.SearchHidden = value; }
        }

        /// <summary>
        /// Gets or sets the available search operators for the column (default JqGridSearchOperators.Eq).
        /// </summary>
        public JqGridSearchOperators SearchOperators
        {
            get { return SearchOptions.SearchOperators.Value; }
            set { SearchOptions.SearchOperators = value; }
        }

        internal JqGridColumnSearchOptions SearchOptions { get; private set; }

        /// <summary>
        /// Gets or sets the type of the search field (default JqGridColumnSearchTypes.Text).
        /// </summary>
        public JqGridColumnSearchTypes SearchType { get; set; }
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
            SearchOptions.SearchOperators = JqGridSearchOperators.Eq;
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
    }
}
