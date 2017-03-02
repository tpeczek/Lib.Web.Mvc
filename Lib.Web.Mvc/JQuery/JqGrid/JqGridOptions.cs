using System;
using System.Collections.Generic;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// jqGrid options
    /// </summary>
    public class JqGridOptions
    {
        #region Fields
        private List<JqGridColumnModel> _columnsModel;
        private List<string> _columnsNames;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the jqGrid compatibility mode.
        /// </summary>
        public JqGridCompatibilityModes CompatibilityMode { get; set; }

        /// <summary>
        /// Gets the grid identifier which will be used for table (id='{0}'), pager div (id='{0}Pager') and in JavaScript.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after every inserted row.
        /// </summary>
        public string AfterInsertRow { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after the edited cell is edited.
        /// </summary>
        public string AfterEditCell { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after calling the method restoreCell or the user press ESC leaving the changes.
        /// </summary>
        public string AfterRestoreCell { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after the cell has been successfully saved.
        /// </summary>
        public string AfterSaveCell { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after the cell and other data is posted to the server.
        /// </summary>
        public string AfterSubmitCell { get; set; }

        /// <summary>
        /// Gets or sets the class that is used for alternate rows.
        /// </summary>
        public string AltClass { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the grid should be "zebra-striped".
        /// </summary>
        public bool AltRows { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the grid should auto encode/decode the data.
        /// </summary>
        public bool AutoEncode { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the grid width will be recalculated automatically to the width of the parent element.
        /// </summary>
        public bool AutoWidth { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before requesting any data.
        /// </summary>
        public string BeforeRequest { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised when the user click on the row, but before selecting it.
        /// </summary>
        public string BeforeSelectRow { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before editing the cell.
        /// </summary>
        public string BeforeEditCell { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before validation of values if any.
        /// </summary>
        public string BeforeSaveCell { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before submit the cell content to the server.
        /// </summary>
        public string BeforeSubmitCell { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before responce of the server is processed.
        /// </summary>
        public string BeforeProcessing { get; set; }

        /// <summary>
        /// Gets or sets the caption for the grid.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the padding plus border width of the cell.
        /// </summary>
        public int CellLayout { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if cell editing is enabled
        /// </summary>
        public bool CellEditingEnabled { get; set; }

        /// <summary>
        /// Gets or sets the cell editing submit mode
        /// </summary>
        public JqGridCellEditingSubmitModes CellEditingSubmitMode { get; set; }

        /// <summary>
        /// Gets or set the URL for cell editing submit
        /// </summary>
        public string CellEditingUrl { get; set; }

        /// <summary>
        /// Gets the list of columns parameters descriptions.
        /// </summary>
        public List<JqGridColumnModel> ColumnsModels { get { return _columnsModel; } }

        /// <summary>
        /// Gets the list of columns names.
        /// </summary>
        public List<string> ColumnsNames { get { return _columnsNames; } }

        /// <summary>
        /// Gets or sets the list of columns indexes for remaping (default null).
        /// </summary>
        public List<int> ColumnsRemaping { get; set;  }

        /// <summary>
        /// Gets or sets the string of data which will be used when DataType is set to JqGridDataTypes.XmlString or JqGridDataTypes.JsonString (default null).
        /// </summary>
        public string DataString { get; set; }

        /// <summary>
        /// Gets or sets the type of information to expect to represent data in the grid (default JqGridDataTypes.Xml).
        /// </summary>
        public JqGridDataTypes DataType { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if jqGrid should be using jQuery.empty for the the row and all its children elements.
        /// </summary>
        public bool DeepEmpty { get; set; }

        /// <summary>
        /// Gets or sets the language direction in grid.
        /// </summary>
        public JqGridLanguageDirections Direction { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if dynamic scrolling is enabled.
        /// </summary>
        public JqGridDynamicScrollingModes DynamicScrollingMode { get; set; }

        /// <summary>
        /// Gets or sets the timeout (in miliseconds) if DynamicScrollingMode is set to JqGridDynamicScrollingModes.HoldVisibleRows
        /// </summary>
        public int DynamicScrollingTimeout { get; set; }

        /// <summary>
        /// Gets or sets the url for inline and form editing.
        /// </summary>
        public string EditingUrl { get; set; }

        /// <summary>
        /// Gets or sets the information to be displayed when the returned (or the current) number of records is zero.
        /// </summary>
        public string EmptyRecords { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the tree is expanded and/or collapsed when user clicks on the text of the expanded column, not only on the image.
        /// </summary>
        public bool ExpandColumnClick { get; set; }

        /// <summary>
        /// Gets or sets the name of column which should be used to expand the tree grid.
        /// </summary>
        public string ExpandColumn { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised when there is a server error while saving cell.
        /// </summary>
        public string ErrorCell { get; set; }

        /// <summary>
        /// Gets or sets the function for event which allows formatting the cell content before editing.
        /// </summary>
        public string FormatCell { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the footer table (with one row) will be placed below the grid records and above the pager. The number of columns equal of these from ColumnsModels.
        /// </summary>
        public bool FooterEnabled { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after all the data is loaded into the grid and all other processes are complete.
        /// </summary>
        public string GridComplete { get; set; }

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
        public bool GridView { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the grouping is enabled.
        /// </summary>
        public bool GroupingEnabled { get; set; }

        /// <summary>
        /// Gets or sets the grouping view options.
        /// </summary>
        public JqGridGroupingView GroupingView { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the title attribute is added to the column headers.
        /// </summary>
        public bool HeaderTitles { get; set; }

        /// <summary>
        /// Gets or sets the height of the grid in pixels (default 'auto').
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the grid is initialy hidden (no data loaded, only caption layer is shown).
        /// Takes effect only if the Caption property is not empty string and HiddenEnabled is set to true.
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the show/hide grid button is enabled.
        /// Takes effect only if the Caption property is not empty string.
        /// </summary>
        public bool HiddenEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value which defines whether mouse hovering over the grid data rows is enabled.
        /// </summary>
        public bool HoverRows { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the local searching is case-sensitive.
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// Gets or sets the JSON reader for the grid.
        /// </summary>
        public JqGridJsonReader JsonReader { get; set; }

        /// <summary>
        /// Gets or sets the function for pre-callback to modify the XMLHttpRequest object before it is sent.
        /// </summary>
        public string LoadBeforeSend { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised immediately after every server request.
        /// </summary>
        public string LoadComplete { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after the request fails.
        /// </summary>
        public string LoadError { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the grid should load the data only once and all further manipulationsshould be done on the client side.
        /// </summary>
        public bool LoadOnce { get; set; }

        /// <summary>
        /// Gets or sets the type of request to make (default JqGridMethodTypes.Get).
        /// </summary>
        public JqGridMethodTypes MethodType { get; set; }

        /// <summary>
        /// Gets or sets the key which should be pressed when the user makes multiselection.
        /// </summary>
        public JqGridMultiKeys? MultiKey { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the multiselection is done only when the checkbox is clicked.
        /// </summary>
        public bool MultiBoxOnly { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the multiselection is enabled.
        /// </summary>
        public bool MultiSelect { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the sorting by multiple columns is enabled.
        /// </summary>
        public bool MultiSort { get; set; }

        /// <summary>
        /// Gets or sets the width of the multiselect colum.
        /// </summary>
        public int MultiSelectWidth { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised when user clicks on particular cell in the grid.
        /// </summary>
        public string OnCellSelect { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised immediately after row was double clicked.
        /// </summary>
        public string OnDoubleClickRow { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after clicking to hide or show grid.
        /// </summary>
        public string OnHeaderClick { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised only once before populating the data.
        /// </summary>
        public string OnInitGrid { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before populating the data after page index/size change.
        /// </summary>
        public string OnPaging { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised immediately after row was right clicked.
        /// </summary>
        public string OnRightClickRow { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised when MultipleSelect option is true and user clicks on the header checkbox.
        /// </summary>
        public string OnSelectAll { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after the cell is selected for editing.
        /// </summary>
        public string OnSelectCell { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised immediately after row was clicked.
        /// </summary>
        public string OnSelectRow { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised immediately after sortable column was clicked and before sorting the data.
        /// </summary>
        public string OnSortCol { get; set; }

        /// <summary>
        /// Gets or sets if grid should use a pager bar to navigate through the records (default: false).
        /// </summary>
        public bool Pager { get; set; }

        /// <summary>
        /// Gets or sets the additional data which will be added to the request.
        /// </summary>
        public object PostData { get; set; }

        /// <summary>
        /// Gets or sets the JavaScript which will dynamically generate the additional data which will be added to the request (this property takes precedence over PostData).
        /// </summary>
        public string PostDataScript { get; set; }

        /// <summary>
        /// Gets or sets customized names for jqGrid request parameters.
        /// </summary>
        public JqGridParametersNames ParametersNames { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised when user starts resizing a column.
        /// </summary>
        public string ResizeStart { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after the column is resized.
        /// </summary>
        public string ResizeStop { get; set; }

        /// <summary>
        /// Gets or sets the function which can add attributes to the row during the creation of the data (dynamically).
        /// </summary>
        public string RowAttributes { get; set; }

        /// <summary>
        /// Gets or sets an array to construct a select box element in the pager in which user can change the number of the visible rows.
        /// </summary>
        public List<int> RowsList { get; set; }

        /// <summary>
        /// Gets or sets how many records should be displayed in the grid (default 20).
        /// </summary>
        public int RowsNumber { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether a new column, which purpose is to count the number of available rows, should be added at left of the grid.
        /// </summary>
        public bool RowsNumbers { get; set; }

        /// <summary>
        /// Gets or sets the width of the row number column.
        /// </summary>
        public int RowsNumbersWidth { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the columns width should be recalculated to fit the width of the grid.
        /// </summary>
        public bool ShrinkToFit { get; set; }

        /// <summary>
        /// Gets or sets the width of vertical scrollbar
        /// </summary>
        public int ScrollOffset { get; set; }

        /// <summary>
        /// Gets or sets the function for event which can serialize the data passed to the ajax request when the cell is being saved.
        /// </summary>
        public string SerializeCellData { get; set; }

        /// <summary>
        /// Gets or sets the function for event which can serialize the data passed to the ajax request.
        /// </summary>
        public string SerializeGridData { get; set; }

        /// <summary>
        /// Gets or sets the function for event which can serialize the data passed to the subgrid ajax request.
        /// </summary>
        public string SerializeSubGridData { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the columns can be reordered by dragging and dropping them with the mouse.
        /// </summary>
        /// <remarks>This option requires the jQuery UI sortable widget and the jQuery UI Addons module.</remarks>
        public bool Sortable { get; set; }

        /// <summary>
        /// Gets or sets the initial sorting column index, when  using data returned from server (default: String.Empty)
        /// </summary>
        public string SortingName { get; set; }

        /// <summary>
        /// Gets or sets the initial sorting order, when  using data returned from server (default JqGridSortingOrders.Asc)
        /// </summary>
        public JqGridSortingOrders SortingOrder { get; set; }

        /// <summary>
        /// Gets or set the CSS framework  
        /// </summary>
        /// <remarks>
        /// Available since Guriddo jqGrid 5.0
        /// </remarks>
        public JqGridStyleUIOptions StyleUI { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if subgrid is enabled.
        /// </summary>
        public bool SubgridEnabled { get; set; }

        /// <summary>
        /// Gets or sets the subgrid model.
        /// </summary>
        public JqGridSubgridModel SubgridModel { get; set; }

        /// <summary>
        /// Gets or sets the url for subgrid data requests.
        /// </summary>
        public string SubgridUrl { get; set; }

        /// <summary>
        /// Gets or sets the width of subgrid expand/colapse column.
        /// </summary>
        public int SubgridColumnWidth { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised just before expanding the subgrid.
        /// </summary>
        public string SubGridBeforeExpand { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised when the user clicks on the plus icon of the grid.
        /// </summary>
        public string SubGridRowExpanded { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised when the user clicks on the minus icon of the grid.
        /// </summary>
        public string SubGridRowColapsed { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if jqGrid should place a pager element at top of the grid below the caption (if available).
        /// </summary>
        public bool TopPager { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if TreeGrid is enabled.
        /// </summary>
        public bool TreeGridEnabled { get; set; }

        /// <summary>
        /// Gets or sets the model for TreeGrid.
        /// </summary>
        public JqGridTreeGridModels TreeGridModel { get; set; }

        /// <summary>
        /// Gets or sets the url for data requests (default null)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the values from user data should be placed on footer.
        /// </summary>
        public bool UserDataOnFooter { get; set; }

        /// <summary>
        /// Gets or sets if grid should display the beginning and ending record number out of the total number of records in the query (default: false)
        /// </summary>
        public bool ViewRecords { get; set; }

        /// <summary>
        /// Gets or sets the width of the grid in pixels (default 'auto').
        /// </summary>
        public int? Width { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridOptions class.
        /// </summary>
        /// <param name="id">Identifier, which will be used for table (id='{0}'), pager div (id='{0}Pager'), filter grid div (id='{0}Search') and in JavaScript.</param>
        public JqGridOptions(string id)
        {
            CompatibilityMode = JqGridCompatibilityModes.JqGrid;
            Id = id;
            _columnsModel = new List<JqGridColumnModel>();
            _columnsNames = new List<string>();

            AfterInsertRow = null;
            AfterEditCell = null;
            AfterRestoreCell = null;
            AfterSaveCell = null;
            AfterSubmitCell = null;
            AltClass = JqGridOptionsDefaults.AltClass;
            AltRows = false;
            AutoEncode = false;
            AutoWidth = false;
            BeforeRequest = null;
            BeforeSelectRow = null;
            BeforeEditCell = null;
            BeforeSaveCell = null;
            BeforeSubmitCell = null;
            Caption = String.Empty;
            CellLayout = JqGridOptionsDefaults.CellLayout;
            CellEditingEnabled = false;
            CellEditingSubmitMode = JqGridCellEditingSubmitModes.Remote;
            CellEditingUrl = null;
            ColumnsRemaping = null;
            DataString = null;
            DataType = JqGridDataTypes.Xml;
            DeepEmpty = false;
            Direction = JqGridLanguageDirections.Ltr;
            DynamicScrollingMode = JqGridDynamicScrollingModes.Disabled;
            DynamicScrollingTimeout = 200;
            EditingUrl = null;
            EmptyRecords = JqGridOptionsDefaults.EmptyRecords;
            ExpandColumn = null;
            ExpandColumnClick = true;
            ErrorCell = null;
            FormatCell = null;
            FooterEnabled = false;
            GridComplete = null;
            GridView = false;
            GroupingEnabled = false;
            GroupingView = null;
            HeaderTitles = false;
            Height = null;
            Hidden = false;
            HiddenEnabled = true;
            HoverRows = true;
            IgnoreCase = false;
            JsonReader = JqGridResponse.JsonReader;
            LoadBeforeSend = null;
            LoadComplete = null;
            LoadError = null;
            LoadOnce = false;
            MethodType = JqGridMethodTypes.Get;
            MultiBoxOnly = false;
            MultiKey = null;
            MultiSelect = false;
            MultiSelectWidth = 20;
            MultiSort = false;
            OnCellSelect = null;
            OnDoubleClickRow = null;
            OnHeaderClick = null;
            OnPaging = null;
            OnRightClickRow = null;
            OnSelectAll = null;
            OnSelectCell = null;
            OnSelectRow = null;
            OnSortCol = null;
            Pager = false;
            ParametersNames = JqGridRequest.ParameterNames;
            ResizeStart = null;
            ResizeStop = null;
            RowsList = null;
            RowsNumber = 20;
            RowsNumbers = false;
            RowsNumbersWidth = 25;
            ScrollOffset = 18;
            ShrinkToFit = true;
            SerializeCellData = null;
            SerializeGridData = null;
            SerializeSubGridData = null;
            Sortable = false;
            SortingName = String.Empty;
            SortingOrder = JqGridSortingOrders.Asc;
            StyleUI = JqGridStyleUIOptions.jQueryUI;
            SubgridColumnWidth = 20;
            SubgridEnabled = false;
            SubgridModel = null;
            SubgridUrl = String.Empty;
            SubGridBeforeExpand = null;
            SubGridRowColapsed = null;
            SubGridRowExpanded = null;
            TopPager = false;
            TreeGridEnabled = false;
            TreeGridModel = JqGridTreeGridModels.Nested;
            Url = null;
            UserDataOnFooter = false;
            ViewRecords = false;
            Width = null;
        }
        #endregion

        #region Methods
        internal bool UseDataString()
        {
            return (this.DataType == JqGridDataTypes.JsonString || this.DataType == JqGridDataTypes.XmlString);
        }
        #endregion
    }
}
