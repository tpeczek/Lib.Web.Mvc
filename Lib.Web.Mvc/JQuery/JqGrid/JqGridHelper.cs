using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Helper class for generating jqGrid HMTL and JavaScript
    /// </summary>
    /// <typeparam name="TModel">Type of model for this grid</typeparam>
    public class JqGridHelper<TModel> : IJqGridHelper
    {
        #region Fields
        private JqGridOptions<TModel> _options;
        private JqGridInlineNavigatorOptions _inlineNavigatorOptions;
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
        private bool _groupHeadersUseColSpanStyle = false;
        private IEnumerable<JqGridGroupHeader> _groupHeaders = null;
        private bool _setFrozenColumns = false;
        private bool _asSubgrid = false;
        private object _subgridHelper = null;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the grid identifier.
        /// </summary>
        public string Id { get { return _options.Id; } }

        private string GridSelector { get { return _asSubgrid ? "'#' + subgridTableId" : String.Format("'#{0}'", Id); } }

        /// <summary>
        /// Gets the grid pager identifier.
        /// </summary>
        public string PagerId { get { return _options.Id + "Pager"; } }

        private string TopPagerSelector { get { return _asSubgrid ? "'#' + subgridTableId + '_toppager'" : String.Format("'#{0}_toppager'", Id); } }

        private string PagerSelector { get { return _asSubgrid ? "'#' + subgridPagerId" : String.Format("'#{0}'", PagerId); } }

        /// <summary>
        /// Gets the filter grid (div) placeholder identifier.
        /// </summary>
        public string FilterGridId { get { return _options.Id + "Search"; } }

        private string FilterGridSelector { get { return _asSubgrid ? "'#' + subgridFilterGridId" : String.Format("'#{0}'", FilterGridId); } }
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
        /// <param name="altClass">The class that is used for alternate rows.</param>
        /// <param name="altRows">The value indicating if the grid should be "zebra-striped".</param>
        /// <param name="autoEncode">The value indicating if the grid should auto encode/decode the data.</param>
        /// <param name="autoWidth">The value indicating if the grid width will be recalculated automatically to the width of the parent element.</param>
        /// <param name="beforeRequest">The function for event which is raised before requesting any data.</param>
        /// <param name="beforeSelectRow">The function for event which is raised when the user click on the row, but before selecting it.</param>
        /// <param name="beforeEditCell">The function for event which is raised before editing the cell.</param>
        /// <param name="beforeSaveCell">The function for event which is raised before validation of values if any.</param>
        /// <param name="beforeSubmitCell">The function for event which is raised before submit the cell content to the server.</param>
        /// <param name="beforeProcessing">This function for event which is raised before proccesing the data returned from the server.</param>
        /// <param name="caption">The caption for the grid.</param>
        /// <param name="cellLayout">The padding plus border width of the cell.</param>
        /// <param name="cellEditingEnabled">The value indicating if cell editing is enabled.</param>
        /// <param name="cellEditingSubmitMode">The cell editing submit mode.</param>
        /// <param name="cellEditingUrl">The URL for cell editing submit.</param>
        /// <param name="dataString">The string of data which will be used when DataType is set to JqGridDataTypes.XmlString or JqGridDataTypes.JsonString.</param>
        /// <param name="dataType">The type of information to expect to represent data in the grid.</param>
        /// <param name="deepEmpty">The value indicating if jqGrid should be using jQuery.empty for the the row and all its children elements.</param>
        /// <param name="direction">The language direction in grid.</param>
        /// <param name="dynamicScrollingMode">The value which defines if dynamic scrolling is enabled.</param>
        /// <param name="dynamicScrollingTimeout">The timeout (in miliseconds) if DynamicScrollingMode is set to JqGridDynamicScrollingModes.HoldVisibleRows.</param>
        /// <param name="editingUrl">The url for inline and form editing</param>
        /// <param name="emptyRecords">The information to be displayed when the returned (or the current) number of records is zero.</param>
        /// <param name="expandColumnClick">The value which defines whether the tree is expanded and/or collapsed when user clicks on the text of the expanded column, not only on the image.</param>
        /// <param name="expandColumn">The name of column which should be used to expand the tree grid.</param>
        /// <param name="errorCell">The function for event which is raised when there is a server error while saving cell.</param>
        /// <param name="formatCell">The function for event which allows formatting the cell content before editing.</param>
        /// <param name="footerEnabled">The value indicating if the footer table (with one row) will be placed below the grid records and above the pager.</param>
        /// <param name="gridView">The value indicating if all the data should be inserted into DOM with one jQuery call.</param>
        /// <param name="groupingEnabled">The value indicating if the grouping is enabled.</param>
        /// <param name="groupingView">The grouping view options.</param>
        /// <param name="height">The height of the grid in pixels (default 'auto').</param>
        /// <param name="headerTitles">The value which defines whether the title attribute is added to the column headers.</param>
        /// <param name="hidden">The value which defines whether the grid is initialy hidden (no data loaded, only caption layer is shown). Takes effect only if the caption is not empty string and hiddenEnabled is true.</param>
        /// <param name="hoverRows">The value which defines whether the mouse hovering over the grid data rows is enabled</param>
        /// <param name="ignoreCase">The value which defines whether the local searching is case-sensitive.</param>
        /// <param name="hiddenEnabled">The value which defines whether the show/hide grid button is enabled. Takes effect only if the caption is not empty string.</param>
        /// <param name="jsonReader">The JSON reader for the grid.</param>
        /// <param name="loadBeforeSend">The function for pre-callback to modify the XMLHttpRequest object before it is sent.</param>
        /// <param name="loadError">The function for event which is raised after the request fails.</param>
        /// <param name="loadComplete">The function for event which is raised immediately after every server request.</param>
        /// <param name="loadOnce">The value which defines whether the grid should load the data only once and all further manipulationsshould be done on the client side.</param>
        /// <param name="methodType">The type of request to make.</param>
        /// <param name="multiKey">The key which should be pressed when the user makes multiselection.</param>
        /// <param name="multiBoxOnly">The value which defines whether the multiselection is done only when the checkbox is clicked.</param>
        /// <param name="multiSelect">The value which defines whether the multiselection is enabled.</param>
        /// <param name="multiSelectWidth">The width of the multiselect colum.</param>
        /// <param name="multiSort">The value which defines whether the sorting by multiple columns is enabled.</param>
        /// <param name="gridComplete">The function for event which is raised after all the data is loaded into the grid and all other processes are complete.</param>
        /// <param name="onCellSelect">The function for event which is raised when user clicks on particular cell in the grid.</param>
        /// <param name="onDoubleClickRow">The function for event which is raised immediately after row was double clicked.</param>
        /// <param name="onHeaderClick">The function for event which is raised after clicking to hide or show grid.</param>
        /// <param name="onInitGrid">The function for event which is raised only once before populating the data.</param>
        /// <param name="onPaging">The function for event which is raised before populating the data after page index/size change.</param>
        /// <param name="onRightClickRow">The function for event which is raised immediately after row was right clicked.</param>
        /// <param name="onSelectAll">The function for event which is raised when MultipleSelect option is true and user clicks on the header checkbox.</param>
        /// <param name="onSelectCell">The function for event which is raised after the cell is selected for editing.</param>
        /// <param name="onSelectRow">The function for event which is raised immediately after row was clicked.</param>
        /// <param name="onSortCol">The function for event which is raised immediately after sortable column was clicked and before sorting the data.</param>
        /// <param name="pager">If grid should use a pager bar to navigate through the records.</param>
        /// <param name="parametersNames">The customized names for jqGrid request parameters.</param>
        /// <param name="postData">The additional data which will be added to the request.</param>
        /// <param name="postDataScript">The JavaScript which will dynamically generate the additional data which will be added to the request (this property takes precedence over postData).</param>
        /// <param name="resizeStart">The function for event which is raised when user starts resizing a column.</param>
        /// <param name="resizeStop">The function for event which is raised after the column is resized.</param>
        /// <param name="rowAttributes">The function which can add attributes to the row during the creation of the data (dynamically).</param>
        /// <param name="rowsList">The array to construct a select box element in the pager in which user can change the number of the visible rows.</param>
        /// <param name="rowsNumber">How many records should be displayed in the grid.</param>
        /// <param name="rowsNumbers">The value which defines whether a new column, which purpose is to count the number of available rows, should be added at left of the grid.</param>
        /// <param name="rowsNumbersWidth">The width of the row number column.</param>
        /// <param name="shrinkToFit">The value which defines whether the columns width should be recalculated to fit the width of the grid.</param>
        /// <param name="scrollOffset">The width of vertical scrollbar.</param>
        /// <param name="serializeCellData">The function for event which can serialize the data passed to the ajax request when the cell is being saved.</param>
        /// <param name="serializeGridData">The function for event which can serialize the data passed to the ajax request.</param>
        /// <param name="serializeSubGridData">The function for event which can serialize the data passed to the subgrid ajax request.</param>
        /// <param name="sortable">The value which defines whether the columns can be reordered by dragging and dropping them with the mouse.</param>
        /// <param name="sortingName">The initial sorting column index, when  using data returned from server.</param>
        /// <param name="sortingOrder">The initial sorting order, when  using data returned from server.</param>
        /// <param name="styleUI">The CSS framework.</param>
        /// <param name="subgridEnabled">The value which defines if subgrid is enabled.</param>
        /// <param name="subgridModel">The subgrid model.</param>
        /// <param name="subgridHelper">The subgrid helper for "Subgrid as Grid" scenario. If this option has value, the SugridModel, SubgridUrl and SubGridRowExpanded options are ignored. It will also add additional parameter 'id' to the request.</param>
        /// <param name="subgridUrl">The url for subgrid data requests.</param>
        /// <param name="subgridColumnWidth">The width of subgrid expand/colapse column.</param>
        /// <param name="subGridBeforeExpand">The function for event which is raised just before expanding the subgrid.</param>
        /// <param name="subGridRowColapsed">The function for event which is raised when the user clicks on the plus icon of the grid.</param>
        /// <param name="subGridRowExpanded">The function for event which is raised when the user clicks on the minus icon of the grid.</param>
        /// <param name="topPager">The value indicating if jqGrid should place a pager element at top of the grid below the caption (if available).</param>
        /// <param name="treeGridEnabled">The value which defines if TreeGrid is enabled.</param>
        /// <param name="treeGridModel">The model for TreeGrid.</param>
        /// <param name="url">The url for data requests.</param>
        /// <param name="userDataOnFooter">The value indicating if the values from user data should be placed on footer.</param>
        /// <param name="viewRecords">If grid should display the beginning and ending record number out of the total number of records in the query.</param>
        /// <param name="width">The width of the grid in pixels.</param>
        /// <param name="compatibilityMode">The compatibility mode.</param>
        public JqGridHelper(string id, string afterInsertRow = null, string afterEditCell = null, string afterRestoreCell = null, string afterSaveCell = null, string afterSubmitCell = null, string altClass = JqGridOptionsDefaults.AltClass, bool altRows = false, bool autoEncode = false, bool autoWidth = false, string beforeRequest = null, string beforeSelectRow = null, string beforeEditCell = null, string beforeSaveCell = null, string beforeSubmitCell = null, string beforeProcessing = null, string caption = null, int cellLayout = JqGridOptionsDefaults.CellLayout, bool cellEditingEnabled = false, JqGridCellEditingSubmitModes cellEditingSubmitMode = JqGridCellEditingSubmitModes.Remote, string cellEditingUrl = null, string dataString = null, JqGridDataTypes dataType = JqGridDataTypes.Xml, bool deepEmpty = false, JqGridLanguageDirections direction = JqGridLanguageDirections.Ltr, JqGridDynamicScrollingModes dynamicScrollingMode = JqGridDynamicScrollingModes.Disabled, int dynamicScrollingTimeout = 200, string editingUrl = null, string emptyRecords = JqGridOptionsDefaults.EmptyRecords, bool expandColumnClick = true, string expandColumn = null, int? height = null, string errorCell = null, string formatCell = null, bool footerEnabled = false, bool gridView = false, bool groupingEnabled = false, JqGridGroupingView groupingView = null, bool headerTitles = false, bool hidden = false, bool hiddenEnabled = true, bool hoverRows = true, bool ignoreCase = false, JqGridJsonReader jsonReader = null, string loadBeforeSend = null, string loadError = null, string loadComplete = null, bool loadOnce = false, JqGridMethodTypes methodType = JqGridMethodTypes.Get, JqGridMultiKeys? multiKey = null, bool multiBoxOnly = false, bool multiSelect = false, int multiSelectWidth = 20, bool multiSort = false, string gridComplete = null, string onCellSelect = null, string onDoubleClickRow = null, string onHeaderClick = null, string onInitGrid = null, string onPaging = null, string onRightClickRow = null, string onSelectAll = null, string onSelectCell = null, string onSelectRow = null, string onSortCol = null, bool pager = false, JqGridParametersNames parametersNames = null, object postData = null, string postDataScript = null, string resizeStart = null, string resizeStop = null, string rowAttributes = null, List<int> rowsList = null, int rowsNumber = 20, bool rowsNumbers = false, int rowsNumbersWidth = 25, bool shrinkToFit = true, int scrollOffset = 18, string serializeCellData = null, string serializeGridData = null, string serializeSubGridData = null, bool sortable = false, string sortingName = "", JqGridSortingOrders sortingOrder = JqGridSortingOrders.Asc, JqGridStyleUIOptions styleUI = JqGridStyleUIOptions.jQueryUI, bool subgridEnabled = false, JqGridSubgridModel subgridModel = null, object subgridHelper = null, string subgridUrl = null, int subgridColumnWidth = 20, string subGridBeforeExpand = null, string subGridRowColapsed = null, string subGridRowExpanded = null, bool topPager = false, bool treeGridEnabled = false, JqGridTreeGridModels treeGridModel = JqGridTreeGridModels.Nested, string url = null, bool userDataOnFooter = false, bool viewRecords = false, int? width = null, JqGridCompatibilityModes compatibilityMode = JqGridCompatibilityModes.JqGrid)
            : this(new JqGridOptions<TModel>(id) { CompatibilityMode = compatibilityMode, AfterInsertRow = afterInsertRow, AfterEditCell = afterEditCell, AfterRestoreCell = afterRestoreCell, AfterSaveCell = afterSaveCell, AfterSubmitCell = afterSubmitCell, AltClass = altClass, AltRows = altRows, AutoEncode = autoEncode, AutoWidth = autoWidth, BeforeRequest = beforeRequest, BeforeSelectRow = beforeSelectRow, BeforeEditCell = beforeEditCell, BeforeSaveCell = beforeSaveCell, BeforeSubmitCell = beforeSubmitCell, BeforeProcessing = beforeProcessing, Caption = caption, CellLayout = cellLayout, CellEditingEnabled = cellEditingEnabled, CellEditingSubmitMode = cellEditingSubmitMode, CellEditingUrl = cellEditingUrl, DataString = dataString, DataType = dataType, DeepEmpty = deepEmpty, Direction = direction, DynamicScrollingMode = dynamicScrollingMode, DynamicScrollingTimeout = dynamicScrollingTimeout, EditingUrl = editingUrl, EmptyRecords = emptyRecords, ExpandColumnClick = expandColumnClick, ExpandColumn = expandColumn, ErrorCell = errorCell, FormatCell = formatCell, FooterEnabled = footerEnabled, GridView = gridView, GroupingEnabled = groupingEnabled, GroupingView = groupingView, Height = height, HeaderTitles = headerTitles, Hidden = hidden, HiddenEnabled = hiddenEnabled, HoverRows = hoverRows, IgnoreCase = ignoreCase, JsonReader = jsonReader, LoadBeforeSend = loadBeforeSend, LoadError = loadError, LoadComplete = loadComplete, LoadOnce = loadOnce, MethodType = methodType, MultiBoxOnly = multiBoxOnly, MultiKey = multiKey, MultiSelect = multiSelect, MultiSelectWidth = multiSelectWidth, MultiSort = multiSort, GridComplete = gridComplete, OnCellSelect = onCellSelect, OnDoubleClickRow = onDoubleClickRow, OnHeaderClick = onHeaderClick, OnInitGrid = onInitGrid, OnPaging = onPaging, OnRightClickRow = onRightClickRow, OnSelectAll = onSelectAll, OnSelectRow = onSelectRow, OnSelectCell = onSelectCell, OnSortCol = onSortCol, Pager = pager, ParametersNames = parametersNames, PostData = postData, PostDataScript = postDataScript, RowAttributes = rowAttributes, RowsList = rowsList, RowsNumber = rowsNumber, RowsNumbers = rowsNumbers, RowsNumbersWidth = rowsNumbersWidth, ShrinkToFit = shrinkToFit, ScrollOffset = scrollOffset, SerializeCellData = serializeCellData, SerializeGridData = serializeGridData, SerializeSubGridData = serializeSubGridData, Sortable = sortable, SortingName = sortingName, SortingOrder = sortingOrder, StyleUI = styleUI, SubgridEnabled = subgridEnabled, SubgridModel = subgridModel, SubgridUrl = subgridUrl, SubgridColumnWidth = subgridColumnWidth, SubGridBeforeExpand = subGridBeforeExpand, SubGridRowColapsed = subGridRowColapsed, SubGridRowExpanded = subGridRowExpanded, TopPager = topPager, TreeGridEnabled = treeGridEnabled, TreeGridModel = treeGridModel, Url = url, UserDataOnFooter = userDataOnFooter, ViewRecords = viewRecords, Width = width }, subgridHelper)
        {
            _options.JsonReader = _options.JsonReader ?? JqGridResponse.JsonReader;
            _options.ParametersNames = _options.ParametersNames ?? JqGridRequest.ParameterNames;
        }

        /// <summary>
        /// Initializes a new instance of the JqGridHelper class.
        /// </summary>
        /// <param name="options">Options for the grid</param>
        /// <param name="subgridHelper">The subgrid helper for "Subgrid as Grid" scenario. If this option has value, the SugridModel, SubgridUrl and SubGridRowExpanded options are ignored. It will also add additional parameter 'id' to the request.</param>
        public JqGridHelper(JqGridOptions<TModel> options, object subgridHelper = null)
        {
            _options = options;

            if (subgridHelper != null)
            {
                Type subgridHelperType = subgridHelper.GetType();
                if (!subgridHelperType.IsGenericType || subgridHelperType.GetGenericTypeDefinition() != typeof(JqGridHelper<>))
                    throw new ArgumentException("The object must be of type JqGridHelper<T>.", "subgridHelper");
                _subgridHelper = subgridHelper;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the HTML that is used to render the table placeholder for the grid. 
        /// </summary>
        /// <returns>The HTML that represents the table placeholder for jqGrid</returns>
        public MvcHtmlString GetTableHtml()
        {
            return MvcHtmlString.Create(String.Format("<table id='{0}'></table>", Id));
        }

        /// <summary>
        /// Returns the HTML that is used to render the pager (div) placeholder for the grid. 
        /// </summary>
        /// <returns>The HTML that represents the pager (div) placeholder for jqGrid</returns>
        public MvcHtmlString GetPagerHtml()
        {
            return MvcHtmlString.Create(String.Format("<div id='{0}'></div>", PagerId));
        }

        /// <summary>
        /// Returns the HTML that is used to render the filter grid (div) placeholder for the grid. 
        /// </summary>
        /// <returns>The HTML that represents the filter grid (div) placeholder for jqGrid</returns>
        public MvcHtmlString GetFilterGridHtml()
        {
            return MvcHtmlString.Create(String.Format("<div id='{0}'></div>", FilterGridId));
        }

        /// <summary>
        /// Returns the HTML that is used to render the table placeholder for the grid with pager placeholder below it and filter grid (if enabled) placeholder above it.
        /// </summary>
        /// <returns>The HTML that represents the table placeholder for jqGrid with pager placeholder below i</returns>
        public MvcHtmlString GetHtml()
        {
            if (_filterGridModel != null && _options.Pager)
                return MvcHtmlString.Create(GetFilterGridHtml().ToHtmlString() + GetTableHtml().ToHtmlString() + GetPagerHtml().ToHtmlString());
            else if (_filterGridModel != null)
                return MvcHtmlString.Create(GetFilterGridHtml().ToHtmlString() + GetTableHtml().ToHtmlString());
            else if (_options.Pager)
                return MvcHtmlString.Create(GetTableHtml().ToHtmlString() + GetPagerHtml().ToHtmlString());
            else
                return MvcHtmlString.Create(GetTableHtml().ToHtmlString());
        }

        /// <summary>
        /// Return the JavaScript that is used to initialize jqGrid with given options.
        /// </summary>
        /// <returns>The JavaScript that initializes jqGrid with give options</returns>
        /// <exception cref="System.InvalidOperationException">
        /// <list type="bullet">
        /// <item><description>TreeGrid and data grouping are both enabled.</description></item>
        /// <item><description>Rows numbers and data grouping are both enabled.</description></item>
        /// <item><description>Dynamic scrolling and data grouping are both enabled.</description></item>
        /// <item><description>TreeGrid and GridView are both enabled.</description></item>
        /// <item><description>SubGrid and GridView are both enabled.</description></item>
        /// <item><description>AfterInsertRow event and GridView are both enabled.</description></item>
        /// </list> 
        /// </exception>
        public MvcHtmlString GetJavaScript()
        {
            ValidateLimitations();

            StringBuilder javaScriptBuilder = new StringBuilder();

            javaScriptBuilder.AppendFormat("$({0}).jqGrid({{", GridSelector).AppendLine();
            AppendColumnsNames(javaScriptBuilder);
            AppendColumnsModels(javaScriptBuilder);
            AppendOptions(javaScriptBuilder);
            javaScriptBuilder.Append("})");

            foreach (JqGridColumnModel columnModel in _options.ColumnsModels)
            {
                if (columnModel.LabelOptions != null)
                    AppendColumnLabelOptions(columnModel.Name, columnModel.LabelOptions, javaScriptBuilder);
            }

            if (_options.FooterEnabled && _footerData != null && _footerData.Count > 0)
                AppendFooterData(javaScriptBuilder);

            if (_navigatorOptions != null)
                AppendNavigator(javaScriptBuilder);

            if (_inlineNavigatorOptions != null)
                AppendInlineNavigator(javaScriptBuilder);

            if (_filterToolbar)
                AppendFilterToolbar(javaScriptBuilder);

            if (_groupHeaders != null && _groupHeaders.Count() > 0)
                AppendGroupHeaders(javaScriptBuilder);

            if (_setFrozenColumns)
                javaScriptBuilder.Append(".jqGrid('setFrozenColumns')");

            javaScriptBuilder.AppendLine(";");

            if (_filterGridModel != null)
                AppendFilterGrid(javaScriptBuilder);

            return MvcHtmlString.Create(javaScriptBuilder.ToString());
        }

        internal string GetSubGridRowExpanded()
        {
            _asSubgrid = true;

            StringBuilder subGridRowExpandedBuilder = new StringBuilder();
            subGridRowExpandedBuilder.AppendLine("function(subgridId, rowId) {");

            if (_filterGridModel != null)
            {
                subGridRowExpandedBuilder.AppendLine("var subgridFilterGridId = subgridId + '_s';");
                subGridRowExpandedBuilder.AppendLine("jQuery('#' + subgridId).append('<div id=\"' + subgridFilterGridId + '\"></div>');");
            }

            subGridRowExpandedBuilder.AppendLine("var subgridTableId = subgridId + '_t';");
            subGridRowExpandedBuilder.AppendLine("jQuery('#' + subgridId).append('<table id=\"' + subgridTableId + '\"></table>');");

            if (_options.Pager)
            {
                subGridRowExpandedBuilder.AppendLine("var subgridPagerId = subgridId + '_p';");
                subGridRowExpandedBuilder.AppendLine("jQuery('#' + subgridId).append('<div id=\"' + subgridPagerId + '\"></div>');");
            }

            subGridRowExpandedBuilder.AppendLine(GetJavaScript().ToString());

            subGridRowExpandedBuilder.Append("}");
            return subGridRowExpandedBuilder.ToString();
        }

        private void ValidateLimitations()
        {
            if (_options.TreeGridEnabled && _options.GroupingEnabled)
                throw new InvalidOperationException("TreeGrid and data grouping can not be enabled at the same time.");

            if (_options.RowsNumbers && _options.GroupingEnabled)
                throw new InvalidOperationException("Rows numbers and data grouping can not be enabled at the same time.");

            if (_options.DynamicScrollingMode != JqGridDynamicScrollingModes.Disabled && _options.GroupingEnabled)
                throw new InvalidOperationException("Dynamic scrolling and data grouping can not be enabled at the same time.");

            if (_options.TreeGridEnabled && _options.GridView)
                throw new InvalidOperationException("TreeGrid and GridView can not be enabled at the same time.");

            if (_options.SubgridEnabled && _options.GridView)
                throw new InvalidOperationException("SubGrid and GridView can not be enabled at the same time.");

            if (!String.IsNullOrWhiteSpace(_options.AfterInsertRow) && _options.GridView)
                throw new InvalidOperationException("AfterInsertRow event and GridView can not be enabled at the same time.");
        }

        private void AppendColumnsNames(StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.Append("colNames: [");

            foreach(string columnName in _options.ColumnsNames)
                javaScriptBuilder.AppendFormat("'{0}',", columnName);

            if (javaScriptBuilder[javaScriptBuilder.Length - 1] == ',')
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);

            javaScriptBuilder.AppendLine("],");
        }

        private void AppendColumnsModels(StringBuilder javaScriptBuilder)
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

                if (!String.IsNullOrWhiteSpace(columnModel.CellAttributes))
                    javaScriptBuilder.AppendFormat("cellattr: {0}, ", columnModel.CellAttributes);

                if (!String.IsNullOrWhiteSpace(columnModel.Classes))
                    javaScriptBuilder.AppendFormat("classes: '{0}', ", columnModel.Classes);

                if (columnModel.DateFormat != JqGridOptionsDefaults.DateFormat)
                    javaScriptBuilder.AppendFormat("datefmt: '{0}', ", columnModel.DateFormat);

                if (columnModel.Editable)
                {
                    javaScriptBuilder.Append("editable: true, ");
                    if (columnModel.EditType != JqGridColumnEditTypes.Text)
                        javaScriptBuilder.AppendFormat("edittype: '{0}', ", columnModel.EditType.ToString().ToLower());
                    AppendEditOptions(columnModel.EditOptions, javaScriptBuilder);
                    AppendColumnRules("editrules", columnModel.EditRules, javaScriptBuilder);
                    AppendFormOptions(columnModel.FormOptions, javaScriptBuilder);
                }

                if (columnModel.Fixed)
                    javaScriptBuilder.Append("fixed: true, ");

                if (columnModel.Frozen)
                    javaScriptBuilder.Append("frozen: true, ");

                if (!String.IsNullOrWhiteSpace(columnModel.Formatter))
                {
                    javaScriptBuilder.AppendFormat("formatter: {0}, ", columnModel.Formatter);
                    AppendFormatterOptions(columnModel.Formatter, columnModel.FormatterOptions, javaScriptBuilder);
                    if (!String.IsNullOrWhiteSpace(columnModel.UnFormatter))
                        javaScriptBuilder.AppendFormat("unformat: {0}, ", columnModel.UnFormatter);
                }

                if (columnModel.InitialSortingOrder != JqGridSortingOrders.Asc)
                    javaScriptBuilder.Append("firstsortorder: 'desc', ");

                if (columnModel.Hidden)
                    javaScriptBuilder.Append("hidden: true, ");

                if (columnModel.HideInDialog)
                    javaScriptBuilder.Append("hidedlg: true, ");

                if (!String.IsNullOrWhiteSpace(columnModel.JsonMapping))
                    javaScriptBuilder.AppendFormat("jsonmap: '{0}', ", columnModel.JsonMapping);

                if (columnModel.Key)
                    javaScriptBuilder.Append("key: true, ");

                if (!columnModel.Resizable)
                    javaScriptBuilder.Append("resizable: false, ");

                if (columnModel.Searchable)
                {
                    if (columnModel.SearchType != JqGridColumnSearchTypes.Text)
                        javaScriptBuilder.AppendFormat("stype: '{0}', ", columnModel.SearchType.ToString().ToLower());
                    AppendSearchOptions(columnModel.SearchOptions, javaScriptBuilder);
                    AppendColumnRules("searchrules", columnModel.SearchRules, javaScriptBuilder);
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
                {
                    javaScriptBuilder.AppendFormat("sortable: false, ");
                }
                else if (columnModel.SortType != JqGridColumnSortTypes.Text)
                {
                    if (columnModel.SortType == JqGridColumnSortTypes.Function && !String.IsNullOrEmpty(columnModel.SortFunction))
                        javaScriptBuilder.AppendFormat("sorttype: {0}, ", columnModel.SortFunction);
                    else if (columnModel.SortType != JqGridColumnSortTypes.Function)
                        javaScriptBuilder.AppendFormat("sorttype: '{0}', ", columnModel.SortType.ToString().ToLower());
                }

                if (!columnModel.Title)
                    javaScriptBuilder.AppendFormat("title: false, ");

                if (columnModel.Width != 150)
                    javaScriptBuilder.AppendFormat("width: {0}, ", columnModel.Width);

                if (!columnModel.Viewable)
                    javaScriptBuilder.AppendFormat("viewable: false, ");

                if (!String.IsNullOrWhiteSpace(columnModel.XmlMapping))
                    javaScriptBuilder.AppendFormat("xmlmap: '{0}', ", columnModel.XmlMapping);

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

        private void AppendEditOptions(JqGridColumnEditOptions editOptions, StringBuilder javaScriptBuilder)
        {
            if (editOptions != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                javaScriptBuilder.Append("editoptions: { ");

                if (!String.IsNullOrWhiteSpace(editOptions.CustomElementFunction))
                    javaScriptBuilder.AppendFormat("custom_element: {0}, ", editOptions.CustomElementFunction);

                if (!String.IsNullOrWhiteSpace(editOptions.CustomValueFunction))
                    javaScriptBuilder.AppendFormat("custom_value: {0}, ", editOptions.CustomValueFunction);

                if (editOptions.InternalDataEvents != null)
                {
                    if (editOptions.DataEvents == null)
                        editOptions.DataEvents = new List<JqGridColumnDataEvent>();

                    foreach (JqGridColumnDataEvent dataEvent in editOptions.InternalDataEvents)
                        editOptions.DataEvents.Add(new JqGridColumnDataEvent(dataEvent.Type, String.Format(dataEvent.Function, _options.Id)));
                }
                AppendElementOptions(editOptions, serializer, javaScriptBuilder);

                if (editOptions.NullIfEmpty)
                    javaScriptBuilder.AppendFormat("NullIfEmpty: true, ", editOptions.CustomValueFunction);

                if (editOptions.HtmlAttributes != null && editOptions.HtmlAttributes.Count > 0)
                {
                    string htmlAttributesSerialized = serializer.Serialize(editOptions.HtmlAttributes);
                    javaScriptBuilder.AppendFormat("{0}, ", htmlAttributesSerialized.Substring(1, htmlAttributesSerialized.Length - 2));
                }

                if (!String.IsNullOrWhiteSpace(editOptions.PostDataScript))
                    javaScriptBuilder.AppendFormat("postData: {0}, ", editOptions.PostDataScript);
                else if (editOptions.PostData != null)
                {
                    serializer = new JavaScriptSerializer();
                    javaScriptBuilder.AppendFormat("postData: {0}, ", serializer.Serialize(editOptions.PostData));
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

        private void AppendElementOptions(JqGridColumnElementOptions elementOptions, JavaScriptSerializer serializer, StringBuilder javaScriptBuilder)
        {
            if (!String.IsNullOrWhiteSpace(elementOptions.BuildSelect))
                javaScriptBuilder.AppendFormat("buildSelect: {0}, ", elementOptions.BuildSelect);

            if (elementOptions.DataEvents != null && elementOptions.DataEvents.Count() > 0)
            {
                javaScriptBuilder.Append("dataEvents: [ ");
                foreach (JqGridColumnDataEvent dataEvent in elementOptions.DataEvents)
                {
                    if (dataEvent.Data == null)
                        javaScriptBuilder.AppendFormat("{{ type: '{0}', fn: {1} }}, ", dataEvent.Type, dataEvent.Function);
                    else
                        javaScriptBuilder.AppendFormat("{{ type: '{0}', data: {1}, fn: {2} }}, ", dataEvent.Type, serializer.Serialize(dataEvent.Data), dataEvent.Function);
                }
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                javaScriptBuilder.Append(" ], ");
            }

            if (elementOptions.JQueryUIWidgetDataInitRenderer != null)
                javaScriptBuilder.AppendFormat("dataInit: {0}, ", elementOptions.JQueryUIWidgetDataInitRenderer(_options.CompatibilityMode, GridSelector));
            else if (!String.IsNullOrWhiteSpace(elementOptions.DataInit))
                javaScriptBuilder.AppendFormat("dataInit: {0}, ", elementOptions.DataInit);

            if (!String.IsNullOrWhiteSpace(elementOptions.DataUrl))
                javaScriptBuilder.AppendFormat("dataUrl: '{0}', ", elementOptions.DataUrl);

            if (!String.IsNullOrWhiteSpace(elementOptions.DefaultValue))
                javaScriptBuilder.AppendFormat("defaultValue: '{0}', ", elementOptions.DefaultValue);

            if (!String.IsNullOrWhiteSpace(elementOptions.Value))
                javaScriptBuilder.AppendFormat("value: '{0}', ", elementOptions.Value);
            else if (elementOptions.ValueDictionary != null)
                javaScriptBuilder.AppendFormat("value: {0}, ", serializer.Serialize(elementOptions.ValueDictionary));
        }

        private static void AppendColumnRules(string rulesName, JqGridColumnRules rules, StringBuilder javaScriptBuilder)
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
                    javaScriptBuilder.AppendFormat("maxValue: {0}, ", rules.MaxValue.Value);

                if (rules.MinValue.HasValue)
                    javaScriptBuilder.AppendFormat("minValue: {0}, ", rules.MinValue.Value);

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

        private static void AppendFormOptions(JqGridColumnFormOptions formOptions, StringBuilder javaScriptBuilder)
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

        private static void AppendFormatterOptions(string formatter, JqGridColumnFormatterOptions formatterOptions, StringBuilder javaScriptBuilder)
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
                    case JqGridColumnPredefinedFormatters.Actions:
                        javaScriptBuilder.AppendFormat("{0}{1}{2}", formatterOptions.EditButton ? String.Empty : "editbutton: false, ", formatterOptions.DeleteButton ? String.Empty : "delbutton: false, ", !formatterOptions.UseFormEditing ? String.Empty : "editformbutton: true, ");

                        if (formatterOptions.EditButton)
                        {
                            if (!formatterOptions.UseFormEditing && formatterOptions.InlineEditingOptions != null)
                            {
                                if (formatterOptions.InlineEditingOptions.Keys)
                                    javaScriptBuilder.Append("keys: true, ");

                                if (!String.IsNullOrWhiteSpace(formatterOptions.InlineEditingOptions.OnEditFunction))
                                    javaScriptBuilder.AppendFormat("onEdit: {0}, ", formatterOptions.InlineEditingOptions.OnEditFunction);

                                if (!String.IsNullOrWhiteSpace(formatterOptions.InlineEditingOptions.SuccessFunction))
                                    javaScriptBuilder.AppendFormat("onSuccess: {0}, ", formatterOptions.InlineEditingOptions.SuccessFunction);

                                if (!String.IsNullOrWhiteSpace(formatterOptions.InlineEditingOptions.Url))
                                    javaScriptBuilder.AppendFormat("url: '{0}', ", formatterOptions.InlineEditingOptions.Url);

                                if (!String.IsNullOrWhiteSpace(formatterOptions.InlineEditingOptions.ExtraParamScript))
                                    javaScriptBuilder.AppendFormat("extraparam: {0}, ", formatterOptions.InlineEditingOptions.ExtraParamScript);
                                else if (formatterOptions.InlineEditingOptions.ExtraParam != null)
                                {
                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    javaScriptBuilder.AppendFormat("extraparam: {0}, ", serializer.Serialize(formatterOptions.InlineEditingOptions.ExtraParam));
                                }

                                if (!String.IsNullOrWhiteSpace(formatterOptions.InlineEditingOptions.AfterSaveFunction))
                                    javaScriptBuilder.AppendFormat("afterSave: {0}, ", formatterOptions.InlineEditingOptions.AfterSaveFunction);

                                if (!String.IsNullOrWhiteSpace(formatterOptions.InlineEditingOptions.ErrorFunction))
                                    javaScriptBuilder.AppendFormat("onError: {0}, ", formatterOptions.InlineEditingOptions.ErrorFunction);

                                if (!String.IsNullOrWhiteSpace(formatterOptions.InlineEditingOptions.AfterRestoreFunction))
                                    javaScriptBuilder.AppendFormat("afterRestore: {0}, ", formatterOptions.InlineEditingOptions.AfterRestoreFunction);

                                if (!formatterOptions.InlineEditingOptions.RestoreAfterError)
                                    javaScriptBuilder.Append("restoreAfterError: false, ");

                                if (formatterOptions.InlineEditingOptions.MethodType != JqGridMethodTypes.Post)
                                    javaScriptBuilder.Append("mtype: 'GET', ");
                            }
                            else if (formatterOptions.UseFormEditing && formatterOptions.FormEditingOptions != null)
                                AppendNavigatorActionOptions("editOptions: ", formatterOptions.FormEditingOptions, javaScriptBuilder);
                        }

                        if (formatterOptions.DeleteButton && formatterOptions.DeleteOptions != null)
                            AppendNavigatorActionOptions("delOptions: ", formatterOptions.DeleteOptions, javaScriptBuilder);
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

        private void AppendSearchOptions(JqGridColumnSearchOptions searchOptions, StringBuilder javaScriptBuilder)
        {
            if (searchOptions != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                javaScriptBuilder.Append("searchoptions: { ");

                AppendElementOptions(searchOptions, serializer, javaScriptBuilder);

                if (searchOptions.HtmlAttributes != null && searchOptions.HtmlAttributes.Count > 0)
                    javaScriptBuilder.AppendFormat("attr: {0}, ", serializer.Serialize(searchOptions.HtmlAttributes));

                if (!searchOptions.ClearSearch)
                    javaScriptBuilder.AppendFormat("clearSearch: false, ");

                if (searchOptions.SearchHidden)
                    javaScriptBuilder.AppendFormat("searchhidden: true, ");

                AppendSearchOperators(searchOptions.SearchOperators, javaScriptBuilder);

                if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
                {
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                    javaScriptBuilder.Append(" }, ");
                }
                else
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 17, 17);
            }
        }

        private static void AppendSearchOperators(JqGridSearchOperators? searchOperators, StringBuilder javaScriptBuilder)
        {
            if (searchOperators.HasValue && searchOperators.Value != (JqGridSearchOperators)32768)
            {
                javaScriptBuilder.Append("sopt: [ ");
                foreach (JqGridSearchOperators searchOperator in Enum.GetValues(typeof(JqGridSearchOperators)))
                {
                    if (searchOperator != JqGridSearchOperators.EqualOrNotEqual && searchOperator != JqGridSearchOperators.NoTextOperators && searchOperator != JqGridSearchOperators.TextOperators && (searchOperators.Value & searchOperator) == searchOperator)
                        javaScriptBuilder.AppendFormat("'{0}',", Enum.GetName(typeof(JqGridSearchOperators), searchOperator).ToLower());
                }
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                javaScriptBuilder.Append("], ");
            }
        }

        private void AppendOptions(StringBuilder javaScriptBuilder)
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

            if (_options.AltClass != JqGridOptionsDefaults.AltClass)
                javaScriptBuilder.AppendFormat("altclass: '{0}',", _options.AltClass).AppendLine();

            if (_options.AltRows)
                javaScriptBuilder.Append("altRows: true,").AppendLine();

            if (_options.AutoEncode)
                javaScriptBuilder.Append("autoencode: true,").AppendLine();

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

            if (!String.IsNullOrWhiteSpace(_options.BeforeProcessing))
                javaScriptBuilder.AppendFormat("beforeProcessing: {0},", _options.BeforeProcessing).AppendLine();

            if (_options.CellLayout != JqGridOptionsDefaults.CellLayout)
                javaScriptBuilder.AppendFormat("cellLayout: {0},", _options.CellLayout).AppendLine();

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
            else if (_asSubgrid)
            {
                if (_options.Url.Contains("?"))
                    javaScriptBuilder.AppendFormat("url: '{0}&id=' + encodeURIComponent(rowId),", _options.Url).AppendLine();
                else
                    javaScriptBuilder.AppendFormat("url: '{0}?id=' + encodeURIComponent(rowId),", _options.Url).AppendLine();
            }
            else
                javaScriptBuilder.AppendFormat("url: '{0}',", _options.Url).AppendLine();

            if (_options.DataType != JqGridDataTypes.Xml)
                javaScriptBuilder.AppendFormat("datatype: '{0}',", _options.DataType.ToString().ToLower()).AppendLine();

            if (_options.DeepEmpty)
                javaScriptBuilder.Append("deepempty: true,").AppendLine();

            if (_options.Direction != JqGridLanguageDirections.Ltr)
                javaScriptBuilder.Append("direction: 'rtl',").AppendLine();

            if (_options.DynamicScrollingMode == JqGridDynamicScrollingModes.HoldAllRows)
                javaScriptBuilder.Append("scroll: true,").AppendLine();
            else if (_options.DynamicScrollingMode == JqGridDynamicScrollingModes.HoldVisibleRows)
            {
                javaScriptBuilder.Append("scroll: 10,").AppendLine();
                if (_options.DynamicScrollingTimeout != 200)
                    javaScriptBuilder.AppendFormat("scrollTimeout: {0},", _options.DynamicScrollingTimeout).AppendLine();
            }

            if (_options.EmptyRecords != JqGridOptionsDefaults.EmptyRecords)
                javaScriptBuilder.AppendFormat("emptyrecords: '{0}',", _options.EmptyRecords).AppendLine();

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

            if (_options.GridView)
                javaScriptBuilder.Append("gridview: true,").AppendLine();

            if (_options.GroupingEnabled)
            {
                javaScriptBuilder.Append("grouping: true,").AppendLine();
                AppendGroupingView(javaScriptBuilder);
            }

            if (_options.HeaderTitles)
                javaScriptBuilder.Append("headertitles: true,").AppendLine();

            if (!_options.HoverRows)
                javaScriptBuilder.Append("hoverrows: false").AppendLine();

            if (_options.IgnoreCase)
                javaScriptBuilder.Append("ignoreCase: true,").AppendLine();

            AppendJsonReader(javaScriptBuilder);

            if (!String.IsNullOrWhiteSpace(_options.LoadBeforeSend))
                javaScriptBuilder.AppendFormat("loadBeforeSend: {0},", _options.LoadBeforeSend).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.LoadComplete))
                javaScriptBuilder.AppendFormat("loadComplete: {0},", _options.LoadComplete).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.LoadError))
                javaScriptBuilder.AppendFormat("loadError: {0},", _options.LoadError).AppendLine();

            if (_options.LoadOnce)
                javaScriptBuilder.Append("loadonce: true,").AppendLine();

            if (_options.MethodType != JqGridMethodTypes.Get)
                javaScriptBuilder.AppendFormat("mtype: 'POST',").AppendLine();

            if (_options.MultiKey.HasValue)
                javaScriptBuilder.AppendFormat("multikey: '{0}Key',", _options.MultiKey.Value.ToString().ToLower()).AppendLine();

            if (_options.MultiBoxOnly)
                javaScriptBuilder.Append("multiboxonly: true,").AppendLine();

            if (_options.MultiSelect)
                javaScriptBuilder.Append("multiselect: true,").AppendLine();

            if (_options.MultiSelectWidth != 20)
                javaScriptBuilder.AppendFormat("multiselectWidth: {0},", _options.MultiSelectWidth).AppendLine();

            if (_options.MultiSort)
                javaScriptBuilder.Append("multiSort: true,").AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.GridComplete))
                javaScriptBuilder.AppendFormat("gridComplete: {0},", _options.GridComplete).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnCellSelect))
                javaScriptBuilder.AppendFormat("onCellSelect: {0},", _options.OnCellSelect).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnDoubleClickRow))
                javaScriptBuilder.AppendFormat("ondblClickRow: {0},", _options.OnDoubleClickRow).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnHeaderClick))
                javaScriptBuilder.AppendFormat("onHeaderClick: {0},", _options.OnHeaderClick).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnInitGrid))
                javaScriptBuilder.AppendFormat("onInitGrid: {0},", _options.OnInitGrid).AppendLine();

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
                javaScriptBuilder.AppendFormat("pager: {0},", PagerSelector).AppendLine();

            AppendParametersNames(javaScriptBuilder);

            if (!String.IsNullOrWhiteSpace(_options.PostDataScript))
                javaScriptBuilder.AppendFormat("postData: {0},", _options.PostDataScript).AppendLine();
            else if (_options.PostData != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                javaScriptBuilder.AppendFormat("postData: {0},", serializer.Serialize(_options.PostData));
            }

            if (!String.IsNullOrWhiteSpace(_options.ResizeStart))
                javaScriptBuilder.AppendFormat("resizeStart: {0},", _options.ResizeStart).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.ResizeStop))
                javaScriptBuilder.AppendFormat("resizeStop: {0},", _options.ResizeStop).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.RowAttributes))
                javaScriptBuilder.AppendFormat("rowattr: {0},", _options.RowAttributes).AppendLine();

            if (_options.RowsList != null && _options.RowsList.Count > 0)
            {
                javaScriptBuilder.Append("rowList: [");
                javaScriptBuilder.Append(String.Join(",", _options.RowsList));
                javaScriptBuilder.Append("],").AppendLine();
            }

            if (_options.RowsNumber != 20)
                javaScriptBuilder.AppendFormat("rowNum: {0},", _options.RowsNumber).AppendLine();

            if (_options.RowsNumbers)
                javaScriptBuilder.Append("rownumbers: true,").AppendLine();

            if (_options.RowsNumbersWidth != 25)
                javaScriptBuilder.AppendFormat("rownumWidth: {0},", _options.RowsNumbersWidth).AppendLine();

            if (_options.ColumnsRemaping != null && _options.ColumnsRemaping.Count > 0)
            {
                javaScriptBuilder.Append("remapColumns: [");
                javaScriptBuilder.Append(String.Join(",", _options.ColumnsRemaping));
                javaScriptBuilder.Append("],").AppendLine();
            }

            if (!_options.ShrinkToFit)
                javaScriptBuilder.Append("shrinkToFit: false,").AppendLine();

            if (_options.ScrollOffset != 18)
                javaScriptBuilder.AppendFormat("scrollOffset: {0},", _options.ScrollOffset).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.SerializeCellData))
                javaScriptBuilder.AppendFormat("serializeCellData: {0},", _options.SerializeCellData).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.SerializeGridData))
                javaScriptBuilder.AppendFormat("serializeGridData: {0},", _options.SerializeGridData).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.SerializeSubGridData))
                javaScriptBuilder.AppendFormat("serializeSubGridData: {0},", _options.SerializeSubGridData).AppendLine();

            if (_options.Sortable)
                javaScriptBuilder.Append("sortable: true,").AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.SortingName))
                javaScriptBuilder.AppendFormat("sortname: '{0}',", _options.SortingName).AppendLine();

            if (_options.SortingOrder != JqGridSortingOrders.Asc)
                javaScriptBuilder.Append("sortorder: 'desc',").AppendLine();

            if (_options.StyleUI != JqGridStyleUIOptions.jQueryUI)
                javaScriptBuilder.AppendFormat("styleUI: '{0}',", _options.StyleUI.ToString()).AppendLine();

            if (_options.SubgridEnabled)
            {
                javaScriptBuilder.Append("subGrid: true,").AppendLine();

                if (_options.SubgridColumnWidth != 20)
                    javaScriptBuilder.AppendFormat("subGridWidth: {0},", _options.SubgridColumnWidth).AppendLine();

                if (!String.IsNullOrWhiteSpace(_options.SubGridBeforeExpand))
                    javaScriptBuilder.AppendFormat("subGridBeforeExpand: {0},", _options.SubGridBeforeExpand).AppendLine();

                if (_subgridHelper != null)
                {
                    Type subgridHelperType = _subgridHelper.GetType();
                    javaScriptBuilder.AppendFormat("subGridRowExpanded: {0},", subgridHelperType.GetMethod("GetSubGridRowExpanded", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(_subgridHelper, null)).AppendLine();
                }
                else
                {
                    AppendSubgridModel(javaScriptBuilder);

                    if (!String.IsNullOrWhiteSpace(_options.SubgridUrl))
                        javaScriptBuilder.AppendFormat("subGridUrl: '{0}',", _options.SubgridUrl).AppendLine();


                    if (!String.IsNullOrWhiteSpace(_options.SubGridRowExpanded))
                        javaScriptBuilder.AppendFormat("subGridRowExpanded: {0},", _options.SubGridRowExpanded).AppendLine();
                }

                if (!String.IsNullOrWhiteSpace(_options.SubGridRowColapsed))
                    javaScriptBuilder.AppendFormat("subGridRowColapsed: {0},", _options.SubGridRowColapsed).AppendLine();
            }

            if (_options.TopPager)
                javaScriptBuilder.AppendFormat("toppager: true,").AppendLine();

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

        private void AppendJsonReader(StringBuilder javaScriptBuilder)
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

        private void AppendParametersNames(StringBuilder javaScriptBuilder)
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

        private void AppendGroupingView(StringBuilder javaScriptBuilder)
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

                if (_options.GroupingView.IsInTheSameGroupCallbacks != null && _options.GroupingView.IsInTheSameGroupCallbacks.Length > 0)
                {
                    javaScriptBuilder.Append("isInTheSameGroup: [");
                    foreach (string isInTheSameGroupCallback in _options.GroupingView.IsInTheSameGroupCallbacks)
                        javaScriptBuilder.AppendFormat("{0}, ", isInTheSameGroupCallback);
                    javaScriptBuilder.Insert(javaScriptBuilder.Length - 2, ']');
                }

                if (_options.GroupingView.FormatDisplayFieldCallbacks != null && _options.GroupingView.FormatDisplayFieldCallbacks.Length > 0)
                {
                    javaScriptBuilder.Append("formatDisplayField: [");
                    foreach (string formatDisplayFieldCallback in _options.GroupingView.FormatDisplayFieldCallbacks)
                        javaScriptBuilder.AppendFormat("{0}, ", formatDisplayFieldCallback);
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

        private void AppendSubgridModel(StringBuilder javaScriptBuilder)
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

        private void AppendNavigator(StringBuilder javaScriptBuilder)
        {
            string navigatorPagerSelector = _navigatorOptions.Pager == JqGridNavigatorPagers.Top ? TopPagerSelector : PagerSelector;
            javaScriptBuilder.AppendFormat(".jqGrid('navGrid', {0},", navigatorPagerSelector).AppendLine();
            AppendNavigatorOptions(javaScriptBuilder);

            if (_navigatorEditActionOptions != null || _navigatorAddActionOptions != null || _navigatorDeleteActionOptions != null || _navigatorSearchActionOptions != null || _navigatorViewActionOptions != null)
            {
                javaScriptBuilder.AppendLine(",");
                AppendNavigatorActionOptions(String.Empty, _navigatorEditActionOptions, javaScriptBuilder);
                if (_navigatorAddActionOptions != null || _navigatorDeleteActionOptions != null || _navigatorSearchActionOptions != null || _navigatorViewActionOptions != null)
                {
                    javaScriptBuilder.AppendLine(",");
                    AppendNavigatorActionOptions(String.Empty, _navigatorAddActionOptions, javaScriptBuilder);
                    if (_navigatorDeleteActionOptions != null || _navigatorSearchActionOptions != null || _navigatorViewActionOptions != null)
                    {
                        javaScriptBuilder.AppendLine(",");
                        AppendNavigatorActionOptions(String.Empty, _navigatorDeleteActionOptions, javaScriptBuilder);
                        if (_navigatorSearchActionOptions != null || _navigatorViewActionOptions != null)
                        {
                            javaScriptBuilder.AppendLine(",");
                            AppendNavigatorActionOptions(String.Empty, _navigatorSearchActionOptions, javaScriptBuilder);
                            if (_navigatorViewActionOptions != null)
                            {
                                javaScriptBuilder.AppendLine(",");
                                AppendNavigatorActionOptions(String.Empty, _navigatorViewActionOptions, javaScriptBuilder);
                            }
                        }
                    }
                }
            }

            javaScriptBuilder.Append(")");

            foreach (JqGridNavigatorControlOptions controlOptions in _navigatorControlsOptions)
            {
                if (controlOptions is JqGridNavigatorButtonOptions)
                {
                    JqGridNavigatorButtonOptions buttonOptions = (JqGridNavigatorButtonOptions)controlOptions;
                    AppendNavigatorButton(navigatorPagerSelector, buttonOptions, javaScriptBuilder);
                    if (_navigatorOptions.Pager == JqGridNavigatorPagers.Standard && controlOptions.CloneToTop)
                        AppendNavigatorButton(TopPagerSelector, buttonOptions, javaScriptBuilder);
                }
                else if (controlOptions is JqGridNavigatorSeparatorOptions)
                {
                    JqGridNavigatorSeparatorOptions separatorOptions = (JqGridNavigatorSeparatorOptions)controlOptions;
                    AppendNavigatorSeparator(navigatorPagerSelector, separatorOptions, javaScriptBuilder);
                    if (_navigatorOptions.Pager == JqGridNavigatorPagers.Standard && controlOptions.CloneToTop)
                        AppendNavigatorSeparator(TopPagerSelector, separatorOptions, javaScriptBuilder);
                }
            }
        }

        private static void AppendBaseNavigatorOptions(JqGridNavigatorOptionsBase baseNavigatorOptions, StringBuilder javaScriptBuilder)
        {
            if (!baseNavigatorOptions.Add)
                javaScriptBuilder.Append("add: false, ");

            if (baseNavigatorOptions.AddIcon != JqGridNavigatorDefaults.AddIcon)
                javaScriptBuilder.AppendFormat("addicon: '{0}', ", baseNavigatorOptions.AddIcon);

            if (!String.IsNullOrEmpty(baseNavigatorOptions.AddText))
                javaScriptBuilder.AppendFormat("addtext: '{0}', ", baseNavigatorOptions.AddText);

            if (baseNavigatorOptions.AddToolTip != JqGridNavigatorDefaults.AddToolTip)
                javaScriptBuilder.AppendFormat("addtitle: '{0}', ", baseNavigatorOptions.AddToolTip);

            if (!baseNavigatorOptions.Edit)
                javaScriptBuilder.Append("edit: false, ");

            if (baseNavigatorOptions.EditIcon != JqGridNavigatorDefaults.EditIcon)
                javaScriptBuilder.AppendFormat("editicon: '{0}', ", baseNavigatorOptions.EditIcon);

            if (!String.IsNullOrEmpty(baseNavigatorOptions.EditText))
                javaScriptBuilder.AppendFormat("edittext: '{0}', ", baseNavigatorOptions.EditText);

            if (baseNavigatorOptions.EditToolTip != JqGridNavigatorDefaults.EditToolTip)
                javaScriptBuilder.AppendFormat("edittitle: '{0}', ", baseNavigatorOptions.EditToolTip);

            if (baseNavigatorOptions.Position != JqGridAlignments.Left)
                javaScriptBuilder.AppendFormat("position: '{0}', ", baseNavigatorOptions.Position.ToString().ToLower());
        }

        private void AppendNavigatorOptions(StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.Append("{ ");

            AppendBaseNavigatorOptions(_navigatorOptions, javaScriptBuilder);

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

        private static void AppendNavigatorActionOptions(string optionsName, JqGridNavigatorActionOptions actionOptions, StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.AppendFormat("{0}{{ ", optionsName);

            if (actionOptions != null)
            {
                if (actionOptions.Left != 0)
                    javaScriptBuilder.AppendFormat("left: {0}, ", actionOptions.Left);

                if (actionOptions.Top != 0)
                    javaScriptBuilder.AppendFormat("top: {0}, ", actionOptions.Top);

                if (actionOptions.DataWidth.HasValue)
                    javaScriptBuilder.AppendFormat("datawidth: {0}, ", actionOptions.DataWidth.Value);

                if (actionOptions.Height.HasValue)
                    javaScriptBuilder.AppendFormat("height: {0}, ", actionOptions.Height.Value);

                if (actionOptions.DataHeight.HasValue)
                    javaScriptBuilder.AppendFormat("dataheight: {0}, ", actionOptions.DataHeight.Value);

                if (actionOptions.Modal)
                    javaScriptBuilder.Append("modal: true, ");

                if (!actionOptions.Dragable)
                    javaScriptBuilder.Append("drag: false, ");

                if (!actionOptions.Resizable)
                    javaScriptBuilder.Append("resize: false, ");

                if (!actionOptions.UseJqModal)
                    javaScriptBuilder.Append("jqModal: false, ");

                if (actionOptions.CloseOnEscape)
                    javaScriptBuilder.Append("closeOnEscape: true, ");

                if (actionOptions.Overlay != 30)
                    javaScriptBuilder.AppendFormat("overlay: {0}, ", actionOptions.Overlay);

                if (!String.IsNullOrWhiteSpace(actionOptions.OnClose))
                    javaScriptBuilder.AppendFormat("onClose: {0}, ", actionOptions.OnClose);

                JqGridNavigatorFormActionOptions formActionOptions = actionOptions as JqGridNavigatorFormActionOptions;
                if (formActionOptions != null)
                {
                    AppendNavigatorFormActionOptions(formActionOptions, javaScriptBuilder);

                    IJqGridNavigatorPageableFormActionOptions pageableFormActionOptions = formActionOptions as IJqGridNavigatorPageableFormActionOptions;
                    if (pageableFormActionOptions != null)
                        AppendNavigatorPageableFormActionOptions(pageableFormActionOptions, javaScriptBuilder);

                    JqGridNavigatorModifyActionOptions modifyActionOptions = formActionOptions as JqGridNavigatorModifyActionOptions;
                    if (modifyActionOptions != null)
                    {
                        AppendNavigatorModifyActionOptions(modifyActionOptions, javaScriptBuilder);
                        JqGridNavigatorEditActionOptions editActionOptions = modifyActionOptions as JqGridNavigatorEditActionOptions;
                        if (editActionOptions != null)
                            AppendNavigatorEditActionOptions(editActionOptions, javaScriptBuilder);
                        else
                            AppendNavigatorDeleteActionOptions(modifyActionOptions as JqGridNavigatorDeleteActionOptions, javaScriptBuilder);
                    }
                    else
                        AppendNavigatorViewActionOptions(formActionOptions as JqGridNavigatorViewActionOptions, javaScriptBuilder);
                }
                else
                    AppendNavigatorSearchActionOptions(actionOptions as JqGridNavigatorSearchActionOptions, javaScriptBuilder);

            }

            if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
            {
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                if (!String.IsNullOrEmpty(optionsName))
                    javaScriptBuilder.Append(" }, ");
                else
                    javaScriptBuilder.Append(" }");
            }
            else if (!String.IsNullOrEmpty(optionsName))
            {
                int optionsNameLength = optionsName.Length + 2;
                javaScriptBuilder.Remove(javaScriptBuilder.Length - optionsNameLength, optionsNameLength);
            }
            else
                javaScriptBuilder.Append("}");
        }

        private static void AppendNavigatorFormActionOptions(JqGridNavigatorFormActionOptions formActionOptions, StringBuilder javaScriptBuilder)
        {
            if (!String.IsNullOrWhiteSpace(formActionOptions.BeforeInitData))
                javaScriptBuilder.AppendFormat("beforeInitData: {0}, ", formActionOptions.BeforeInitData);

            if (!String.IsNullOrWhiteSpace(formActionOptions.BeforeShowForm))
                javaScriptBuilder.AppendFormat("beforeShowForm: {0}, ", formActionOptions.BeforeShowForm);
        }

        private static void AppendNavigatorPageableFormActionOptions(IJqGridNavigatorPageableFormActionOptions pageableFormActionOptions, StringBuilder javaScriptBuilder)
        {
            if (!pageableFormActionOptions.NavigationKeys.IsDefault())
                javaScriptBuilder.AppendFormat("navkeys: [{0}, {1}, {2}], ", pageableFormActionOptions.NavigationKeys.Enabled.ToString().ToLower(), (int)pageableFormActionOptions.NavigationKeys.RecordUp, (int)pageableFormActionOptions.NavigationKeys.RecordDown);

            if (!pageableFormActionOptions.ViewPagerButtons)
                javaScriptBuilder.Append("viewPagerButtons: false, ");

            if (pageableFormActionOptions.RecreateForm)
                javaScriptBuilder.Append("recreateForm: true, ");
        }

        private static void AppendNavigatorModifyActionOptions(JqGridNavigatorModifyActionOptions modifyActionOptions, StringBuilder javaScriptBuilder)
        {
            if (!String.IsNullOrWhiteSpace(modifyActionOptions.Url))
                javaScriptBuilder.AppendFormat("url: '{0}', ", modifyActionOptions.Url);

            if (modifyActionOptions.MethodType != JqGridMethodTypes.Post)
                javaScriptBuilder.Append("mtype: 'GET', ");

            if (!modifyActionOptions.ReloadAfterSubmit)
                javaScriptBuilder.Append("reloadAfterSubmit: false, ");

            if (!String.IsNullOrWhiteSpace(modifyActionOptions.AfterShowForm))
                javaScriptBuilder.AppendFormat("afterShowForm: {0}, ", modifyActionOptions.AfterShowForm);

            if (!String.IsNullOrWhiteSpace(modifyActionOptions.AfterSubmit))
                javaScriptBuilder.AppendFormat("afterSubmit: {0}, ", modifyActionOptions.AfterSubmit);

            if (!String.IsNullOrWhiteSpace(modifyActionOptions.BeforeSubmit))
                javaScriptBuilder.AppendFormat("beforeSubmit: {0}, ", modifyActionOptions.BeforeSubmit);

            if (!String.IsNullOrWhiteSpace(modifyActionOptions.OnClickSubmit))
                javaScriptBuilder.AppendFormat("onclickSubmit: {0}, ", modifyActionOptions.OnClickSubmit);

            if (!String.IsNullOrWhiteSpace(modifyActionOptions.ErrorTextFormat))
                javaScriptBuilder.AppendFormat("errorTextFormat: {0}, ", modifyActionOptions.ErrorTextFormat);
        }

        private static void AppendNavigatorEditActionOptions(JqGridNavigatorEditActionOptions editActionOptions, StringBuilder javaScriptBuilder)
        {
            if (editActionOptions.Width != 300)
                javaScriptBuilder.AppendFormat("width: {0}, ", editActionOptions.Width);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            if (!String.IsNullOrWhiteSpace(editActionOptions.ExtraDataScript))
                javaScriptBuilder.AppendFormat("editData: {0},", editActionOptions.ExtraDataScript).AppendLine();
            else if (editActionOptions.ExtraData != null)
                javaScriptBuilder.AppendFormat("editData: {0}, ", serializer.Serialize(editActionOptions.ExtraData));

            if (editActionOptions.AjaxOptions != null)
                javaScriptBuilder.AppendFormat("ajaxEditOptions: {0}, ", serializer.Serialize(editActionOptions.AjaxOptions));

            if (!String.IsNullOrWhiteSpace(editActionOptions.SerializeData))
                javaScriptBuilder.AppendFormat("serializeEditData: {0}, ", editActionOptions.SerializeData);

            if (editActionOptions.AddedRowPosition != JqGridNewRowPositions.First)
                javaScriptBuilder.Append("addedrow: 'last', ");

            if (!editActionOptions.ClearAfterAdd)
                javaScriptBuilder.Append("clearAfterAdd: false, ");

            if (editActionOptions.CloseAfterAdd)
                javaScriptBuilder.Append("closeAfterAdd: true, ");

            if (editActionOptions.CloseAfterEdit)
                javaScriptBuilder.Append("closeAfterEdit: true, ");

            if (editActionOptions.CheckOnSubmit)
                javaScriptBuilder.Append("checkOnSubmit: true, ");

            if (editActionOptions.CheckOnUpdate)
                javaScriptBuilder.Append("checkOnUpdate: true, ");

            if (!String.IsNullOrWhiteSpace(editActionOptions.TopInfo))
                javaScriptBuilder.AppendFormat("topinfo: '{0}', ", editActionOptions.TopInfo);

            if (!String.IsNullOrWhiteSpace(editActionOptions.BottomInfo))
                javaScriptBuilder.AppendFormat("bottominfo: '{0}', ", editActionOptions.BottomInfo);

            if (editActionOptions.SaveKeyEnabled || (editActionOptions.SaveKey != (char)13))
                javaScriptBuilder.AppendFormat("savekey: [{0}, {1}], ", editActionOptions.SaveKeyEnabled.ToString().ToLower(), (int)editActionOptions.SaveKey);

            if (!editActionOptions.SaveButtonIcon.Equals(JqGridFormButtonIcon.SaveIcon))
                javaScriptBuilder.AppendFormat("saveicon: [{0}, '{1}', '{2}'], ", editActionOptions.SaveButtonIcon.Enabled.ToString().ToLower(), editActionOptions.SaveButtonIcon.Position.ToString().ToLower(), editActionOptions.SaveButtonIcon.Class);

            if (!editActionOptions.CloseButtonIcon.Equals(JqGridFormButtonIcon.CloseIcon))
                javaScriptBuilder.AppendFormat("closeicon: [{0}, '{1}', '{2}'], ", editActionOptions.CloseButtonIcon.Enabled.ToString().ToLower(), editActionOptions.CloseButtonIcon.Position.ToString().ToLower(), editActionOptions.CloseButtonIcon.Class);

            if (!String.IsNullOrWhiteSpace(editActionOptions.AfterClickPgButtons))
                javaScriptBuilder.AppendFormat("afterclickPgButtons: {0}, ", editActionOptions.AfterClickPgButtons);

            if (!String.IsNullOrWhiteSpace(editActionOptions.AfterComplete))
                javaScriptBuilder.AppendFormat("afterComplete: {0}, ", editActionOptions.AfterComplete);

            if (!String.IsNullOrWhiteSpace(editActionOptions.BeforeCheckValues))
                javaScriptBuilder.AppendFormat("beforeCheckValues: {0}, ", editActionOptions.BeforeCheckValues);

            if (!String.IsNullOrWhiteSpace(editActionOptions.OnClickPgButtons))
                javaScriptBuilder.AppendFormat("onclickPgButtons: {0}, ", editActionOptions.OnClickPgButtons);

            if (!String.IsNullOrWhiteSpace(editActionOptions.OnInitializeForm))
                javaScriptBuilder.AppendFormat("onInitializeForm: {0}, ", editActionOptions.OnInitializeForm);
        }

        private static void AppendNavigatorDeleteActionOptions(JqGridNavigatorDeleteActionOptions deleteActionOptions, StringBuilder javaScriptBuilder)
        {
            if (deleteActionOptions.Width != 240)
                javaScriptBuilder.AppendFormat("width: {0}, ", deleteActionOptions.Width);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            if (!String.IsNullOrWhiteSpace(deleteActionOptions.ExtraDataScript))
                javaScriptBuilder.AppendFormat("delData: {0},", deleteActionOptions.ExtraDataScript).AppendLine();
            else if (deleteActionOptions.ExtraData != null)
                javaScriptBuilder.AppendFormat("delData: {0}, ", serializer.Serialize(deleteActionOptions.ExtraData));

            if (deleteActionOptions.AjaxOptions != null)
                javaScriptBuilder.AppendFormat("ajaxDelOptions: {0}, ", serializer.Serialize(deleteActionOptions.AjaxOptions));

            if (!String.IsNullOrWhiteSpace(deleteActionOptions.SerializeData))
                javaScriptBuilder.AppendFormat("serializeDelData: {0}, ", deleteActionOptions.SerializeData);

            if (!deleteActionOptions.DeleteButtonIcon.Equals(JqGridFormButtonIcon.DeleteIcon))
                javaScriptBuilder.AppendFormat("delicon: [{0}, '{1}', '{2}'], ", deleteActionOptions.DeleteButtonIcon.Enabled.ToString().ToLower(), deleteActionOptions.DeleteButtonIcon.Position.ToString().ToLower(), deleteActionOptions.DeleteButtonIcon.Class);

            if (!deleteActionOptions.CancelButtonIcon.Equals(JqGridFormButtonIcon.CancelIcon))
                javaScriptBuilder.AppendFormat("cancelicon: [{0}, '{1}', '{2}'], ", deleteActionOptions.CancelButtonIcon.Enabled.ToString().ToLower(), deleteActionOptions.CancelButtonIcon.Position.ToString().ToLower(), deleteActionOptions.CancelButtonIcon.Class);
        }

        private static void AppendNavigatorViewActionOptions(JqGridNavigatorViewActionOptions viewActionOptions, StringBuilder javaScriptBuilder)
        {
            if (viewActionOptions.Width != 0)
                javaScriptBuilder.AppendFormat("width: {0}, ", viewActionOptions.Width);

            if (viewActionOptions.LabelsWidth != "30%")
                javaScriptBuilder.AppendFormat("labelswidth: '{0}', ", viewActionOptions.LabelsWidth);

            if (!viewActionOptions.CloseButtonIcon.Equals(JqGridFormButtonIcon.CloseIcon))
                javaScriptBuilder.AppendFormat("closeicon: [{0}, '{1}', '{2}'], ", viewActionOptions.CloseButtonIcon.Enabled.ToString().ToLower(), viewActionOptions.CloseButtonIcon.Position.ToString().ToLower(), viewActionOptions.CloseButtonIcon.Class);
        }

        private static void AppendNavigatorSearchActionOptions(JqGridNavigatorSearchActionOptions searchActionOptions, StringBuilder javaScriptBuilder)
        {
            if (searchActionOptions.Width != 450)
                javaScriptBuilder.AppendFormat("width: {0}, ", searchActionOptions.Width);

            if (!String.IsNullOrWhiteSpace(searchActionOptions.AfterRedraw))
                javaScriptBuilder.AppendFormat("afterRedraw: {0}, ", searchActionOptions.AfterRedraw);

            if (!String.IsNullOrWhiteSpace(searchActionOptions.AfterShowSearch))
                javaScriptBuilder.AppendFormat("afterShowSearch: {0}, ", searchActionOptions.AfterShowSearch);

            if (!String.IsNullOrWhiteSpace(searchActionOptions.BeforeShowSearch))
                javaScriptBuilder.AppendFormat("beforeShowSearch: {0}, ", searchActionOptions.BeforeShowSearch);

            if (!String.IsNullOrEmpty(searchActionOptions.Caption))
                javaScriptBuilder.AppendFormat("caption: '{0}', ", searchActionOptions.Caption);

            if (searchActionOptions.CloseAfterSearch)
                javaScriptBuilder.Append("closeAfterSearch: true, ");

            if (searchActionOptions.CloseAfterReset)
                javaScriptBuilder.Append("closeAfterReset: true, ");

            if (!searchActionOptions.ErrorCheck)
                javaScriptBuilder.Append("errorcheck: false, ");

            if (!String.IsNullOrEmpty(searchActionOptions.SearchText))
                javaScriptBuilder.AppendFormat("Find: '{0}', ", searchActionOptions.SearchText);

            if (searchActionOptions.AdvancedSearching)
                javaScriptBuilder.Append("multipleSearch: true, ");

            if (searchActionOptions.AdvancedSearchingWithGroups)
                javaScriptBuilder.Append("multipleGroup: true, ");

            if (!searchActionOptions.CloneSearchRowOnAdd)
                javaScriptBuilder.Append("cloneSearchRowOnAdd: false, ");

            if (!String.IsNullOrWhiteSpace(searchActionOptions.OnInitializeSearch))
                javaScriptBuilder.AppendFormat("onInitializeSearch: {0}, ", searchActionOptions.OnInitializeSearch);

            if (!String.IsNullOrWhiteSpace(searchActionOptions.OnReset))
                javaScriptBuilder.AppendFormat("onReset: {0}, ", searchActionOptions.OnReset);

            if (!String.IsNullOrWhiteSpace(searchActionOptions.OnSearch))
                javaScriptBuilder.AppendFormat("onSearch: {0}, ", searchActionOptions.OnSearch);

            if (searchActionOptions.RecreateFilter)
                javaScriptBuilder.Append("recreateFilter: true, ");

            if (!String.IsNullOrEmpty(searchActionOptions.ResetText))
                javaScriptBuilder.AppendFormat("Reset: '{0}', ", searchActionOptions.ResetText);

            AppendSearchOperators(searchActionOptions.SearchOperators, javaScriptBuilder);

            if (searchActionOptions.ShowOnLoad)
                javaScriptBuilder.Append("showOnLoad: true, ");

            if (searchActionOptions.ShowQuery)
                javaScriptBuilder.Append("showQuery: true, ");

            if (!String.IsNullOrEmpty(searchActionOptions.Layer))
                javaScriptBuilder.AppendFormat("layer: '{0}', ", searchActionOptions.Layer);

            if (searchActionOptions.Templates != null && searchActionOptions.Templates.Count > 0)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.RegisterConverters(new JavaScriptConverter[] { new Lib.Web.Mvc.JQuery.JqGrid.Serialization.JqGridScriptConverter() });

                javaScriptBuilder.Append("tmplNames: [],");
                int templateNameIndex = javaScriptBuilder.Length - 2;
                javaScriptBuilder.Append("tmplFilters: [");
                foreach (KeyValuePair<string, JqGridRequestSearchingFilters> template in searchActionOptions.Templates)
                {
                    javaScriptBuilder.Insert(templateNameIndex, "'" + template.Key + "', ");
                    templateNameIndex += template.Key.Length + 4;
                    javaScriptBuilder.Append(serializer.Serialize(template.Value) + ", ");
                }
                javaScriptBuilder.Remove(templateNameIndex - 2, 2);
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                javaScriptBuilder.Append("],");
            }
        }

        private void AppendNavigatorButton(string navigatorPagerSelector, JqGridNavigatorButtonOptions buttonOptions, StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.AppendFormat(".jqGrid('navButtonAdd', {0}, {{ ", navigatorPagerSelector);
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

        private void AppendNavigatorSeparator(string navigatorPagerSelector, JqGridNavigatorSeparatorOptions separatorOptions, StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.AppendFormat(".jqGrid('navSeparatorAdd', {0}, {{ ", navigatorPagerSelector);

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

        private void AppendInlineNavigator(StringBuilder javaScriptBuilder)
        {
            string navigatorPagerSelector = _navigatorOptions.Pager == JqGridNavigatorPagers.Top ? TopPagerSelector : PagerSelector;

            javaScriptBuilder.AppendFormat(".jqGrid('inlineNav', {0}, ", navigatorPagerSelector).AppendLine();

            AppendInlineNavigatorOptions(javaScriptBuilder);

            javaScriptBuilder.Append(")");
        }

        private void AppendInlineNavigatorOptions(StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.Append("{ ");

            AppendBaseNavigatorOptions(_inlineNavigatorOptions, javaScriptBuilder);

            if (!_inlineNavigatorOptions.Save)
                javaScriptBuilder.Append("save: false, ");

            if (_inlineNavigatorOptions.SaveIcon != JqGridNavigatorDefaults.SaveIcon)
                javaScriptBuilder.AppendFormat("saveicon: '{0}', ", _inlineNavigatorOptions.SaveIcon);

            if (!String.IsNullOrEmpty(_inlineNavigatorOptions.SaveText))
                javaScriptBuilder.AppendFormat("savetext: '{0}', ", _inlineNavigatorOptions.SaveText);

            if (_inlineNavigatorOptions.SaveToolTip != JqGridNavigatorDefaults.SaveToolTip)
                javaScriptBuilder.AppendFormat("savetitle: '{0}', ", _inlineNavigatorOptions.SaveToolTip);

            if (!_inlineNavigatorOptions.Cancel)
                javaScriptBuilder.Append("cancel: false, ");

            if (_inlineNavigatorOptions.CancelIcon != JqGridNavigatorDefaults.CancelIcon)
                javaScriptBuilder.AppendFormat("cancelicon: '{0}', ", _inlineNavigatorOptions.CancelIcon);

            if (!String.IsNullOrEmpty(_inlineNavigatorOptions.CancelText))
                javaScriptBuilder.AppendFormat("canceltext: '{0}', ", _inlineNavigatorOptions.CancelText);

            if (_inlineNavigatorOptions.CancelToolTip != JqGridNavigatorDefaults.CancelToolTip)
                javaScriptBuilder.AppendFormat("canceltitle: '{0}', ", _inlineNavigatorOptions.CancelToolTip);

            AppendInlineNavigatorAddActionOptions(_inlineNavigatorOptions.AddActionOptions, javaScriptBuilder);
            AppendInlineNavigatorActionOptions("editParams", _inlineNavigatorOptions.ActionOptions, javaScriptBuilder);

            if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
            {
                javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                javaScriptBuilder.Append(" }");
            }
            else
                javaScriptBuilder.Append("}");
        }

        private static void AppendInlineNavigatorActionOptions(string actionName, JqGridInlineNavigatorActionOptions actionOptions, StringBuilder javaScriptBuilder)
        {
            if (actionOptions != null)
            {
                javaScriptBuilder.AppendFormat("{0}: {{ ", actionName);

                if (actionOptions.Keys)
                    javaScriptBuilder.Append("keys: true, ");

                if (!String.IsNullOrWhiteSpace(actionOptions.OnEditFunction))
                    javaScriptBuilder.AppendFormat("oneditfunc: {0}, ", actionOptions.OnEditFunction);

                if (!String.IsNullOrWhiteSpace(actionOptions.SuccessFunction))
                    javaScriptBuilder.AppendFormat("successfunc: {0}, ", actionOptions.SuccessFunction);

                if (!String.IsNullOrWhiteSpace(actionOptions.Url))
                    javaScriptBuilder.AppendFormat("url: '{0}', ", actionOptions.Url);

                if (!String.IsNullOrWhiteSpace(actionOptions.ExtraParamScript))
                    javaScriptBuilder.AppendFormat("extraparam: {0}, ", actionOptions.ExtraParamScript);
                else if (actionOptions.ExtraParam != null)
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    javaScriptBuilder.AppendFormat("extraparam: {0}, ", serializer.Serialize(actionOptions.ExtraParam));
                }

                if (!String.IsNullOrWhiteSpace(actionOptions.AfterSaveFunction))
                    javaScriptBuilder.AppendFormat("aftersavefunc: {0}, ", actionOptions.AfterSaveFunction);

                if (!String.IsNullOrWhiteSpace(actionOptions.ErrorFunction))
                    javaScriptBuilder.AppendFormat("errorfunc: {0}, ", actionOptions.ErrorFunction);

                if (!String.IsNullOrWhiteSpace(actionOptions.AfterRestoreFunction))
                    javaScriptBuilder.AppendFormat("afterrestorefunc: {0}, ", actionOptions.AfterRestoreFunction);

                if (!actionOptions.RestoreAfterError)
                    javaScriptBuilder.Append("restoreAfterError: false, ");

                if (actionOptions.MethodType != JqGridMethodTypes.Post)
                    javaScriptBuilder.Append("mtype: 'GET', ");

                if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
                {
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                    javaScriptBuilder.Append(" }, ");
                }
                else
                {
                    int actionNameLength = actionName.Length + 4;
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - actionNameLength, actionNameLength);
                }
            }
        }

        private static void AppendInlineNavigatorAddActionOptions(JqGridInlineNavigatorAddActionOptions actionOptions, StringBuilder javaScriptBuilder)
        {
            if (actionOptions != null)
            {
                javaScriptBuilder.Append("addParams: { ");

                if (actionOptions.RowId != JqGridNavigatorDefaults.NewRowId)
                    javaScriptBuilder.AppendFormat("rowID: '{0}', ", actionOptions.RowId);

                if (actionOptions.InitData != null)
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    javaScriptBuilder.AppendFormat("initdata: {0}, ", serializer.Serialize(actionOptions.InitData));
                }

                if (actionOptions.Position != JqGridNewRowPositions.First)
                    javaScriptBuilder.AppendFormat("position: 'last', ");

                if (actionOptions.UseDefaultValues)
                    javaScriptBuilder.Append("useDefValues: true, ");

                if (actionOptions.UseFormatter)
                    javaScriptBuilder.Append("useFormatter: true, ");

                AppendInlineNavigatorActionOptions("addRowParams", actionOptions.Options, javaScriptBuilder);

                if (javaScriptBuilder[javaScriptBuilder.Length - 2] == ',')
                {
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
                    javaScriptBuilder.Append(" }, ");
                }
                else
                {
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 13, 13);
                }
            }
        }

        private void AppendFilterToolbar(StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.Append(".jqGrid('filterToolbar'");

            if (_filterToolbarOptions != null)
            {
                javaScriptBuilder.Append(", { ");

                AppendFilterOptions(_filterToolbarOptions, javaScriptBuilder);

                if (_filterToolbarOptions.DefaultSearchOperator != JqGridSearchOperators.Bw)
                    javaScriptBuilder.AppendFormat("defaultSearch: '{0}',", _filterToolbarOptions.DefaultSearchOperator.ToString().ToLower());

                if (_filterToolbarOptions.GroupingOperator != JqGridSearchGroupingOperators.And)
                    javaScriptBuilder.Append("groupOp: 'OR',");

                if (!_filterToolbarOptions.SearchOnEnter)
                    javaScriptBuilder.Append("searchOnEnter: false,");

                if (_filterToolbarOptions.SearchOperators)
                    javaScriptBuilder.Append("searchOperators: true,");

                if (_filterToolbarOptions.OperandToolTip != JqGridFilterToolbarDefaults.OperandToolTip)
                    javaScriptBuilder.AppendFormat("operandTitle: '{0}',", _filterToolbarOptions.OperandToolTip );

                if (_filterToolbarOptions.Operands != null && _filterToolbarOptions.Operands.Count > 0 && _filterToolbarOptions.Operands != JqGridFilterToolbarDefaults.Operands)
                {
                    int javaScriptBuilderPosition = javaScriptBuilder.Length;

                    foreach (KeyValuePair<JqGridSearchOperators, string> operand in _filterToolbarOptions.Operands)
                    {
                        string defaultShortText;
                        if (JqGridFilterToolbarDefaults.Operands.TryGetValue(operand.Key, out defaultShortText) && operand.Value != defaultShortText)
                            javaScriptBuilder.AppendFormat("'{0}':'{1}',", operand.Key.ToString().ToLower(), operand.Value);
                    }

                    if (javaScriptBuilderPosition != javaScriptBuilder.Length)
                    {
                        javaScriptBuilder.Insert(javaScriptBuilderPosition, "operands: {");
                        javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                        javaScriptBuilder.Append("},");
                    }
                }

                if (_filterToolbarOptions.StringResult)
                    javaScriptBuilder.Append("stringResult: true,");

                javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                javaScriptBuilder.Append(" }");
            }

            javaScriptBuilder.Append(")");
        }

        private void AppendFilterGrid(StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.AppendFormat("$({0}).jqGrid('filterGrid', {1}, {{", FilterGridSelector, GridSelector).AppendLine();
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
                AppendFilterOptions(_filterGridOptions, javaScriptBuilder);

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

        private static void AppendFilterOptions(JqGridFilterOptions options, StringBuilder javaScriptBuilder)
        {
            if (options != null)
            {
                if (!String.IsNullOrWhiteSpace(options.AfterClear))
                    javaScriptBuilder.AppendFormat("afterClear: {0},", options.AfterClear);

                if (!String.IsNullOrWhiteSpace(options.AfterSearch))
                    javaScriptBuilder.AppendFormat("afterSearch: {0},", options.AfterSearch);

                if (options.AutoSearch.HasValue)
                    javaScriptBuilder.AppendFormat("autosearch: {0},", options.AutoSearch.Value.ToString().ToLower());

                if (!String.IsNullOrWhiteSpace(options.BeforeClear))
                    javaScriptBuilder.AppendFormat("beforeClear: {0},", options.BeforeClear);

                if (!String.IsNullOrWhiteSpace(options.BeforeSearch))
                    javaScriptBuilder.AppendFormat("beforeSearch: {0},", options.BeforeSearch);
            }
        }

        private void AppendFooterData(StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.Append(".jqGrid('footerData', 'set', { ");

            foreach(KeyValuePair<string, object> footerValue in _footerData)
                javaScriptBuilder.AppendFormat(" {0}: '{1}', ", footerValue.Key, Convert.ToString(footerValue.Value));

            javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2);
            javaScriptBuilder.AppendFormat(" }}, {0})", _footerDataUseFormatters.ToString().ToLower());
        }

        private void AppendGroupHeaders(StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.Append(".jqGrid('setGroupHeaders', { ");

            if (_groupHeadersUseColSpanStyle)
                javaScriptBuilder.Append("useColSpanStyle: true, ");

            javaScriptBuilder.Append("groupHeaders: [ ");
            foreach (JqGridGroupHeader groupHeader in _groupHeaders)
                javaScriptBuilder.AppendFormat("{{ startColumnName: '{0}', numberOfColumns: {1}, titleText: '{2}' }}, ", groupHeader.StartColumn, groupHeader.NumberOfColumns, groupHeader.Title);
            javaScriptBuilder.Remove(javaScriptBuilder.Length - 2, 2).Append(" ]");

            javaScriptBuilder.Append("})");
        }

        private static void AppendColumnLabelOptions(string columnName, JqGridColumnLabelOptions options, StringBuilder javaScriptBuilder)
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
        /// Adds column with actions predefined formatter.
        /// </summary>
        /// <param name="name">Name for the column.</param>
        /// <param name="position">Position of the column.</param>
        /// <param name="width">Width of the column.</param>
        /// <param name="editButton">Value indicating if edit button is enabled.</param>
        /// <param name="deleteButton">Value indicating if delete button is enabled.</param>
        /// <param name="useFormEditing">Value indicating if form editing should be used instead of inline editing.</param>
        /// <param name="inlineEditingOptions">Options for inline editing (RestoreAfterError and MethodType options are ignored in this context).</param>
        /// <param name="formEditingOptions">Options for form editing.</param>
        /// <param name="deleteOptions">Options for deleting.</param>
        /// <returns>JqGridHelper instance.</returns>
        /// <remarks>It is adviced to set JqGridResponse.JsonReader.RepeatItems to false if you want to use this method, otherwise jqGrid will not skip this column while mapping data.</remarks>
        public JqGridHelper<TModel> AddActionsColumn(string name, int position = 0, int width = 60, bool editButton = true, bool deleteButton = true, bool useFormEditing = false, JqGridInlineNavigatorActionOptions inlineEditingOptions = null, JqGridNavigatorEditActionOptions formEditingOptions = null, JqGridNavigatorDeleteActionOptions deleteOptions = null)
        {
            JqGridColumnModel actionsColumnModel = new JqGridColumnModel(name);
            actionsColumnModel.Width = width;
            actionsColumnModel.Resizable = false;
            actionsColumnModel.Searchable = false;
            actionsColumnModel.Sortable = false;
            actionsColumnModel.Viewable = false;
            actionsColumnModel.Formatter = JqGridColumnPredefinedFormatters.Actions;
            actionsColumnModel.FormatterOptions = new JqGridColumnFormatterOptions()
            {
                EditButton = editButton,
                DeleteButton = deleteButton,
                UseFormEditing = useFormEditing,
                InlineEditingOptions = inlineEditingOptions,
                FormEditingOptions = formEditingOptions,
                DeleteOptions = deleteOptions
            };

            if (position <= 0)
            {
                _options.ColumnsNames.Insert(0, String.Empty);
                _options.ColumnsModels.Insert(0, actionsColumnModel);
            }
            else if (position >= _options.ColumnsModels.Count)
            {
                _options.ColumnsNames.Add(String.Empty);
                _options.ColumnsModels.Add(actionsColumnModel);
            }
            else
            {
                _options.ColumnsNames.Insert(position, String.Empty);
                _options.ColumnsModels.Insert(position, actionsColumnModel);
            }

            return this;
        }

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
        /// <exception cref="System.ArgumentNullException">The <paramref name="options"/> parameter is null.</exception>
        public JqGridHelper<TModel> Navigator(JqGridNavigatorOptions options, JqGridNavigatorEditActionOptions editActionOptions = null, JqGridNavigatorEditActionOptions addActionOptions = null, JqGridNavigatorDeleteActionOptions deleteActionOptions = null, JqGridNavigatorSearchActionOptions searchActionOptions = null, JqGridNavigatorViewActionOptions viewActionOptions = null)
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
        /// Enables Inline Navigator for this JqGridHelper instance.
        /// </summary>
        /// <param name="options">The options for the Inline Navigator.</param>
        /// <returns>JqGridHelper instance with enabled Inline Navigator.</returns>
        /// <exception cref="System.ArgumentNullException">The <paramref name="options"/> parameter is null.</exception>
        /// <exception cref="System.InvalidOperationException">Navigator method has not been called.</exception>
        public JqGridHelper<TModel> InlineNavigator(JqGridInlineNavigatorOptions options)
        {
            if (_navigatorOptions == null)
                throw new InvalidOperationException("In order to call InlineNavigator method you must call Navigator method first.");

            if (options == null)
                throw new ArgumentNullException("options");

            _inlineNavigatorOptions = options;

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
        /// <param name="cloneToTop">The value which defines if the button added to the bottom pager should be coppied to the top pager.</param>
        /// <returns>JqGridHelper instance.</returns>
        /// <exception cref="System.InvalidOperationException">Navigator method has not been called.</exception>
        public JqGridHelper<TModel> AddNavigatorButton(string caption = JqGridNavigatorDefaults.ButtonCaption, string icon = JqGridNavigatorDefaults.ButtonIcon, string onClick = null, JqGridNavigatorButtonPositions position = JqGridNavigatorButtonPositions.Last, string toolTip = "", string cursor = JqGridNavigatorDefaults.ButtonCursor, bool cloneToTop = false)
        {
            if (_navigatorOptions == null)
                throw new InvalidOperationException("In order to call AddNavigatorButton method you must call Navigator method first.");

            _navigatorControlsOptions.Add(new JqGridNavigatorButtonOptions()
            {
                CloneToTop = cloneToTop,
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
        /// <exception cref="System.ArgumentNullException">The <paramref name="options"/> parameter is null.</exception>
        /// <exception cref="System.InvalidOperationException">Navigator method has not been called.</exception>
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
        /// <param name="cloneToTop">The value which defines if the separator added to the bottom pager should be coppied to the top pager.</param>
        /// <returns>JqGridHelper instance.</returns>
        /// <exception cref="System.InvalidOperationException">Navigator method has not been called.</exception>
        public JqGridHelper<TModel> AddNavigatorSeparator(string @class = JqGridNavigatorDefaults.SeparatorClass, string content = "", bool cloneToTop = false)
        {
            if (_navigatorOptions == null)
                throw new InvalidOperationException("In order to call AddNavigatorSeparator method you must call Navigator method first.");

            _navigatorControlsOptions.Add(new JqGridNavigatorSeparatorOptions()
            {
                CloneToTop = cloneToTop,
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
        /// <exception cref="System.InvalidOperationException">Navigator method has not been called.</exception>
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
        /// <exception cref="System.InvalidOperationException">The filter grid model has not been provided and the automatic generation is not possible because the count of items in ColumnsModel  is different from ColumnsNames.</exception>
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

        /// <summary>
        /// Sets grouping headers for this JqGridHelper instance.
        /// </summary>
        /// <param name="groupHeaders">Settings for grouping headers.</param>
        /// <param name="useColSpanStyle">The value which determines if the non grouping header cell should have cell above it (false), or the column should be treated as one combining boot (true).</param>
        /// <returns>JqGridHelper instance with grouping header.</returns>
        /// <exception cref="System.InvalidOperationException">Columns sorting (reordering) is enabled. </exception>
        public JqGridHelper<TModel> SetGroupHeaders(IEnumerable<JqGridGroupHeader> groupHeaders, bool useColSpanStyle = false)
        {
            if (_options.Sortable)
                throw new InvalidOperationException("Header grouping can not be set-up when columns sorting (reordering) is enabled.");

            _groupHeaders = groupHeaders;
            _groupHeadersUseColSpanStyle = useColSpanStyle;
            return this;
        }

        /// <summary>
        /// Sets frozen columns for this JqGridHelper instance.
        /// </summary>
        /// <returns>JqGridHelper instance with frozen columns.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// <list type="bullet">
        /// <item><description>TreeGrid is enabled.</description></item>
        /// <item><description>SubGrid is enabled.</description></item>
        /// <item><description>Columns sorting (reordering) is enabled.</description></item>
        /// <item><description>Dynamic scrolling is enabled.</description></item>
        /// <item><description>Data grouping is enabled.</description></item>
        /// </list> 
        /// </exception>
        public JqGridHelper<TModel> SetFrozenColumns()
        {
            if (_options.TreeGridEnabled)
                throw new InvalidOperationException("Frozen columns can not be set-up when TreeGrid is enabled.");

            if (_options.SubgridEnabled)
                throw new InvalidOperationException("Frozen columns can not be set-up when SubGrid is enabled.");

            if (_options.Sortable)
                throw new InvalidOperationException("Frozen columns can not be set-up when columns sorting (reordering) is enabled.");

            if (_options.DynamicScrollingMode != JqGridDynamicScrollingModes.Disabled)
                throw new InvalidOperationException("Frozen columns can not be set-up when dynamic scrolling is enabled.");

            if (_options.GroupingEnabled)
                throw new InvalidOperationException("Frozen columns can not be set-up when data grouping is enabled.");

            _setFrozenColumns = true;
            return this;
        }
        #endregion
    }
}
