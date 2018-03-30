using System;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
	/// <summary>
	/// Specifies the mapping properties for the column.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class JqGridColumnMappingAttribute : Attribute, IMetadataAware
	{
		private bool? _key;

		#region Properties
		/// <summary>
		/// Gets or sets the JSON mapping for the column in the incoming JSON string.
		/// </summary>
		public string JsonMapping { get; set; }

		/// <summary>
		/// Defines if this column value should be used as unique row id (in case there is no id from the server). 
		/// </summary>
		public bool Key
		{
			get => _key ?? false;
			set => _key = value;
		}

		/// <summary>
		/// Gets or sets the XML mapping for the column in the incomming XML file.
		/// </summary>
		public string XmlMapping { get; set; }
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the JqGridColumnMappingAttribute class.
		/// </summary>
		public JqGridColumnMappingAttribute()
		{
			JsonMapping = null;
			XmlMapping = null;
		}
		#endregion

		#region IMetadataAware
		/// <summary>
		/// Provides metadata to the model metadata creation process.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		public void OnMetadataCreated(ModelMetadata metadata)
		{
			metadata.SetColumnJsonMapping(JsonMapping);
			metadata.SetColumnKey(_key);
			metadata.SetColumnXmlMapping(XmlMapping);
		}
		#endregion
	}
}
