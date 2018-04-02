using System;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;

namespace Lib.Web.Mvc.JQuery.JqGrid.Serialization
{
    internal class JqGridScriptConverter : JavaScriptConverter
    {
        #region Fields
        private static ReadOnlyCollection<Type> _supportedTypes = new ReadOnlyCollection<Type>(new List<Type>()
        {
            typeof(JqGridOptions),
            typeof(JqGridColumnModel),
            typeof(JqGridJsonReader),
            typeof(JqGridParametersNames),
            typeof(JqGridGroupingView),
            typeof(JqGridColumnEditOptions),
            typeof(JqGridColumnRules),
            typeof(JqGridColumnFormOptions),
            typeof(JqGridColumnFormatterOptions),
            typeof(JqGridColumnSearchOptions),
            typeof(JqGridSubgridModel),
            typeof(JqGridResponse),
            typeof(JqGridRequestSearchingFilters),
            typeof(JqGridRequestSearchingFilter)
        });
        #endregion

        #region JavaScriptConverter Members
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            object obj = null;

            if (dictionary != null)
            {
                if (type == typeof(JqGridOptions))
                    obj = DeserializeJqGridOptions(dictionary, serializer);
                else if (type == typeof(JqGridColumnModel))
                    obj = DeserializeJqGridColumnModel(dictionary, serializer);
                else if (type == typeof(JqGridJsonReader))
                    obj = DeserializeJqGridJsonReader(dictionary, serializer);
                else if (type == typeof(JqGridParametersNames))
                    obj = DeserializeJqGridParametersNames(dictionary, serializer);
                else if (type == typeof(JqGridGroupingView))
                    obj = DeserializeJqGridGroupingView(dictionary, serializer);
                else if (type == typeof(JqGridColumnEditOptions))
                    obj = DeserializeJqGridColumnEditOptions(dictionary, serializer);
                else if (type == typeof(JqGridColumnRules))
                    obj = DeserializeJqGridColumnRules(dictionary, serializer);
                else if (type == typeof(JqGridColumnFormOptions))
                    obj = DeserializeJqGridColumnFormOptions(dictionary, serializer);
                else if (type == typeof(JqGridColumnFormatterOptions))
                    obj = DeserializeJqGridColumnFormatterOptions(dictionary, serializer);
                else if (type == typeof(JqGridColumnSearchOptions))
                    obj = DeserializeJqGridColumnSearchOptions(dictionary, serializer);
                else if (type == typeof(JqGridSubgridModel))
                    obj = DeserializeJqGridSubgridModel(dictionary, serializer);
                else if (type == typeof(JqGridRequestSearchingFilters))
                    obj = DeserializeJqGridRequestSearchingFilters(dictionary, serializer);
                else if (type == typeof(JqGridRequestSearchingFilter))
                    obj = DeserializeJqGridRequestSearchingFilter(dictionary, serializer);
            }

            return obj;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var serializedObj = new Dictionary<string, object>();

            if (obj != null)
            {
                if (obj is JqGridOptions)
                    SerializeJqGridOptions((JqGridOptions)obj, serializer, serializedObj);
                else if (obj is JqGridColumnModel)
                    SerializeJqGridColumnModel((JqGridColumnModel)obj, serializer, serializedObj);
                else if (obj is JqGridJsonReader)
                    SerializeJqGridJsonReader((JqGridJsonReader)obj, serializer, serializedObj);
                else if (obj is JqGridParametersNames)
                    SerializeJqGridParametersNames((JqGridParametersNames)obj, serializer, serializedObj);
                else if (obj is JqGridGroupingView)
                    SerializeJqGridGroupingView((JqGridGroupingView)obj, serializer, serializedObj);
                else if (obj is JqGridColumnEditOptions)
                    SerializeJqGridColumnEditOptions((JqGridColumnEditOptions)obj, serializer, serializedObj);
                else if (obj is JqGridColumnRules)
                    SerializeJqGridColumnRules((JqGridColumnRules)obj, serializer, serializedObj);
                else if (obj is JqGridColumnFormatterOptions)
                    SerializeJqGridColumnFormatterOptions((JqGridColumnFormatterOptions)obj, serializer, serializedObj);
                else if (obj is JqGridColumnFormOptions)
                    SerializeJqGridColumnFormOptions((JqGridColumnFormOptions)obj, serializer, serializedObj);
                else if (obj is JqGridColumnSearchOptions)
                    SerializeJqGridColumnSearchOptions((JqGridColumnSearchOptions)obj, serializer, serializedObj);
                else if (obj is JqGridSubgridModel)
                    SerializeJqGridSubgridModel((JqGridSubgridModel)obj, serializer, serializedObj);
                else if (obj is JqGridResponse)
                    SerializeJqGridResponse((JqGridResponse)obj, serializer, serializedObj);
                else if (obj is JqGridRequestSearchingFilters)
                    SerializeJqGridRequestSearchingFilters((JqGridRequestSearchingFilters)obj, serializer, serializedObj);
                else if (obj is JqGridRequestSearchingFilter)
                    SerializeJqGridRequestSearchingFilter((JqGridRequestSearchingFilter)obj, serializer, serializedObj);
            }

            return serializedObj;
        }

        public override IEnumerable<Type> SupportedTypes => _supportedTypes;

        #endregion

