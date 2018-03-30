using System;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    internal static class JqGridModelMetadataExtensions
    {
        #region Fields
        private const string _alignmentKey = "JqGridColumnModel.Alignment";
        private const string _cellAttributesKey = "JqGridColumnModel.CellAttributes";
        private const string _classesKey = "JqGridColumnModel.Classes";
        private const string _dateFormatKey = "JqGridColumnModel.DateFormat";
        private const string _fixedKey = "JqGridColumnModel.Fixed";
        private const string _frozenKey = "JqGridColumnModel.Frozen";
        private const string _hideInDialogKey = "JqGridColumnModel.HideInDialog";
        private const string _resizableKey = "JqGridColumnModel.Resizable";
        private const string _titleKey = "JqGridColumnModel.Title";
        private const string _widthKey = "JqGridColumnModel.Width";
        private const string _viewableKey = "JqGridColumnModel.Viewable";
        private const string _editableKey = "JqGridColumnModel.Editable";
        private const string _editOptionsKey = "JqGridColumnModel.EditOptions";
        private const string _editRulesKey = "JqGridColumnModel.EditRules";
        private const string _formOptionsKey = "JqGridColumnModel.FormOptions";
        private const string _editTypeKey = "JqGridColumnModel.EditType";
        private const string _formatterKey = "JqGridColumnModel.Formatter";
        private const string _formatterOptionsKey = "JqGridColumnModel.FormatterOptions";
        private const string _unFormatterKey = "JqGridColumnModel.UnFormatter";
        private const string _labelOptionsKey = "JqGridColumnModel.LabelOptions";
        private const string _searchableKey = "JqGridColumnModel.Searchable";
        private const string _searchOptionsKey = "JqGridColumnModel.SearchOptions";
        private const string _searchRulesKey = "JqGridColumnModel.SearchRules";
        private const string _searchTypeKey = "JqGridColumnModel.SearchType";
        private const string _sortableKey = "JqGridColumnModel.Sortable";
        private const string _sortTypeKey = "JqGridColumnModel.SortType";
        private const string _sortFunctionKey = "JqGridColumnModel.SortFunction";
        private const string _indexKey = "JqGridColumnModel.Index";
        private const string _initialSortingOrderKey = "JqGridColumnModel.InitialSortingOrder";
        private const string _summaryTypeKey = "JqGridColumnModel.SummaryType";
        private const string _summaryTemplateKey = "JqGridColumnModel.SummaryTemplate";
        private const string _summaryFunctionKey = "JqGridColumnModel.SummaryFunction";
        private const string _jsonMappingKey = "JqGridColumnModel.JsonMapping";
        private const string _keyKey = "JqGridColumnModel.Key";
        private const string _xmlMappingKey = "JqGridColumnModel.XmlMapping";
        #endregion

        #region Methods
        internal static void SetColumnAlignment(this ModelMetadata metadata, JqGridAlignments alignment)
        {
            metadata.AdditionalValues.Add(_alignmentKey, alignment);
        }

        internal static JqGridAlignments GetColumnAlignment(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_alignmentKey))
                return (JqGridAlignments)metadata.AdditionalValues[_alignmentKey];
            return JqGridAlignments.Default;
        }

        internal static void SetColumnCellAttributes(this ModelMetadata metadata, SettedString cellAttributes)
        {
            metadata.AdditionalValues.Add(_cellAttributesKey, cellAttributes);
        }

        internal static SettedString GetColumnCellAttributes(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_cellAttributesKey))
                return (SettedString)metadata.AdditionalValues[_cellAttributesKey];
            return null;
        }

        internal static void SetColumnClasses(this ModelMetadata metadata, SettedString classes)
        {
            metadata.AdditionalValues.Add(_classesKey, classes);
        }

        internal static SettedString GetColumnClasses(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_classesKey))
                return (SettedString)metadata.AdditionalValues[_classesKey];
            return null;
        }

        internal static void SetColumnDateFormat(this ModelMetadata metadata, SettedString dateFormat)
        {
            metadata.AdditionalValues.Add(_dateFormatKey, dateFormat);
        }

        internal static SettedString GetColumnDateFormat(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_dateFormatKey))
                return (SettedString)metadata.AdditionalValues[_dateFormatKey];
            return null;
        }

        internal static void SetColumnFixed(this ModelMetadata metadata, bool? @fixed)
        {
            metadata.AdditionalValues.Add(_fixedKey, @fixed);
        }

        internal static bool? GetColumnFixed(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_fixedKey))
                return (bool?)metadata.AdditionalValues[_fixedKey];
            return null;
        }

        internal static void SetColumnFrozen(this ModelMetadata metadata, bool? frozen)
        {
            metadata.AdditionalValues.Add(_frozenKey, frozen);
        }

        internal static bool? GetColumnFrozen(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_frozenKey))
                return (bool?)metadata.AdditionalValues[_frozenKey];
            return null;
        }

        internal static void SetColumnHideInDialog(this ModelMetadata metadata, bool? hideInDialog)
        {
            metadata.AdditionalValues.Add(_hideInDialogKey, hideInDialog);
        }

        internal static bool? GetColumnHideInDialog(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_hideInDialogKey))
                return (bool?)metadata.AdditionalValues[_hideInDialogKey];
            return null;
        }

        internal static void SetColumnResizable(this ModelMetadata metadata, bool? resizable)
        {
            metadata.AdditionalValues.Add(_resizableKey, resizable);
        }

        internal static bool? GetColumnResizable(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_resizableKey))
                return (bool?)metadata.AdditionalValues[_resizableKey];
            return null;
        }

        internal static void SetColumnTitle(this ModelMetadata metadata, bool? title)
        {
            metadata.AdditionalValues.Add(_titleKey, title);
        }

        internal static bool? GetColumnTitle(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_titleKey))
                return (bool?)metadata.AdditionalValues[_titleKey];
            return null;
        }

        internal static void SetColumnWidth(this ModelMetadata metadata, int? width)
        {
            metadata.AdditionalValues.Add(_widthKey, width);
        }

        internal static int? GetColumnWidth(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_widthKey))
                return (int?)metadata.AdditionalValues[_widthKey];
            return null;
        }

        internal static void SetColumnViewable(this ModelMetadata metadata, bool? viewable)
        {
            metadata.AdditionalValues.Add(_viewableKey, viewable);
        }

        internal static bool? GetColumnViewable(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_viewableKey))
                return (bool?)metadata.AdditionalValues[_viewableKey];
            return null;
        }

        internal static void SetColumnLabelOptions(this ModelMetadata metadata, JqGridColumnLabelOptions labelOptions)
        {
            metadata.AdditionalValues.Add(_labelOptionsKey, labelOptions);
        }

        internal static void SetColumnEditable(this ModelMetadata metadata, bool? editable)
        {
            metadata.AdditionalValues.Add(_editableKey, editable);
        }

        internal static bool? GetColumnEditable(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_editableKey))
                return (bool?)metadata.AdditionalValues[_editableKey];
            return null;
        }

        internal static void SetColumnEditOptions(this ModelMetadata metadata, JqGridColumnEditOptions editOptions)
        {
            metadata.AdditionalValues.Add(_editOptionsKey, editOptions);
        }

        internal static JqGridColumnEditOptions GetColumnEditOptions(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_editOptionsKey))
                return (JqGridColumnEditOptions)metadata.AdditionalValues[_editOptionsKey];
            return null;
        }

        internal static void SetColumnEditRules(this ModelMetadata metadata, JqGridColumnRules editRules)
        {
            metadata.AdditionalValues.Add(_editRulesKey, editRules);
        }

        internal static JqGridColumnRules GetColumnEditRules(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_editRulesKey))
                return (JqGridColumnRules)metadata.AdditionalValues[_editRulesKey];
            return null;
        }

        internal static void SetColumnEditType(this ModelMetadata metadata, JqGridColumnEditTypes editType)
        {
            metadata.AdditionalValues.Add(_editTypeKey, editType);
        }

        internal static JqGridColumnEditTypes GetColumnEditType(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_editTypeKey))
                return (JqGridColumnEditTypes)metadata.AdditionalValues[_editTypeKey];
            return JqGridColumnEditTypes.Default;
        }

        internal static void SetColumnFormOptions(this ModelMetadata metadata, JqGridColumnFormOptions formOptions)
        {
            metadata.AdditionalValues.Add(_formOptionsKey, formOptions);
        }

        internal static JqGridColumnFormOptions GetColumnFormOptions(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_formOptionsKey))
                return (JqGridColumnFormOptions)metadata.AdditionalValues[_formOptionsKey];
            return null;
        }

        internal static void SetColumnFormatter(this ModelMetadata metadata, SettedString formatter)
        {
            metadata.AdditionalValues.Add(_formatterKey, formatter);
        }

        internal static SettedString GetColumnFormatter(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_formatterKey))
                return (SettedString)metadata.AdditionalValues[_formatterKey];
            return null;
        }

        internal static void SetColumnFormatterOptions(this ModelMetadata metadata, JqGridColumnFormatterOptions formatterOptions)
        {
            metadata.AdditionalValues.Add(_formatterOptionsKey, formatterOptions);
        }

        internal static JqGridColumnFormatterOptions GetColumnFormatterOptions(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_formatterOptionsKey))
                return (JqGridColumnFormatterOptions)metadata.AdditionalValues[_formatterOptionsKey];
            return null;
        }

        internal static void SetColumnUnFormatter(this ModelMetadata metadata, SettedString unFormatter)
        {
            metadata.AdditionalValues.Add(_unFormatterKey, unFormatter);
        }

        internal static SettedString GetColumnUnFormatter(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_unFormatterKey))
                return (SettedString)metadata.AdditionalValues[_unFormatterKey];
            return null;
        }

        internal static JqGridColumnLabelOptions GetColumnLabelOptions(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_labelOptionsKey))
                return (JqGridColumnLabelOptions)metadata.AdditionalValues[_labelOptionsKey];
            return null;
        }

        internal static void SetColumnSearchable(this ModelMetadata metadata, bool? searchable)
        {
            metadata.AdditionalValues.Add(_searchableKey, searchable);
        }

        internal static bool? GetColumnSearchable(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_searchableKey))
                return (bool?)metadata.AdditionalValues[_searchableKey];
            return null;
        }

        internal static void SetColumnSearchOptions(this ModelMetadata metadata, JqGridColumnSearchOptions searchOptions)
        {
            metadata.AdditionalValues.Add(_searchOptionsKey, searchOptions);
        }

        internal static JqGridColumnSearchOptions GetColumnSearchOptions(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_searchOptionsKey))
                return (JqGridColumnSearchOptions)metadata.AdditionalValues[_searchOptionsKey];
            return null;
        }

        internal static void SetColumnSearchRules(this ModelMetadata metadata, JqGridColumnRules searchRules)
        {
            metadata.AdditionalValues.Add(_searchRulesKey, searchRules);
        }

        internal static JqGridColumnRules GetColumnSearchRules(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_searchRulesKey))
                return (JqGridColumnRules)metadata.AdditionalValues[_searchRulesKey];
            return null;
        }

        internal static void SetColumnSearchType(this ModelMetadata metadata, JqGridColumnSearchTypes searchType)
        {
            metadata.AdditionalValues.Add(_searchTypeKey, searchType);
        }

        internal static JqGridColumnSearchTypes GetColumnSearchType(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_searchTypeKey))
                return (JqGridColumnSearchTypes)metadata.AdditionalValues[_searchTypeKey];
            return JqGridColumnSearchTypes.Default;
        }

        internal static void SetColumnSortable(this ModelMetadata metadata, bool? sortable)
        {
            metadata.AdditionalValues.Add(_sortableKey, sortable);
        }

        internal static bool? GetColumnSortable(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_sortableKey))
                return (bool?)metadata.AdditionalValues[_sortableKey];
            return null;
        }

        internal static void SetColumnSortType(this ModelMetadata metadata, JqGridColumnSortTypes sortType)
        {
            metadata.AdditionalValues.Add(_sortTypeKey, sortType);
        }

        internal static JqGridColumnSortTypes GetColumnSortType(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_sortTypeKey))
                return (JqGridColumnSortTypes)metadata.AdditionalValues[_sortTypeKey];
            return JqGridColumnSortTypes.Default;
        }

        internal static void SetColumnSortFunction(this ModelMetadata metadata, SettedString sortFunction)
        {
            metadata.AdditionalValues.Add(_sortFunctionKey, sortFunction);
        }

        internal static SettedString GetColumnSortFunction(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_sortFunctionKey))
                return (SettedString)metadata.AdditionalValues[_sortFunctionKey];
            return null;
        }

        internal static void SetColumnIndex(this ModelMetadata metadata, SettedString index)
        {
            metadata.AdditionalValues.Add(_indexKey, index);
        }

        internal static SettedString GetColumnIndex(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_indexKey))
                return (SettedString)metadata.AdditionalValues[_indexKey];
            return null;
        }

        internal static void SetColumnInitialSortingOrder(this ModelMetadata metadata, JqGridSortingOrders initialSortingOrder)
        {
            metadata.AdditionalValues.Add(_initialSortingOrderKey, initialSortingOrder);
        }

        internal static JqGridSortingOrders GetColumnInitialSortingOrder(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_initialSortingOrderKey))
                return (JqGridSortingOrders)metadata.AdditionalValues[_initialSortingOrderKey];
            return JqGridSortingOrders.Default;
        }

        internal static void SetColumnSummaryType(this ModelMetadata metadata, JqGridColumnSummaryTypes type)
        {
            metadata.AdditionalValues.Add(_summaryTypeKey, type);
        }

        internal static JqGridColumnSummaryTypes? GetColumnSummaryType(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_summaryTypeKey))
                return (JqGridColumnSummaryTypes)metadata.AdditionalValues[_summaryTypeKey];
            return null;
        }

        internal static void SetColumnSummaryTemplate(this ModelMetadata metadata, SettedString template)
        {
            metadata.AdditionalValues.Add(_summaryTemplateKey, template);
        }

        internal static SettedString GetColumnSummaryTemplate(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_summaryTemplateKey))
                return (SettedString)metadata.AdditionalValues[_summaryTemplateKey];
            return null;
        }

        internal static void SetColumnSummaryFunction(this ModelMetadata metadata, string function)
        {
            metadata.AdditionalValues.Add(_summaryFunctionKey, function);
        }

        internal static string GetColumnSummaryFunction(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_summaryFunctionKey))
                return (string)metadata.AdditionalValues[_summaryFunctionKey];
            return null;
        }

        internal static void SetColumnJsonMapping(this ModelMetadata metadata, string jsonMapping)
        {
            metadata.AdditionalValues.Add(_jsonMappingKey, jsonMapping);
        }

        internal static string GetColumnJsonMapping(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_jsonMappingKey))
                return (string)metadata.AdditionalValues[_jsonMappingKey];
            return null;
        }

        internal static void SetColumnKey(this ModelMetadata metadata, bool? key)
        {
            metadata.AdditionalValues.Add(_keyKey, key);
        }

        internal static bool? GetColumnKey(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_keyKey))
                return (bool?)metadata.AdditionalValues[_keyKey];
            return null;
        }

        internal static void SetColumnXmlMapping(this ModelMetadata metadata, string xmlMapping)
        {
            metadata.AdditionalValues.Add(_xmlMappingKey, xmlMapping);
        }

        internal static string GetColumnXmlMapping(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(_xmlMappingKey))
                return (string)metadata.AdditionalValues[_xmlMappingKey];
            return String.Empty;
        }
        #endregion
    }
}
