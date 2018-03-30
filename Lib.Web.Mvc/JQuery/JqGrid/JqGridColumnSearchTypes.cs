namespace Lib.Web.Mvc.JQuery.JqGrid
{
	/// <summary>
	/// Defines available types of search fields for jqGrid
	/// </summary>
	public enum JqGridColumnSearchTypes
	{
		/// <summary>
		/// Use JqGrid default value
		/// </summary>
		Default,
		/// <summary>
		/// Input element of type text.
		/// </summary>
		Text,
		/// <summary>
		/// Select element
		/// </summary>
		Select,
		/// <summary>
		/// jQuery UI Autocomplete widget
		/// </summary>
		JQueryUIAutocomplete,
		/// <summary>
		/// jQuery UI Datepicker widget
		/// </summary>
		JQueryUIDatepicker,
		/// <summary>
		/// jQuery UI Spinner widget
		/// </summary>
		JQueryUISpinner
	}
}
