using System;
using System.Web.Mvc;
using System.Web.Routing;
using JetBrains.Annotations;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
	/// <inheritdoc />
	/// <summary>
	/// Specifies the searching options for column
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class JqGridColumnSearchableAttribute : JqGridColumnElementAttribute
	{
		#region Properties
		/// <summary>
		/// Gets or sets the value which defines if Clear ("X") button is available at the end of search field for this column in jqGrid filter toolbar.
		/// </summary>
		public bool ClearSearch
		{
			get => SearchOptions.ClearSearch ?? true;
			set => SearchOptions.ClearSearch = value;
		}

		/// <summary>
		/// Gets or sets if the value is required while searching.
		/// </summary>
		public bool RequiredValidation
		{
			get => Rules.Required ?? false;
			set => Rules.Required = value;
		}

		/// <summary>
		/// Gets the value defining if this column can be searched.
		/// </summary>
		public bool Searchable { get; }

		/// <summary>
		/// Gets or sets the value which defines if hidden column can be searched.
		/// </summary>
		public bool SearchHidden
		{
			get => SearchOptions.SearchHidden ?? false;
			set => SearchOptions.SearchHidden = value;
		}

		/// <summary>
		/// Gets or sets the available search operators for the column (default JqGridSearchOperators.Eq).
		/// </summary>
		public JqGridSearchOperators SearchOperators
		{
			get => SearchOptions.SearchOperators;
			set => SearchOptions.SearchOperators = value;
		}

		private JqGridColumnSearchOptions SearchOptions
		{
			get => Options as JqGridColumnSearchOptions;
			set => Options = value;
		}

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
		public JqGridColumnSearchableAttribute(bool searchable = true)
		{
			Searchable = searchable;
			SearchOptions = new JqGridColumnSearchOptions();
			SearchType = JqGridColumnSearchTypes.Default;
		}

		/// <summary>
		/// Initializes a new instance of the JqGridColumnSearchableAttribute class and make column searchable force.
		/// </summary>
		/// <param name="dataUrlRouteName">Route name for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select) or jQuery UI Autocomplete widget (if SearchType is JqGridColumnSearchTypes.Autocomplete).</param>
		public JqGridColumnSearchableAttribute(string dataUrlRouteName)
			: this()
		{
			if (string.IsNullOrWhiteSpace(dataUrlRouteName))
				throw new ArgumentNullException(nameof(dataUrlRouteName));


			DataUrlRouteName = dataUrlRouteName;
			DataUrlRouteData = new RouteValueDictionary();
		}

		/// <summary>
		/// Initializes a new instance of the JqGridColumnSearchableAttribute class and make column searchable force.
		/// </summary>
		/// <param name="dataUrlAction">Action for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select) or jQuery UI Autocomplete widget (if SearchType is JqGridColumnSearchTypes.Autocomplete).</param>
		/// <param name="dataUrlController">Controller for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select) or jQuery UI Autocomplete widget (if SearchType is JqGridColumnSearchTypes.Autocomplete).</param>
		public JqGridColumnSearchableAttribute([AspMvcAction] string dataUrlAction, [AspMvcController] string dataUrlController) :
			this(dataUrlAction, dataUrlController, null)
		{ }

		/// <summary>
		/// Initializes a new instance of the JqGridColumnSearchableAttribute class and make column searchable force.
		/// </summary>
		/// <param name="dataUrlAction">Action for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select) or jQuery UI Autocomplete widget (if SearchType is JqGridColumnSearchTypes.Autocomplete).</param>
		/// <param name="dataUrlController">Controller for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select) or jQuery UI Autocomplete widget (if SearchType is JqGridColumnSearchTypes.Autocomplete).</param>
		/// <param name="dataUrlAreaName">Area for the URL to get the AJAX data for the select element (if is JqGridColumnSearchTypes.Select) or jQuery UI Autocomplete widget (if SearchType is JqGridColumnSearchTypes.Autocomplete).</param>
		public JqGridColumnSearchableAttribute([AspMvcAction] string dataUrlAction, [AspMvcController] string dataUrlController, [AspMvcArea] string dataUrlAreaName)
			: this()
		{
			if (string.IsNullOrWhiteSpace(dataUrlAction))
				throw new ArgumentNullException(nameof(dataUrlAction));

			if (string.IsNullOrWhiteSpace(dataUrlController))
				throw new ArgumentNullException(nameof(dataUrlController));

			DataUrlRouteData = new RouteValueDictionary();
			DataUrlRouteData["controller"] = dataUrlController;
			DataUrlRouteData["action"] = dataUrlAction;

			if (dataUrlAreaName != null)
				DataUrlRouteData["area"] = dataUrlAreaName;
		}

		/// <summary>
		/// Initializes a new instance of the JqGridColumnSearchableAttribute class and make column searchable force.
		/// </summary>
		/// <param name="valueProviderType">The type of class which contains a method which will provide data for select element (if is JqGridColumnSearchTypes.Select). This class must have public parameterless constructor.</param>
		/// <param name="valueProviderMethodName">The name of method which will provide data for select element (if is JqGridColumnSearchTypes.Select). This method must return an object which implements IDictionary&lt;string, string&gt;.</param>
		public JqGridColumnSearchableAttribute(Type valueProviderType, string valueProviderMethodName)
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
			SearchOptions.DataEvents = DataEvents;
			if (!string.IsNullOrWhiteSpace(DataUrlRouteName) || DataUrlRouteData != null)
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
	}
}
