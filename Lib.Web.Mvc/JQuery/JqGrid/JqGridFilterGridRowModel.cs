using System;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
	/// <summary>
	/// Class which represents row for jqGrid filter grid.
	/// </summary>
	public class JqGridFilterGridRowModel
	{
		#region Properties
		/// <summary>
		/// Gets the corresponding column name from columns models.
		/// </summary>
		public string ColumnName { get; }

		/// <summary>
		/// Gets or sets the default value for the search input element.
		/// </summary>
		public string DefaultValue { get; set; }

		/// <summary>
		/// Gets the label of the field.
		/// </summary>
		public string Label { get; }

		/// <summary>
		/// Gets or sets the type of the search field (default JqGridColumnSearchTypes.Text).
		/// </summary>
		public JqGridColumnSearchTypes SearchType { get; set; }

		/// <summary>
		/// Gets or sets the URL to get the AJAX data for the select element (if SearchType is JqGridColumnSearchTypes.Select)
		/// </summary>
		public string SelectUrl { get; set; }
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the JqGridRecord class.
		/// </summary>
		/// <param name="columnName">The corresponding column name from columns models.</param>
		/// <param name="label">The label of the field.</param>
		public JqGridFilterGridRowModel(string columnName, string label)
		{
			if (string.IsNullOrWhiteSpace(columnName))
				throw new ArgumentNullException(nameof(columnName));

			if (string.IsNullOrWhiteSpace(label))
				throw new ArgumentNullException(nameof(label));

			ColumnName = columnName;
			Label = label;
			SearchType = JqGridColumnSearchTypes.Text;
		}
		#endregion
	}
}
