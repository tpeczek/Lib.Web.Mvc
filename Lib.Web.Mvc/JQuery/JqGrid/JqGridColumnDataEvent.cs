using System;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
	/// <summary>
	/// Class which represents data event for jqGrid editable or searchable column
	/// </summary>
	public class JqGridColumnDataEvent
	{
		#region Properties
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public string Type { get; }

		/// <summary>
		/// Gets the additional data for the event.
		/// </summary>
		public object Data { get; }

		/// <summary>
		/// Gets the function which will be called on the event.
		/// </summary>
		public string Function { get; }
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the JqGridColumnDataEvent class.
		/// </summary>
		/// <param name="type">The type of the event.</param>
		/// <param name="function">The function which will be called on the event.</param>
		/// <param name="data">The additional (optional) data for the event.</param>
		public JqGridColumnDataEvent(string type, string function, object data = null)
		{
			if (string.IsNullOrWhiteSpace(type))
				throw new ArgumentNullException(nameof(type));

			if (string.IsNullOrWhiteSpace(function))
				throw new ArgumentNullException(nameof(function));

			Type = type;
			Function = function;
			Data = data;
		}
		#endregion
	}
}
