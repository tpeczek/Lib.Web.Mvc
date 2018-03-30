using System.Collections.Generic;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
	internal class JqGridColumnLabelOptions
	{
		#region Properties
		internal string Label { get; set; }

		internal string Class { get; set; }

		internal IDictionary<string, object> CssStyles { get; set; }

		internal IDictionary<string, object> HtmlAttributes { get; set; }
		#endregion

		#region Constructor
		internal JqGridColumnLabelOptions()
		{
			Label = string.Empty;
			Class = string.Empty;
			CssStyles = null;
			HtmlAttributes = null;
		}
		#endregion
	}
}
