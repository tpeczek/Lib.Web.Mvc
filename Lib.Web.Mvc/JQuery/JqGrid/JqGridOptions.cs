using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Options passed to helper's constructor
    /// </summary>
    public class JqGridOptions
    {
        #region Fields
        private string _afterInsertRow;
        private string _afterEditCell;
        private string _afterRestoreCell;
        private string _afterSaveCell;
        private string _afterSubmitCell;
        private string _altClass;
        private string _beforeRequest;
        private string _beforeSelectRow;
        private string _beforeEditCell;
        private string _beforeSaveCell;
        private string _beforeSubmitCell;
        private string _beforeProcessing;
        private string _caption;
        private string _cellEditingUrl;
        private string _editingUrl;
        private string _emptyRecords;
        private string _expandColumn;
        private string _errorCell;
        private string _formatCell;
        private string _gridComplete;
        private string _loadBeforeSend;
        private string _loadComplete;
        private string _loadError;
        private string _onCellSelect;
        private string _onDoubleClickRow;
        private string _onHeaderClick;
        private string _onInitGrid;
        private string _onPaging;
        private string _onRightClickRow;
        private string _onSelectAll;
        private string _onSelectCell;
        private string _onSelectRow;
        private string _onSortCol;
        private object _postData;
        private string _postDataScript;
        private string _resizeStart;
        private string _resizeStop;
        private string _rowAttributes;
        private string _serializeCellData;
        private string _serializeGridData;
        private string _serializeSubGridData;
        private string _sortingName;
        private string _subgridUrl;
        private string _subGridBeforeExpand;
        private string _subGridRowExpanded;
        private string _subGridRowColapsed;
        private string _url;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the grid identifier which will be used for table (id='{0}'), pager div (id='{0}Pager') and in JavaScript.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the list of columns parameters descriptions.
        /// </summary>
        public List<JqGridColumnModel> ColumnsModels { get; private set; }

        /// <summary>
        /// Gets the list of columns names.
        /// </summary>
        public List<string> ColumnsNames { get; private set; }

        /// <summary>
        /// Gets or sets the list of columns indexes for remaping (default null).
        /// </summary>
        public List<int> ColumnsRemaping { get; set; }

        /// <summary>
        /// Gets or sets default compatibility mode
        /// </summary>
        public static JqGridCompatibilityModes DefaultCompatibilityMode { get; set; } = JqGridCompatibilityModes.JqGrid;

        /// <summary>
        /// Gets or sets the jqGrid compatibility mode.
        /// </summary>
        public JqGridCompatibilityModes CompatibilityMode { get; set; } = DefaultCompatibilityMode;

        internal bool IsAfterInsertRowSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised after every inserted row.
        /// </summary>
        public string AfterInsertRow
        {
            get => _afterInsertRow;
            set
            {
                _afterInsertRow = value;
                IsAfterInsertRowSetted = true;
            }
        }

        internal bool IsAfterEditCellSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised after the edited cell is edited.
        /// </summary>
        public string AfterEditCell
        {
            get => _afterEditCell;
            set
            {
                _afterEditCell = value;
                IsAfterInsertRowSetted = true;
            }
        }

        internal bool IsAfterRestoreCellSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised after calling the method restoreCell or the user press ESC leaving the changes.
        /// </summary>
        public string AfterRestoreCell
        {
            get => _afterRestoreCell;
            set
            {
                _afterRestoreCell = value;
                IsAfterRestoreCellSetted = true;
            }
        }

        internal bool IsAfterSaveCellSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised after the cell has been successfully saved.
        /// </summary>
        public string AfterSaveCell
        {
            get => _afterSaveCell;
            set
            {
                _afterSaveCell = value;
                IsAfterSaveCellSetted = true;
            }
        }

        internal bool IsAfterSumbitCellSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised after the cell and other data is posted to the server.
        /// </summary>
        public string AfterSubmitCell
        {
            get => _afterSubmitCell;
            set
            {
                _afterSubmitCell = value;
                IsAfterSumbitCellSetted = true;
            }
        }

        internal bool IsAltClassSetted { get; private set; }
        /// <summary>
        /// Gets or sets the class that is used for alternate rows.
        /// </summary>
        /// <remarks>In Guriddo JqGrid this options is deprecated as of version 5.2</remarks>
        public string AltClass
        {
            get => _altClass;
            set
            {
                _altClass = value;
                IsAltClassSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if the grid should be "zebra-striped".
        /// </summary>
        public bool? AltRows { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the grid should auto encode/decode the data.
        /// </summary>
        public bool? AutoEncode { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the grid width will be recalculated automatically to the width of the parent element.
        /// </summary>
        public bool? AutoWidth { get; set; }

        internal bool IsBeforeRequestSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised before requesting any data.
        /// </summary>
        public string BeforeRequest
        {
            get => _beforeRequest;
            set
            {
                _beforeRequest = value;
                IsBeforeRequestSetted = true;
            }
        }

        internal bool IsBeforeSelectRowSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised when the user click on the row, but before selecting it.
        /// </summary>
        public string BeforeSelectRow
        {
            get => _beforeSelectRow;
            set
            {
                _beforeSelectRow = value;
                IsBeforeSelectRowSetted = true;
            }
        }

        internal bool IsBeforeEditCellSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised before editing the cell.
        /// </summary>
        public string BeforeEditCell
        {
            get => _beforeEditCell;
            set
            {
                _beforeEditCell = value;
                IsBeforeEditCellSetted = true;
            }
        }

        internal bool IsBeforeSaveCellSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised before validation of values if any.
        /// </summary>
        public string BeforeSaveCell
        {
            get => _beforeSaveCell;
            set
            {
                _beforeSaveCell = value;
                IsBeforeSaveCellSetted = true;
            }
        }

        internal bool IsBeforeSubmitCellSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised before submit the cell content to the server.
        /// </summary>
        public string BeforeSubmitCell
        {
            get => _beforeSubmitCell;
            set
            {
                _beforeSubmitCell = value;
                IsBeforeSubmitCellSetted = true;
            }
        }

        internal bool IsBeforeProcessingSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised before responce of the server is processed.
        /// </summary>
        public string BeforeProcessing
        {
            get => _beforeProcessing;
            set
            {
                _beforeProcessing = value;
                IsBeforeProcessingSetted = true;
            }
        }

        internal bool IsCaptionSetted { get; private set; }
        /// <summary>
        /// Gets or sets the caption for the grid.
        /// </summary>
        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                IsCaptionSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the padding plus border width of the cell.
        /// </summary>
        public int? CellLayout { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if cell editing is enabled
        /// </summary>
        public bool? CellEditingEnabled { get; set; }

        /// <summary>
        /// Gets or sets the cell editing submit mode
        /// </summary>
        public JqGridCellEditingSubmitModes CellEditingSubmitMode { get; set; } = JqGridCellEditingSubmitModes.Default;

        internal bool IsCellEditingUrlSetted { get; private set; }
        /// <summary>
        /// Gets or set the URL for cell editing submit
        /// </summary>
        public string CellEditingUrl
        {
            get => _cellEditingUrl;
            set
            {
                _cellEditingUrl = value;
                IsCellEditingUrlSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the string of data which will be used when DataType is set to JqGridDataTypes.XmlString or JqGridDataTypes.JsonString (default null).
        /// </summary>
        public string DataString { get; set; }

        /// <summary>
        /// Gets or sets the type of information to expect to represent data in the grid (default JqGridDataTypes.Xml).
        /// </summary>
        public JqGridDataTypes DataType { get; set; } = JqGridDataTypes.Default;

        /// <summary>
        /// Gets or sets the value indicating if jqGrid should be using jQuery.empty for the the row and all its children elements.
        /// </summary>
        public bool? DeepEmpty { get; set; }

        /// <summary>
        /// Gets or sets the language direction in grid.
        /// </summary>
        public JqGridLanguageDirections Direction { get; set; } = JqGridLanguageDirections.Default;

        /// <summary>
        /// Gets or sets the value which defines if dynamic scrolling is enabled.
        /// </summary>
        public JqGridDynamicScrollingModes DynamicScrollingMode { get; set; } = JqGridDynamicScrollingModes.Default;

        /// <summary>
        /// Gets or sets the timeout (in miliseconds) if DynamicScrollingMode is set to JqGridDynamicScrollingModes.HoldVisibleRows
        /// </summary>
        public int? DynamicScrollingTimeout { get; set; }

        internal bool IsEditingUrlSetted { get; private set; }
        /// <summary>
        /// Gets or sets the url for inline and form editing.
        /// </summary>
        public string EditingUrl
        {
            get => _editingUrl;
            set
            {
                _editingUrl = value;
                IsEditingUrlSetted = true;
            }
        }

        internal bool IsEmptyRecordsSetted { get; private set; }
        /// <summary>
        /// Gets or sets the information to be displayed when the returned (or the current) number of records is zero.
        /// </summary>
        public string EmptyRecords
        {
            get => _emptyRecords;
            set
            {
                _emptyRecords = value;
                IsEmptyRecordsSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the value which defines whether the tree is expanded and/or collapsed when user clicks on the text of the expanded column, not only on the image.
        /// </summary>
        public bool? ExpandColumnClick { get; set; }

        internal bool IsExpandColumnSetted { get; private set; }
        /// <summary>
        /// Gets or sets the name of column which should be used to expand the tree grid.
        /// </summary>
        public string ExpandColumn
        {
            get => _expandColumn;
            set
            {
                _expandColumn = value;
                IsExpandColumnSetted = true;
            }
        }

        internal bool IsErrorCellSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised when there is a server error while saving cell.
        /// </summary>
        public string ErrorCell
        {
            get => _errorCell;
            set
            {
                _errorCell = value;
                IsErrorCellSetted = true;
            }
        }

        internal bool IsFormatCellSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which allows formatting the cell content before editing.
        /// </summary>
        public string FormatCell
        {
            get => _formatCell;
            set
            {
                _formatCell = value;
                IsFormatCellSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if the footer table (with one row) will be placed below the grid records and above the pager. The number of columns equal of these from ColumnsModels.
        /// </summary>
        public bool? FooterEnabled { get; set; }

        internal bool IsGridCompleteSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised after all the data is loaded into the grid and all other processes are complete.
        /// </summary>
        public string GridComplete
        {
            get => _gridComplete;
            set
            {
                _gridComplete = value;
                IsGridCompleteSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if all the data should be inserted into DOM with one jQuery call (5 to 10 times faster).
        /// </summary>
        /// <remarks>Following features can not be used when this option is enabled:
        /// <list type="bullet">
        /// <item><description>TreeGrid</description></item>
        /// <item><description>SubGrid</description></item>
        /// <item><description>AfterInsertRow event.</description></item>
        /// </list>
        /// </remarks>
        public bool? GridView { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the grouping is enabled.
        /// </summary>
        public bool? GroupingEnabled { get; set; }

        /// <summary>
        /// Gets or sets the grouping view options.
        /// </summary>
        public JqGridGroupingView GroupingView { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the title attribute is added to the column headers.
        /// </summary>
        public bool? HeaderTitles { get; set; }

        /// <summary>
        /// Gets or sets the height of the grid in pixels (default 'auto').
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the grid is initialy hidden (no data loaded, only caption layer is shown).
        /// Takes effect only if the Caption property is not empty string and HiddenEnabled is set to true.
        /// </summary>
        public bool? Hidden { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the show/hide grid button is enabled.
        /// Takes effect only if the Caption property is not empty string.
        /// </summary>
        public bool? HiddenEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value which defines whether mouse hovering over the grid data rows is enabled.
        /// </summary>
        public bool? HoverRows { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the local searching is case-sensitive.
        /// </summary>
        public bool? IgnoreCase { get; set; }

        /// <summary>
        /// Gets or sets the JSON reader for the grid, set this if you want to change JsonReader for this grid
        /// </summary>
        public JqGridJsonReader JsonReader { get; set; }

        internal bool IsLoadBeforeSendSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for pre-callback to modify the XMLHttpRequest object before it is sent.
        /// </summary>
        public string LoadBeforeSend
        {
            get => _loadBeforeSend;
            set
            {
                _loadBeforeSend = value;
                IsLoadBeforeSendSetted = true;
            }
        }

        internal bool IsLoadCompleteSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised immediately after every server request.
        /// </summary>
        public string LoadComplete
        {
            get => _loadComplete;
            set
            {
                _loadComplete = value;
                IsLoadCompleteSetted = true;
            }
        }

        internal bool IsLoadErrorSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised after the request fails.
        /// </summary>
        public string LoadError
        {
            get => _loadError;
            set
            {
                _loadError = value;
                IsLoadErrorSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the value which defines whether the grid should load the data only once and all further manipulationsshould be done on the client side.
        /// </summary>
        public bool? LoadOnce { get; set; }

        /// <summary>
        /// Gets or sets the type of request to make (default JqGridMethodTypes.Get).
        /// </summary>
        public JqGridMethodTypes MethodType { get; set; } = JqGridMethodTypes.Default;

        /// <summary>
        /// Gets or sets the key which should be pressed when the user makes multiselection.
        /// </summary>
        public JqGridMultiKeys MultiKey { get; set; } = JqGridMultiKeys.Default;

        /// <summary>
        /// Gets or sets the value which defines whether the multiselection is done only when the checkbox is clicked.
        /// </summary>
        public bool? MultiBoxOnly { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the multiselection is enabled.
        /// </summary>
        public bool? MultiSelect { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the sorting by multiple columns is enabled.
        /// </summary>
        public bool? MultiSort { get; set; }

        /// <summary>
        /// Gets or sets the width of the multiselect colum.
        /// </summary>
        public int? MultiSelectWidth { get; set; }

        internal bool IsOnCellSelectSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised when user clicks on particular cell in the grid.
        /// </summary>
        public string OnCellSelect
        {
            get => _onCellSelect;
            set
            {
                _onCellSelect = value;
                IsOnCellSelectSetted = true;
            }
        }

        internal bool IsOnDoubleClickRowSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised immediately after row was double clicked.
        /// </summary>
        public string OnDoubleClickRow
        {
            get => _onDoubleClickRow;
            set
            {
                _onDoubleClickRow = value;
                IsOnDoubleClickRowSetted = true;
            }
        }

        internal bool IsOnHeaderClickSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised after clicking to hide or show grid.
        /// </summary>
        public string OnHeaderClick
        {
            get => _onHeaderClick;
            set
            {
                _onHeaderClick = value;
                IsOnHeaderClickSetted = true;
            }
        }

        internal bool IsOnInitGridSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised only once before populating the data.
        /// </summary>
        public string OnInitGrid
        {
            get => _onInitGrid;
            set
            {
                _onInitGrid = value;
                IsOnInitGridSetted = true;
            }
        }

        internal bool IsOnPagingSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised before populating the data after page index/size change.
        /// </summary>
        public string OnPaging
        {
            get => _onPaging;
            set
            {
                _onPaging = value;
                IsOnPagingSetted = true;
            }
        }

        internal bool IsOnRightClickRowSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised immediately after row was right clicked.
        /// </summary>
        public string OnRightClickRow
        {
            get => _onRightClickRow;
            set
            {
                _onRightClickRow = value;
                IsOnRightClickRowSetted = true;
            }
        }

        internal bool IsOnSelectAllSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised when MultipleSelect option is true and user clicks on the header checkbox.
        /// </summary>
        public string OnSelectAll
        {
            get => _onSelectAll;
            set
            {
                _onSelectAll = value;
                IsOnSelectAllSetted = true;
            }
        }

        internal bool IsOnSelectCellSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised after the cell is selected for editing.
        /// </summary>
        public string OnSelectCell
        {
            get => _onSelectCell;
            set
            {
                _onSelectCell = value;
                IsOnSelectCellSetted = true;
            }
        }

        internal bool IsOnSelectRowSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised immediately after row was clicked.
        /// </summary>
        public string OnSelectRow
        {
            get => _onSelectRow;
            set
            {
                _onSelectRow = value;
                IsOnSelectRowSetted = true;
            }
        }

        internal bool IsOnSortColSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised immediately after sortable column was clicked and before sorting the data.
        /// </summary>
        public string OnSortCol
        {
            get => _onSortCol;
            set
            {
                _onSortCol = value;
                IsOnSortColSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets if grid should use a pager bar to navigate through the records (default: false).
        /// </summary>
        public bool Pager { get; set; }

        internal bool IsPostDataSetted { get; private set; }
        /// <summary>
        /// Gets or sets the additional data which will be added to the request.
        /// </summary>
        public object PostData
        {
            get => _postData;
            set
            {
                _postData = value;
                IsPostDataSetted = true;
            }
        }

        internal bool IsPostDataScriptSetted { get; private set; }
        /// <summary>
        /// Gets or sets the JavaScript which will dynamically generate the additional data which will be added to the request (this property takes precedence over PostData).
        /// </summary>
        public string PostDataScript
        {
            get => _postDataScript;
            set
            {
                _postDataScript = value;
                IsPostDataScriptSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets customized names for jqGrid request parameters. Set this if you want redefine ParametersNames for this grid.
        /// </summary>
        public JqGridParametersNames ParametersNames { get; set; }

        internal bool IsResizeStartSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised when user starts resizing a column.
        /// </summary>
        public string ResizeStart
        {
            get => _resizeStart;
            set
            {
                _resizeStart = value;
                IsResizeStartSetted = true;
            }
        }

        internal bool IsResizeStopSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised after the column is resized.
        /// </summary>
        public string ResizeStop
        {
            get => _resizeStop;
            set
            {
                _resizeStop = value;
                IsResizeStopSetted = true;
            }
        }

        internal bool IsRowAttributesSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function which can add attributes to the row during the creation of the data (dynamically).
        /// </summary>
        public string RowAttributes
        {
            get => _rowAttributes;
            set
            {
                _rowAttributes = value;
                IsRowAttributesSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets an array to construct a select box element in the pager in which user can change the number of the visible rows.
        /// </summary>
        public IEnumerable<int> RowsList { get; set; }

        /// <summary>
        /// Gets or sets how many records should be displayed in the grid (default 20).
        /// </summary>
        public int? RowsNumber { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether a new column, which purpose is to count the number of available rows, should be added at left of the grid.
        /// </summary>
        public bool? RowsNumbers { get; set; }

        /// <summary>
        /// Gets or sets the width of the row number column.
        /// </summary>
        public int? RowsNumbersWidth { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the columns width should be recalculated to fit the width of the grid.
        /// </summary>
        public bool? ShrinkToFit { get; set; }

        /// <summary>
        /// Gets or sets the width of vertical scrollbar
        /// </summary>
        public int? ScrollOffset { get; set; }

        internal bool IsSerializeCellDataSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which can serialize the data passed to the ajax request when the cell is being saved.
        /// </summary>
        public string SerializeCellData
        {
            get => _serializeCellData;
            set
            {
                _serializeCellData = value;
                IsSerializeCellDataSetted = true;
            }
        }

        internal bool IsSerializeGridDataSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which can serialize the data passed to the ajax request.
        /// </summary>
        public string SerializeGridData
        {
            get => _serializeGridData;
            set
            {
                _serializeGridData = value;
                IsSerializeGridDataSetted = true;
            }
        }

        internal bool IsSerializeSubGridDataSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which can serialize the data passed to the subgrid ajax request.
        /// </summary>
        public string SerializeSubGridData
        {
            get => _serializeSubGridData;
            set
            {
                _serializeSubGridData = value;
                IsSerializeSubGridDataSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the value which defines whether the columns can be reordered by dragging and dropping them with the mouse.
        /// </summary>
        /// <remarks>This option requires the jQuery UI sortable widget and the jQuery UI Addons module.</remarks>
        public bool? Sortable { get; set; }

        internal bool IsSortingNameSetted { get; private set; }
        /// <summary>
        /// Gets or sets the initial sorting column index, when  using data returned from server (default: String.Empty)
        /// </summary>
        public string SortingName
        {
            get => _sortingName;
            set
            {
                _sortingName = value;
                IsSortingNameSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the initial sorting order, when  using data returned from server (default JqGridSortingOrders.Asc)
        /// </summary>
        public JqGridSortingOrders SortingOrder { get; set; } = JqGridSortingOrders.Default;

        /// <summary>
        /// Gets or set the CSS framework  
        /// </summary>
        /// <remarks>
        /// Available since Guriddo jqGrid 5.0
        /// </remarks>
        public JqGridStyleUIOptions StyleUI { get; set; } = JqGridStyleUIOptions.Default;

        /// <summary>
        /// Gets or sets the value which defines if subgrid is enabled.
        /// </summary>
        public bool? SubgridEnabled { get; set; }

        /// <summary>
        /// Gets or sets the subgrid model.
        /// </summary>
        public JqGridSubgridModel SubgridModel { get; set; }

        internal bool IsSubgridUrlSetted { get; private set; }
        /// <summary>
        /// Gets or sets the url for subgrid data requests.
        /// </summary>
        public string SubgridUrl
        {
            get => _subgridUrl;
            set
            {
                _subgridUrl = value;
                IsSubgridUrlSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the width of subgrid expand/colapse column.
        /// </summary>
        public int? SubgridColumnWidth { get; set; }

        internal bool IsSubGridBeforeExpandSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised just before expanding the subgrid.
        /// </summary>
        public string SubGridBeforeExpand
        {
            get => _subGridBeforeExpand;
            set
            {
                _subGridBeforeExpand = value;
                IsSubGridBeforeExpandSetted = true;
            }
        }

        internal bool IsSubGridRowExpandedSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised when the user clicks on the plus icon of the grid.
        /// </summary>
        public string SubGridRowExpanded
        {
            get => _subGridRowExpanded;
            set
            {
                _subGridRowExpanded = value;
                IsSubGridRowExpandedSetted = true;
            }
        }

        internal bool IsSubGridRowColapsedSetted { get; private set; }
        /// <summary>
        /// Gets or sets the function for event which is raised when the user clicks on the minus icon of the grid.
        /// </summary>
        public string SubGridRowColapsed
        {
            get => _subGridRowColapsed;
            set
            {
                _subGridRowColapsed = value;
                IsSubGridRowColapsedSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if jqGrid should place a pager element at top of the grid below the caption (if available).
        /// </summary>
        public bool? TopPager { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if TreeGrid is enabled.
        /// </summary>
        public bool? TreeGridEnabled { get; set; }

        /// <summary>
        /// Gets or sets the model for TreeGrid.
        /// </summary>
        public JqGridTreeGridModels TreeGridModel { get; set; }

        internal bool IsUrlSetted { get; private set; }
        /// <summary>
        /// Gets or sets the url for data requests (default null)
        /// </summary>
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                IsUrlSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if the values from user data should be placed on footer.
        /// </summary>
        public bool? UserDataOnFooter { get; set; }

        /// <summary>
        /// Gets or sets if grid should display the beginning and ending record number out of the total number of records in the query (default: false)
        /// </summary>
        public bool? ViewRecords { get; set; }

        /// <summary>
        /// Gets or sets the width of the grid in pixels (default 'auto').
        /// </summary>
        public int? Width { get; set; }

        internal bool IsUsedDataString => DataType == JqGridDataTypes.JsonString || DataType == JqGridDataTypes.XmlString;

        #endregion

        #region Constructors
        /// <summary>
        /// Initialize new instant of JqGridOptions.
        /// </summary>
        /// <param name="id">Id of Grid</param>
        public JqGridOptions(string id)
        {
            Id = id;
            ColumnsModels = new List<JqGridColumnModel>();
            ColumnsNames = new List<string>();
        }

        /// <summary>
        /// Initialize new instant of JqGridOptions.
        /// </summary>
        /// <param name="id">Id of Grid</param>
        /// <param name="modelType">Type of used model</param>
        public JqGridOptions(string id, Type modelType)
            : this(id)
        {
            SetModel(modelType);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Sets the grid columns models
        /// </summary>
        /// <param name="modelType"></param>
        public void SetModel(Type modelType)
        {
            ColumnsModels = new List<JqGridColumnModel>();
            ColumnsNames = new List<string>();
            var jqGridModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, modelType);
            foreach (var propertyMetadata in jqGridModelMetadata.Properties.Where(p => p.IsValidForColumn()))
            {
                var columnModel = new JqGridColumnModel(propertyMetadata);
                ColumnsModels.Add(columnModel);
                ColumnsNames.Add(propertyMetadata.GetDisplayName());
            }
        }

        #endregion
    }
}