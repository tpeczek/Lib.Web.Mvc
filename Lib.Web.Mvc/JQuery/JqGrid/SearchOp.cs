namespace Lib.Web.Mvc.JQuery.JqGrid
{
	/// <summary>
	/// Defines available operators for search fields for jqGrid
	/// </summary>
	public static class SearchOp
	{
		/// <summary>Equal</summary>
		public const string Eq = "Eq";
		/// <summary>Not equal</summary>
		public const string Ne = "Ne";
		/// <summary>Combines equal and not equal</summary>
		public const string EqualOrNotEqual = "Eq,Ne";
		/// <summary>Less</summary>
		public const string Lt = "Lt";
		/// <summary>Less or equal</summary>
		public const string Le = "Le";
		/// <summary>Greater</summary>
		public const string Gt = "Gt";
		/// <summary>Greater or equal</summary>
		public const string Ge = "Ge";
		/// <summary>Begins with</summary>
		public const string Bw = "Bw";
		/// <summary>Does not begin with</summary>
		public const string Bn = "Bn";
		/// <summary>Is in</summary>
		public const string In = "In";
		/// <summary>Is not in</summary>
		public const string Ni = "Ni";
		/// <summary>Ends with</summary>
		public const string Ew = "Ew";
		/// <summary>Does not end with</summary>
		public const string En = "En";
		/// <summary>Contains</summary>
		public const string Cn = "Cn";
		/// <summary>Does not contain</summary>
		public const string Nc = "Nc";
		/// <summary>Is null</summary>
		public const string Nu = "Nu";
		/// <summary>Is not null</summary>
		public const string Nn = "Nn";
		/// <summary>Combines equal, not equal, less, less or equal, greater, greater or equal, is null, is not null.</summary>
		public const string NoTextOperators = "Eq,Ne,Lt,Le,Gt,Ge,Nu,Nn";
		/// <summary>Combines equal, not equal, begins with, does not begin with, ends with, does not end with, contains and does not contain, is null, is not null</summary>
		public const string TextOperators = "Cn,Nc,Eq,Ne,Bw,Bn,Ew,En,Nu,Nn";
		/// <summary>Combines is null, is not null</summary>
		public const string NullOperators = "Nu,Nn";
		/// <summary>Combines equal, less or equal, greater or equal.</summary>
		public const string DateOperators = "Eq,Le,Ge";
	}
}