        #region Methods
        private static JqGridOptions DeserializeJqGridOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            var id = GetStringFromSerializedObj(serializedObj, "id");
            if (!string.IsNullOrWhiteSpace(id))
            {
                var obj = new JqGridOptions(id);
                if (serializedObj.ContainsKey("altclass"))
                    obj.AltClass = serializedObj["altclass"]?.ToString();
                obj.AltRows = GetBooleanFromSerializedObj(serializedObj, "altRows");
                obj.AutoEncode = GetBooleanFromSerializedObj(serializedObj, "autoencode");
                obj.AutoWidth = GetBooleanFromSerializedObj(serializedObj, "autowidth");
                if (serializedObj.ContainsKey("caption"))
                    obj.Caption = serializedObj["caption"]?.ToString();
                obj.CellLayout = GetInt32FromSerializedObj(serializedObj, "cellLayout");
                obj.CellEditingEnabled = GetBooleanFromSerializedObj(serializedObj, "cellEdit");
                obj.CellEditingSubmitMode = GetEnumFromSerializedObj(serializedObj, "cellsubmit", JqGridCellEditingSubmitModes.Default);
                if (serializedObj.ContainsKey("cellurl"))
                    obj.CellEditingUrl = serializedObj["cellurl"]?.ToString();

                if (serializedObj.ContainsKey("colModel") && serializedObj["colModel"] is ArrayList)
                {
                    foreach (var innerSerializedObj in (ArrayList)serializedObj["colModel"])
                    {
                        if (innerSerializedObj is IDictionary<string, object>)
                        {
                            var columnModel = DeserializeJqGridColumnModel((IDictionary<string, object>)innerSerializedObj, serializer);
                            if (columnModel != null)
                                obj.ColumnsModels.Add(columnModel);
                        }
                    }
                }

                if (serializedObj.ContainsKey("colNames") && serializedObj["colNames"] is ArrayList)
                {
                    foreach (var innerSerializedObj in (ArrayList)serializedObj["colNames"])
                        obj.ColumnsNames.Add(innerSerializedObj.ToString());
                }

                if (serializedObj.ContainsKey("datastr"))
                    obj.DataString = serializedObj["datastr"]?.ToString();
                obj.DataType = GetEnumFromSerializedObj(serializedObj, "datatype", JqGridDataTypes.Default);
                obj.DeepEmpty = GetBooleanFromSerializedObj(serializedObj, "deepempty");
                obj.Direction = GetEnumFromSerializedObj(serializedObj, "direction", JqGridLanguageDirections.Default);
                obj.DynamicScrollingMode = JqGridDynamicScrollingModes.Default;
                if (serializedObj.ContainsKey("scroll") && serializedObj["scroll"] != null)
                {
                    if (serializedObj["scroll"] is bool b && b)
                        obj.DynamicScrollingMode = JqGridDynamicScrollingModes.HoldAllRows;
                    else if (serializedObj["scroll"] is int)
                        obj.DynamicScrollingMode = JqGridDynamicScrollingModes.HoldVisibleRows;
                }

                obj.DynamicScrollingTimeout = GetInt32FromSerializedObj(serializedObj, "scrollTimeout");
                if (serializedObj.ContainsKey("editurl"))
                    obj.EditingUrl = serializedObj["editurl"]?.ToString();
                if (serializedObj.ContainsKey("emptyrecords"))
                    obj.EmptyRecords = serializedObj["emptyrecords"]?.ToString();
                obj.ExpandColumnClick = GetBooleanFromSerializedObj(serializedObj, "ExpandColClick");
                if (serializedObj.ContainsKey("ExpandColumn"))
                    obj.ExpandColumn = serializedObj["ExpandColumn"]?.ToString();
                obj.FooterEnabled = GetBooleanFromSerializedObj(serializedObj, "footerrow");
                obj.UserDataOnFooter = GetBooleanFromSerializedObj(serializedObj, "userDataOnFooter");
                obj.GridView = GetBooleanFromSerializedObj(serializedObj, "gridview");

                obj.GroupingEnabled = GetBooleanFromSerializedObj(serializedObj, "grouping");
                if (serializedObj.ContainsKey("groupingView") && serializedObj["groupingView"] is IDictionary<string, object>)
                    obj.GroupingView = DeserializeJqGridGroupingView((IDictionary<string, object>)serializedObj["groupingView"], serializer);

                obj.Height = GetInt32FromSerializedObj(serializedObj, "height");
                obj.Hidden = GetBooleanFromSerializedObj(serializedObj, "hiddengrid");
                obj.HiddenEnabled = GetBooleanFromSerializedObj(serializedObj, "hidegrid");
                obj.HoverRows = GetBooleanFromSerializedObj(serializedObj, "hoverrows");
                obj.IgnoreCase = GetBooleanFromSerializedObj(serializedObj, "ignoreCase");
                obj.LoadOnce = GetBooleanFromSerializedObj(serializedObj, "loadonce");
                obj.MethodType = GetEnumFromSerializedObj(serializedObj, "mtype", JqGridMethodTypes.Default);
                obj.MultiKey = GetEnumFromSerializedObj(serializedObj, "multikey", JqGridMultiKeys.Default);
                obj.MultiBoxOnly = GetBooleanFromSerializedObj(serializedObj, "multiboxonly");
                obj.MultiSelect = GetBooleanFromSerializedObj(serializedObj, "multiselect");
                obj.MultiSelectWidth = GetInt32FromSerializedObj(serializedObj, "multiselectWidth");
                obj.MultiSort = GetBooleanFromSerializedObj(serializedObj, "multiSort");

                if (serializedObj.ContainsKey("pager") && serializedObj["pager"] != null)
                    obj.Pager = true;

                if (serializedObj.ContainsKey("jsonReader") && serializedObj["jsonReader"] is IDictionary<string, object>)
                    obj.JsonReader = DeserializeJqGridJsonReader((IDictionary<string, object>)serializedObj["jsonReader"], serializer);
                else
                    obj.JsonReader = JqGridResponse.JsonReader;

                if (serializedObj.ContainsKey("prmNames") && serializedObj["prmNames"] is IDictionary<string, object>)
                    obj.ParametersNames = DeserializeJqGridParametersNames((IDictionary<string, object>)serializedObj["prmNames"], serializer);
                else
                    obj.ParametersNames = JqGridRequest.ParameterNames;

                obj.ColumnsRemaping = GetInt32ArrayFromSerializedObj(serializedObj, "remapColumns");
                obj.RowsList = GetInt32ArrayFromSerializedObj(serializedObj, "rowList");
                obj.RowsNumber = GetInt32FromSerializedObj(serializedObj, "rowNum");
                obj.RowsNumbers = GetBooleanFromSerializedObj(serializedObj, "rownumbers");
                obj.RowsNumbersWidth = GetInt32FromSerializedObj(serializedObj, "rownumWidth");
                obj.ShrinkToFit = GetBooleanFromSerializedObj(serializedObj, "shrinkToFit");
                obj.ScrollOffset = GetInt32FromSerializedObj(serializedObj, "scrollOffset");
                obj.Sortable = GetBooleanFromSerializedObj(serializedObj, "sortable");
                if (serializedObj.ContainsKey("sortname"))
                    obj.SortingName = serializedObj["sortname"]?.ToString();
                obj.SortingOrder = GetEnumFromSerializedObj(serializedObj, "sortorder", JqGridSortingOrders.Default);
                obj.StyleUI = GetEnumFromSerializedObj(serializedObj, "styleUI", JqGridStyleUIOptions.Default);
                obj.SubgridEnabled = GetBooleanFromSerializedObj(serializedObj, "subGrid");

                if (serializedObj.ContainsKey("subGridModel") && serializedObj["subGridModel"] is IDictionary<string, object>)
                    obj.SubgridModel = DeserializeJqGridSubgridModel((IDictionary<string, object>)serializedObj["subGridModel"], serializer);

                if (serializedObj.ContainsKey("subGridUrl"))
                    obj.SubgridUrl = serializedObj["subGridUrl"]?.ToString();
                obj.SubgridColumnWidth = GetInt32FromSerializedObj(serializedObj, "subGridWidth");
                obj.TopPager = GetBooleanFromSerializedObj(serializedObj, "toppager");
                obj.TreeGridEnabled = GetBooleanFromSerializedObj(serializedObj, "treeGrid");
                obj.TreeGridModel = GetEnumFromSerializedObj(serializedObj, "treeGridModel", JqGridTreeGridModels.Default);
                if (serializedObj.ContainsKey("url"))
                    obj.Url = serializedObj["url"]?.ToString();
                obj.ViewRecords = GetBooleanFromSerializedObj(serializedObj, "viewrecords");
                obj.Width = GetInt32FromSerializedObj(serializedObj, "width");

                return obj;
            }
            return null;
        }

        private static void SerializeJqGridOptions(JqGridOptions obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("id", obj.Id);

            if (obj.IsAltClassSetted)
                serializedObj.Add("altclass", obj.AltClass);

            if (obj.AltRows.HasValue)
                serializedObj.Add("altRows", obj.AltRows.Value);

            if (obj.AutoEncode.HasValue)
                serializedObj.Add("autoencode", obj.AutoEncode.Value);

            if (obj.AutoWidth.HasValue)
                serializedObj.Add("autowidth", obj.AutoWidth.Value);

            if (obj.CellLayout.HasValue)
                serializedObj.Add("cellLayout", obj.CellLayout.Value);

            if (obj.CellEditingEnabled.HasValue)
                serializedObj.Add("cellEdit", obj.CellEditingEnabled.Value);

            if (obj.CellEditingSubmitMode != JqGridCellEditingSubmitModes.Default)
                serializedObj.Add("cellsubmit", obj.CellEditingSubmitMode);

            if (obj.IsCellEditingUrlSetted)
                serializedObj.Add("cellurl", obj.CellEditingUrl);

            serializedObj.Add("colModel", obj.ColumnsModels);
            serializedObj.Add("colNames", obj.ColumnsNames);

            if (obj.IsCaptionSetted)
                serializedObj.Add("caption", obj.Caption);

            if (obj.HiddenEnabled.HasValue)
                serializedObj.Add("hidegrid", obj.HiddenEnabled.Value);

            if (obj.Hidden.HasValue)
                serializedObj.Add("hiddengrid", obj.Hidden.Value);

            if (obj.IsUsedDataString)
                serializedObj.Add("datastr", obj.DataString);
            else
                serializedObj.Add("url", obj.Url);

            if (obj.DataType != JqGridDataTypes.Default)
                serializedObj.Add("datatype", obj.DataType.ToString().ToLower());

            if (obj.DeepEmpty.HasValue)
                serializedObj.Add("deepempty", obj.DeepEmpty.Value);

            if (obj.Direction != JqGridLanguageDirections.Default)
                serializedObj.Add("direction", obj.Direction.ToString().ToLower());

            if (obj.DynamicScrollingMode == JqGridDynamicScrollingModes.Disabled)
                serializedObj.Add("scroll", false);
            if (obj.DynamicScrollingMode == JqGridDynamicScrollingModes.HoldAllRows)
                serializedObj.Add("scroll", true);
            else if (obj.DynamicScrollingMode == JqGridDynamicScrollingModes.HoldVisibleRows)
                serializedObj.Add("scroll", 10);

            if (obj.DynamicScrollingTimeout.HasValue)
                serializedObj.Add("scrollTimeout", obj.DynamicScrollingTimeout);

            if (obj.IsEmptyRecordsSetted)
                serializedObj.Add("emptyrecords", obj.EmptyRecords);

            if (obj.IsEditingUrlSetted)
                serializedObj.Add("editurl", obj.EditingUrl);

            if (obj.FooterEnabled.HasValue)
            {
                serializedObj.Add("footerrow", obj.FooterEnabled.Value);
                if (obj.FooterEnabled.Value && obj.UserDataOnFooter.HasValue)
                    serializedObj.Add("userDataOnFooter", obj.UserDataOnFooter.Value);
            }

            if (obj.GridView.HasValue)
                serializedObj.Add("gridview", obj.GridView.Value);

            if (obj.GroupingEnabled.HasValue)
            {
                serializedObj.Add("grouping", obj.GroupingEnabled.Value);

                if (obj.GroupingEnabled.Value && obj.GroupingView != null)
                    serializedObj.Add("groupingView", obj.GroupingView);
            }

            if (obj.Height.HasValue)
                serializedObj.Add("height", obj.Height.Value);
            else
                serializedObj.Add("height", "100%");

            if (obj.HoverRows.HasValue)
                serializedObj.Add("hoverrows", obj.HoverRows.Value);

            if (obj.IgnoreCase.HasValue)
                serializedObj.Add("ignoreCase", obj.IgnoreCase.Value);

            if (obj.LoadOnce.HasValue)
                serializedObj.Add("loadonce", obj.LoadOnce.Value);

            if (obj.MethodType != JqGridMethodTypes.Default)
                serializedObj.Add("mtype", obj.MethodType.ToString().ToUpper());

            if (obj.MultiKey != JqGridMultiKeys.Default)
                if (obj.MultiKey == JqGridMultiKeys.Disable)
                    serializedObj.Add("multikey", null);
                else
                    serializedObj.Add("multikey", $"{obj.MultiKey.ToString().ToLower()}Key");

            if (obj.MultiBoxOnly.HasValue)
                serializedObj.Add("multiboxonly", obj.MultiBoxOnly.Value);

            if (obj.MultiSelect.HasValue)
                serializedObj.Add("multiselect", obj.MultiSelect.Value);

            if (obj.MultiSelectWidth.HasValue)
                serializedObj.Add("multiselectWidth", obj.MultiSelectWidth.Value);

            if (obj.MultiSort.HasValue)
                serializedObj.Add("multiSort", obj.MultiSort.Value);

            if (obj.Pager)
                serializedObj.Add("pager", $"#{obj.Id}Pager");

            if (obj.JsonReader != null && !obj.JsonReader.IsGlobal)
                serializedObj.Add("jsonReader", obj.JsonReader);

            if (obj.ParametersNames != null && !obj.ParametersNames.IsGlobal)
                serializedObj.Add("prmNames", obj.ParametersNames);

            serializedObj.Add("remapColumns", obj.ColumnsRemaping);

            if (obj.RowsList != null && obj.RowsList.Any())
                serializedObj.Add("rowList", obj.RowsList);

            if (obj.RowsNumber.HasValue)
                serializedObj.Add("rowNum", obj.RowsNumber.Value);

            if (obj.RowsNumbers.HasValue)
                serializedObj.Add("rownumbers", obj.RowsNumbers.Value);

            if (obj.RowsNumbersWidth.HasValue)
                serializedObj.Add("rownumWidth", obj.RowsNumbersWidth.Value);

            if (obj.ShrinkToFit.HasValue)
                serializedObj.Add("shrinkToFit", obj.ShrinkToFit.Value);

            if (obj.ScrollOffset.HasValue)
                serializedObj.Add("scrollOffset", obj.ScrollOffset.Value);

            if (obj.Sortable.HasValue)
                serializedObj.Add("sortable", obj.Sortable.Value);

            if (obj.IsSortingNameSetted)
                serializedObj.Add("sortname", obj.SortingName);

            if (obj.SortingOrder != JqGridSortingOrders.Default)
                serializedObj.Add("sortorder", obj.SortingOrder.ToString().ToLower());

            if (obj.StyleUI != JqGridStyleUIOptions.Default)
                serializedObj.Add("styleUI", obj.StyleUI.ToString());

            if (obj.SubgridEnabled.HasValue)
            {
                serializedObj.Add("subGrid", obj.SubgridEnabled.Value);

                if (obj.SubgridEnabled.Value)
                {
                    if (obj.SubgridModel != null)
                        serializedObj.Add("subGridModel", obj.SubgridModel);

                    if (obj.IsSubgridUrlSetted)
                        serializedObj.Add("subGridUrl", obj.SubgridUrl);

                    if (obj.SubgridColumnWidth.HasValue)
                        serializedObj.Add("subGridWidth", obj.SubgridColumnWidth.Value);
                }
            }

            if (obj.TopPager.HasValue)
                serializedObj.Add("toppager", obj.TopPager.Value);

            if (obj.TreeGridEnabled.HasValue)
            {
                serializedObj.Add("treeGrid", obj.TreeGridEnabled.Value);

                if (obj.TreeGridEnabled.Value)
                {
                    if (obj.TreeGridModel != JqGridTreeGridModels.Default)
                        serializedObj.Add("treeGridModel", obj.TreeGridModel.ToString().ToLower());

                    if (obj.ExpandColumnClick.HasValue)
                        serializedObj.Add("ExpandColClick", obj.ExpandColumnClick);

                    if (obj.IsExpandColumnSetted)
                        serializedObj.Add("ExpandColumn", obj.ExpandColumn);
                }
            }

            if (obj.ViewRecords.HasValue)
                serializedObj.Add("viewrecords", obj.ViewRecords.Value);

            if (obj.Width.HasValue)
                serializedObj.Add("width", obj.Width.Value);
        }

        private static JqGridColumnModel DeserializeJqGridColumnModel(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            var name = GetStringFromSerializedObj(serializedObj, "name");
            if (!string.IsNullOrWhiteSpace(name))
            {
                var obj = new JqGridColumnModel(name);

                obj.Alignment = GetEnumFromSerializedObj(serializedObj, "align", obj.Alignment);
                obj.DateFormat = GetSettedStringFromSerializedObj(serializedObj, "datefmt");
                obj.Classes = GetSettedStringFromSerializedObj(serializedObj, "classes");
                obj.Editable = GetBooleanFromSerializedObj(serializedObj, "editable");
                obj.EditType = GetEnumFromSerializedObj(serializedObj, "edittype", JqGridColumnEditTypes.Default);

                if (serializedObj.ContainsKey("editoptions") && serializedObj["editoptions"] is IDictionary<string, object>)
                    obj.EditOptions = DeserializeJqGridColumnEditOptions((IDictionary<string, object>)serializedObj["editoptions"], serializer);

                if (serializedObj.ContainsKey("editrules") && serializedObj["editrules"] is IDictionary<string, object>)
                    obj.EditRules = DeserializeJqGridColumnRules((IDictionary<string, object>)serializedObj["editrules"], serializer);

                obj.Fixed = GetBooleanFromSerializedObj(serializedObj, "fixed");
                obj.Frozen = GetBooleanFromSerializedObj(serializedObj, "frozen");
                obj.Hidden = GetBooleanFromSerializedObj(serializedObj, "hidden");
                obj.HideInDialog = GetBooleanFromSerializedObj(serializedObj, "hidedlg");

                obj.Formatter = GetSettedStringFromSerializedObj(serializedObj, "formatter");
                if (obj.Formatter?.IsSetted ?? false)
                    obj.Formatter.Value = '\'' + obj.Formatter.Value + '\'';

                if (serializedObj.ContainsKey("formatoptions") && serializedObj["formatoptions"] is IDictionary<string, object>)
                    obj.FormatterOptions = DeserializeJqGridColumnFormatterOptions((IDictionary<string, object>)serializedObj["formatoptions"], serializer);
                //obj.UnFormatter = GetStringFromSerializedObj(serializedObj, "unformat");

                if (serializedObj.ContainsKey("formoptions") && serializedObj["formoptions"] is IDictionary<string, object>)
                    obj.FormOptions = DeserializeJqGridColumnFormOptions((IDictionary<string, object>)serializedObj["formoptions"], serializer);

                obj.InitialSortingOrder = GetEnumFromSerializedObj(serializedObj, "firstsortorder", JqGridSortingOrders.Default);
                obj.JsonMapping = GetStringFromSerializedObj(serializedObj, "jsonmap");
                obj.Key = GetBooleanFromSerializedObj(serializedObj, "key");
                obj.Resizable = GetBooleanFromSerializedObj(serializedObj, "resizable");
                obj.SummaryType = GetEnumFromSerializedObj<JqGridColumnSummaryTypes>(serializedObj, "summaryType");
                if (!obj.SummaryType.HasValue)
                {
                    obj.SummaryFunction = GetStringFromSerializedObj(serializedObj, "summaryType");
                    if (!string.IsNullOrWhiteSpace(obj.SummaryFunction))
                        obj.SummaryType = JqGridColumnSummaryTypes.Custom;
                }

                obj.SummaryTemplate = GetSettedStringFromSerializedObj(serializedObj, "summaryTpl");
                obj.Sortable = GetBooleanFromSerializedObj(serializedObj, "sortable");
                obj.SortType = GetEnumFromSerializedObj(serializedObj, "sorttype", JqGridColumnSortTypes.Default);
                obj.Index = GetSettedStringFromSerializedObj(serializedObj, "index");
                obj.Searchable = GetBooleanFromSerializedObj(serializedObj, "search");
                obj.SearchType = GetEnumFromSerializedObj(serializedObj, "stype", JqGridColumnSearchTypes.Default);

                if (serializedObj.ContainsKey("searchoptions") && serializedObj["searchoptions"] is IDictionary<string, object>)
                    obj.SearchOptions = DeserializeJqGridColumnSearchOptions((IDictionary<string, object>)serializedObj["searchoptions"], serializer);

                if (serializedObj.ContainsKey("searchrules") && serializedObj["searchrules"] is IDictionary<string, object>)
                    obj.SearchRules = DeserializeJqGridColumnRules((IDictionary<string, object>)serializedObj["searchrules"], serializer);

                obj.Title = GetBooleanFromSerializedObj(serializedObj, "title");
                obj.Width = GetInt32FromSerializedObj(serializedObj, "width");
                obj.Viewable = GetBooleanFromSerializedObj(serializedObj, "viewable");
                obj.XmlMapping = GetStringFromSerializedObj(serializedObj, "xmlmap");

                return obj;
            }
            return null;
        }

        private static void SerializeJqGridColumnModel(JqGridColumnModel obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            if (obj.Alignment != JqGridAlignments.Default)
                serializedObj.Add("align", obj.Alignment.ToString().ToLower());

            if (obj.Classes?.IsSetted ?? false)
                serializedObj.Add("classes", obj.Classes);

            if (obj.DateFormat?.IsSetted ?? false)
                serializedObj.Add("datefmt", obj.DateFormat);

            if (obj.Editable.HasValue)
            {
                serializedObj.Add("editable", obj.Editable.Value);
                if (obj.Editable.Value)
                {
                    if (obj.EditType != JqGridColumnEditTypes.Default)
                        serializedObj.Add("edittype", obj.EditType.ToString().ToLower());

                    if (obj.EditOptions != null)
                        serializedObj.Add("editoptions", obj.EditOptions);

                    if (obj.EditRules != null)
                        serializedObj.Add("editrules", obj.EditRules);

                    if (obj.FormOptions != null)
                        serializedObj.Add("formoptions", obj.FormOptions);
                }
            }

            if (obj.Fixed.HasValue)
                serializedObj.Add("fixed", obj.Fixed.Value);

            if (obj.Frozen.HasValue)
                serializedObj.Add("frozen", obj.Frozen.Value);

            if (obj.InitialSortingOrder != JqGridSortingOrders.Default)
                serializedObj.Add("firstsortorder", obj.InitialSortingOrder.ToString().ToLower());

            if ((obj.Formatter?.IsSetted ?? false) && obj.Formatter.Value[0] == '\'' && obj.Formatter.Value[obj.Formatter.Value.Length - 1] == '\'')
            {
                serializedObj.Add("formatter", obj.Formatter.Value.Substring(1, obj.Formatter.Value.Length - 2));

                if (obj.FormatterOptions != null)
                    serializedObj.Add("formatoptions", obj.FormatterOptions);
            }

            //if (!String.IsNullOrWhiteSpace(obj.UnFormatter))
            //    serializedObj.Add("unformat", obj.UnFormatter);

            if (obj.Hidden.HasValue)
                serializedObj.Add("hidden", obj.Hidden.Value);

            if (obj.HideInDialog.HasValue)
                serializedObj.Add("hidedlg", obj.HideInDialog.Value);

            if (!string.IsNullOrWhiteSpace(obj.JsonMapping))
                serializedObj.Add("jsonmap", obj.JsonMapping);

            if (obj.Key.HasValue)
                serializedObj.Add("key", obj.Key.Value);

            if (obj.Resizable.HasValue)
                serializedObj.Add("resizable", obj.Resizable.Value);

            if (obj.SummaryType.HasValue)
            {
                if (obj.SummaryType.Value != JqGridColumnSummaryTypes.Custom)
                    serializedObj.Add("summaryType", obj.SummaryType.Value.ToString().ToLower());
                else
                    serializedObj.Add("summaryType", obj.SummaryFunction);
            }

            if (obj.SummaryTemplate?.IsSetted ?? false)
                serializedObj.Add("summaryTpl", obj.SummaryTemplate.Value);

            if (obj.Sortable.HasValue)
                serializedObj.Add("sortable", obj.Sortable.Value);

            if (obj.SortType != JqGridColumnSortTypes.Default && obj.SortType != JqGridColumnSortTypes.Function)
                serializedObj.Add("sorttype", obj.SortType.ToString().ToLower());

            if (obj.Index?.IsSetted ?? false)
                serializedObj.Add("index", obj.Index.Value);

            if (obj.Searchable.HasValue)
            {
                serializedObj.Add("search", obj.Searchable.Value);
                if (obj.Searchable.Value)
                {
                    if (obj.SearchType != JqGridColumnSearchTypes.Default)
                        serializedObj.Add("stype", obj.SearchType.ToString().ToLower());

                    if (obj.SearchOptions != null)
                        serializedObj.Add("searchoptions", obj.SearchOptions);

                    if (obj.SearchRules != null)
                        serializedObj.Add("searchrules", obj.SearchRules);
                }
            }

            serializedObj.Add("name", obj.Name);

            if (obj.Title.HasValue)
                serializedObj.Add("title", obj.Title.Value);

            if (obj.Width.HasValue)
                serializedObj.Add("width", obj.Width);

            if (obj.Viewable.HasValue)
                serializedObj.Add("viewable", obj.Viewable.Value);

            if (!string.IsNullOrWhiteSpace(obj.XmlMapping))
                serializedObj.Add("xmlmap", obj.XmlMapping);
        }

        private static JqGridColumnEditOptions DeserializeJqGridColumnEditOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            var obj = new JqGridColumnEditOptions();

            if (serializedObj.ContainsKey("custom_element"))
                obj.CustomElementFunction = serializedObj["custom_element"]?.ToString();
            serializedObj.Remove("custom_element");
            if (serializedObj.ContainsKey("custom_value"))
                obj.CustomValueFunction = serializedObj["custom_value"]?.ToString();
            serializedObj.Remove("custom_value");
            if (serializedObj.ContainsKey("dataUrl"))
                obj.DataUrl = serializedObj["dataUrl"]?.ToString();
            serializedObj.Remove("dataUrl");
            if (serializedObj.ContainsKey("defaultValue"))
                obj.DefaultValue = serializedObj["defaultValue"]?.ToString();
            serializedObj.Remove("defaultValue");

            if (serializedObj.ContainsKey("value") && serializedObj["value"] is IDictionary<string, object>)
                obj.ValueDictionary = ((IDictionary<string, object>)serializedObj["value"]).ToDictionary(k => k.Key, v => v.Value.ToString());
            else
                obj.Value = GetStringFromSerializedObj(serializedObj, "value");
            serializedObj.Remove("value");

            obj.NullIfEmpty = GetBooleanFromSerializedObj(serializedObj, "NullIfEmpty");
            serializedObj.Remove("NullIfEmpty");
            obj.HtmlAttributes = serializedObj;

            return obj;
        }

        private static void SerializeJqGridColumnEditOptions(JqGridColumnEditOptions obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            if (obj.IsCustomElementFunctionSetted)
                serializedObj.Add("custom_element", obj.CustomElementFunction);

            if (obj.IsCustomValueFunctionSetted)
                serializedObj.Add("custom_value", obj.CustomValueFunction);

            SerializeJqGridColumnElementOptions(obj, serializer, serializedObj);

            if (obj.NullIfEmpty.HasValue)
                serializedObj.Add("NullIfEmpty", obj.NullIfEmpty.Value);

            if (obj.HtmlAttributes != null)
                foreach (var htmlAttribute in obj.HtmlAttributes)
                    serializedObj.Add(htmlAttribute.Key, htmlAttribute.Value);
        }

        private static JqGridJsonReader DeserializeJqGridJsonReader(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            var obj = new JqGridJsonReader();

            obj.PageIndex = GetStringFromSerializedObj(serializedObj, "page", JqGridResponse.JsonReader.PageIndex);
            obj.RecordId = GetStringFromSerializedObj(serializedObj, "id", JqGridResponse.JsonReader.RecordId);
            obj.Records = GetStringFromSerializedObj(serializedObj, "root", JqGridResponse.JsonReader.Records);
            obj.RecordValues = GetStringFromSerializedObj(serializedObj, "cell", JqGridResponse.JsonReader.RecordValues);
            obj.RepeatItems = GetBooleanFromSerializedObj(serializedObj, "repeatitems", JqGridResponse.JsonReader.RepeatItems);
            obj.TotalPagesCount = GetStringFromSerializedObj(serializedObj, "total", JqGridResponse.JsonReader.TotalPagesCount);
            obj.TotalRecordsCount = GetStringFromSerializedObj(serializedObj, "records", JqGridResponse.JsonReader.TotalRecordsCount);
            obj.UserData = GetStringFromSerializedObj(serializedObj, "userdata", JqGridResponse.JsonReader.UserData);

            if (serializedObj.ContainsKey("subgrid") && serializedObj["subgrid"] is IDictionary<string, object>)
            {
                var serializedSubgrid = (IDictionary<string, object>)serializedObj["subgrid"];
                obj.SubgridReader.Records = GetStringFromSerializedObj(serializedSubgrid, "root", JqGridResponse.JsonReader.SubgridReader.Records);
                obj.SubgridReader.RecordValues = GetStringFromSerializedObj(serializedSubgrid, "cell", JqGridResponse.JsonReader.SubgridReader.RecordValues);
                obj.SubgridReader.RepeatItems = GetBooleanFromSerializedObj(serializedSubgrid, "repeatitems", JqGridResponse.JsonReader.SubgridReader.RepeatItems);
            }

            return obj;
        }

        private static void SerializeJqGridJsonReader(JqGridJsonReader obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            if (obj.Records != JqGridResponse.JsonReader.Records)
                serializedObj.Add("root", obj.Records);

            if (obj.PageIndex != JqGridResponse.JsonReader.PageIndex)
                serializedObj.Add("page", obj.PageIndex);

            if (obj.TotalPagesCount != JqGridResponse.JsonReader.TotalPagesCount)
                serializedObj.Add("total", obj.TotalPagesCount);

            if (obj.TotalRecordsCount != JqGridResponse.JsonReader.TotalRecordsCount)
                serializedObj.Add("records", obj.TotalRecordsCount);

            if (!obj.RepeatItems)
                serializedObj.Add("repeatitems", false);

            if (obj.RecordValues != JqGridResponse.JsonReader.RecordValues)
                serializedObj.Add("cell", obj.RecordValues);

            if (obj.RecordId != JqGridResponse.JsonReader.RecordId)
                serializedObj.Add("id", obj.RecordId);

            if (obj.UserData != JqGridResponse.JsonReader.UserData)
                serializedObj.Add("userdata", obj.UserData);

            if (!obj.SubgridReader.IsDefault())
            {
                var serializedSubgrid = new Dictionary<string, object>();

                if (obj.SubgridReader.Records != JqGridResponse.JsonReader.SubgridReader.Records)
                    serializedSubgrid.Add("root", obj.SubgridReader.Records);

                if (!obj.SubgridReader.RepeatItems)
                    serializedSubgrid.Add("repeatitems", false);

                if (obj.SubgridReader.RecordValues != JqGridResponse.JsonReader.SubgridReader.RecordValues)
                    serializedSubgrid.Add("cell", obj.SubgridReader.RecordValues);

                serializedObj.Add("subgrid", serializedSubgrid);
            }
        }

        private static JqGridParametersNames DeserializeJqGridParametersNames(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            var obj = new JqGridParametersNames();

            obj.PageIndex = GetStringFromSerializedObj(serializedObj, "page", JqGridRequest.ParameterNames.PageIndex);
            obj.RecordsCount = GetStringFromSerializedObj(serializedObj, "rows", JqGridRequest.ParameterNames.RecordsCount);
            obj.SortingName = GetStringFromSerializedObj(serializedObj, "sort", JqGridRequest.ParameterNames.SortingName);
            obj.SortingOrder = GetStringFromSerializedObj(serializedObj, "order", JqGridRequest.ParameterNames.SortingOrder);
            obj.Searching = GetStringFromSerializedObj(serializedObj, "search", JqGridRequest.ParameterNames.Searching);
            obj.Id = GetStringFromSerializedObj(serializedObj, "id", JqGridRequest.ParameterNames.Id);
            obj.Operator = GetStringFromSerializedObj(serializedObj, "oper", JqGridRequest.ParameterNames.Operator);
            obj.EditOperator = GetStringFromSerializedObj(serializedObj, "editoper", JqGridRequest.ParameterNames.EditOperator);
            obj.AddOperator = GetStringFromSerializedObj(serializedObj, "addoper", JqGridRequest.ParameterNames.AddOperator);
            obj.DeleteOperator = GetStringFromSerializedObj(serializedObj, "deloper", JqGridRequest.ParameterNames.DeleteOperator);
            obj.SubgridId = GetStringFromSerializedObj(serializedObj, "subgridid", JqGridRequest.ParameterNames.SubgridId);
            obj.PagesCount = GetStringFromSerializedObj(serializedObj, "npage", JqGridRequest.ParameterNames.PagesCount);
            obj.TotalRows = GetStringFromSerializedObj(serializedObj, "totalrows", JqGridRequest.ParameterNames.TotalRows);

            return obj;
        }

        private static void SerializeJqGridParametersNames(JqGridParametersNames obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            if (obj.PageIndex != JqGridRequest.ParameterNames.PageIndex)
                serializedObj.Add("page", obj.PageIndex);

            if (obj.RecordsCount != JqGridRequest.ParameterNames.RecordsCount)
                serializedObj.Add("rows", obj.RecordsCount);

            if (obj.SortingName != JqGridRequest.ParameterNames.SortingName)
                serializedObj.Add("sort", obj.SortingName);

            if (obj.SortingOrder != JqGridRequest.ParameterNames.SortingOrder)
                serializedObj.Add("order", obj.SortingOrder);

            if (obj.Searching != JqGridRequest.ParameterNames.Searching)
                serializedObj.Add("search", obj.Searching);

            if (obj.Id != JqGridRequest.ParameterNames.Id)
                serializedObj.Add("id", obj.Id);

            if (obj.Operator != JqGridRequest.ParameterNames.Operator)
                serializedObj.Add("oper", obj.Operator);

            if (obj.EditOperator != JqGridRequest.ParameterNames.EditOperator)
                serializedObj.Add("editoper", obj.EditOperator);

            if (obj.AddOperator != JqGridRequest.ParameterNames.AddOperator)
                serializedObj.Add("addoper", obj.AddOperator);

            if (obj.DeleteOperator != JqGridRequest.ParameterNames.DeleteOperator)
                serializedObj.Add("deloper", obj.DeleteOperator);

            if (obj.SubgridId != JqGridRequest.ParameterNames.SubgridId)
                serializedObj.Add("subgridid", obj.SubgridId);

            if (obj.PagesCount != JqGridRequest.ParameterNames.PagesCount)
                serializedObj.Add("npage", obj.PagesCount);

            if (obj.TotalRows != JqGridRequest.ParameterNames.TotalRows)
                serializedObj.Add("totalrows", obj.TotalRows);
        }

        private static JqGridGroupingView DeserializeJqGridGroupingView(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            var obj = new JqGridGroupingView();

            if (serializedObj.ContainsKey("groupField") && serializedObj["groupField"] is ArrayList)
                obj.Fields = (string[])((ArrayList)serializedObj["groupField"]).ToArray(typeof(string));

            if (serializedObj.ContainsKey("groupOrder") && serializedObj["groupOrder"] is ArrayList)
                obj.Orders = (JqGridSortingOrders[])((ArrayList)serializedObj["groupOrder"]).ToArray(typeof(JqGridSortingOrders));

            if (serializedObj.ContainsKey("groupText") && serializedObj["groupText"] is ArrayList)
                obj.Texts = (string[])((ArrayList)serializedObj["groupText"]).ToArray(typeof(string));

            if (serializedObj.ContainsKey("groupSummary") && serializedObj["groupSummary"] is ArrayList)
                obj.Summary = (bool[])((ArrayList)serializedObj["groupSummary"]).ToArray(typeof(bool));

            if (serializedObj.ContainsKey("groupColumnShow") && serializedObj["groupColumnShow"] is ArrayList)
                obj.ColumnShow = (bool[])((ArrayList)serializedObj["groupColumnShow"]).ToArray(typeof(bool));

            obj.SummaryOnHide = GetBooleanFromSerializedObj(serializedObj, "showSummaryOnHide", false);
            obj.DataSorted = GetBooleanFromSerializedObj(serializedObj, "groupDataSorted", false);
            obj.Collapse = GetBooleanFromSerializedObj(serializedObj, "groupCollapse", false);
            obj.PlusIcon = GetStringFromSerializedObj(serializedObj, "plusicon", JqGridOptionsDefaults.GroupingPlusIcon);
            obj.MinusIcon = GetStringFromSerializedObj(serializedObj, "minusicon", JqGridOptionsDefaults.GroupingMinusIcon);

            return obj;
        }

        private static void SerializeJqGridGroupingView(JqGridGroupingView obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            if (obj.Fields != null && obj.Fields.Length > 0)
                serializedObj.Add("groupField", obj.Fields);

            if (obj.Orders != null && obj.Orders.Length > 0 && obj.Orders.Contains(JqGridSortingOrders.Desc))
                serializedObj.Add("groupOrder", obj.Orders);

            if (obj.Texts != null && obj.Texts.Length > 0)
                serializedObj.Add("groupText", obj.Texts);

            if (obj.Summary != null && obj.Summary.Length > 0 && obj.Summary.Contains(true))
                serializedObj.Add("groupSummary", obj.Summary);

            if (obj.ColumnShow != null && obj.ColumnShow.Length > 0 && obj.ColumnShow.Contains(false))
                serializedObj.Add("groupColumnShow", obj.ColumnShow);

            if (obj.SummaryOnHide)
                serializedObj.Add("showSummaryOnHide", true);

            if (obj.DataSorted)
                serializedObj.Add("groupDataSorted", true);

            if (obj.Collapse)
                serializedObj.Add("groupCollapse", true);

            if (obj.PlusIcon != JqGridOptionsDefaults.GroupingPlusIcon)
                serializedObj.Add("plusicon", obj.PlusIcon);

            if (obj.MinusIcon != JqGridOptionsDefaults.GroupingMinusIcon)
                serializedObj.Add("minusicon", obj.MinusIcon);
        }

        private static JqGridColumnRules DeserializeJqGridColumnRules(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            var obj = new JqGridColumnRules();

            obj.Custom = GetBooleanFromSerializedObj(serializedObj, "custom");
            if (serializedObj.ContainsKey("custom_func"))
                obj.CustomFunction = serializedObj["custom_func"]?.ToString();
            obj.Date = GetBooleanFromSerializedObj(serializedObj, "date");
            obj.EditHidden = GetBooleanFromSerializedObj(serializedObj, "edithidden");
            obj.Email = GetBooleanFromSerializedObj(serializedObj, "email");
            obj.Integer = GetBooleanFromSerializedObj(serializedObj, "integer", false);
            obj.MaxValue = GetDoubleFromSerializedObj(serializedObj, "maxValue");
            obj.MinValue = GetDoubleFromSerializedObj(serializedObj, "minValue");
            obj.Number = GetBooleanFromSerializedObj(serializedObj, "number", false);
            obj.Required = GetBooleanFromSerializedObj(serializedObj, "required");
            obj.Time = GetBooleanFromSerializedObj(serializedObj, "time");
            obj.Url = GetBooleanFromSerializedObj(serializedObj, "url");

            return obj;
        }

        private static void SerializeJqGridColumnRules(JqGridColumnRules obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            if (obj.Custom.HasValue)
                serializedObj.Add("custom", obj.Custom.Value);

            if (obj.IsCustomFunctionSetted)
                serializedObj.Add("custom_func", obj.CustomFunction);

            if (obj.Date.HasValue)
                serializedObj.Add("date", obj.Date.Value);

            if (obj.EditHidden.HasValue)
                serializedObj.Add("edithidden", obj.EditHidden.Value);

            if (obj.Email.HasValue)
                serializedObj.Add("email", obj.Email.Value);

            if (obj.Integer)
                serializedObj.Add("integer", true);

            if (obj.MaxValue.HasValue)
                serializedObj.Add("maxValue", obj.MaxValue.Value);

            if (obj.MinValue.HasValue)
                serializedObj.Add("minValue", obj.MinValue.Value);

            if (obj.Number)
                serializedObj.Add("number", true);

            if (obj.Required.HasValue)
                serializedObj.Add("required", obj.Required.Value);

            if (obj.Time.HasValue)
                serializedObj.Add("time", obj.Time.Value);

            if (obj.Url.HasValue)
                serializedObj.Add("url", obj.Url.Value);
        }

        private static JqGridColumnFormOptions DeserializeJqGridColumnFormOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            var obj = new JqGridColumnFormOptions();

            obj.ColumnPosition = GetInt32FromSerializedObj(serializedObj, "colpos");
            if (serializedObj.ContainsKey("elmprefix"))
                obj.ElementPrefix = serializedObj["elmprefix"]?.ToString();
            if (serializedObj.ContainsKey("elmsuffix"))
                obj.ElementSuffix = serializedObj["elmsuffix"]?.ToString();
            if (serializedObj.ContainsKey("label"))
                obj.Label = serializedObj["label"]?.ToString();
            obj.RowPosition = GetInt32FromSerializedObj(serializedObj, "rowpos");

            return obj;
        }

        private static void SerializeJqGridColumnFormOptions(JqGridColumnFormOptions obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            if (obj.ColumnPosition.HasValue)
                serializedObj.Add("colpos", obj.ColumnPosition.Value);

            if (obj.IsElementPrefixSetted)
                serializedObj.Add("elmprefix", obj.ElementPrefix);

            if (obj.IsElementSuffixSetted)
                serializedObj.Add("elmsuffix", obj.ElementSuffix);

            if (obj.IsLabelSetted)
                serializedObj.Add("label", obj.Label);

            if (obj.RowPosition.HasValue)
                serializedObj.Add("rowpos", obj.RowPosition.Value);
        }

        private static JqGridColumnFormatterOptions DeserializeJqGridColumnFormatterOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            var obj = new JqGridColumnFormatterOptions();

            if (serializedObj.ContainsKey("addParam"))
                obj.AddParam = serializedObj["addParam"]?.ToString();
            if (serializedObj.ContainsKey("baseLinkUrl"))
                obj.BaseLinkUrl = serializedObj["baseLinkUrl"]?.ToString();
            obj.DecimalPlaces = GetInt32FromSerializedObj(serializedObj, "decimalPlaces");
            if (serializedObj.ContainsKey("decimalSeparator"))
                obj.DecimalSeparator = serializedObj["decimalSeparator"]?.ToString();
            if (serializedObj.ContainsKey("defaultValue"))
                obj.DefaultValue = serializedObj["defaultValue"]?.ToString();
            obj.Disabled = GetBooleanFromSerializedObj(serializedObj, "disabled");
            if (serializedObj.ContainsKey("idName"))
                obj.IdName = serializedObj["idName"]?.ToString();
            if (serializedObj.ContainsKey("srcformat"))
                obj.OutputFormat = serializedObj["srcformat"]?.ToString();
            if (serializedObj.ContainsKey("prefix"))
                obj.Prefix = serializedObj["prefix"]?.ToString();
            if (serializedObj.ContainsKey("showAction"))
                obj.ShowAction = serializedObj["showAction"]?.ToString();
            if (serializedObj.ContainsKey("newformat"))
                obj.SourceFormat = serializedObj["newformat"]?.ToString();
            if (serializedObj.ContainsKey("suffix"))
                obj.Suffix = serializedObj["suffix"]?.ToString();
            if (serializedObj.ContainsKey("target"))
                obj.Target = serializedObj["target"]?.ToString();
            if (serializedObj.ContainsKey("thousandsSeparator"))
                obj.ThousandsSeparator = serializedObj["thousandsSeparator"]?.ToString();
            return obj;
        }

        private static void SerializeJqGridColumnFormatterOptions(JqGridColumnFormatterOptions obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            if (obj.IsAddParamSetted)
                serializedObj.Add("addParam", obj.AddParam);
            if (obj.IsBaseLinkUrlSetted)
                serializedObj.Add("baseLinkUrl", obj.BaseLinkUrl);
            if (obj.DecimalPlaces.HasValue)
                serializedObj.Add("decimalPlaces", obj.DecimalPlaces.Value);
            if (obj.IsDecimalSeparatorSetted)
                serializedObj.Add("decimalSeparator", obj.DecimalSeparator);
            if (obj.IsDefaultValueSetted)
                serializedObj.Add("defaultValue", obj.DefaultValue);
            if (obj.Disabled.HasValue)
                serializedObj.Add("disabled", obj.Disabled.Value);
            if (obj.IsIdNameSetted)
                serializedObj.Add("idName", obj.IdName);
            if (obj.IsOutputFormatSetted)
                serializedObj.Add("srcformat", obj.OutputFormat);
            if (obj.IsPrefixSetted)
                serializedObj.Add("prefix", obj.Prefix);
            if (obj.IsShowActionSetted)
                serializedObj.Add("showAction", obj.ShowAction);
            if (obj.IsSourceFormatSetted)
                serializedObj.Add("newformat", obj.SourceFormat);
            if (obj.IsSuffixSetted)
                serializedObj.Add("suffix", obj.Suffix);
            if (obj.IsTargetSetted)
                serializedObj.Add("target", obj.Target);
            if (obj.IsThousandsSeparatorSetted)
                serializedObj.Add("thousandsSeparator", obj.ThousandsSeparator);
        }

        private static void SerializeJqGridColumnElementOptions(JqGridColumnElementOptions obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            if (obj.IsDataUrlSetted)
                serializedObj.Add("dataUrl", obj.DataUrl);

            if (obj.IsDefaultValueSetted)
                serializedObj.Add("defaultValue", obj.DefaultValue);

            if (obj.IsValueSetted)
                serializedObj.Add("value", obj.Value);
            else if (obj.ValueDictionary != null)
                serializedObj.Add("value", obj.ValueDictionary);
        }

        private static JqGridColumnSearchOptions DeserializeJqGridColumnSearchOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            var obj = new JqGridColumnSearchOptions();

            obj.ClearSearch = GetBooleanFromSerializedObj(serializedObj, "clearSearch");
            if (serializedObj.ContainsKey("dataUrl"))
                obj.DataUrl = serializedObj["dataUrl"]?.ToString();
            if (serializedObj.ContainsKey("defaultValue"))
                obj.DefaultValue = serializedObj["defaultValue"]?.ToString();

            if (serializedObj.ContainsKey("value") && serializedObj["value"] is IDictionary<string, object>)
                obj.ValueDictionary = ((IDictionary<string, object>)serializedObj["value"]).ToDictionary(k => k.Key, v => v.Value.ToString());
            else
                obj.Value = GetStringFromSerializedObj(serializedObj, "value");

            if (serializedObj.ContainsKey("attr") && serializedObj["attr"] is IDictionary<string, object>)
                obj.HtmlAttributes = (IDictionary<string, object>)serializedObj["attr"];

            obj.SearchHidden = GetBooleanFromSerializedObj(serializedObj, "searchhidden");

            if (serializedObj.ContainsKey("sopt") && serializedObj["sopt"] is ArrayList)
            {
                JqGridSearchOperators? searchOperators = null;
                foreach (var innerSerializedObj in (ArrayList)serializedObj["sopt"])
                {
                    if (Enum.TryParse(innerSerializedObj.ToString(), true, out JqGridSearchOperators searchOperator))
                    {
                        if (searchOperators.HasValue)
                            searchOperators = searchOperators | searchOperator;
                        else
                            searchOperators = searchOperator;
                    }
                }

                if (searchOperators.HasValue)
                    obj.SearchOperators = searchOperators.Value;
            }

            return obj;
        }

        private static void SerializeJqGridColumnSearchOptions(JqGridColumnSearchOptions obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            SerializeJqGridColumnElementOptions(obj, serializer, serializedObj);

            if (obj.HtmlAttributes != null && obj.HtmlAttributes.Count > 0)
                serializedObj.Add("attr", obj.HtmlAttributes);

            if (obj.ClearSearch.HasValue)
                serializedObj.Add("clearSearch", obj.ClearSearch);

            if (obj.SearchHidden.HasValue)
                serializedObj.Add("searchhidden", obj.SearchHidden);

            if (obj.SearchOperators != JqGridSearchOperators.Default)
            {
                var searchOperators = new List<string>();
                foreach (JqGridSearchOperators searchOperator in Enum.GetValues(typeof(JqGridSearchOperators)))
                {
                    if (searchOperator != JqGridSearchOperators.EqualOrNotEqual && searchOperator != JqGridSearchOperators.NoTextOperators && searchOperator != JqGridSearchOperators.TextOperators && (obj.SearchOperators & searchOperator) == searchOperator)
                        searchOperators.Add(Enum.GetName(typeof(JqGridSearchOperators), searchOperator).ToLower());
                }
                serializedObj.Add("sopt", searchOperators);
            }
        }

        private static JqGridSubgridModel DeserializeJqGridSubgridModel(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            var obj = new JqGridSubgridModel();

            if (serializedObj.ContainsKey("name") && serializedObj["name"] is ArrayList)
            {
                foreach (var innerSerializedObj in (ArrayList)serializedObj["name"])
                    obj.ColumnsNames.Add(innerSerializedObj.ToString());
            }

            if (serializedObj.ContainsKey("align") && serializedObj["align"] is ArrayList)
            {
                foreach (var innerSerializedObj in (ArrayList)serializedObj["align"])
                {
                    Enum.TryParse(innerSerializedObj.ToString(), true, out JqGridAlignments alignment);
                    obj.ColumnsAlignments.Add(alignment);
                }
            }

            if (serializedObj.ContainsKey("width") && serializedObj["width"] is ArrayList)
            {
                foreach (var innerSerializedObj in (ArrayList)serializedObj["width"])
                {
                    if (innerSerializedObj is int i)
                        obj.ColumnsWidths.Add(i);
                }
            }

            return obj;
        }

        private static void SerializeJqGridSubgridModel(JqGridSubgridModel obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("name", obj.ColumnsNames);

            var alignments = new List<string>();
            foreach (var alignment in obj.ColumnsAlignments)
                alignments.Add(alignment.ToString().ToLower());
            serializedObj.Add("align", alignments);

            serializedObj.Add("width", obj.ColumnsWidths);
        }

        private static void SerializeJqGridResponse(JqGridResponse obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            var jsonReader = obj.Reader ?? JqGridResponse.JsonReader;

            if (!obj.IsSubgridResponse)
            {
                serializedObj.Add(jsonReader.PageIndex, obj.PageIndex + 1);
                serializedObj.Add(jsonReader.TotalRecordsCount, obj.TotalRecordsCount);
                serializedObj.Add(jsonReader.TotalPagesCount, obj.TotalPagesCount);

                if (obj.UserData != null)
                    serializedObj.Add(jsonReader.UserData, obj.UserData);
            }

            serializedObj.Add(obj.IsSubgridResponse ? jsonReader.SubgridReader.Records : jsonReader.Records, SerializeJqGridRecords(obj.Records, obj.IsSubgridResponse, jsonReader));
        }

        private static List<object> SerializeJqGridRecords(List<JqGridRecord> objs, bool isSubgridResponse, JqGridJsonReader jsonReader)
        {
            var isRecordIndexInt = int.TryParse(jsonReader.RecordId, out var recordIdIndex);
            var isRecordValuesEmpty = string.IsNullOrWhiteSpace(jsonReader.RecordValues);
            var repeatItems = isSubgridResponse ? jsonReader.SubgridReader.RepeatItems : jsonReader.RepeatItems;

            if (!isSubgridResponse)
            {
                if (repeatItems && isRecordValuesEmpty && !isRecordIndexInt)
                    throw new InvalidOperationException("JqGridJsonReader.RecordId must be a number when JqGridJsonReader.RepeatItems is set to true and JqGridJsonReader.RecordValues is set to empty string.");

                if (repeatItems && !isRecordValuesEmpty && isRecordIndexInt)
                    throw new InvalidOperationException("JqGridJsonReader.RecordValues can't be an empty string when JqGridJsonReader.RepeatItems is set to true and JqGridJsonReader.RecordId is a number.");
            }

            var serializedObjs = new List<object>();

            foreach (var obj in objs)
            {
                var adjacencyTreeRecord = obj as JqGridAdjacencyTreeRecord;
                var nestedSetTreeRecord = obj as JqGridNestedSetTreeRecord;
                if (repeatItems)
                {
                    var values = obj.Values;

                    if (!isSubgridResponse)
                    {
                        if (adjacencyTreeRecord != null)
                        {
                            values.Add(adjacencyTreeRecord.Level);
                            values.Add(adjacencyTreeRecord.ParentId);
                            values.Add(adjacencyTreeRecord.Leaf);
                            values.Add(adjacencyTreeRecord.Expanded);
                        }
                        else if (nestedSetTreeRecord != null)
                        {
                            values.Add(nestedSetTreeRecord.Level);
                            values.Add(nestedSetTreeRecord.LeftField);
                            values.Add(nestedSetTreeRecord.RightField);
                            values.Add(nestedSetTreeRecord.Leaf);
                            values.Add(nestedSetTreeRecord.Expanded);
                        }
                    }

                    if (isRecordValuesEmpty)
                    {
                        if (!isSubgridResponse && Convert.ToString(values[recordIdIndex]) != obj.Id)
                            values.Insert(recordIdIndex, obj.Id);
                        serializedObjs.Add(values);
                    }
                    else
                    {
                        var serializedObj = new Dictionary<string, object>();
                        serializedObj.Add(isSubgridResponse ? jsonReader.SubgridReader.RecordValues : jsonReader.RecordValues, values);
                        if (!isSubgridResponse)
                            serializedObj.Add(jsonReader.RecordId, obj.Id);
                        serializedObjs.Add(serializedObj);
                    }
                }
                else
                {
                    if (obj.Value == null)
                        throw new InvalidOperationException("JqGridRecord.Value can't be null when JqGridJsonReader.RepeatItems is set to false.");

                    var serializedObj = obj.GetValuesAsDictionary();

                    if (!isSubgridResponse)
                    {
                        if (adjacencyTreeRecord != null)
                        {
                            serializedObj.Add("level", adjacencyTreeRecord.Level);
                            serializedObj.Add("parent", adjacencyTreeRecord.ParentId);
                            serializedObj.Add("isLeaf", adjacencyTreeRecord.Leaf);
                            serializedObj.Add("expanded", adjacencyTreeRecord.Expanded);
                        }
                        else if (nestedSetTreeRecord != null)
                        {
                            serializedObj.Add("level", nestedSetTreeRecord.Level);
                            serializedObj.Add("lft", nestedSetTreeRecord.LeftField);
                            serializedObj.Add("rgt", nestedSetTreeRecord.RightField);
                            serializedObj.Add("isLeaf", nestedSetTreeRecord.Leaf);
                            serializedObj.Add("expanded", nestedSetTreeRecord.Expanded);
                        }
                    }

                    if (!isSubgridResponse && !isRecordIndexInt && !serializedObj.ContainsKey(jsonReader.RecordId))
                        serializedObj.Add(jsonReader.RecordId, obj.Id);

                    serializedObjs.Add(serializedObj);
                }
            }

            return serializedObjs;
        }

        private static JqGridRequestSearchingFilters DeserializeJqGridRequestSearchingFilters(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            var obj = new JqGridRequestSearchingFilters();

            obj.GroupingOperator = GetEnumFromSerializedObj<JqGridSearchGroupingOperators>(serializedObj, "groupOp", JqGridSearchGroupingOperators.And);
            if (serializedObj.ContainsKey("rules") && serializedObj["rules"] is ArrayList)
            {
                foreach (var innerSerializedObj in (ArrayList)serializedObj["rules"])
                {
                    if (innerSerializedObj is IDictionary<string, object>)
                    {
                        var searchingFilter = DeserializeJqGridRequestSearchingFilter((IDictionary<string, object>)innerSerializedObj, serializer);
                        obj.Filters.Add(searchingFilter);
                    }
                }
            }
            if (serializedObj.ContainsKey("groups") && serializedObj["groups"] is ArrayList)
            {
                foreach (var innerSerializedObj in (ArrayList)serializedObj["groups"])
                {
                    if (innerSerializedObj is IDictionary<string, object>)
                    {
                        var searchingFilters = DeserializeJqGridRequestSearchingFilters((IDictionary<string, object>)innerSerializedObj, serializer);
                        obj.Groups.Add(searchingFilters);
                    }
                }
            }

            return obj;
        }

        private static void SerializeJqGridRequestSearchingFilters(JqGridRequestSearchingFilters obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("groupOp", obj.GroupingOperator.ToString().ToUpper());

            if (obj.Filters != null && obj.Filters.Count > 0)
                serializedObj.Add("rules", obj.Filters);

            if (obj.Groups != null && obj.Groups.Count > 0)
                serializedObj.Add("groups", obj.Groups);
        }

        private static JqGridRequestSearchingFilter DeserializeJqGridRequestSearchingFilter(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            var obj = new JqGridRequestSearchingFilter();

            obj.SearchingName = GetStringFromSerializedObj(serializedObj, "field");
            obj.SearchingOperator = GetEnumFromSerializedObj(serializedObj, "op", JqGridSearchOperators.Eq);
            obj.SearchingValue = GetStringFromSerializedObj(serializedObj, "data");
            return obj;
        }

        private static void SerializeJqGridRequestSearchingFilter(JqGridRequestSearchingFilter obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("field", obj.SearchingName);
            serializedObj.Add("op", obj.SearchingOperator.ToString().ToLower());
            serializedObj.Add("data", obj.SearchingValue);
        }

        private static bool? GetBooleanFromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && serializedObj[key] is bool)
                return (bool)serializedObj[key];
            return null;
        }

        private static bool GetBooleanFromSerializedObj(IDictionary<string, object> serializedObj, string key, bool defaultValue)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && serializedObj[key] is bool)
                return (bool)serializedObj[key];
            return defaultValue;
        }

        private static double? GetDoubleFromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && double.TryParse(serializedObj[key].ToString(), out var value))
                return value;
            return null;
        }

        private static TEnum? GetEnumFromSerializedObj<TEnum>(IDictionary<string, object> serializedObj, string key) where TEnum : struct
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && Enum.TryParse<TEnum>(serializedObj[key].ToString(), true, out var value))
                return value;
            return null;
        }

        private static TEnum GetEnumFromSerializedObj<TEnum>(IDictionary<string, object> serializedObj, string key, TEnum defaultValue) where TEnum : struct
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && Enum.TryParse(serializedObj[key].ToString(), true, out TEnum value))
                return value;
            return defaultValue;
        }

        private static int? GetInt32FromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && serializedObj[key] is int)
                return (int)serializedObj[key];
            return null;
        }

        private static int GetInt32FromSerializedObj(IDictionary<string, object> serializedObj, string key, int defaultValue)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && serializedObj[key] is int)
                return (int)serializedObj[key];
            return defaultValue;
        }

        private static List<int> GetInt32ArrayFromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            List<int> array = null;

            if (serializedObj.ContainsKey(key) && serializedObj[key] is ArrayList)
            {
                array = new List<int>();
                var serializedArray = (ArrayList)serializedObj[key];
                foreach (var serializedItem in serializedArray)
                {
                    if (serializedItem is int i)
                        array.Add(i);
                }
            }

            return array;
        }

        private static string GetStringFromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null)
                return serializedObj[key].ToString();
            return null;
        }

        private static string GetStringFromSerializedObj(IDictionary<string, object> serializedObj, string key, string defaultValue)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null)
                return serializedObj[key].ToString();
            return defaultValue;
        }

        private static SettedString GetSettedStringFromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null)
                return new SettedString(true, serializedObj[key].ToString());
            return new SettedString(false, null);
        }
        #endregion
    }
}
