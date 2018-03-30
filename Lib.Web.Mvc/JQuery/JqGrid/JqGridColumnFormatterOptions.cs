using Lib.Web.Mvc.JQuery.JqGrid.Constants;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
	/// <summary>
	/// Class which represents options for predefined formatter.
	/// </summary>
	public class JqGridColumnFormatterOptions
	{
		#region Fields
		private int? _decimalPlaces;
		private string _decimalSeparator;
		private string _defaultValue;
		private string _prefix;
		private string _suffix;
		private string _sourceFormat;
		private string _outputFormat;
		private string _baseLinkUrl;
		private string _showAction;
		private string _addParam;
		private string _target;
		private string _idName;
		private string _thousandsSeparator;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the decimal places.
		/// </summary>
		public int? DecimalPlaces
		{
			get => _decimalPlaces;
			set => _decimalPlaces = value < 0 ? null : value;
		}

		internal bool IsDecimalSeparatorSetted { get; private set; }
		/// <summary>
		/// Gets or sets the decimal separator.
		/// </summary>
		public string DecimalSeparator
		{
			get => _decimalSeparator;
			set
			{
				_decimalSeparator = value;
				IsDecimalSeparatorSetted = true;
			}
		}

		internal bool IsDefaultValueSetted { get; private set; }
		/// <summary>
		/// Gets or sets the default value.
		/// </summary>
		public string DefaultValue
		{
			get => _defaultValue;
			set
			{
				_defaultValue = value;
				IsDefaultValueSetted = true;
			}
		}

		/// <summary>
		/// Gets or sets the value indicating if checkbox is disabled.
		/// </summary>
		public bool? Disabled { get; set; }

		internal bool IsPrefixSetted { get; private set; }
		/// <summary>
		/// Gets or sets the currency prefix.
		/// </summary>
		public string Prefix
		{
			get => _prefix;
			set
			{
				_prefix = value;
				IsPrefixSetted = true;
			}
		}

		internal bool IsSuffixSetted { get; private set; }
		/// <summary>
		/// Gets or sets the currency suffix.
		/// </summary>
		public string Suffix
		{
			get => _suffix;
			set
			{
				_suffix = value;
				IsSuffixSetted = true;
			}
		}

		internal bool IsSourceFormatSetted { get; private set; }
		/// <summary>
		/// Gets or sets the date source format.
		/// </summary>
		public string SourceFormat
		{
			get => _sourceFormat;
			set
			{
				_sourceFormat = value;
				IsSourceFormatSetted = true;
			}
		}

		internal bool IsOutputFormatSetted { get; private set; }
		/// <summary>
		/// Gets or sets the date output format.
		/// </summary>
		public string OutputFormat
		{
			get => _outputFormat;
			set
			{
				_outputFormat = value;
				IsOutputFormatSetted = true;
			}
		}

		internal bool IsBaseLinkUrlSetted { get; private set; }
		/// <summary>
		/// Gets or sets the link for showlink formatter.
		/// </summary>
		public string BaseLinkUrl
		{
			get => _baseLinkUrl;
			set
			{
				_baseLinkUrl = value;
				IsBaseLinkUrlSetted = true;
			}
		}

		internal bool IsShowActionSetted { get; private set; }
		/// <summary>
		/// Gets or sets the additional value which is added after the BaseLinkUrl.
		/// </summary>
		public string ShowAction
		{
			get => _showAction;
			set
			{
				_showAction = value;
				IsShowActionSetted = true;
			}
		}

		internal bool IsAddParamSetted { get; private set; }
		/// <summary>
		/// Gets or sets the additional parameter which can be added after the IdName property.
		/// </summary>
		public string AddParam
		{
			get => _addParam;
			set
			{
				_addParam = value;
				IsAddParamSetted = true;
			}
		}

		internal bool IsTargetSetted { get; private set; }
		/// <summary>
		/// Gets or sets the target.
		/// </summary>
		public string Target
		{
			get => _target;
			set
			{
				_target = value;
				IsTargetSetted = true;
			}
		}

		internal bool IsIdNameSetted { get; private set; }
		/// <summary>
		/// Gets or sets the first parameter that is added after the ShowAction.
		/// </summary>
		public string IdName
		{
			get => _idName;
			set
			{
				_idName = value;
				IsIdNameSetted = true;
			}
		}

		internal bool IsThousandsSeparatorSetted { get; private set; }
		/// <summary>
		/// Gets or sets the thousands separator.
		/// </summary>
		public string ThousandsSeparator
		{
			get => _thousandsSeparator;
			set
			{
				_thousandsSeparator = value;
				IsThousandsSeparatorSetted = true;
			}
		}

		internal bool EditButton { get; set; }

		internal bool DeleteButton { get; set; }

		internal bool UseFormEditing { get; set; }

		internal JqGridInlineNavigatorActionOptions InlineEditingOptions { get; set; }

		internal JqGridNavigatorEditActionOptions FormEditingOptions { get; set; }

		internal JqGridNavigatorDeleteActionOptions DeleteOptions { get; set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes new instance of JqGridColumnFormatterOptions class.
		/// </summary>
		public JqGridColumnFormatterOptions()
		{

			EditButton = true;
			DeleteButton = true;
			UseFormEditing = false;
			InlineEditingOptions = null;
			FormEditingOptions = null;
			DeleteOptions = null;
		}
		#endregion

		#region Methods
		internal bool IsDefault(string formatter)
		{
			switch (formatter)
			{
				case JqGridColumnPredefinedFormatters.Integer:
					return !IsDefaultValueSetted && !IsThousandsSeparatorSetted;
				case JqGridColumnPredefinedFormatters.Number:
					return !DecimalPlaces.HasValue && !IsDecimalSeparatorSetted && !IsDefaultValueSetted && !IsThousandsSeparatorSetted;
				case JqGridColumnPredefinedFormatters.Currency:
					return !DecimalPlaces.HasValue && !IsDecimalSeparatorSetted && !IsDefaultValueSetted && !IsPrefixSetted && !IsSuffixSetted && !IsThousandsSeparatorSetted;
				case JqGridColumnPredefinedFormatters.Date:
					return !IsSourceFormatSetted && !IsOutputFormatSetted;
				case JqGridColumnPredefinedFormatters.Link:
					return string.IsNullOrWhiteSpace(Target);
				case JqGridColumnPredefinedFormatters.ShowLink:
					return !IsBaseLinkUrlSetted && !IsShowActionSetted && !IsAddParamSetted && !IsTargetSetted && !IsIdNameSetted;
				case JqGridColumnPredefinedFormatters.CheckBox:
					return !Disabled.HasValue;
				case JqGridColumnPredefinedFormatters.Actions:
					return EditButton && DeleteButton && !UseFormEditing && InlineEditingOptions == null && FormEditingOptions == null && DeleteOptions == null;
				default:
					return true;
			}
		}
		#endregion
	}
}
