using Lib.Web.Mvc.JQuery.JqGrid.Constants;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
	/// <summary>
	/// Class which represents options for jqGrid Navigator button.
	/// </summary>
	public class JqGridNavigatorButtonOptions : JqGridNavigatorControlOptions
	{
		#region Properties
		/// <summary>
		/// Gets or sets the caption for the button.
		/// </summary>
		public string Caption { get; set; }

		/// <summary>
		/// Gets or sets the icon (form UI theme images) for the button. If this property is set to "none" only text will appear.
		/// </summary>
		public string Icon { get; set; }

		/// <summary>
		/// Gets or sets the id for TD element holding the button (optional).
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the function for button click action.
		/// </summary>
		public string OnClick { get; set; }

		/// <summary>
		/// Gets or sets the position where the button will be added.
		/// </summary>
		public JqGridNavigatorButtonPositions Position { get; set; }

		/// <summary>
		/// Gets or sets the tooltip for the button.
		/// </summary>
		public string ToolTip { get; set; }

		/// <summary>
		/// Gets or sets the value which determines the cursor when user mouseover the button.
		/// </summary>
		public string Cursor { get; set; }
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the JqGridNavigatorButtonOptions class.
		/// </summary>
		public JqGridNavigatorButtonOptions()
			: base()
		{
			Caption = JqGridNavigatorDefaults.ButtonCaption;
			Icon = JqGridNavigatorDefaults.ButtonIcon;
			Id = null;
			OnClick = null;
			Position = JqGridNavigatorButtonPositions.Last;
			ToolTip = string.Empty;
			Cursor = JqGridNavigatorDefaults.ButtonCursor;
		}
		#endregion
	}
}
