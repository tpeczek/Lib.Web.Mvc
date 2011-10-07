using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Helper class for generating jqGrid HMTL and JavaScript
    /// </summary>
    /// <typeparam name="TModel">Type of model for this grid</typeparam>
    public class JqGridHelper<TModel>
    {
        #region Fields
        private JqGridOptions<TModel> _options;
        private JqGridNavigatorOptions _navigatorOptions;
        private JqGridNavigatorActionOptions _navigatorEditActionOptions;
        private JqGridNavigatorActionOptions _navigatorAddActionOptions;
        private JqGridNavigatorActionOptions _navigatorDeleteActionOptions;
        private JqGridNavigatorActionOptions _navigatorSearchActionOptions;
        private JqGridNavigatorActionOptions _navigatorViewActionOptions;
        private List<JqGridNavigatorControlOptions> _navigatorControlsOptions = new List<JqGridNavigatorControlOptions>();
        private bool _filterToolbar = false;
        private JqGridFilterToolbarOptions _filterToolbarOptions;
        private List<JqGridFilterGridRowModel> _filterGridModel;
        private JqGridFilterGridOptions _filterGridOptions;
        private IDictionary<string, object> _footerData = null;
        private bool _footerDataUseFormatters = true;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridHelper class.
        /// </summary>
        /// <param name="id">Identifier, which will be used for table (id='{0}'), pager div (id='{0}Pager'), filter grid div (id='{0}Search') and in JavaScript.</param>
        /// <param name="afterInsertRow">The function for event which is raised after every inserted row.</param>
        /// <param name="afterEditCell">The function for event which is raised after the edited cell is edited.</param>
        /// <param name="afterRestoreCell">The function for event which is raised after calling the method restoreCell or the user press ESC leaving the changes.</param>
        /// <param name="afterSaveCell">The function for event which is raised after the cell has been successfully saved.</param>
        /// <param name="afterSubmitCell">The function for event which is raised after the cell and other data is posted to the server.</param>
        /// <param name="autoWidth">The value indicating if the grid width will be recalculated automatically to the width of the parent element.</param>
        /// <param name="beforeRequest">The function for event which is raised before requesting any data.</param>
        /// <param name="beforeSelectRow">The function for event which is raised when the user click on the row, but before selecting it.</param>
        /// <param name="beforeEditCell">The function for event which is raised before editing the cell.</param>
        /// <param name="beforeSaveCell">The function for event which is raised before validation of values if any.</param>
        /// <param name="beforeSubmitCell">The function for event which is raised before submit the cell content to the server.</param>
        /// <param name="caption">The caption for the grid.</param>
        /// <param name="cellEditingEnabled">The value indicating if cell editing is enabled.</param>
        /// <param name="cellEditingSubmitMode">The cell editing submit mode.</param>
        /// <param name="cellEditingUrl">The URL for cell editing submit.</param>
        /// <param name="dataString">The string of data which will be used when DataType is set to JqGridDataTypes.XmlString or JqGridDataTypes.JsonString.</param>
        /// <param name="dataType">The type of information to expect to represent data in the grid.</param>
        /// <param name="dynamicScrollingMode">The value which defines if dynamic scrolling is enabled.</param>
        /// <param name="dynamicScrollingTimeout">The timeout (in miliseconds) if DynamicScrollingMode is set to JqGridDynamicScrollingModes.HoldVisibleRows.</param>
        /// <param name="editingUrl">The url for inline and form editing</param>
        /// <param name="expandColumnClick">The value which defines whether the tree is expanded and/or collapsed when user clicks on the text of the expanded column, not only on the image.</param>
        /// <param name="expandColumn">The name of column which should be used to expand the tree grid.</param>
        /// <param name="errorCell">The function for event which is raised when there is a server error while saving cell.</param>
        /// <param name="formatCell">The function for event which allows formatting the cell content before editing.</param>
        /// <param name="footerEnabled">The value indicating if the footer table (with one row) will be placed below the grid records and above the pager.</param>
        /// <param name="groupingEnabled">The value indicating if the grouping is enabled.</param>
        /// <param name="groupingView">The grouping view options.</param>
        /// <param name="height">The height of the grid in pixels (default 'auto').</param>
        /// <param name="hidden">The value which defines whether the grid is initialy hidden (no data loaded, only caption layer is shown). Takes effect only if the caption is not empty string and hiddenEnabled is true.</param>
        /// <param name="hiddenEnabled">The value which defines whether the show/hide grid button is enabled. Takes effect only if the caption is not empty string.</param>
        /// <param name="jsonReader">The JSON reader for the grid.</param>
        /// <param name="loadBeforeSend">The function for pre-callback to modify the XMLHttpRequest object before it is sent.</param>
        /// <param name="loadError">The function for event which is raised after the request fails.</param>
        /// <param name="loadComplete">The function for event which is raised immediately after every server request.</param>
        /// <param name="methodType">The type of request to make.</param>
        /// <param name="gridComplete">The function for event which is raised after all the data is loaded into the grid and all other processes are complete.</param>
        /// <param name="onCellSelect">The function for event which is raised when user clicks on particular cell in the grid.</param>
        /// <param name="onDoubleClickRow">The function for event which is raised immediately after row was double clicked.</param>
        /// <param name="onHeaderClick">The function for event which is raised after clicking to hide or show grid.</param>
        /// <param name="onPaging">The function for event which is raised before populating the data after page index/size change.</param>
        /// <param name="onRightClickRow">The function for event which is raised immediately after row was right clicked.</param>
        /// <param name="onSelectAll">The function for event which is raised when MultipleSelect option is true and user clicks on the header checkbox.</param>
        /// <param name="onSelectCell">The function for event which is raised after the cell is selected for editing.</param>
        /// <param name="onSelectRow">The function for event which is raised immediately after row was clicked.</param>
        /// <param name="onSortCol">The function for event which is raised immediately after sortable column was clicked and before sorting the data.</param>
        /// <param name="pager">If grid should use a pager bar to navigate through the records.</param>
        /// <param name="parametersNames">The customized names for jqGrid request parameters.</param>
        /// <param name="resizeStart">The function for event which is raised when user starts resizing a column.</param>
        /// <param name="resizeStop">The function for event which is raised after the column is resized.</param>
        /// <param name="rowsList">The array to construct a select box element in the pager in which user can change the number of the visible rows.</param>
        /// <param name="rowsNumber">How many records should be displayed in the grid.</param>
        /// <param name="scrollOffset">The width of vertical scrollbar.</param>
        /// <param name="serializeCellData">The function for event which can serialize the data passed to the ajax request when the cell is being saved.</param>
        /// <param name="serializeGridData">The function for event which can serialize the data passed to the ajax request.</param>
        /// <param name="serializeSubGridData">The function for event which can serialize the data passed to the subgrid ajax request.</param>
        /// <param name="sortingName">The initial sorting column index, when  using data returned from server.</param>
        /// <param name="sortingOrder">The initial sorting order, when  using data returned from server.</param>
        /// <param name="subgridEnabled">The value which defines if subgrid is enabled.</param>
        /// <param name="subgridModel">The subgrid model.</param>
        /// <param name="subgridUrl">The url for subgrid data requests.</param>
        /// <param name="subgridColumnWidth">The width of subgrid expand/colapse column.</param>
        /// <param name="subGridBeforeExpand">The function for event which is raised just before expanding the subgrid.</param>
        /// <param name="subGridRowColapsed">The function for event which is raised when the user clicks on the plus icon of the grid.</param>
        /// <param name="subGridRowExpanded">The function for event which is raised when the user clicks on the minus icon of the grid.</param>
        /// <param name="treeGridEnabled">The value which defines if TreeGrid is enabled.</param>
        /// <param name="treeGridModel">The model for TreeGrid.</param>
        /// <param name="url">The url for data requests.</param>
        /// <param name="userDataOnFooter">The value indicating if the values from user data should be placed on footer.</param>
        /// <param name="viewRecords">If grid should display the beginning and ending record number out of the total number of records in the query.</param>
        /// <param name="width">The width of the grid in pixels.</param>
        public JqGridHelper(string id, string afterInsertRow = null, string afterEditCell = null, string afterRestoreCell = null, string afterSaveCell = null, string afterSubmitCell = null, bool autoWidth = false, string beforeRequest = null, string beforeSelectRow = null, string beforeEditCell = null, string beforeSaveCell = null, string beforeSubmitCell = null, string caption = null, bool cellEditingEnabled = false, JqGridCellEditingSubmitModes cellEditingSubmitMode = JqGridCellEditingSubmitModes.Remote, string cellEditingUrl = null, string dataString = null, JqGridDataTypes dataType = JqGridDataTypes.Xml, JqGridDynamicScrollingModes dynamicScrollingMode = JqGridDynamicScrollingModes.Disabled, int dynamicScrollingTimeout = 200, string editingUrl = null, bool expandColumnClick = true, string expandColumn = null, int? height = null, string errorCell = null, string formatCell = null, bool footerEnabled = false, bool groupingEnabled = false, JqGridGroupingView groupingView = null, bool hidden = false, bool hiddenEnabled = true, JqGridJsonReader jsonReader = null, string loadBeforeSend = null, string loadError = null, string loadComplete = null, JqGridMethodTypes methodType = JqGridMethodTypes.Get, string gridComplete = null, string onCellSelect = null, string onDoubleClickRow = null, string onHeaderClick = null, string onPaging = null, string onRightClickRow = null, string onSelectAll = null, string onSelectCell = null, string onSelectRow = null, string onSortCol = null, bool pager = false, JqGridParametersNames parametersNames = null, string resizeStart = null, string resizeStop = null, List<int> rowsList = null, int rowsNumber = 20, int scrollOffset = 18, string serializeCellData = null, string serializeGridData = null, string serializeSubGridData = null, string sortingName = "", JqGridSortingOrders sortingOrder = JqGridSortingOrders.Asc, bool subgridEnabled = false, JqGridSubgridModel subgridModel = null, string subgridUrl = null, int subgridColumnWidth = 20, string subGridBeforeExpand = null, string subGridRowColapsed = null, string subGridRowExpanded = null, bool treeGridEnabled = false, JqGridTreeGridModels treeGridModel = JqGridTreeGridModels.Nested, string url = null, bool userDataOnFooter = false, bool viewRecords = false, int? width = null)
            : this(new JqGridOptions<TModel>(id) { AfterInsertRow = afterInsertRow, AfterEditCell = afterEditCell, AfterRestoreCell = afterRestoreCell, AfterSaveCell = afterSaveCell, AfterSubmitCell = afterSubmitCell, AutoWidth = autoWidth, BeforeRequest = beforeRequest, BeforeSelectRow = beforeSelectRow, BeforeEditCell = beforeEditCell, BeforeSaveCell = beforeSaveCell, BeforeSubmitCell = beforeSubmitCell, Caption = caption, CellEditingEnabled = cellEditingEnabled, CellEditingSubmitMode = cellEditingSubmitMode, CellEditingUrl = cellEditingUrl, DataString = dataString, DataType = dataType, DynamicScrollingMode = dynamicScrollingMode, DynamicScrollingTimeout = dynamicScrollingTimeout, EditingUrl = editingUrl, ExpandColumnClick = expandColumnClick, ExpandColumn = expandColumn, ErrorCell = errorCell, FormatCell = formatCell, FooterEnabled = footerEnabled, GroupingEnabled = groupingEnabled, GroupingView = groupingView, Height = height, Hidden = hidden, HiddenEnabled = hiddenEnabled, JsonReader = (jsonReader != null) ? jsonReader : JqGridResponse.JsonReader, LoadBeforeSend = loadBeforeSend, LoadError = loadError, LoadComplete = loadComplete, MethodType = methodType, GridComplete = gridComplete, OnCellSelect = onCellSelect, OnDoubleClickRow = onDoubleClickRow, OnHeaderClick = onHeaderClick, OnPaging = onPaging, OnRightClickRow = onRightClickRow, OnSelectAll = onSelectAll, OnSelectRow = onSelectRow, OnSelectCell = onSelectCell, OnSortCol = onSortCol, Pager = pager, ParametersNames = (parametersNames != null) ? parametersNames : JqGridRequest.ParameterNames, RowsList = rowsList, RowsNumber = rowsNumber, ScrollOffset = scrollOffset, SerializeCellData = serializeCellData, SerializeGridData = serializeGridData, SerializeSubGridData = serializeSubGridData, SortingName = sortingName, SortingOrder = sortingOrder, SubgridEnabled = subgridEnabled, SubgridModel = subgridModel, SubgridUrl = subgridUrl, SubgridColumnWidth = subgridColumnWidth, SubGridBeforeExpand = subGridBeforeExpand, SubGridRowColapsed = subGridRowColapsed, SubGridRowExpanded = subGridRowExpanded, TreeGridEnabled = treeGridEnabled, TreeGridModel = treeGridModel, Url = url, UserDataOnFooter = userDataOnFooter, ViewRecords = viewRecords, Width = width })
        { }

        /// <summary>
        /// Initializes a new instance of the JqGridHelper class.
        /// </summary>
        /// <param name="options">Options for the grid</param>
        public JqGridHelper(JqGridOptions<TModel> options)
        {
            _options = options;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the HTML that is used to render the table placeholder for the grid. 
        /// </summary>
        /// <returns>The HTML that represents the table placeholder for jqGrid</returns>
        public MvcHtmlString GetTableHtml()
        {
            return MvcHtmlString.Create(String.Format("<table id='{0}'></table>", _options.Id));
        }

        /// <summary>
        /// Returns the HTML that is used to render the pager (div) placeholder for the grid. 
        /// </summary>
        /// <returns>The HTML that represents the pager (div) placeholder for jqGrid</returns>
        public MvcHtmlString GetPagerHtml()
        {
            return MvcHtmlString.Create(String.Format("<div id='{0}Pager'></div>", _options.Id));
        }

        /// <summary>
        /// Returns the HTML that is used to render the filter grid (div) placeholder for the grid. 
        /// </summary>
        /// <returns>The HTML that represents the filter grid (div) placeholder for jqGrid</returns>
        public MvcHtmlString GetFilterGridHtml()
        {
            return MvcHtmlString.Create(String.Format("<div id='{0}Search'></div>", _options.Id));
        }

        /// <summary>
        /// Returns the HTML that is used to render the table placeholder for the grid with pager placeholder below it and filter grid (if enabled) placeholder above it.
        /// </summary>
        /// <returns>The HTML that represents the table placeholder for jqGrid with pager placeholder below i</returns>
        public MvcHtmlString GetHtml()
        {
            if (_filterGridModel != null)
                return MvcHtmlString.Create(GetFilterGridHtml().ToHtmlString() + GetTableHtml().ToHtmlString() + GetPagerHtml().ToHtmlString());
            else
                return MvcHtmlString.Create(GetTableHtml().ToHtmlString() + GetPagerHtml().ToHtmlString());
        }

        /// <summary>
        /// Return the JavaScript that is used to initialize jqGrid with given options.
        /// </summary>
        /// <returns>The JavaScript that initializes jqGrid with give options</returns>
        public MvcHtmlString GetJavaScript()
        {
            StringBuilder javaScriptBuilder = new StringBuilder();

            javaScriptBuilder.AppendFormat("$('#{0}').jqGrid({{", _options.Id).AppendLine();
            AppendColumnsNames(ref javaScriptBuilder);
            AppendColumnsModels(ref javaScriptBuilder);
            AppendOptions(ref javaScriptBuilder);
            javaScriptBuilder.Append("})");

            foreach (JqGridColumnModel columnModel in _options.ColumnsModels)
            {
                if (columnModel.LabelOptions != null)
                    AppendColumnLabelOptions(columnModel.Name, columnModel.LabelOptions, ref javaScriptBuilder);
            }

            if (_options.FooterEnabled && _footerData != null && _footerData.Count > 0)
                AppendFooterData(ref javaScriptBuilder);

            if (_navigatorOptions != null)
                AppendNavigator(ref javaScriptBuilder);

            if (_filterToolbar)
                AppendFilterToolbar(ref javaScriptBuilder);

            javaScriptBuilder.AppendLine(";");

            if (_filterGridModel != null)
                AppendFilterGrid(ref javaScriptBuilder);

            return MvcHtmlString.Create(javaScriptBuilder.ToString());
        }

        private void AppendColumnsNames(ref StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.Append("colNames: [");

            foreach(string columnName in _options.ColumnsNames)
                javaScriptBuilder.AppendFormat("'{0}',", columnName);

            if (javaScriptBuilder[javaScriptBuilder.Length - 1] == ',')
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);

            javaScriptBuilder.AppendLine("],");
        }

        private void AppendColumnsModels(ref StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.AppendLine("colModel: [");

            int lastColumnModelIndex = _options.ColumnsModels.Count - 1;
            for (int columnModelIndex = 0; columnModelIndex < _options.ColumnsModels.Count; columnModelIndex++)
            {
                JqGridColumnModel columnModel = _options.ColumnsModels[columnModelIndex];
                javaScriptBuilder.Append("{ ");

                if (columnModel.Alignment != JqGridAlignments.Left)
                    javaScriptBuilder.AppendFormat("align: '{0}', ", columnModel.Alignment.ToString().ToLower());

                if (!String.IsNullOrWhiteSpace(columnModel.Index))
                    javaScriptBuilder.AppendFormat("index: '{0}', ", columnModel.Index);

                if (!String.IsNullOrWhiteSpace(columnModel.Classes))
                    javaScriptBuilder.AppendFormat("classes: '{0}', ", columnModel.Classes);

                if (columnModel.Editable)
                {
                    javaScriptBuilder.Append("editable: true, ");
                    if (columnModel.EditType != JqGridColumnEditTypes.Text)
                        javaScriptBuilder.AppendFormat("edittype: '{0}', ", columnModel.EditType.ToString().ToLower());
                    AppendEditOptions(columnModel.EditOptions, ref javaScriptBuilder);
                    AppendColumnRules("editrules", columnModel.EditRules, ref javaScriptBuilder);
                    AppendFormOptions(columnModel.FormOptions, ref javaScriptBuilder);
                }

                if (columnModel.Fixed)
                    javaScriptBuilder.Append("fixed: true, ");

                if (!String.IsNullOrWhiteSpace(columnModel.Formatter))
                {
                    javaScriptBuilder.AppendFormat("formatter: {0}, ", columnModel.Formatter);
                    AppendFormatterOptions(columnModel.Formatter, columnModel.FormatterOptions, ref javaScriptBuilder);
                    if (!String.IsNullOrWhiteSpace(columnModel.UnFormatter))
                        javaScriptBuilder.AppendFormat("unformat: {0}, ", columnModel.UnFormatter);
                }

                if (columnModel.InitialSortingOrder != JqGridSortingOrders.Asc)
                    javaScriptBuilder.Append("firstsortorder: 'desc', ");

                if (columnModel.Hidden)
                    javaScriptBuilder.Append("hidden: true, ");

                if (!columnModel.Resizable)
                    javaScriptBuilder.Append("resizable: false, ");

                if (columnModel.Searchable)
                {
                    if (columnModel.SearchType != JqGridColumnSearchTypes.Text)
                        javaScriptBuilder.AppendFormat("stype: '{0}', ", columnModel.SearchType.ToString().ToLower());
                    AppendSearchOptions(columnModel.SearchOptions, ref javaScriptBuilder);
                    AppendColumnRules("searchrules", columnModel.SearchRules, ref javaScriptBuilder);
                }
                else
                    javaScriptBuilder.AppendFormat("search: false, ");

                if (_options.GroupingEnabled)
                {
                    if (columnModel.SummaryType.HasValue)
                    {
                        if (columnModel.SummaryType.Value != JqGridColumnSummaryTypes.Custom)
                            javaScriptBuilder.AppendFormat("summaryType: '{0}', ", columnModel.SummaryType.Value.ToString().ToLower());
                        else
                            javaScriptBuilder.AppendFormat("summaryType: {0}, ", columnModel.SummaryFunction);
                    }

                    if (columnModel.SummaryTemplate != "{0}")
                        javaScriptBuilder.AppendFormat("summaryTpl: '{0}', ", columnModel.SummaryTemplate);
                }

                if (!columnModel.Sortable)
                    javaScriptBuilder.AppendFormat("sortable: false, ");

                if (columnModel.Width != 150)
                    javaScriptBuilder.AppendFormat("width: {0}, ", columnModel.Width);

                javaScriptBuilder.AppendFormat("name: '{0}' }}", columnModel.Name);

                if (lastColumnModelIndex == columnModelIndex)
                    javaScriptBuilder.AppendLine();
                else
                    javaScriptBuilder.AppendLine(",");
            }
            
            if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 1);

            javaScriptBuilder.AppendLine("],");
        }

        private static void AppendEditOptions(JqGridColumnEditOptions editOptions, ref StringBuilder javaScriptBuilder)
        {
            if (editOptions != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                javaScriptBuilder.Append("editoptions: { ");

                if (!String.IsNullOrWhiteSpace(editOptions.CustomElementFunction))
                    javaScriptBuilder.AppendFormat("custom_element: {0}, ", editOptions.CustomElementFunction);

                if (!String.IsNullOrWhiteSpace(editOptions.CustomValueFunction))
                    javaScriptBuilder.AppendFormat("custom_value: {0}, ", editOptions.CustomValueFunction);

                AppendElementOptions(editOptions, serializer, ref javaScriptBuilder);

                if (editOptions.NullIfEmpty)
                    javaScriptBuilder.AppendFormat("NullIfEmpty: true, ", editOptions.CustomValueFunction);

                if (editOptions.HtmlAttributes != null && editOptions.HtmlAttributes.Count > 0)
                {
                    string htmlAttributesSerialized = serializer.Serialize(editOptions.HtmlAttributes);
                    javaScriptBuilder.AppendFormat("{0}, ", htmlAttributesSerialized.Substring(1, htmlAttributesSerialized.Length - 2));
                }

                if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
                {
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                    javaScriptBuilder.Append(" }, ");
                }
                else
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 15, 15);
            }
        }

        private static void AppendElementOptions(JqGridColumnElementOptions elementOptions, JavaScriptSerializer serializer, ref StringBuilder javaScriptBuilder)
        {
            if (!String.IsNullOrWhiteSpace(elementOptions.BuildSelect))
                javaScriptBuilder.AppendFormat("buildSelect: {0}, ", elementOptions.BuildSelect);

            if (elementOptions.DataEvents != null && elementOptions.DataEvents.Count() > 0)
            {
                javaScriptBuilder.Append("dataEvents: [ ");
                foreach (JqGridColumnDataEvent dataEvent in elementOptions.DataEvents)
                {
                    if (dataEvent.Data == null)
                        javaScriptBuilder.AppendFormat("{ type: '{0}', fn: {1} }, ", dataEvent.Type, dataEvent.Function);
                    else
                        javaScriptBuilder.AppendFormat("{ type: '{0}', data: {1}, fn: {2} }, ", dataEvent.Type, serializer.Serialize(dataEvent.Data), dataEvent.Function);
                }
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                javaScriptBuilder.Append(" ], ");
            }

            if (!String.IsNullOrWhiteSpace(elementOptions.DataInit))
                javaScriptBuilder.AppendFormat("dataInit: {0}, ", elementOptions.DataInit);

            if (!String.IsNullOrWhiteSpace(elementOptions.DataUrl))
                javaScriptBuilder.AppendFormat("dataUrl: '{0}', ", elementOptions.DataUrl);

            if (!String.IsNullOrWhiteSpace(elementOptions.DefaultValue))
                javaScriptBuilder.AppendFormat("defaultValue: '{0}', ", elementOptions.DefaultValue);
        }

        private static void AppendColumnRules(string rulesName, JqGridColumnRules rules, ref StringBuilder javaScriptBuilder)
        {
            if (rules != null)
            {
                javaScriptBuilder.AppendFormat("{0}: {{ ", rulesName);

                if (rules.Custom)
                {
                    javaScriptBuilder.Append("custom: true, ");

                    if (!String.IsNullOrWhiteSpace(rules.CustomFunction))
                        javaScriptBuilder.AppendFormat("custom_func: {0}, ", rules.CustomFunction);
                }

                if (rules.Date)
                    javaScriptBuilder.Append("date: true, ");

                if (rules.EditHidden)
                    javaScriptBuilder.Append("edithidden: true, ");

                if (rules.Email)
                    javaScriptBuilder.Append("email: true, ");

                if (rules.Integer)
                    javaScriptBuilder.Append("integer: true, ");

                if (rules.MaxValue.HasValue)
                    javaScriptBuilder.AppendFormat("maxValue: {0},", rules.MaxValue.Value);

                if (rules.MinValue.HasValue)
                    javaScriptBuilder.AppendFormat("minValue: {0},", rules.MinValue.Value);

                if (rules.Number)
                    javaScriptBuilder.Append("number: true, ");

                if (rules.Required)
                    javaScriptBuilder.Append("required: true, ");

                if (rules.Time)
                    javaScriptBuilder.Append("time: true, ");

                if (rules.Url)
                    javaScriptBuilder.Append("url: true, ");

                if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
                {
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                    javaScriptBuilder.Append(" }, ");
                }
                else
                {
                    int rulesNameLength = rulesName.Length + 4;
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - rulesNameLength, rulesNameLength);
                }
            }
        }

        private static void AppendFormOptions(JqGridColumnFormOptions formOptions, ref StringBuilder javaScriptBuilder)
        {
            if (formOptions != null)
            {
                javaScriptBuilder.Append("formoptions: { ");

                if (formOptions.ColumnPosition.HasValue)
                    javaScriptBuilder.AppendFormat("colpos: {0},", formOptions.ColumnPosition.Value);

                if (!String.IsNullOrWhiteSpace(formOptions.ElementPrefix))
                    javaScriptBuilder.AppendFormat("elmprefix: '{0}',", formOptions.ElementPrefix);

                if (!String.IsNullOrWhiteSpace(formOptions.ElementSuffix))
                    javaScriptBuilder.AppendFormat("elmsuffix: '{0}',", formOptions.ElementSuffix);

                if (!String.IsNullOrWhiteSpace(formOptions.Label))
                    javaScriptBuilder.AppendFormat("label: '{0}',", formOptions.Label);

                if (formOptions.RowPosition.HasValue)
                    javaScriptBuilder.AppendFormat("rowpos: {0},", formOptions.RowPosition.Value);

                if (javaScriptBuilder[javaScriptBuilder.Length - 1] == ',')
                {
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                    javaScriptBuilder.Append(" }, ");
                }
                else
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 15, 15);
            }
        }

        private static void AppendFormatterOptions(string formatter, JqGridColumnFormatterOptions formatterOptions, ref StringBuilder javaScriptBuilder)
        {
            if (formatterOptions != null && !formatterOptions.IsDefault(formatter))
            { 
                javaScriptBuilder.Append("formatoptions: { ");

                switch (formatter)
                {
                    case JqGridColumnPredefinedFormatters.Integer:
                        javaScriptBuilder.AppendFormat("{0}{1}", (formatterOptions.DefaultValue == JqGridOptionsDefaults.IntegerFormatterDefaultValue) ? String.Empty : String.Format("defaultValue: '{0}', ", formatterOptions.DefaultValue), (formatterOptions.ThousandsSeparator == JqGridOptionsDefaults.FormatterThousandsSeparator) ? String.Empty : String.Format("thousandsSeparator: '{0}', ", formatterOptions.ThousandsSeparator));
                        break;
                    case JqGridColumnPredefinedFormatters.Number:
                        javaScriptBuilder.AppendFormat("{0}{1}{2}{3}", (formatterOptions.DecimalPlaces == JqGridOptionsDefaults.FormatterDecimalPlaces) ? String.Empty : String.Format("decimalPlaces: {0}, ", formatterOptions.DecimalPlaces), (formatterOptions.DecimalSeparator == JqGridOptionsDefaults.FormatterDecimalSeparator) ? String.Empty : String.Format("decimalSeparator: '{0}', ", formatterOptions.DecimalSeparator), (formatterOptions.DefaultValue == JqGridOptionsDefaults.NumberFormatterDefaultValue) ? String.Empty : String.Format("defaultValue: '{0}', ", formatterOptions.DefaultValue), (formatterOptions.ThousandsSeparator == JqGridOptionsDefaults.FormatterThousandsSeparator) ? String.Empty : String.Format("thousandsSeparator: '{0}', ", formatterOptions.ThousandsSeparator));
                        break;
                    case JqGridColumnPredefinedFormatters.Currency:
                        javaScriptBuilder.AppendFormat("{0}{1}{2}{3}{4}{5}", (formatterOptions.DecimalPlaces == JqGridOptionsDefaults.FormatterDecimalPlaces) ? String.Empty : String.Format("decimalPlaces: {0}, ", formatterOptions.DecimalPlaces), (formatterOptions.DecimalSeparator == JqGridOptionsDefaults.FormatterDecimalSeparator) ? String.Empty : String.Format("decimalSeparator: '{0}', ", formatterOptions.DecimalSeparator), (formatterOptions.DefaultValue == JqGridOptionsDefaults.NumberFormatterDefaultValue) ? String.Empty : String.Format("defaultValue: '{0}', ", formatterOptions.DefaultValue), String.IsNullOrWhiteSpace(formatterOptions.Prefix) ? String.Empty : String.Format("prefix: '{0}', ", formatterOptions.Prefix), String.IsNullOrWhiteSpace(formatterOptions.Suffix) ? String.Empty : String.Format("suffix: '{0}', ", formatterOptions.Suffix), (formatterOptions.ThousandsSeparator == JqGridOptionsDefaults.FormatterThousandsSeparator) ? String.Empty : String.Format("thousandsSeparator: '{0}', ", formatterOptions.ThousandsSeparator));
                        break;
                    case JqGridColumnPredefinedFormatters.Date:
                        javaScriptBuilder.AppendFormat("{0}{1}", (formatterOptions.SourceFormat == JqGridOptionsDefaults.FormatterSourceFormat) ? String.Empty : String.Format("srcformat: '{0}', ", formatterOptions.SourceFormat), (formatterOptions.OutputFormat == JqGridOptionsDefaults.FormatterOutputFormat) ? String.Empty : String.Format("newformat: '{0}', ", formatterOptions.OutputFormat));
                        break;
                    case JqGridColumnPredefinedFormatters.Link:
                        javaScriptBuilder.AppendFormat("target: '{0}', ", formatterOptions.Target);
                        break;
                    case JqGridColumnPredefinedFormatters.ShowLink:
                        javaScriptBuilder.AppendFormat("{0}{1}{2}{3}{4}", String.IsNullOrWhiteSpace(formatterOptions.BaseLinkUrl) ? String.Empty : String.Format("baseLinkUrl: '{0}', ", formatterOptions.BaseLinkUrl), String.IsNullOrWhiteSpace(formatterOptions.ShowAction) ? String.Empty : String.Format("showAction: '{0}', ", formatterOptions.ShowAction), String.IsNullOrWhiteSpace(formatterOptions.AddParam) ? String.Empty : String.Format("addParam: '{0}', ", formatterOptions.AddParam), String.IsNullOrWhiteSpace(formatterOptions.Target) ? String.Empty : String.Format("target: '{0}', ", formatterOptions.Target), (formatterOptions.IdName == JqGridOptionsDefaults.FormatterIdName) ? String.Empty : String.Format("idName: '{0}', ", formatterOptions.IdName));
                        break;
                    case JqGridColumnPredefinedFormatters.CheckBox:
                        javaScriptBuilder.Append("disabled: false, ");
                        break;
                }

                if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
                {
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                    javaScriptBuilder.Append(" }, ");
                }
                else
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 17, 17);
            }
        }

        private static void AppendSearchOptions(JqGridColumnSearchOptions searchOptions, ref StringBuilder javaScriptBuilder)
        {
            if (searchOptions != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                javaScriptBuilder.Append("searchoptions: { ");

                AppendElementOptions(searchOptions, serializer, ref javaScriptBuilder);

                if (searchOptions.HtmlAttributes != null && searchOptions.HtmlAttributes.Count > 0)
                    javaScriptBuilder.AppendFormat("attr: {0}, ", serializer.Serialize(searchOptions.HtmlAttributes));

                if (searchOptions.SearchHidden)
                    javaScriptBuilder.AppendFormat("searchhidden: true, ");

                if (searchOptions.SearchOperators != (JqGridSearchOperators)16383)
                {
                    javaScriptBuilder.Append("sopt: [ ");
                    foreach (JqGridSearchOperators searchOperator in Enum.GetValues(typeof(JqGridSearchOperators)))
                    {
                        if ((searchOptions.SearchOperators & searchOperator) == searchOperator)
                            javaScriptBuilder.AppendFormat("'{0}',", Enum.GetName(typeof(JqGridSearchOperators), searchOperator).ToLower());
                    }
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                    javaScriptBuilder.Append("], ");
                }

                if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
                {
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                    javaScriptBuilder.Append(" }, ");
                }
                else
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 17, 17);
            }
        }

        private void AppendOptions(ref StringBuilder javaScriptBuilder)
        {
            if (!String.IsNullOrWhiteSpace(_options.AfterInsertRow))
                javaScriptBuilder.AppendFormat("afterInsertRow: {0},", _options.AfterInsertRow).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.AfterEditCell))
                javaScriptBuilder.AppendFormat("afterEditCell: {0},", _options.AfterEditCell).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.AfterRestoreCell))
                javaScriptBuilder.AppendFormat("afterRestoreCell: {0},", _options.AfterRestoreCell).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.AfterSaveCell))
                javaScriptBuilder.AppendFormat("afterSaveCell: {0},", _options.AfterSaveCell).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.AfterSubmitCell))
                javaScriptBuilder.AppendFormat("afterSubmitCell: {0},", _options.AfterSubmitCell).AppendLine();

            if (_options.AutoWidth)
                javaScriptBuilder.Append("autowidth: true,").AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.BeforeRequest))
                javaScriptBuilder.AppendFormat("beforeRequest: {0},", _options.BeforeRequest).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.BeforeSelectRow))
                javaScriptBuilder.AppendFormat("beforeSelectRow: {0},", _options.BeforeSelectRow).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.BeforeEditCell))
                javaScriptBuilder.AppendFormat("beforeEditCell: {0},", _options.BeforeEditCell).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.BeforeSaveCell))
                javaScriptBuilder.AppendFormat("beforeSaveCell: {0},", _options.BeforeSaveCell).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.BeforeSubmitCell))
                javaScriptBuilder.AppendFormat("beforeSubmitCell: {0},", _options.BeforeSubmitCell).AppendLine();

            if (_options.CellEditingEnabled)
            {
                javaScriptBuilder.Append("cellEdit: true,").AppendLine();
                if (_options.CellEditingSubmitMode != JqGridCellEditingSubmitModes.Remote)
                    javaScriptBuilder.Append("cellsubmit: 'clientArray',").AppendLine();

                if (!String.IsNullOrWhiteSpace(_options.CellEditingUrl))
                    javaScriptBuilder.AppendFormat("cellurl: '{0}',", _options.CellEditingUrl).AppendLine();
            }

            if (!String.IsNullOrEmpty(_options.Caption))
            {
                javaScriptBuilder.AppendFormat("caption: '{0}',", _options.Caption).AppendLine();

                if (!_options.HiddenEnabled)
                    javaScriptBuilder.Append("hidegrid: false,").AppendLine();
                else if (_options.Hidden)
                    javaScriptBuilder.Append("hiddengrid: true,").AppendLine();
            }

            if (_options.UseDataString())
                javaScriptBuilder.AppendFormat("datastr: '{0}',", _options.DataString).AppendLine();
            else
                javaScriptBuilder.AppendFormat("url: '{0}',", _options.Url).AppendLine();

            if (_options.DataType != JqGridDataTypes.Xml)
                javaScriptBuilder.AppendFormat("datatype: '{0}',", _options.DataType.ToString().ToLower()).AppendLine();

            if (_options.DynamicScrollingMode == JqGridDynamicScrollingModes.HoldAllRows)
                javaScriptBuilder.Append("scroll: true,").AppendLine();
            else if (_options.DynamicScrollingMode == JqGridDynamicScrollingModes.HoldVisibleRows)
            {
                javaScriptBuilder.Append("scroll: 10,").AppendLine();
                if (_options.DynamicScrollingTimeout != 200)
                    javaScriptBuilder.AppendFormat("scrollTimeout: {0},", _options.DynamicScrollingTimeout).AppendLine();
            }

            if (!String.IsNullOrWhiteSpace(_options.EditingUrl))
                javaScriptBuilder.AppendFormat("editurl: '{0}',", _options.EditingUrl).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.ErrorCell))
                javaScriptBuilder.AppendFormat("errorCell: {0},", _options.ErrorCell).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.FormatCell))
                javaScriptBuilder.AppendFormat("formatCell: {0},", _options.FormatCell).AppendLine();

            if (_options.FooterEnabled)
            {
                javaScriptBuilder.Append("footerrow: true,").AppendLine();
                if (_options.UserDataOnFooter)
                    javaScriptBuilder.Append("userDataOnFooter: true,").AppendLine();
            }

            if (_options.GroupingEnabled)
            {
                javaScriptBuilder.Append("grouping: true,").AppendLine();
                AppendGroupingView(ref javaScriptBuilder);
            }

            AppendJsonReader(ref javaScriptBuilder);

            if (!String.IsNullOrWhiteSpace(_options.LoadBeforeSend))
                javaScriptBuilder.AppendFormat("loadBeforeSend: {0},", _options.LoadBeforeSend).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.LoadComplete))
                javaScriptBuilder.AppendFormat("loadComplete: {0},", _options.LoadComplete).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.LoadError))
                javaScriptBuilder.AppendFormat("loadError: {0},", _options.LoadError).AppendLine();

            if (_options.MethodType != JqGridMethodTypes.Get)
                javaScriptBuilder.AppendFormat("mtype: 'POST',").AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.GridComplete))
                javaScriptBuilder.AppendFormat("gridComplete: {0},", _options.GridComplete).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnCellSelect))
                javaScriptBuilder.AppendFormat("onCellSelect: {0},", _options.OnCellSelect).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnDoubleClickRow))
                javaScriptBuilder.AppendFormat("ondblClickRow: {0},", _options.OnDoubleClickRow).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnHeaderClick))
                javaScriptBuilder.AppendFormat("onHeaderClick: {0},", _options.OnHeaderClick).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnPaging))
                javaScriptBuilder.AppendFormat("onPaging: {0},", _options.OnPaging).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnRightClickRow))
                javaScriptBuilder.AppendFormat("onRightClickRow: {0},", _options.OnRightClickRow).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnSelectAll))
                javaScriptBuilder.AppendFormat("onSelectAll: {0},", _options.OnSelectAll).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnSelectCell))
                javaScriptBuilder.AppendFormat("onSelectCell: {0},", _options.OnSelectCell).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnSelectRow))
                javaScriptBuilder.AppendFormat("onSelectRow: {0},", _options.OnSelectRow).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnSortCol))
                javaScriptBuilder.AppendFormat("onSortCol: {0},", _options.OnSortCol).AppendLine();

            if (_options.Pager)
                javaScriptBuilder.AppendFormat("pager: '#{0}Pager',", _options.Id).AppendLine();

            AppendParametersNames(ref javaScriptBuilder);

            if (!String.IsNullOrWhiteSpace(_options.ResizeStart))
                javaScriptBuilder.AppendFormat("resizeStart: {0},", _options.ResizeStart).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.ResizeStop))
                javaScriptBuilder.AppendFormat("resizeStop: {0},", _options.ResizeStop).AppendLine();

            if (_options.RowsList != null && _options.RowsList.Count > 0)
            {
                javaScriptBuilder.Append("rowList: [");
                foreach (int rowsNumber in _options.RowsList)
                    javaScriptBuilder.AppendFormat("{0},", rowsNumber);
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                javaScriptBuilder.Append("],").AppendLine();
            }

            if (_options.RowsNumber != 20)
                javaScriptBuilder.AppendFormat("rowNum: {0},", _options.RowsNumber).AppendLine();

            if (_options.ScrollOffset != 18)
                javaScriptBuilder.AppendFormat("scrollOffset: {0},", _options.ScrollOffset).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.SerializeCellData))
                javaScriptBuilder.AppendFormat("serializeCellData: {0},", _options.SerializeCellData).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.SerializeGridData))
                javaScriptBuilder.AppendFormat("serializeGridData: {0},", _options.SerializeGridData).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.SerializeSubGridData))
                javaScriptBuilder.AppendFormat("serializeSubGridData: {0},", _options.SerializeSubGridData).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.SortingName))
                javaScriptBuilder.AppendFormat("sortname: '{0}',", _options.SortingName).AppendLine();

            if (_options.SortingOrder != JqGridSortingOrders.Asc)
                javaScriptBuilder.AppendFormat("sortorder: 'desc',").AppendLine();

            if (_options.SubgridEnabled)
            {
                javaScriptBuilder.AppendFormat("subGrid: true,").AppendLine();

                AppendSubgridModel(ref javaScriptBuilder);

                if (!String.IsNullOrWhiteSpace(_options.SubgridUrl))
                    javaScriptBuilder.AppendFormat("subGridUrl: '{0}',", _options.SubgridUrl).AppendLine();

                if (_options.SubgridColumnWidth != 20)
                    javaScriptBuilder.AppendFormat("subGridWidth: {0},", _options.SubgridColumnWidth).AppendLine();

                if (!String.IsNullOrWhiteSpace(_options.SubGridBeforeExpand))
                    javaScriptBuilder.AppendFormat("subGridBeforeExpand: {0},", _options.SubGridBeforeExpand).AppendLine();

                if (!String.IsNullOrWhiteSpace(_options.SubGridRowColapsed))
                    javaScriptBuilder.AppendFormat("subGridRowExpanded: {0},", _options.SubGridRowColapsed).AppendLine();

                if (!String.IsNullOrWhiteSpace(_options.SubGridRowExpanded))
                    javaScriptBuilder.AppendFormat("subGridRowColapsed: {0},", _options.SubGridRowExpanded).AppendLine();
            }

            if (_options.TreeGridEnabled)
            {
                if (!_options.ExpandColumnClick)
                    javaScriptBuilder.AppendFormat("ExpandColClick: false,").AppendLine();

                if (!String.IsNullOrWhiteSpace(_options.ExpandColumn))
                    javaScriptBuilder.AppendFormat("ExpandColumn: '{0}',", _options.ExpandColumn).AppendLine();

                javaScriptBuilder.AppendFormat("treeGrid: true,").AppendLine();

                if (_options.TreeGridModel != JqGridTreeGridModels.Nested)
                    javaScriptBuilder.AppendFormat("treeGridModel: 'adjacency',").AppendLine();
            }

            if (_options.ViewRecords)
                javaScriptBuilder.AppendFormat("viewrecords: true,").AppendLine();

            if (_options.Width.HasValue)
                javaScriptBuilder.AppendFormat("width: {0},", _options.Width.Value).AppendLine();

            if (_options.Height.HasValue)
                javaScriptBuilder.AppendFormat("height: {0}", _options.Height.Value).AppendLine();
            else
                javaScriptBuilder.AppendLine("height: '100%'");
        }

        private void AppendJsonReader(ref StringBuilder javaScriptBuilder)
        {
            if (!_options.JsonReader.IsDefault())
            {
                javaScriptBuilder.Append("jsonReader: { ");

                if (_options.JsonReader.Records != JqGridOptionsDefaults.ResponseRecords)
                    javaScriptBuilder.AppendFormat("root: '{0}', ", _options.JsonReader.Records);

                if (_options.JsonReader.PageIndex != JqGridOptionsDefaults.ResponsePageIndex)
                    javaScriptBuilder.AppendFormat("page: '{0}', ", _options.JsonReader.PageIndex);

                if (_options.JsonReader.TotalPagesCount != JqGridOptionsDefaults.ResponseTotalPagesCount)
                    javaScriptBuilder.AppendFormat("total: '{0}', ", _options.JsonReader.TotalPagesCount);

                if (_options.JsonReader.TotalRecordsCount != JqGridOptionsDefaults.ResponseTotalRecordsCount)
                    javaScriptBuilder.AppendFormat("records: '{0}', ", _options.JsonReader.TotalRecordsCount);

                if (!_options.JsonReader.RepeatItems)
                    javaScriptBuilder.Append("repeatitems: false, ");

                if (_options.JsonReader.RecordValues != JqGridOptionsDefaults.ResponseRecordValues)
                    javaScriptBuilder.AppendFormat("cell: '{0}', ", _options.JsonReader.RecordValues);

                if (_options.JsonReader.RecordId != JqGridOptionsDefaults.ResponseRecordId)
                    javaScriptBuilder.AppendFormat("id: '{0}', ", _options.JsonReader.RecordId);

                if (_options.JsonReader.UserData != JqGridOptionsDefaults.ResponseUserData)
                    javaScriptBuilder.AppendFormat("userdata: '{0}', ", _options.JsonReader.UserData);

                if (!_options.JsonReader.SubgridReader.IsDefault())
                {
                    javaScriptBuilder.Append("subgrid: { ");

                    if (_options.JsonReader.SubgridReader.Records != JqGridOptionsDefaults.ResponseRecords)
                        javaScriptBuilder.AppendFormat("root: '{0}', ", _options.JsonReader.SubgridReader.Records);

                    if (!_options.JsonReader.SubgridReader.RepeatItems)
                        javaScriptBuilder.Append("repeatitems: false, ");

                    if (_options.JsonReader.SubgridReader.RecordValues != JqGridOptionsDefaults.ResponseRecordValues)
                        javaScriptBuilder.AppendFormat("cell: '{0}', ", _options.JsonReader.SubgridReader.RecordValues);

                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                    javaScriptBuilder.Append(" }, ");
                }

                javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                javaScriptBuilder.Append(" }, ").AppendLine();
            }
        }

        private void AppendParametersNames(ref StringBuilder javaScriptBuilder)
        {
            if (!_options.ParametersNames.IsDefault())
            {
                javaScriptBuilder.Append("prmNames: { ");

                if (_options.ParametersNames.PageIndex != JqGridOptionsDefaults.RequestPageIndex)
                    javaScriptBuilder.AppendFormat("page: '{0}', ", _options.ParametersNames.PageIndex);

                if (_options.ParametersNames.RecordsCount != JqGridOptionsDefaults.RequestRecordsCount)
                    javaScriptBuilder.AppendFormat("rows: '{0}', ", _options.ParametersNames.RecordsCount);

                if (_options.ParametersNames.SortingName != JqGridOptionsDefaults.RequestSortingName)
                    javaScriptBuilder.AppendFormat("sort: '{0}', ", _options.ParametersNames.SortingName);

                if (_options.ParametersNames.SortingOrder != JqGridOptionsDefaults.RequestSortingOrder)
                    javaScriptBuilder.AppendFormat("order: '{0}', ", _options.ParametersNames.SortingOrder);

                if (_options.ParametersNames.Searching != JqGridOptionsDefaults.RequestSearching)
                    javaScriptBuilder.AppendFormat("search: '{0}', ", _options.ParametersNames.Searching);

                if (_options.ParametersNames.Id != JqGridOptionsDefaults.RequestId)
                    javaScriptBuilder.AppendFormat("id: '{0}', ", _options.ParametersNames.Id);

                if (_options.ParametersNames.Operator != JqGridOptionsDefaults.RequestOperator)
                    javaScriptBuilder.AppendFormat("oper: '{0}', ", _options.ParametersNames.Operator);

                if (_options.ParametersNames.EditOperator != JqGridOptionsDefaults.RequestEditOperator)
                    javaScriptBuilder.AppendFormat("editoper: '{0}', ", _options.ParametersNames.EditOperator);

                if (_options.ParametersNames.AddOperator != JqGridOptionsDefaults.RequestAddOperator)
                    javaScriptBuilder.AppendFormat("addoper: '{0}', ", _options.ParametersNames.AddOperator);

                if (_options.ParametersNames.DeleteOperator != JqGridOptionsDefaults.RequestDeleteOperator)
                    javaScriptBuilder.AppendFormat("deloper: '{0}', ", _options.ParametersNames.DeleteOperator);

                if (_options.ParametersNames.SubgridId != JqGridOptionsDefaults.RequestSubgridId)
                    javaScriptBuilder.AppendFormat("subgridid: '{0}', ", _options.ParametersNames.SubgridId);

                if (!String.IsNullOrWhiteSpace(_options.ParametersNames.PagesCount))
                    javaScriptBuilder.AppendFormat("npage: '{0}', ", _options.ParametersNames.PagesCount);

                if (_options.ParametersNames.TotalRows != JqGridOptionsDefaults.RequestTotalRows)
                    javaScriptBuilder.AppendFormat("totalrows: '{0}', ", _options.ParametersNames.TotalRows);

                javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                javaScriptBuilder.Append(" }, ").AppendLine();
            }
        }

        private void AppendGroupingView(ref StringBuilder javaScriptBuilder)
        {
            if (_options.GroupingView != null)
            {
                javaScriptBuilder.Append("groupingView: { ");

                if (_options.GroupingView.Fields != null && _options.GroupingView.Fields.Length > 0)
                {
                    javaScriptBuilder.Append("groupField: [");
                    foreach(string field in _options.GroupingView.Fields)
                        javaScriptBuilder.AppendFormat("'{0}', ", field);
                    javaScriptBuilder.Insert(javaScriptBuilder.Length - 2, ']');
                }

                if (_options.GroupingView.Orders != null && _options.GroupingView.Orders.Length > 0 && _options.GroupingView.Orders.Contains(JqGridSortingOrders.Desc))
                {
                    javaScriptBuilder.Append("groupOrder: [");
                    foreach (JqGridSortingOrders order in _options.GroupingView.Orders)
                        javaScriptBuilder.AppendFormat("'{0}', ", order.ToString().ToLower());
                    javaScriptBuilder.Insert(javaScriptBuilder.Length - 2, ']');
                }

                if (_options.GroupingView.Texts != null && _options.GroupingView.Texts.Length > 0)
                {
                    javaScriptBuilder.Append("groupText: [");
                    foreach (string text in _options.GroupingView.Texts)
                        javaScriptBuilder.AppendFormat("'{0}', ", text);
                    javaScriptBuilder.Insert(javaScriptBuilder.Length - 2, ']');
                }

                if (_options.GroupingView.Summary != null && _options.GroupingView.Summary.Length > 0 && _options.GroupingView.Summary.Contains(true))
                {
                    javaScriptBuilder.Append("groupSummary: [");
                    foreach (bool summary in _options.GroupingView.Summary)
                        javaScriptBuilder.AppendFormat("{0}, ", summary.ToString().ToLower());
                    javaScriptBuilder.Insert(javaScriptBuilder.Length - 2, ']');
                }

                if (_options.GroupingView.ColumnShow != null && _options.GroupingView.ColumnShow.Length > 0 && _options.GroupingView.ColumnShow.Contains(false))
                {
                    javaScriptBuilder.Append("groupColumnShow: [");
                    foreach (bool columnShow in _options.GroupingView.ColumnShow)
                        javaScriptBuilder.AppendFormat("{0}, ", columnShow.ToString().ToLower());
                    javaScriptBuilder.Insert(javaScriptBuilder.Length - 2, ']');
                }

                if (_options.GroupingView.SummaryOnHide)
                    javaScriptBuilder.Append("showSummaryOnHide: true, ");

                if (_options.GroupingView.DataSorted)
                    javaScriptBuilder.Append("groupDataSorted: true, ");

                if (_options.GroupingView.Collapse)
                    javaScriptBuilder.Append("groupCollapse: true, ");

                if (_options.GroupingView.PlusIcon != JqGridOptionsDefaults.GroupingPlusIcon)
                    javaScriptBuilder.AppendFormat("plusicon: '{0}', ", _options.GroupingView.PlusIcon);

                if (_options.GroupingView.MinusIcon != JqGridOptionsDefaults.GroupingMinusIcon)
                    javaScriptBuilder.AppendFormat("minusicon: '{0}', ", _options.GroupingView.MinusIcon);

                if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
                {
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                    javaScriptBuilder.Append(" }, ").AppendLine();
                }
                else
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 16, 16);
            }
        }

        private void AppendSubgridModel(ref StringBuilder javaScriptBuilder)
        {
            if (_options.SubgridModel != null)
            {
                javaScriptBuilder.AppendLine("subGridModel: [{ ");

                javaScriptBuilder.Append("name: [");
                foreach (string columnName in _options.SubgridModel.ColumnsNames)
                    javaScriptBuilder.AppendFormat("'{0}',", columnName);
                if (_options.SubgridModel.ColumnsNames.Count > 0)
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                javaScriptBuilder.AppendLine("],");

                javaScriptBuilder.Append("align: [");
                foreach (JqGridAlignments alignments in _options.SubgridModel.ColumnsAlignments)
                    javaScriptBuilder.AppendFormat("'{0}',", alignments.ToString().ToLower());
                if (_options.SubgridModel.ColumnsAlignments.Count > 0)
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                javaScriptBuilder.AppendLine("],");

                javaScriptBuilder.Append("width: [");
                foreach (int width in _options.SubgridModel.ColumnsWidths)
                    javaScriptBuilder.AppendFormat("{0},", width);
                if (_options.SubgridModel.ColumnsWidths.Count > 0)
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                javaScriptBuilder.AppendLine("]");

                javaScriptBuilder.AppendLine(" }],");
            }
        }

        private void AppendNavigator(ref StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.AppendFormat(".jqGrid('navGrid', '#{0}Pager',", _options.Id).AppendLine();
            AppendNavigatorOptions(ref javaScriptBuilder);

            if (_navigatorEditActionOptions != null || _navigatorAddActionOptions != null || _navigatorDeleteActionOptions != null || _navigatorSearchActionOptions != null || _navigatorViewActionOptions != null)
            {
                javaScriptBuilder.AppendLine(",");
                AppendNavigatorActionOptions(_navigatorEditActionOptions, ref javaScriptBuilder);
                if (_navigatorAddActionOptions != null || _navigatorDeleteActionOptions != null || _navigatorSearchActionOptions != null || _navigatorViewActionOptions != null)
                {
                    javaScriptBuilder.AppendLine(",");
                    AppendNavigatorActionOptions(_navigatorAddActionOptions, ref javaScriptBuilder);
                    if (_navigatorDeleteActionOptions != null || _navigatorSearchActionOptions != null || _navigatorViewActionOptions != null)
                    {
                        javaScriptBuilder.AppendLine(",");
                        AppendNavigatorActionOptions(_navigatorDeleteActionOptions, ref javaScriptBuilder);
                        if (_navigatorSearchActionOptions != null || _navigatorViewActionOptions != null)
                        {
                            javaScriptBuilder.AppendLine(",");
                            AppendNavigatorActionOptions(_navigatorSearchActionOptions, ref javaScriptBuilder);
                            if (_navigatorViewActionOptions != null)
                            {
                                javaScriptBuilder.AppendLine(",");
                                AppendNavigatorActionOptions(_navigatorViewActionOptions, ref javaScriptBuilder);
                            }
                        }
                    }
                }
            }

            javaScriptBuilder.Append(")");

            foreach (JqGridNavigatorControlOptions controlOptions in _navigatorControlsOptions)
            {
                if (controlOptions is JqGridNavigatorButtonOptions)
                    AppendNavigatorButton((JqGridNavigatorButtonOptions)controlOptions, ref javaScriptBuilder);
                else if (controlOptions is JqGridNavigatorSeparatorOptions)
                    AppendNavigatorSeparator((JqGridNavigatorSeparatorOptions)controlOptions, ref javaScriptBuilder);
            }
        }

        private void AppendNavigatorOptions(ref StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.Append("{ ");

            if (!_navigatorOptions.Add)
                javaScriptBuilder.Append("add: false, ");

            if (_navigatorOptions.AddIcon != JqGridNavigatorDefaults.AddIcon)
                javaScriptBuilder.AppendFormat("addicon: '{0}', ", _navigatorOptions.AddIcon);

            if (!String.IsNullOrEmpty(_navigatorOptions.AddText))
                javaScriptBuilder.AppendFormat("addtext: '{0}', ", _navigatorOptions.AddText);

            if (_navigatorOptions.AddToolTip != JqGridNavigatorDefaults.AddToolTip)
                javaScriptBuilder.AppendFormat("addtitle: '{0}', ", _navigatorOptions.AddToolTip);

            if (_navigatorOptions.AlertCaption != JqGridNavigatorDefaults.AlertCaption)
                javaScriptBuilder.AppendFormat("alertcap: '{0}', ", _navigatorOptions.AlertCaption);

            if (_navigatorOptions.AlertText != JqGridNavigatorDefaults.AlertText)
                javaScriptBuilder.AppendFormat("alerttext: '{0}', ", _navigatorOptions.AlertText);

            if (_navigatorOptions.CloneToTop)
                javaScriptBuilder.Append("cloneToTop: true, ");

           if (!_navigatorOptions.CloseOnEscape)
                javaScriptBuilder.Append("closeOnEscape: false, ");

            if (!_navigatorOptions.Delete)
                javaScriptBuilder.Append("del: false, ");

            if (_navigatorOptions.DeleteIcon != JqGridNavigatorDefaults.DeleteIcon)
                javaScriptBuilder.AppendFormat("delicon: '{0}', ", _navigatorOptions.DeleteIcon);

            if (!String.IsNullOrEmpty(_navigatorOptions.DeleteText))
                javaScriptBuilder.AppendFormat("deltext: '{0}', ", _navigatorOptions.DeleteText);

            if (_navigatorOptions.DeleteToolTip != JqGridNavigatorDefaults.DeleteToolTip)
                javaScriptBuilder.AppendFormat("deltitle: '{0}', ", _navigatorOptions.DeleteToolTip);

            if (!_navigatorOptions.Edit)
                javaScriptBuilder.Append("edit: false, ");

            if (_navigatorOptions.EditIcon != JqGridNavigatorDefaults.EditIcon)
                javaScriptBuilder.AppendFormat("editicon: '{0}', ", _navigatorOptions.EditIcon);

            if (!String.IsNullOrEmpty(_navigatorOptions.EditText))
                javaScriptBuilder.AppendFormat("edittext: '{0}', ", _navigatorOptions.EditText);

            if (_navigatorOptions.EditToolTip != JqGridNavigatorDefaults.EditToolTip)
                javaScriptBuilder.AppendFormat("edittitle: '{0}', ", _navigatorOptions.EditToolTip);

            if (_navigatorOptions.Position != JqGridAlignments.Left)
                javaScriptBuilder.AppendFormat("position: '{0}', ", _navigatorOptions.Position.ToString().ToLower());

            if (!_navigatorOptions.Refresh)
                javaScriptBuilder.Append("refresh: false, ");

            if (_navigatorOptions.RefreshIcon != JqGridNavigatorDefaults.RefreshIcon)
                javaScriptBuilder.AppendFormat("refreshicon: '{0}', ", _navigatorOptions.RefreshIcon);

            if (!String.IsNullOrEmpty(_navigatorOptions.RefreshText))
                javaScriptBuilder.AppendFormat("refreshtext: '{0}', ", _navigatorOptions.RefreshText);

            if (_navigatorOptions.RefreshToolTip != JqGridNavigatorDefaults.RefreshToolTip)
                javaScriptBuilder.AppendFormat("refreshtitle: '{0}', ", _navigatorOptions.RefreshToolTip);

            if (_navigatorOptions.RefreshMode != JqGridRefreshModes.FirstPage)
                javaScriptBuilder.Append("refreshstate: 'current', ");

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.AfterRefresh))
                javaScriptBuilder.AppendFormat("afterRefresh: {0}, ", _navigatorOptions.AfterRefresh);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.BeforeRefresh))
                javaScriptBuilder.AppendFormat("beforeRefresh: {0}, ", _navigatorOptions.BeforeRefresh);

            if (!_navigatorOptions.Search)
                javaScriptBuilder.Append("search: false, ");

            if (_navigatorOptions.SearchIcon != JqGridNavigatorDefaults.SearchIcon)
                javaScriptBuilder.AppendFormat("searchicon: '{0}', ", _navigatorOptions.SearchIcon);

            if (!String.IsNullOrEmpty(_navigatorOptions.SearchText))
                javaScriptBuilder.AppendFormat("searchtext: '{0}', ", _navigatorOptions.SearchText);

            if (_navigatorOptions.SearchToolTip != JqGridNavigatorDefaults.SearchToolTip)
                javaScriptBuilder.AppendFormat("searchtitle: '{0}', ", _navigatorOptions.SearchToolTip);

            if (_navigatorOptions.View)
                javaScriptBuilder.Append("view: true, ");

            if (_navigatorOptions.ViewIcon != JqGridNavigatorDefaults.ViewIcon)
                javaScriptBuilder.AppendFormat("viewicon: '{0}', ", _navigatorOptions.ViewIcon);

            if (!String.IsNullOrEmpty(_navigatorOptions.ViewText))
                javaScriptBuilder.AppendFormat("viewtext: '{0}', ", _navigatorOptions.ViewText);

            if (_navigatorOptions.ViewToolTip != JqGridNavigatorDefaults.ViewToolTip)
                javaScriptBuilder.AppendFormat("viewtitle: '{0}', ", _navigatorOptions.ViewToolTip);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.AddFunction))
                javaScriptBuilder.AppendFormat("addfunc: {0}, ", _navigatorOptions.AddFunction);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.EditFunction))
                javaScriptBuilder.AppendFormat("editfunc: {0}, ", _navigatorOptions.EditFunction);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.DeleteFunction))
                javaScriptBuilder.AppendFormat("delfunc: {0}, ", _navigatorOptions.DeleteFunction);

            if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
            {
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                javaScriptBuilder.Append(" }");
            }
            else
                javaScriptBuilder.Append("}");
        }

        private static void AppendNavigatorActionOptions(JqGridNavigatorActionOptions actionOptions, ref StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.Append("{ ");

            if (actionOptions != null)
            {
                if (!actionOptions.ClearAfterAdd)
                    javaScriptBuilder.Append("clearAfterAdd: false, ");

                if (actionOptions.CloseAfterAdd)
                    javaScriptBuilder.Append("closeAfterAdd: true, ");

                if (actionOptions.CloseAfterEdit)
                    javaScriptBuilder.Append("closeAfterEdit: true, ");

                if (actionOptions.CloseOnEscape)
                    javaScriptBuilder.Append("closeOnEscape: true, ");

                if (!actionOptions.Dragable)
                    javaScriptBuilder.Append("drag: false, ");

                if (!actionOptions.UseJqModal)
                    javaScriptBuilder.Append("jqModal: false, ");

                if (actionOptions.Modal)
                    javaScriptBuilder.Append("modal: true, ");

                if (actionOptions.MethodType != JqGridMethodTypes.Post)
                    javaScriptBuilder.Append("mtype: 'GET', ");

                if (!String.IsNullOrWhiteSpace(actionOptions.AfterShowForm))
                    javaScriptBuilder.AppendFormat("afterShowForm: {0}, ", actionOptions.AfterShowForm);

                if (!String.IsNullOrWhiteSpace(actionOptions.AfterSubmit))
                    javaScriptBuilder.AppendFormat("afterSubmit: {0}, ", actionOptions.AfterSubmit);

                if (!String.IsNullOrWhiteSpace(actionOptions.BeforeShowForm))
                    javaScriptBuilder.AppendFormat("beforeShowForm: {0}, ", actionOptions.BeforeShowForm);

                if (!String.IsNullOrWhiteSpace(actionOptions.OnClose))
                    javaScriptBuilder.AppendFormat("onClose: {0}, ", actionOptions.OnClose);

                if (!String.IsNullOrWhiteSpace(actionOptions.OnInitializeForm))
                    javaScriptBuilder.AppendFormat("onInitializeForm: {0}, ", actionOptions.OnInitializeForm);

                if (!actionOptions.ReloadAfterSubmit)
                    javaScriptBuilder.Append("reloadAfterSubmit: false, ");

                if (!actionOptions.Resizable)
                    javaScriptBuilder.Append("resize: false, ");

                if (!String.IsNullOrWhiteSpace(actionOptions.Url))
                    javaScriptBuilder.AppendFormat("url: '{0}', ", actionOptions.Url);

                JqGridNavigatorSearchActionOptions searchActionOptions = actionOptions as JqGridNavigatorSearchActionOptions;
                if (searchActionOptions != null)
                {
                    if (!String.IsNullOrWhiteSpace(searchActionOptions.AfterShowSearch))
                        javaScriptBuilder.AppendFormat("afterShowSearch: '{0}', ", searchActionOptions.AfterShowSearch);

                    if (!String.IsNullOrWhiteSpace(searchActionOptions.BeforeShowSearch))
                        javaScriptBuilder.AppendFormat("beforeShowSearch: '{0}', ", searchActionOptions.BeforeShowSearch);

                    if (!String.IsNullOrEmpty(searchActionOptions.Caption))
                        javaScriptBuilder.AppendFormat("caption: '{0}', ", searchActionOptions.Caption);

                    if (searchActionOptions.CloseAfterSearch)
                        javaScriptBuilder.Append("closeAfterSearch: true, ");

                    if (searchActionOptions.CloseAfterReset)
                        javaScriptBuilder.Append("closeAfterReset: true, ");
                    
                    if (!String.IsNullOrEmpty(searchActionOptions.SearchText))
                        javaScriptBuilder.AppendFormat("Find: '{0}', ", searchActionOptions.SearchText);

                    if (searchActionOptions.AdvancedSearching)
                        javaScriptBuilder.Append("multipleSearch: true, ");

                    if (searchActionOptions.AdvancedSearchingWithGroups)
                        javaScriptBuilder.Append("multipleGroup: true, ");

                    if (!searchActionOptions.CloneSearchRowOnAdd)
                        javaScriptBuilder.Append("cloneSearchRowOnAdd: false, ");

                    if (!String.IsNullOrWhiteSpace(searchActionOptions.OnInitializeSearch))
                        javaScriptBuilder.AppendFormat("onInitializeSearch: '{0}', ", searchActionOptions.OnInitializeSearch);

                    if (searchActionOptions.RecreateFilter)
                        javaScriptBuilder.Append("recreateFilter: true, ");

                    if (!String.IsNullOrEmpty(searchActionOptions.ResetText))
                        javaScriptBuilder.AppendFormat("Reset: '{0}', ", searchActionOptions.ResetText);

                    if (searchActionOptions.ShowQuery)
                        javaScriptBuilder.Append("showQuery: true, ");

                    if (searchActionOptions.Overlay != 10)
                        javaScriptBuilder.AppendFormat("overlay: {0}, ", searchActionOptions.Overlay);
                }

                if (actionOptions.Width != 300)
                    javaScriptBuilder.AppendFormat("width: {0}, ", actionOptions.Width);
            }

            if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
            {
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                javaScriptBuilder.Append(" }");
            }
            else
                javaScriptBuilder.Append("}");
        }

        private void AppendNavigatorButton(JqGridNavigatorButtonOptions buttonOptions, ref StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.AppendFormat(".jqGrid('navButtonAdd', '#{0}Pager', {{ ", _options.Id);

            if (buttonOptions.Caption != JqGridNavigatorDefaults.ButtonCaption)
                javaScriptBuilder.AppendFormat("caption: '{0}', ", buttonOptions.Caption);

            if (buttonOptions.Icon != JqGridNavigatorDefaults.ButtonIcon)
                javaScriptBuilder.AppendFormat("buttonicon: '{0}', ", buttonOptions.Icon);

            if (!String.IsNullOrWhiteSpace(buttonOptions.OnClick))
                javaScriptBuilder.AppendFormat("onClickButton: {0}, ", buttonOptions.OnClick);

            if (buttonOptions.Position != JqGridNavigatorButtonPositions.Last)
                javaScriptBuilder.Append("position: 'first', ");

            if (!String.IsNullOrEmpty(buttonOptions.ToolTip))
                javaScriptBuilder.AppendFormat("title: '{0}', ", buttonOptions.ToolTip);

            if (buttonOptions.Cursor != JqGridNavigatorDefaults.ButtonCursor)
                javaScriptBuilder.AppendFormat("cursor: '{0}', ", buttonOptions.Cursor);

            if (!String.IsNullOrWhiteSpace(buttonOptions.Id))
                javaScriptBuilder.AppendFormat("id: '{0}', ", buttonOptions.Id);

            if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
            {
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                javaScriptBuilder.Append(" })");
            }
            else
            {
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 4, 4);
                javaScriptBuilder.Append(")");
            }
        }

        private void AppendNavigatorSeparator(JqGridNavigatorSeparatorOptions separatorOptions, ref StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.AppendFormat(".jqGrid('navSeparatorAdd', '#{0}Pager', {{ ", _options.Id);

            if (separatorOptions.Class != JqGridNavigatorDefaults.SeparatorClass)
                javaScriptBuilder.AppendFormat("sepclass: '{0}', ", separatorOptions.Class);

            if (!String.IsNullOrEmpty(separatorOptions.Content))
                javaScriptBuilder.AppendFormat("sepcontent: '{0}', ", separatorOptions.Content);

            if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
            {
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                javaScriptBuilder.Append(" })");
            }
            else
            {
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 4, 4);
                javaScriptBuilder.Append(")");
            }
        }

        private void AppendFilterToolbar(ref StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.Append(".jqGrid('filterToolbar'");

            if (_filterToolbarOptions != null)
            {
                javaScriptBuilder.Append(", { ");

                AppendFilterOptions(_filterToolbarOptions, ref javaScriptBuilder);

                if (_filterToolbarOptions.DefaultSearchOperator.HasValue)
                    javaScriptBuilder.AppendFormat("defaultSearch: '{0}',", _filterToolbarOptions.DefaultSearchOperator.Value.ToString().ToLower());

                if (_filterToolbarOptions.GroupingOperator.HasValue)
                    javaScriptBuilder.AppendFormat("groupOp: '{0}',", _filterToolbarOptions.GroupingOperator.Value.ToString().ToUpper());

                if (_filterToolbarOptions.SearchOnEnter.HasValue)
                    javaScriptBuilder.AppendFormat("searchOnEnter: {0},", _filterToolbarOptions.SearchOnEnter.Value.ToString().ToLower());

                if (_filterToolbarOptions.StringResult.HasValue)
                    javaScriptBuilder.AppendFormat("stringResult: {0},", _filterToolbarOptions.StringResult.Value.ToString().ToLower());
                
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                javaScriptBuilder.Append(" }");
            }

            javaScriptBuilder.Append(")");
        }

        private void AppendFilterGrid(ref StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.AppendFormat("$('#{0}Search').jqGrid('filterGrid', '#{1}', {{", _options.Id, _options.Id).AppendLine();
            javaScriptBuilder.AppendLine("gridModel: false, gridNames: false,");

            javaScriptBuilder.AppendLine("filterModel: [");

            int lastGridModelIndex = _filterGridModel.Count - 1;
            for (int gridModelIndex = 0; gridModelIndex < _filterGridModel.Count; gridModelIndex++)
            {
                JqGridFilterGridRowModel gridRowModel = _filterGridModel[gridModelIndex];
                javaScriptBuilder.AppendFormat("{{ label: '{0}', name: '{1}', ", gridRowModel.Label, gridRowModel.ColumnName);

                if (!String.IsNullOrWhiteSpace(gridRowModel.DefaultValue))
                    javaScriptBuilder.AppendFormat("defval: '{0}', ", gridRowModel.DefaultValue);

                if (!String.IsNullOrWhiteSpace(gridRowModel.SelectUrl))
                    javaScriptBuilder.AppendFormat("surl: '{0}', ", gridRowModel.SelectUrl);

                javaScriptBuilder.AppendFormat("stype: '{0}' }}", gridRowModel.SearchType.ToString().ToLower());

                if (lastGridModelIndex == gridModelIndex)
                    javaScriptBuilder.AppendLine();
                else
                    javaScriptBuilder.AppendLine(",");
            }

            javaScriptBuilder.Append("]");

            if (_filterGridOptions != null)
            {
                javaScriptBuilder.AppendLine(",");
                AppendFilterOptions(_filterGridOptions, ref javaScriptBuilder);

                if (!String.IsNullOrWhiteSpace(_filterGridOptions.ButtonsClass))
                    javaScriptBuilder.AppendFormat("buttonclass: '{0}',", _filterGridOptions.ButtonsClass);

                if (_filterGridOptions.ClearEnabled.HasValue)
                    javaScriptBuilder.AppendFormat("enableClear: {0},", _filterGridOptions.ClearEnabled.Value.ToString().ToLower());

                if (!String.IsNullOrWhiteSpace(_filterGridOptions.ClearText))
                    javaScriptBuilder.AppendFormat("clearButton: '{0}',", _filterGridOptions.ClearText);

                if (!String.IsNullOrWhiteSpace(_filterGridOptions.FormClass))
                    javaScriptBuilder.AppendFormat("formclass: '{0}',", _filterGridOptions.FormClass);

                if (_filterGridOptions.MarkSearched.HasValue)
                    javaScriptBuilder.AppendFormat("marksearched: {0},", _filterGridOptions.MarkSearched.Value.ToString().ToLower());

                if (_filterGridOptions.SearchEnabled.HasValue)
                    javaScriptBuilder.AppendFormat("enableSearch: {0},", _filterGridOptions.SearchEnabled.Value.ToString().ToLower());

                if (!String.IsNullOrWhiteSpace(_filterGridOptions.SearchText))
                    javaScriptBuilder.AppendFormat("searchButton: '{0}',", _filterGridOptions.SearchText);

                if (!String.IsNullOrWhiteSpace(_filterGridOptions.TableClass))
                    javaScriptBuilder.AppendFormat("tableclass: '{0}',", _filterGridOptions.TableClass);

                if (_filterGridOptions.Type.HasValue)
                    javaScriptBuilder.AppendFormat("formtype: '{0}',", _filterGridOptions.Type.Value.ToString().ToLower());

                if (!String.IsNullOrWhiteSpace(_filterGridOptions.Url))
                    javaScriptBuilder.AppendFormat("url: '{0}',", _filterGridOptions.Url);

                javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
            }

            javaScriptBuilder.AppendLine("});");
        }

        private static void AppendFilterOptions(JqGridFilterOptions options, ref StringBuilder javaScriptBuilder)
        {
            if (options != null)
            {
                if (!String.IsNullOrWhiteSpace(options.AfterClear))
                    javaScriptBuilder.AppendFormat("afterClear: '{0}',", options.AfterClear);

                if (!String.IsNullOrWhiteSpace(options.AfterSearch))
                    javaScriptBuilder.AppendFormat("afterSearch: '{0}',", options.AfterSearch);

                if (options.AutoSearch.HasValue)
                    javaScriptBuilder.AppendFormat("autosearch: {0},", options.AutoSearch.Value.ToString().ToLower());

                if (!String.IsNullOrWhiteSpace(options.BeforeClear))
                    javaScriptBuilder.AppendFormat("beforeClear: '{0}',", options.BeforeClear);

                if (!String.IsNullOrWhiteSpace(options.BeforeSearch))
                    javaScriptBuilder.AppendFormat("beforeSearch: '{0}',", options.BeforeSearch);
            }
        }

        private void AppendFooterData(ref StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.Append(".jqGrid('footerData', 'set', { ");

            foreach(KeyValuePair<string, object> footerValue in _footerData)
                javaScriptBuilder.AppendFormat(" {0}: '{1}', ", footerValue.Key, Convert.ToString(footerValue.Value));

            javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
            javaScriptBuilder.AppendFormat(" }}, {0})", _footerDataUseFormatters.ToString().ToLower());
        }

        private static void AppendColumnLabelOptions(string columnName, JqGridColumnLabelOptions options, ref StringBuilder javaScriptBuilder)
        {
            if (options != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                javaScriptBuilder.AppendFormat(".jqGrid('setLabel', '{0}', ", columnName);
                javaScriptBuilder.AppendFormat("'{0}', ", String.IsNullOrWhiteSpace(options.Label) ? String.Empty : options.Label);

                if (String.IsNullOrWhiteSpace(options.Class))
                {
                    if (options.CssStyles != null)
                        javaScriptBuilder.AppendFormat("{0}, ", serializer.Serialize(options.CssStyles));
                    else if (options.HtmlAttributes != null)
                        javaScriptBuilder.Append("null, ");
                }
                else
                    javaScriptBuilder.AppendFormat("'{0}', ", options.Class);

                if (options.HtmlAttributes != null)
                    javaScriptBuilder.AppendFormat("{0}, ", serializer.Serialize(options.HtmlAttributes));

                javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                javaScriptBuilder.Append(" )");
            }
        }

        private static IDictionary<string, object> AnonymousObjectToDictionary(object anonymousObject)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            if (anonymousObject != null)
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(anonymousObject))
                    dictionary.Add(property.Name, property.GetValue(anonymousObject));
            }

            return dictionary;
        }
        #endregion

        #region Fluent API
        /// <summary>
        /// Enables Navigator for this JqGridHelper instance.
        /// </summary>
        /// <param name="options">The options for the Navigator.</param>
        /// <param name="editActionOptions">The options for the edit action.</param>
        /// <param name="addActionOptions">The options for the add action.</param>
        /// <param name="deleteActionOptions">The options for the delete action.</param>
        /// <param name="searchActionOptions">The options for the search action.</param>
        /// <param name="viewActionOptions">The options for the view action.</param>
        /// <returns>JqGridHelper instance with enabled Navigator.</returns>
        public JqGridHelper<TModel> Navigator(JqGridNavigatorOptions options, JqGridNavigatorActionOptions editActionOptions = null, JqGridNavigatorActionOptions addActionOptions = null, JqGridNavigatorActionOptions deleteActionOptions = null, JqGridNavigatorActionOptions searchActionOptions = null, JqGridNavigatorActionOptions viewActionOptions = null)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            _navigatorOptions = options;
            _navigatorEditActionOptions = editActionOptions;
            _navigatorAddActionOptions = addActionOptions;
            _navigatorDeleteActionOptions = deleteActionOptions;
            _navigatorSearchActionOptions = searchActionOptions;
            _navigatorViewActionOptions = viewActionOptions;

            return this;
        }

        /// <summary>
        /// Adds button to this JqGridHelper instance Navigator.
        /// </summary>
        /// <param name="caption">The caption for the button.</param>
        /// <param name="icon">The icon (form UI theme images) for the button. If this is set to "none" only text will appear.</param>
        /// <param name="onClick">The function for button click action.</param>
        /// <param name="position">The position where the button will be added.</param>
        /// <param name="toolTip">The tooltip for the button.</param>
        /// <param name="cursor">The value which determines the cursor when user mouseover the button.</param>
        /// <returns>JqGridHelper instance.</returns>
        public JqGridHelper<TModel> AddNavigatorButton(string caption = JqGridNavigatorDefaults.ButtonCaption, string icon = JqGridNavigatorDefaults.ButtonIcon, string onClick = null, JqGridNavigatorButtonPositions position = JqGridNavigatorButtonPositions.Last, string toolTip = "", string cursor = JqGridNavigatorDefaults.ButtonCursor)
        {
            if (_navigatorOptions == null)
                throw new InvalidOperationException("In order to call AddNavigatorButton method you must call Navigator method first.");

            _navigatorControlsOptions.Add(new JqGridNavigatorButtonOptions()
            {
                Caption = caption,
                Icon = icon,
                OnClick = onClick,
                Position = position,
                ToolTip = toolTip,
                Cursor = cursor
            });
            
            return this;
        }

        /// <summary>
        /// Adds button to this JqGridHelper instance Navigator.
        /// </summary>
        /// <param name="options">The options for the button.</param>
        /// <returns>JqGridHelper instance.</returns>
        public JqGridHelper<TModel> AddNavigatorButton(JqGridNavigatorButtonOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            if (_navigatorOptions == null)
                throw new InvalidOperationException("In order to call AddNavigatorButton method you must call Navigator method first.");

            _navigatorControlsOptions.Add(options);

            return this;
        }

        /// <summary>
        /// Adds separator to this JqGridHelper instance Navigator.
        /// </summary>
        /// <param name="class">The class for the separator.</param>
        /// <param name="content">The content for the separator.</param>
        /// <returns>JqGridHelper instance.</returns>
        public JqGridHelper<TModel> AddNavigatorSeparator(string @class = JqGridNavigatorDefaults.SeparatorClass, string content = "")
        {
            if (_navigatorOptions == null)
                throw new InvalidOperationException("In order to call AddNavigatorSeparator method you must call Navigator method first.");

            _navigatorControlsOptions.Add(new JqGridNavigatorSeparatorOptions()
            {
                Class = @class,
                Content = content
            });

            return this;
        }

        /// <summary>
        /// Adds separator to this JqGridHelper instance Navigator.
        /// </summary>
        /// <param name="options">The options for the separator.</param>
        /// <returns>JqGridHelper instance.</returns>
        public JqGridHelper<TModel> AddNavigatorSeparator(JqGridNavigatorSeparatorOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            if (_navigatorOptions == null)
                throw new InvalidOperationException("In order to call AddNavigatorSeparator method you must call Navigator method first.");

            _navigatorControlsOptions.Add(options);

            return this;
        }

        /// <summary>
        /// Enables filter toolbar for this JqGridHelper instance.
        /// </summary>
        /// <param name="options">The options for the filter toolbar.</param>
        /// <returns>JqGridHelper instance with enabled filter toolbar.</returns>
        public JqGridHelper<TModel> FilterToolbar(JqGridFilterToolbarOptions options = null)
        {
            _filterToolbar = true;
            _filterToolbarOptions = options;

            return this;
        }

        /// <summary>
        /// Enables filter grid for this JqGridHelper instance.
        /// </summary>
        /// <param name="model">The model for the filter grid (if null, the model will be build based on ColumnsModels and ColumnsNames).</param>
        /// <param name="options">The options for the filter grid.</param>
        /// <returns>JqGridHelper instance with enabled filter grid.</returns>
        public JqGridHelper<TModel> FilterGrid(List<JqGridFilterGridRowModel> model = null, JqGridFilterGridOptions options = null)
        {
            if (model == null)
            {
                if (_options.ColumnsModels.Count != _options.ColumnsNames.Count)
                    throw new InvalidOperationException("Can't build filter grid model because ColumnsModels.Count is not equal to ColumnsNames.Count");

                _filterGridModel = new List<JqGridFilterGridRowModel>();
                for (int i = 0; i < _options.ColumnsModels.Count; i++)
                {
                    if (_options.ColumnsModels[i].Searchable)
                    {
                        _filterGridModel.Add(new JqGridFilterGridRowModel(_options.ColumnsModels[i].Name, _options.ColumnsNames[i])
                        {
                            DefaultValue = _options.ColumnsModels[i].SearchOptions != null ? _options.ColumnsModels[i].SearchOptions.DefaultValue : String.Empty,
                            SearchType = _options.ColumnsModels[i].SearchType,
                            SelectUrl = _options.ColumnsModels[i].SearchOptions != null ? _options.ColumnsModels[i].SearchOptions.DataUrl : String.Empty
                        });
                    }
                }
            }
            else
                _filterGridModel = model;
            _filterGridOptions = options;

            return this;
        }

        /// <summary>
        /// Sets data on footer of this JqGridHelper instance (requires footerEnabled = true).
        /// </summary>
        /// <param name="data">The object with values for the footer. Should have names which are the same as names from ColumnsModels.</param>
        /// <param name="useFormatters">The value indicating if the formatters from columns should be used for footer.</param>
        /// <returns>JqGridHelper instance with footer data.</returns>
        public JqGridHelper<TModel> SetFooterData(object data, bool useFormatters = true)
        {
            return SetFooterData(AnonymousObjectToDictionary(data), useFormatters);
        }

        /// <summary>
        /// Sets data on footer of this JqGridHelper instance (requires footerEnabled = true).
        /// </summary>
        /// <param name="data">The dictionary with values for the footer. Should have keys which are the same as names from ColumnsModels.</param>
        /// <param name="useFormatters">The value indicating if the formatters from columns should be used for footer.</param>
        /// <returns>JqGridHelper instance with footer data.</returns>
        public JqGridHelper<TModel> SetFooterData(IDictionary<string, object> data, bool useFormatters = true)
        {
            _footerData = data;
            _footerDataUseFormatters = useFormatters;
            return this;
        }
        #endregion
    }
}
