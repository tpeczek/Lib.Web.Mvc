﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using JetBrains.Annotations;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the searching options for column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class JqGridColumnSearchableAttribute : JqGridColumnElementAttribute
    {
        #region Properties
        /// <summary>
        /// Gets or sets if the value is required while searching.
        /// </summary>
        public bool RequiredValidation
        {
            get { return Rules.Required; }
            set { Rules.Required = value; }
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

				/// <summary>
				/// Available search operators for the column with the ability to specify their order.
				/// (default SearchOp.Eq).
				/// </summary>
				public string SearchOrderedOperators { 
					get
					{
						if (SearchOptions.SearchOrderedOperators != null && SearchOptions.SearchOrderedOperators.Any())
							return string.Join( ", ", SearchOptions.SearchOrderedOperators.Select( o => o.ToString() ) );
						else
							return null;
					}
					set
					{
						string[] stringOperators = value.Split( ',' );
						JqGridSearchOperators[] operators = new JqGridSearchOperators[stringOperators.Length];
						for ( int i = 0; i < stringOperators.Length; i++ )
						{
							JqGridSearchOperators singleOperator;
							if ( Enum.TryParse( stringOperators[i], true, out singleOperator ) )
							{
								int numberOperator = (int)singleOperator;
								if (( numberOperator != 0 ) && ( numberOperator & ( numberOperator - 1 ) ) == 0)
								{
									operators[i] = singleOperator;
									continue;
								}
							}
							throw new InvalidOperationException("Все элементы массива должны быть одиночными именами элементов перечисления JqGridSearchOperators.");
						}
						SearchOptions.SearchOrderedOperators = operators;
						JqGridSearchOperators sum = 0;
						foreach (var op in operators)
							sum |= op;
						SearchOptions.SearchOperators = sum;
					}
				}

				/// <summary>
				/// When set to false the X icon at end of search field which is responsible to clear
				/// the search data is disabled. the default value is true.
				/// </summary>
				public bool ClearSearch
				{
					get { return SearchOptions.ClearSearch; }
					set { SearchOptions.ClearSearch = value; }
				}

				private JqGridColumnSearchOptions SearchOptions
        {
            get { return (base.Options as JqGridColumnSearchOptions); }
            set { base.Options = value; }
        }

        /// <summary>
        /// Gets or sets the type of the search field (default JqGridColumnSearchTypes.Text).
        /// </summary>
        public JqGridColumnSearchTypes SearchType { get; set; }

        /// <summary>Immediately applying the filter criteria</summary>
        public bool Immediately { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnSearchableAttribute class.
        /// </summary>
        /// <param name="searchable">If this column can be searched</param>
        public JqGridColumnSearchableAttribute(bool searchable)
            : base()
        {
            Searchable = searchable;
            SearchOptions = new JqGridColumnSearchOptions();
            SearchType = JqGridColumnSearchTypes.Text;
        }

        /// <summary>
        /// Initializes a new instance of the JqGridColumnSearchableAttribute class.
        /// </summary>
        /// <param name="searchable">If this column can be searched</param>
        /// <param name="dataUrlRouteName">Route name for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select) or jQuery UI Autocomplete widget (if SearchType is JqGridColumnSearchTypes.Autocomplete).</param>
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
        /// <param name="dataUrlAction">Action for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select) or jQuery UI Autocomplete widget (if SearchType is JqGridColumnSearchTypes.Autocomplete).</param>
        /// <param name="dataUrlController">Controller for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select) or jQuery UI Autocomplete widget (if SearchType is JqGridColumnSearchTypes.Autocomplete).</param>
        public JqGridColumnSearchableAttribute(bool searchable, [AspMvcAction] string dataUrlAction, [AspMvcController] string dataUrlController) :
            this(searchable, dataUrlAction, dataUrlController, null)
        { }

        /// <summary>
        /// Initializes a new instance of the JqGridColumnSearchableAttribute class.
        /// </summary>
        /// <param name="searchable">If this column can be searched</param>
        /// <param name="dataUrlAction">Action for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select) or jQuery UI Autocomplete widget (if SearchType is JqGridColumnSearchTypes.Autocomplete).</param>
        /// <param name="dataUrlController">Controller for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select) or jQuery UI Autocomplete widget (if SearchType is JqGridColumnSearchTypes.Autocomplete).</param>
        /// <param name="dataUrlAreaName">Area for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select) or jQuery UI Autocomplete widget (if SearchType is JqGridColumnSearchTypes.Autocomplete).</param>
        public JqGridColumnSearchableAttribute(bool searchable, [AspMvcAction] string dataUrlAction
					, [AspMvcController] string dataUrlController, [AspMvcArea] string dataUrlAreaName) : this(searchable)
        {
            if (String.IsNullOrWhiteSpace(dataUrlAction))
                throw new ArgumentNullException("dataUrlAction");
            
            if (String.IsNullOrWhiteSpace(dataUrlController))
                throw new ArgumentNullException("dataUrlController");

            DataUrlRouteData = new RouteValueDictionary();
            DataUrlRouteData["controller"] = dataUrlController;
            DataUrlRouteData["action"] = dataUrlAction;

            if (dataUrlAreaName != null)
                DataUrlRouteData["area"] = dataUrlAreaName;
        }

        /// <summary>
        /// Initializes a new instance of the JqGridColumnSearchableAttribute class.
        /// </summary>
        /// <param name="searchable">If this column can be searched</param>
        /// <param name="valueProviderType">The type of class which contains a method which will provide data for select element (if is JqGridColumnSearchTypes.Select). This class must have public parameterless constructor.</param>
        /// <param name="valueProviderMethodName">The name of method which will provide data for select element (if is JqGridColumnSearchTypes.Select). This method must return an object which implements IDictionary&lt;string, string&gt;.</param>
        public JqGridColumnSearchableAttribute(bool searchable, Type valueProviderType, string valueProviderMethodName)
            : this(searchable)
        {
            if (valueProviderType == null)
                throw new ArgumentNullException("valueProviderType");

            if (String.IsNullOrWhiteSpace(valueProviderMethodName))
                throw new ArgumentNullException("valueProviderMethodName");

            ValueProviderType = valueProviderType;
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
            SearchOptions.DataEvents = DataEvents;
            SearchOptions.DataUrl = DataUrl;
            SearchOptions.HtmlAttributes = HtmlAttributes;
            SearchOptions.ValueDictionary = ValueDictionary;

            if (SearchType == JqGridColumnSearchTypes.JQueryUIAutocomplete)
            {
                SearchType = JqGridColumnSearchTypes.Text;
                SearchOptions.ConfigureJQueryUIAutocomplete();
            }
            else if (SearchType == JqGridColumnSearchTypes.JQueryUIDatepicker)
            {
                SearchType = JqGridColumnSearchTypes.Text;
                SearchOptions.ConfigureJQueryUIDatepicker(metadata);
            }
            else if (SearchType == JqGridColumnSearchTypes.JQueryUISpinner)
            {
                SearchType = JqGridColumnSearchTypes.Text;
                SearchOptions.ConfigureJQueryUISpinner();
            }

            metadata.SetColumnSearchable(Searchable);
            metadata.SetColumnSearchOptions(SearchOptions);
            metadata.SetColumnSearchRules(Rules);
            metadata.SetColumnSearchType(SearchType);
        }
        #endregion

        #region Overrides of JqGridColumnElementAttribute

        /// <summary>When overriden in delivered class, provides a list of events to apply to the element.</summary>
        protected override IList<JqGridColumnDataEvent> DataEvents
        {
          get
          {
            if ( Immediately )
              return new List<JqGridColumnDataEvent>
              {
                new JqGridColumnDataEvent("change", "function(e) { $('table.ui-jqgrid-btable', $(this).parents('div.ui-jqgrid'))[0].triggerToolbar(); }"),
              };
						else
              return base.DataEvents;
          }
        }
			        
        #endregion
    }
}
