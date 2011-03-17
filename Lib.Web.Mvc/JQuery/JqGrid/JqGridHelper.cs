using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

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
        private bool _filterToolbar = false;
        private JqGridFilterToolbarOptions _filterToolbarOptions;
        private List<JqGridFilterGridRowModel> _filterGridModel;
        private JqGridFilterGridOptions _filterGridOptions;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridHelper class.
        /// </summary>
        /// <param name="id">Identifier, which will be used for table (id='{0}'), pager div (id='{0}Pager'), filter grid div (id='{0}Search') and in JavaScript.</param>
        /// <param name="cellEditingEnabled">The value indicating if cell editing is enabled.</param>
        /// <param name="cellEditingSubmitMode">The cell editing submit mode.</param>
        /// <param name="cellEditingUrl">The URL for cell editing submit.</param>
        /// <param name="dataString">The string of data which will be used when DataType is set to JqGridDataTypes.XmlString or JqGridDataTypes.JsonString.</param>
        /// <param name="dataType">The type of information to expect to represent data in the grid.</param>
        /// <param name="editingUrl">The url for inline and form editing</param>
        /// <param name="expandColumnClick">The value which defines whether the tree is expanded and/or collapsed when user clicks on the text of the expanded column, not only on the image.</param>
        /// <param name="expandColumn">The name of column which should be used to expand the tree grid.</param>
        /// <param name="height">The height of the grid in pixels (default 'auto').</param>
        /// <param name="methodType">The type of request to make.</param>
        /// <param name="onSelectRow">The name of the function for event which is raised immediately after row was clicked.</param>
        /// <param name="pager">If grid should use a pager bar to navigate through the records.</param>
        /// <param name="rowsNumber">How many records should be displayed in the grid.</param>
        /// <param name="sortingName">The initial sorting column index, when  using data returned from server.</param>
        /// <param name="sortingOrder">The initial sorting order, when  using data returned from server.</param>
        /// <param name="subgridEnabled">The value which defines if subgrid is enabled.</param>
        /// <param name="subgridModel">The subgrid model.</param>
        /// <param name="subgridUrl">The url for subgrid data requests.</param>
        /// <param name="subgridColumnWidth">The width of subgrid expand/colapse column.</param>
        /// <param name="treeGridEnabled">The value which defines if TreeGrid is enabled.</param>
        /// <param name="treeGridModel">The model for TreeGrid.</param>
        /// <param name="url">The url for data requests.</param>
        /// <param name="viewRecords">If grid should display the beginning and ending record number out of the total number of records in the query.</param>
        /// <param name="width">The width of the grid in pixels.</param>
        public JqGridHelper(string id, bool? cellEditingEnabled = null, JqGridCellEditingSubmitModes? cellEditingSubmitMode = null, string cellEditingUrl = null, string dataString = null, JqGridDataTypes dataType = JqGridDataTypes.Xml, string editingUrl = null, bool? expandColumnClick = null, string expandColumn = null, int? height = null, JqGridMethodTypes methodType = JqGridMethodTypes.Get, string onSelectRow = null, bool pager = false, int rowsNumber = 20, string sortingName = "", JqGridSortingOrders sortingOrder = JqGridSortingOrders.Asc, bool? subgridEnabled = null, JqGridSubgridModel subgridModel = null, string subgridUrl = null, int? subgridColumnWidth = null, bool? treeGridEnabled = null, JqGridTreeGridModels? treeGridModel = null, string url = null, bool viewRecords = false, int? width = null)
            : this(new JqGridOptions<TModel>(id) { CellEditingEnabled = cellEditingEnabled, CellEditingSubmitMode = cellEditingSubmitMode, CellEditingUrl = cellEditingUrl, DataString = dataString, DataType = dataType, EditingUrl = editingUrl, ExpandColumnClick = expandColumnClick, ExpandColumn = expandColumn, Height = height, MethodType = methodType, OnSelectRow = onSelectRow, Pager = pager, RowsNumber = rowsNumber, SortingName = sortingName, SortingOrder = sortingOrder, SubgridEnabled = subgridEnabled, SubgridModel = subgridModel, SubgridUrl = subgridUrl, SubgridColumnWidth = subgridColumnWidth, TreeGridEnabled = treeGridEnabled, TreeGridModel = treeGridModel, Url = url, ViewRecords = viewRecords, Width = width })
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
                javaScriptBuilder.AppendFormat("{{ align: '{0}', index: '{1}', ", columnModel.Alignment.ToString().ToLower(), columnModel.Index);

                if (!String.IsNullOrWhiteSpace(columnModel.Classes))
                    javaScriptBuilder.AppendFormat("classes: '{0}', ", columnModel.Classes);

                if (columnModel.Editable.HasValue)
                {
                    javaScriptBuilder.AppendFormat("editable: {0}, ", columnModel.Editable.Value.ToString().ToLower());
                    if (columnModel.Editable.Value)
                    {
                        if (columnModel.EditType.HasValue)
                            javaScriptBuilder.AppendFormat("edittype: '{0}', ", columnModel.EditType.Value.ToString().ToLower());
                        AppendEditOptions(columnModel.EditOptions, ref javaScriptBuilder);
                        AppendEditRules(columnModel.EditRules, ref javaScriptBuilder);
                        AppendFormOptions(columnModel.FormOptions, ref javaScriptBuilder);
                    }
                }

                if (columnModel.Fixed.HasValue)
                    javaScriptBuilder.AppendFormat("fixed: {0}, ", columnModel.Fixed.Value.ToString().ToLower());

                if (!String.IsNullOrWhiteSpace(columnModel.Formatter))
                {
                    javaScriptBuilder.AppendFormat("formatter: {0}, ", columnModel.Formatter);
                    AppendFormatterOptions(columnModel.FormatterOptions, ref javaScriptBuilder);
                    if (!String.IsNullOrWhiteSpace(columnModel.UnFormatter))
                        javaScriptBuilder.AppendFormat("unformat: {0}, ", columnModel.UnFormatter);
                }

                if (columnModel.InitialSortingOrder.HasValue)
                    javaScriptBuilder.AppendFormat("firstsortorder: '{0}', ", columnModel.InitialSortingOrder.Value.ToString().ToLower());

                javaScriptBuilder.AppendFormat("hidden: {0}, ", columnModel.Hidden.ToString().ToLower());

                if (columnModel.Resizable.HasValue)
                    javaScriptBuilder.AppendFormat("resizable: {0}, ", columnModel.Resizable.Value.ToString().ToLower());

                if (columnModel.Searchable.HasValue)
                {
                    javaScriptBuilder.AppendFormat("search: {0}, ", columnModel.Searchable.Value.ToString().ToLower());
                    if (columnModel.Searchable.Value)
                    {
                        if (columnModel.SearchType.HasValue)
                            javaScriptBuilder.AppendFormat("stype: '{0}', ", columnModel.SearchType.Value.ToString().ToLower());
                        AppendSearchOptions(columnModel.SearchOptions, ref javaScriptBuilder);
                    }
                }

                if (columnModel.Sortable.HasValue)
                    javaScriptBuilder.AppendFormat("sortable: {0}, ", columnModel.Sortable.Value.ToString().ToLower());

                if (columnModel.Width.HasValue)
                    javaScriptBuilder.AppendFormat("width: {0}, ", columnModel.Width.Value);

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
                javaScriptBuilder.Append("editoptions: { ");

                if (!String.IsNullOrWhiteSpace(editOptions.CustomElementFunction))
                    javaScriptBuilder.AppendFormat("custom_element: {0},", editOptions.CustomElementFunction);

                if (!String.IsNullOrWhiteSpace(editOptions.CustomValueFunction))
                    javaScriptBuilder.AppendFormat("custom_value: {0},", editOptions.CustomValueFunction);

                if (!String.IsNullOrWhiteSpace(editOptions.DataUrl))
                    javaScriptBuilder.AppendFormat("dataUrl: '{0}',", editOptions.DataUrl);

                if (editOptions.MaximumLength.HasValue)
                    javaScriptBuilder.AppendFormat("maxlength: {0},", editOptions.MaximumLength.Value);

                if (editOptions.MultipleSelect.HasValue)
                    javaScriptBuilder.AppendFormat("multiple: {0},", editOptions.MultipleSelect.Value.ToString().ToLower());

                if (!String.IsNullOrWhiteSpace(editOptions.Source))
                    javaScriptBuilder.AppendFormat("src: '{0}',", editOptions.Source);

                javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                javaScriptBuilder.Append(" }, ");
            }
        }

        private static void AppendEditRules(JqGridColumnEditRules editRules, ref StringBuilder javaScriptBuilder)
        {
            if (editRules != null)
            {
                javaScriptBuilder.Append("editrules: { ");

                if (editRules.Custom.HasValue)
                    javaScriptBuilder.AppendFormat("custom: {0},", editRules.Custom.Value.ToString().ToLower());

                if (!String.IsNullOrWhiteSpace(editRules.CustomFunction))
                    javaScriptBuilder.AppendFormat("custom_func: {0},", editRules.CustomFunction);

                if (editRules.Date.HasValue)
                    javaScriptBuilder.AppendFormat("date: {0},", editRules.Date.Value.ToString().ToLower());

                if (editRules.EditHidden.HasValue)
                    javaScriptBuilder.AppendFormat("edithidden: {0},", editRules.EditHidden.Value.ToString().ToLower());

                if (editRules.Email.HasValue)
                    javaScriptBuilder.AppendFormat("email: {0},", editRules.Email.Value.ToString().ToLower());

                if (editRules.Integer.HasValue)
                    javaScriptBuilder.AppendFormat("integer: {0},", editRules.Integer.Value.ToString().ToLower());

                if (editRules.MaxValue.HasValue)
                    javaScriptBuilder.AppendFormat("maxValue: {0},", editRules.MaxValue.Value);

                if (editRules.MinValue.HasValue)
                    javaScriptBuilder.AppendFormat("minValue: {0},", editRules.MinValue.Value);

                if (editRules.Number.HasValue)
                    javaScriptBuilder.AppendFormat("number: {0},", editRules.Number.Value.ToString().ToLower());

                if (editRules.Required.HasValue)
                    javaScriptBuilder.AppendFormat("required: {0},", editRules.Required.Value.ToString().ToLower());

                if (editRules.Time.HasValue)
                    javaScriptBuilder.AppendFormat("time: {0},", editRules.Time.Value.ToString().ToLower());

                if (editRules.Url.HasValue)
                    javaScriptBuilder.AppendFormat("url: {0},", editRules.Url.Value.ToString().ToLower());

                javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                javaScriptBuilder.Append(" }, ");
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

                javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                javaScriptBuilder.Append(" }, ");
            }
        }

        private static void AppendFormatterOptions(JqGridColumnFormatterOptions formatterOptions, ref StringBuilder javaScriptBuilder)
        {
            if (formatterOptions != null)
            {
                javaScriptBuilder.Append("formatoptions: { ");

                if (!String.IsNullOrWhiteSpace(formatterOptions.AddParam))
                    javaScriptBuilder.AppendFormat("addParam: '{0}',", formatterOptions.AddParam);

                if (!String.IsNullOrWhiteSpace(formatterOptions.BaseLinkUrl))
                    javaScriptBuilder.AppendFormat("baseLinkUrl: '{0}',", formatterOptions.BaseLinkUrl);

                if (formatterOptions.DecimalPlaces.HasValue)
                    javaScriptBuilder.AppendFormat("decimalPlaces: {0},", formatterOptions.DecimalPlaces.Value);

                if (!String.IsNullOrWhiteSpace(formatterOptions.DecimalSeparator))
                    javaScriptBuilder.AppendFormat("decimalSeparator: '{0}',", formatterOptions.DecimalSeparator);

                if (!String.IsNullOrWhiteSpace(formatterOptions.DefaulValue))
                    javaScriptBuilder.AppendFormat("defaulValue: '{0}',", formatterOptions.DefaulValue);

                if (formatterOptions.Disabled.HasValue)
                    javaScriptBuilder.AppendFormat("disabled: {0},", formatterOptions.Disabled.Value.ToString().ToLower());

                if (!String.IsNullOrWhiteSpace(formatterOptions.IdName))
                    javaScriptBuilder.AppendFormat("idName: '{0}',", formatterOptions.IdName);

                if (!String.IsNullOrWhiteSpace(formatterOptions.Prefix))
                    javaScriptBuilder.AppendFormat("prefix: '{0}',", formatterOptions.Prefix);

                if (!String.IsNullOrWhiteSpace(formatterOptions.ShowAction))
                    javaScriptBuilder.AppendFormat("showAction: '{0}',", formatterOptions.ShowAction);

                if (!String.IsNullOrWhiteSpace(formatterOptions.SourceFormat))
                    javaScriptBuilder.AppendFormat("srcformat: '{0}',", formatterOptions.SourceFormat);

                if (!String.IsNullOrWhiteSpace(formatterOptions.Suffix))
                    javaScriptBuilder.AppendFormat("suffix: '{0}',", formatterOptions.Suffix);

                if (!String.IsNullOrWhiteSpace(formatterOptions.Target))
                    javaScriptBuilder.AppendFormat("target: '{0}',", formatterOptions.Target);

                if (!String.IsNullOrWhiteSpace(formatterOptions.TargetFormat))
                    javaScriptBuilder.AppendFormat("newformat: '{0}',", formatterOptions.TargetFormat);

                if (!String.IsNullOrWhiteSpace(formatterOptions.ThousandsSeparator))
                    javaScriptBuilder.AppendFormat("thousandsSeparator: '{0}',", formatterOptions.ThousandsSeparator);

                javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                javaScriptBuilder.Append(" }, ");
            }
        }

        private static void AppendSearchOptions(JqGridColumnSearchOptions searchOptions, ref StringBuilder javaScriptBuilder)
        {
            if (searchOptions != null)
            {
                javaScriptBuilder.Append("searchoptions: { ");

                if (!String.IsNullOrWhiteSpace(searchOptions.DataUrl))
                    javaScriptBuilder.AppendFormat("dataUrl: '{0}',", searchOptions.DataUrl);

                if (!String.IsNullOrWhiteSpace(searchOptions.DefaultValue))
                    javaScriptBuilder.AppendFormat("defaultValue: '{0}',", searchOptions.DefaultValue);

                if (searchOptions.SearchHidden.HasValue)
                    javaScriptBuilder.AppendFormat("searchhidden: {0},", searchOptions.SearchHidden.Value.ToString().ToLower());

                if (searchOptions.SearchOperators.HasValue)
                {
                    javaScriptBuilder.Append("sopt: [ ");
                    foreach (JqGridSearchOperators searchOperator in Enum.GetValues(typeof(JqGridSearchOperators)))
                    {
                        if ((searchOptions.SearchOperators & searchOperator) == searchOperator)
                            javaScriptBuilder.AppendFormat("'{0}',", Enum.GetName(typeof(JqGridSearchOperators), searchOperator).ToLower());
                    }
                    javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                    javaScriptBuilder.Append("],");
                }

                javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
                javaScriptBuilder.Append(" }, ");
            }
        }

        private void AppendOptions(ref StringBuilder javaScriptBuilder)
        {
            if (_options.CellEditingEnabled.HasValue)
            {
                javaScriptBuilder.AppendFormat("cellEdit: {0},", _options.CellEditingEnabled.Value.ToString().ToLower()).AppendLine();
                if (_options.CellEditingEnabled.Value)
                {
                    if (_options.CellEditingSubmitMode.HasValue)
                        javaScriptBuilder.AppendFormat("cellsubmit: '{0}',", _options.CellEditingSubmitMode.Value).AppendLine();

                    if (!String.IsNullOrWhiteSpace(_options.CellEditingUrl))
                        javaScriptBuilder.AppendFormat("cellurl: '{0}',", _options.CellEditingUrl).AppendLine();
                }
            }

            javaScriptBuilder.AppendFormat("datatype: '{0}',", _options.DataType.ToString().ToLower()).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.EditingUrl))
                javaScriptBuilder.AppendFormat("editurl: '{0}',", _options.EditingUrl).AppendLine();

            if (_options.ExpandColumnClick.HasValue)
                javaScriptBuilder.AppendFormat("ExpandColClick: {0},", _options.ExpandColumnClick.Value.ToString().ToLower()).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.ExpandColumn))
                javaScriptBuilder.AppendFormat("ExpandColumn: '{0}',", _options.ExpandColumn).AppendLine();

            if (_options.Height.HasValue)
                javaScriptBuilder.AppendFormat("height: {0},", _options.Height.Value).AppendLine();
            else
                javaScriptBuilder.AppendLine("height: 'auto',");

            javaScriptBuilder.AppendFormat("mtype: '{0}',", _options.MethodType.ToString().ToUpper()).AppendLine();

            if (!String.IsNullOrWhiteSpace(_options.OnSelectRow))
                javaScriptBuilder.AppendFormat("onSelectRow: {0},", _options.OnSelectRow).AppendLine();

            if (_options.Pager)
                javaScriptBuilder.AppendFormat("pager: '#{0}Pager',", _options.Id).AppendLine();

            javaScriptBuilder.AppendFormat("rowNum: {0},", _options.RowsNumber).AppendLine();
            javaScriptBuilder.AppendFormat("sortname: '{0}',", _options.SortingName).AppendLine();
            javaScriptBuilder.AppendFormat("sortorder: '{0}',", _options.SortingOrder.ToString().ToLower()).AppendLine();

            if (_options.SubgridEnabled.HasValue)
            {
                javaScriptBuilder.AppendFormat("subGrid: {0},", _options.SubgridEnabled.Value.ToString().ToLower()).AppendLine();
                if (_options.SubgridEnabled.Value)
                {
                    AppendSubgridModel(ref javaScriptBuilder);

                    if (!String.IsNullOrWhiteSpace(_options.SubgridUrl))
                        javaScriptBuilder.AppendFormat("subGridUrl: '{0}',", _options.SubgridUrl).AppendLine();

                    if (_options.SubgridColumnWidth.HasValue)
                        javaScriptBuilder.AppendFormat("subGridWidth: {0},", _options.SubgridColumnWidth.Value).AppendLine();
                }
            }

            if (_options.TreeGridEnabled.HasValue)
                javaScriptBuilder.AppendFormat("treeGrid: {0},", _options.TreeGridEnabled.Value.ToString().ToLower()).AppendLine();

            if (_options.TreeGridModel.HasValue)
                javaScriptBuilder.AppendFormat("treeGridModel: '{0}',", _options.TreeGridModel.Value.ToString().ToLower()).AppendLine();

            if (_options.UseDataString())
                javaScriptBuilder.AppendFormat("datastr: '{0}',", _options.DataString).AppendLine();
            else
                javaScriptBuilder.AppendFormat("url: '{0}',", _options.Url).AppendLine();

            javaScriptBuilder.AppendFormat("viewrecords: {0},", _options.ViewRecords.ToString().ToLower()).AppendLine();

            if (_options.Width.HasValue)
                javaScriptBuilder.AppendFormat("width: {0}", _options.Width.Value).AppendLine();
            else
                javaScriptBuilder.AppendLine("width: 'auto'");
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
            javaScriptBuilder.AppendFormat(".jqGrid('navGrid', '#{0}Pager',", _options.Id, _options.Id).AppendLine();
            AppendNavigatorOptions(ref javaScriptBuilder);
            if (_navigatorAddActionOptions != null || _navigatorDeleteActionOptions != null || _navigatorEditActionOptions != null || _navigatorSearchActionOptions != null || _navigatorViewActionOptions != null)
            {
                javaScriptBuilder.AppendLine(",");
                AppendNavigatorActionOptions(_navigatorEditActionOptions, ref javaScriptBuilder);
                javaScriptBuilder.AppendLine(",");
                AppendNavigatorActionOptions(_navigatorAddActionOptions, ref javaScriptBuilder);
                javaScriptBuilder.AppendLine(",");
                AppendNavigatorActionOptions(_navigatorDeleteActionOptions, ref javaScriptBuilder);
                javaScriptBuilder.AppendLine(",");
                AppendNavigatorActionOptions(_navigatorSearchActionOptions, ref javaScriptBuilder);
                javaScriptBuilder.AppendLine(",");
                AppendNavigatorActionOptions(_navigatorViewActionOptions, ref javaScriptBuilder);
            }
            javaScriptBuilder.Append(")");
        }

        private void AppendNavigatorOptions(ref StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.Append("{ ");
            javaScriptBuilder.AppendFormat("add: {0},", _navigatorOptions.Add.ToString().ToLower());

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.AddIcon))
                javaScriptBuilder.AppendFormat("addicon: '{0}',", _navigatorOptions.AddIcon);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.AddText))
                javaScriptBuilder.AppendFormat("addtext: '{0}',", _navigatorOptions.AddText);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.AddToolTip))
                javaScriptBuilder.AppendFormat("addtitle: '{0}',", _navigatorOptions.AddToolTip);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.AlertCaption))
                javaScriptBuilder.AppendFormat("alertcap: '{0}',", _navigatorOptions.AlertCaption);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.AlertText))
                javaScriptBuilder.AppendFormat("alerttext: '{0}',", _navigatorOptions.AlertText);

            if (_navigatorOptions.CloneToTop.HasValue)
                javaScriptBuilder.AppendFormat("cloneToTop: {0},", _navigatorOptions.CloneToTop.ToString().ToLower());

            if (_navigatorOptions.CloseOnEscape.HasValue)
                javaScriptBuilder.AppendFormat("closeOnEscape: {0},", _navigatorOptions.CloseOnEscape.ToString().ToLower());

            javaScriptBuilder.AppendFormat("del: {0},", _navigatorOptions.Delete.ToString().ToLower());

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.DeleteIcon))
                javaScriptBuilder.AppendFormat("delicon: '{0}',", _navigatorOptions.DeleteIcon);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.DeleteText))
                javaScriptBuilder.AppendFormat("deltext: '{0}',", _navigatorOptions.DeleteText);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.DeleteToolTip))
                javaScriptBuilder.AppendFormat("deltitle: '{0}',", _navigatorOptions.DeleteToolTip);

            javaScriptBuilder.AppendFormat("edit: {0},", _navigatorOptions.Edit.ToString().ToLower());

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.EditIcon))
                javaScriptBuilder.AppendFormat("editicon: '{0}',", _navigatorOptions.EditIcon);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.EditText))
                javaScriptBuilder.AppendFormat("edittext: '{0}',", _navigatorOptions.EditText);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.EditToolTip))
                javaScriptBuilder.AppendFormat("edittitle: '{0}',", _navigatorOptions.EditToolTip);

            if (_navigatorOptions.Position.HasValue)
                javaScriptBuilder.AppendFormat("position: '{0}',", _navigatorOptions.Position.ToString().ToLower());

            javaScriptBuilder.AppendFormat("refresh: {0},", _navigatorOptions.Refresh.ToString().ToLower());

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.RefreshIcon))
                javaScriptBuilder.AppendFormat("refreshicon: '{0}',", _navigatorOptions.RefreshIcon);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.RefreshText))
                javaScriptBuilder.AppendFormat("refreshtext: '{0}',", _navigatorOptions.RefreshText);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.RefreshToolTip))
                javaScriptBuilder.AppendFormat("refreshtitle: '{0}',", _navigatorOptions.RefreshToolTip);

            if (_navigatorOptions.RefreshMode.HasValue)
                javaScriptBuilder.AppendFormat("refreshstate: '{0}',", _navigatorOptions.RefreshMode.ToString().ToLower());

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.AfterRefresh))
                javaScriptBuilder.AppendFormat("afterRefresh: {0},", _navigatorOptions.AfterRefresh);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.BeforeRefresh))
                javaScriptBuilder.AppendFormat("beforeRefresh: {0},", _navigatorOptions.BeforeRefresh);

            javaScriptBuilder.AppendFormat("search: {0},", _navigatorOptions.Search.ToString().ToLower());

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.SearchIcon))
                javaScriptBuilder.AppendFormat("searchicon: '{0}',", _navigatorOptions.SearchIcon);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.SearchText))
                javaScriptBuilder.AppendFormat("searchtext: '{0}',", _navigatorOptions.SearchText);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.SearchToolTip))
                javaScriptBuilder.AppendFormat("searchtitle: '{0}',", _navigatorOptions.SearchToolTip);

            javaScriptBuilder.AppendFormat("view: {0},", _navigatorOptions.View.ToString().ToLower());

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.ViewIcon))
                javaScriptBuilder.AppendFormat("viewicon: '{0}',", _navigatorOptions.ViewIcon);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.ViewText))
                javaScriptBuilder.AppendFormat("viewtext: '{0}',", _navigatorOptions.ViewText);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.ViewToolTip))
                javaScriptBuilder.AppendFormat("viewtitle: '{0}',", _navigatorOptions.ViewToolTip);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.AddFunction))
                javaScriptBuilder.AppendFormat("addfunc: {0},", _navigatorOptions.AddFunction);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.EditFunction))
                javaScriptBuilder.AppendFormat("editfunc: {0},", _navigatorOptions.EditFunction);

            if (!String.IsNullOrWhiteSpace(_navigatorOptions.DeleteFunction))
                javaScriptBuilder.AppendFormat("delfunc: {0},", _navigatorOptions.DeleteFunction);

            javaScriptBuilder.Remove(javaScriptBuilder.Length - 1, 1);
            javaScriptBuilder.Append(" }");
        }

        private static void AppendNavigatorActionOptions(JqGridNavigatorActionOptions actionOptions, ref StringBuilder javaScriptBuilder)
        {
            javaScriptBuilder.Append("{ ");

            if (actionOptions != null)
            {
                if (actionOptions.ClearAfterAdd.HasValue)
                    javaScriptBuilder.AppendFormat("clearAfterAdd: {0},", actionOptions.ClearAfterAdd.Value.ToString().ToLower());

                if (actionOptions.CloseAfterAdd.HasValue)
                    javaScriptBuilder.AppendFormat("closeAfterAdd: {0},", actionOptions.CloseAfterAdd.Value.ToString().ToLower());

                if (actionOptions.CloseAfterEdit.HasValue)
                    javaScriptBuilder.AppendFormat("closeAfterEdit: {0},", actionOptions.CloseAfterEdit.Value.ToString().ToLower());

                if (actionOptions.CloseOnEscape.HasValue)
                    javaScriptBuilder.AppendFormat("closeOnEscape: {0},", actionOptions.CloseOnEscape.Value.ToString().ToLower());

                if (actionOptions.Dragable.HasValue)
                    javaScriptBuilder.AppendFormat("drag: {0},", actionOptions.Dragable.Value.ToString().ToLower());

                if (actionOptions.UseJqModal.HasValue)
                    javaScriptBuilder.AppendFormat("jqModal: {0},", actionOptions.UseJqModal.Value.ToString().ToLower());

                if (actionOptions.Modal.HasValue)
                    javaScriptBuilder.AppendFormat("modal: {0},", actionOptions.Modal.Value.ToString().ToLower());

                if (actionOptions.MethodType.HasValue)
                    javaScriptBuilder.AppendFormat("mtype: {0},", actionOptions.MethodType.Value.ToString().ToUpper());

                if (!String.IsNullOrWhiteSpace(actionOptions.OnClose))
                    javaScriptBuilder.AppendFormat("onClose: '{0}',", actionOptions.OnClose);

                if (actionOptions.ReloadAfterSubmit.HasValue)
                    javaScriptBuilder.AppendFormat("reloadAfterSubmit: {0},", actionOptions.ReloadAfterSubmit.Value.ToString().ToLower());

                if (actionOptions.Resizable.HasValue)
                    javaScriptBuilder.AppendFormat("resize: {0},", actionOptions.Resizable.Value.ToString().ToLower());

                if (!String.IsNullOrWhiteSpace(actionOptions.Url))
                    javaScriptBuilder.AppendFormat("url: '{0}',", actionOptions.Url);

                JqGridNavigatorSearchActionOptions searchActionOptions = actionOptions as JqGridNavigatorSearchActionOptions;
                if (searchActionOptions != null)
                {
                    if (!String.IsNullOrWhiteSpace(searchActionOptions.AfterShowSearch))
                        javaScriptBuilder.AppendFormat("afterShowSearch: '{0}',", searchActionOptions.AfterShowSearch);

                    if (!String.IsNullOrWhiteSpace(searchActionOptions.BeforeShowSearch))
                        javaScriptBuilder.AppendFormat("beforeShowSearch: '{0}',", searchActionOptions.BeforeShowSearch);

                    if (!String.IsNullOrWhiteSpace(searchActionOptions.Caption))
                        javaScriptBuilder.AppendFormat("caption: '{0}',", searchActionOptions.Caption);

                    if (searchActionOptions.CloseAfterSearch.HasValue)
                        javaScriptBuilder.AppendFormat("closeAfterSearch: {0},", searchActionOptions.CloseAfterSearch.Value.ToString().ToLower());

                    if (searchActionOptions.CloseAfterReset.HasValue)
                        javaScriptBuilder.AppendFormat("closeAfterReset: {0},", searchActionOptions.CloseAfterReset.Value.ToString().ToLower());
                    
                    if (!String.IsNullOrWhiteSpace(searchActionOptions.SearchText))
                        javaScriptBuilder.AppendFormat("Find: '{0}',", searchActionOptions.SearchText);

                    if (searchActionOptions.AdvancedSearching.HasValue)
                        javaScriptBuilder.AppendFormat("multipleSearch: {0},", searchActionOptions.AdvancedSearching.Value.ToString().ToLower());

                    if (searchActionOptions.CloneSearchRowOnAdd.HasValue)
                        javaScriptBuilder.AppendFormat("cloneSearchRowOnAdd: {0},", searchActionOptions.CloneSearchRowOnAdd.Value.ToString().ToLower());

                    if (!String.IsNullOrWhiteSpace(searchActionOptions.OnInitializeSearch))
                        javaScriptBuilder.AppendFormat("onInitializeSearch: '{0}',", searchActionOptions.OnInitializeSearch);

                    if (searchActionOptions.RecreateFilter.HasValue)
                        javaScriptBuilder.AppendFormat("recreateFilter: {0},", searchActionOptions.RecreateFilter.Value.ToString().ToLower());

                    if (!String.IsNullOrWhiteSpace(searchActionOptions.ResetText))
                        javaScriptBuilder.AppendFormat("Reset: '{0},',", searchActionOptions.ResetText);

                    if (searchActionOptions.Overlay.HasValue)
                        javaScriptBuilder.AppendFormat("overlay: {0},", searchActionOptions.Overlay.Value.ToString().ToLower());
                }

                if (actionOptions.Width.HasValue)
                    javaScriptBuilder.AppendFormat("width: {0}", actionOptions.Width.Value);
                else
                    javaScriptBuilder.Append("width: 'auto'");
            }

            javaScriptBuilder.Append(" }");
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
                    if (_options.ColumnsModels[i].Searchable.HasValue && _options.ColumnsModels[i].Searchable.Value)
                    {
                        _filterGridModel.Add(new JqGridFilterGridRowModel(_options.ColumnsModels[i].Name, _options.ColumnsNames[i])
                        {
                            DefaultValue = _options.ColumnsModels[i].SearchOptions != null ? _options.ColumnsModels[i].SearchOptions.DefaultValue : String.Empty,
                            SearchType = _options.ColumnsModels[i].SearchType.HasValue ? _options.ColumnsModels[i].SearchType.Value : JqGridColumnSearchTypes.Text,
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
        #endregion
    }
}
