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
            typeof(JqGridParametersNames),
            typeof(JqGridGroupingView),
            typeof(JqGridColumnEditOptions),
            typeof(JqGridColumnRules),
            typeof(JqGridColumnFormOptions),
            typeof(JqGridColumnFormatterOptions),
            typeof(JqGridColumnSearchOptions),
            typeof(JqGridSubgridModel),
            typeof(JqGridResponse),
            typeof(JqGridRecord),
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
            Dictionary<string, object> serializedObj = new Dictionary<string, object>();

            if (obj != null)
            {
                if (obj is JqGridOptions)
                    SerializeJqGridOptions((JqGridOptions)obj, serializer, ref serializedObj);
                else if (obj is JqGridColumnModel)
                    SerializeJqGridColumnModel((JqGridColumnModel)obj, serializer, ref serializedObj);
                else if (obj is JqGridParametersNames)
                    SerializeJqGridParametersNames((JqGridParametersNames)obj, serializer, ref serializedObj);
                else if (obj is JqGridGroupingView)
                    SerializeJqGridGroupingView((JqGridGroupingView)obj, serializer, ref serializedObj);
                else if (obj is JqGridColumnEditOptions)
                    SerializeJqGridColumnEditOptions((JqGridColumnEditOptions)obj, serializer, ref serializedObj);
                else if (obj is JqGridColumnRules)
                    SerializeJqGridColumnRules((JqGridColumnRules)obj, serializer, ref serializedObj);
                else if (obj is JqGridColumnFormatterOptions)
                    SerializeJqGridColumnFormatterOptions((JqGridColumnFormatterOptions)obj, serializer, ref serializedObj);
                else if (obj is JqGridColumnFormOptions)
                    SerializeJqGridColumnFormOptions((JqGridColumnFormOptions)obj, serializer, ref serializedObj);
                else if (obj is JqGridColumnSearchOptions)
                    SerializeJqGridColumnSearchOptions((JqGridColumnSearchOptions)obj, serializer, ref serializedObj);
                else if (obj is JqGridSubgridModel)
                    SerializeJqGridSubgridModel((JqGridSubgridModel)obj, serializer, ref serializedObj);
                else if (obj is JqGridResponse)
                    SerializeJqGridResponse((JqGridResponse)obj, serializer, ref serializedObj);
                else if (obj is JqGridRecord)
                    SerializeJqGridRecord((JqGridRecord)obj, serializer, ref serializedObj);
            }

            return serializedObj;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return _supportedTypes; }
        }
        #endregion

        #region Methods
        private static JqGridOptions DeserializeJqGridOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            string id = GetStringFromSerializedObj(serializedObj, "id");
            if (!String.IsNullOrWhiteSpace(id))
            {
                JqGridOptions obj = new JqGridOptions(id);
                obj.AutoWidth = GetBooleanFromSerializedObj(serializedObj, "autowidth", false);
                obj.CellEditingEnabled = GetBooleanFromSerializedObj(serializedObj, "cellEdit", false);
                obj.CellEditingSubmitMode = GetEnumFromSerializedObj<JqGridCellEditingSubmitModes>(serializedObj, "cellsubmit", JqGridCellEditingSubmitModes.Remote);
                obj.CellEditingUrl = GetStringFromSerializedObj(serializedObj, "cellurl");

                if (serializedObj.ContainsKey("colModel") && serializedObj["colModel"] is ArrayList)
                {
                    foreach (object innerSerializedObj in (ArrayList)serializedObj["colModel"])
                    {
                        if (innerSerializedObj is IDictionary<string, object>)
                        {
                            JqGridColumnModel columnModel = DeserializeJqGridColumnModel((IDictionary<string, object>)innerSerializedObj, serializer);
                            if (columnModel != null)
                                obj.ColumnsModels.Add(columnModel);
                        }
                    }
                }

                if (serializedObj.ContainsKey("colNames") && serializedObj["colNames"] is ArrayList)
                {
                    foreach (object innerSerializedObj in (ArrayList)serializedObj["colNames"])
                        obj.ColumnsNames.Add(innerSerializedObj.ToString());
                }

                obj.Caption = GetStringFromSerializedObj(serializedObj, "caption");
                obj.DataString = GetStringFromSerializedObj(serializedObj, "datastr");
                obj.DataType = GetEnumFromSerializedObj<JqGridDataTypes>(serializedObj, "datatype", JqGridDataTypes.Xml);

                obj.DynamicScrollingMode = JqGridDynamicScrollingModes.Disabled;
                if (serializedObj.ContainsKey("scroll") && serializedObj["scroll"] != null)
                {
                    if (serializedObj["scroll"] is Boolean && (Boolean)serializedObj["scroll"])
                        obj.DynamicScrollingMode = JqGridDynamicScrollingModes.HoldAllRows;
                    else if (serializedObj["scroll"] is Int32)
                        obj.DynamicScrollingMode = JqGridDynamicScrollingModes.HoldVisibleRows;
                }

                obj.DynamicScrollingTimeout = GetInt32FromSerializedObj(serializedObj, "scrollTimeout", 200);
                obj.EditingUrl = GetStringFromSerializedObj(serializedObj, "editurl");
                obj.ExpandColumnClick = GetBooleanFromSerializedObj(serializedObj, "ExpandColClick", true);
                obj.ExpandColumn = GetStringFromSerializedObj(serializedObj, "ExpandColumn");
                obj.FooterEnabled = GetBooleanFromSerializedObj(serializedObj, "footerrow", false);
                obj.UserDataOnFooter = GetBooleanFromSerializedObj(serializedObj, "userDataOnFooter", false);

                obj.GroupingEnabled = GetBooleanFromSerializedObj(serializedObj, "grouping", false);
                if (serializedObj.ContainsKey("groupingView") && serializedObj["groupingView"] is IDictionary<string, object>)
                    obj.GroupingView = DeserializeJqGridGroupingView((IDictionary<string, object>)serializedObj["groupingView"], serializer);

                obj.Height = GetInt32FromSerializedObj(serializedObj, "height");
                obj.Hidden = GetBooleanFromSerializedObj(serializedObj, "hiddengrid", false);
                obj.HiddenEnabled  = GetBooleanFromSerializedObj(serializedObj, "hidegrid", true);
                obj.MethodType = GetEnumFromSerializedObj<JqGridMethodTypes>(serializedObj, "mtype", JqGridMethodTypes.Get);
                
                if (serializedObj.ContainsKey("pager") && serializedObj["pager"] != null)
                    obj.Pager = true;

                if (serializedObj.ContainsKey("prmNames") && serializedObj["prmNames"] is IDictionary<string, object>)
                    obj.ParametersNames = DeserializeJqGridParametersNames((IDictionary<string, object>)serializedObj["prmNames"], serializer);

                if (serializedObj.ContainsKey("remapColumns") && serializedObj["remapColumns"] is ArrayList)
                {
                    obj.ColumnsRemaping = new List<int>();
                    foreach (object innerSerializedObj in (ArrayList)serializedObj["remapColumns"])
                    {
                        if (innerSerializedObj is Int32)
                            obj.ColumnsRemaping.Add((int)innerSerializedObj);
                    }
                }

                obj.RowsNumber = GetInt32FromSerializedObj(serializedObj, "rowNum", 20);
                obj.ScrollOffset = GetInt32FromSerializedObj(serializedObj, "scrollOffset", 18);
                obj.SortingName = GetStringFromSerializedObj(serializedObj, "sortname");
                obj.SortingOrder = GetEnumFromSerializedObj<JqGridSortingOrders>(serializedObj, "sortorder", JqGridSortingOrders.Asc);
                obj.SubgridEnabled = GetBooleanFromSerializedObj(serializedObj, "subGrid", false);

                if (serializedObj.ContainsKey("subGridModel") && serializedObj["subGridModel"] != null && serializedObj["subGridModel"] is IDictionary<string, object>)
                    obj.SubgridModel = DeserializeJqGridSubgridModel((IDictionary<string, object>)serializedObj["subGridModel"], serializer);

                obj.SubgridUrl = GetStringFromSerializedObj(serializedObj, "subGridUrl");
                obj.SubgridColumnWidth = GetInt32FromSerializedObj(serializedObj, "subGridWidth", 20);
                obj.TreeGridEnabled = GetBooleanFromSerializedObj(serializedObj, "treeGrid", false);
                obj.TreeGridModel = GetEnumFromSerializedObj<JqGridTreeGridModels>(serializedObj, "treeGridModel", JqGridTreeGridModels.Nested);
                obj.Url = GetStringFromSerializedObj(serializedObj, "url");
                obj.ViewRecords = GetBooleanFromSerializedObj(serializedObj, "viewrecords", false);
                obj.Width = GetInt32FromSerializedObj(serializedObj, "width");

                return obj;
            }
            else
                return null;
        }

        private static void SerializeJqGridOptions(JqGridOptions obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("id", obj.Id);

            if (obj.AutoWidth)
                serializedObj.Add("autowidth", true);

            if (obj.CellEditingEnabled)
            {
                serializedObj.Add("cellEdit", true);
                if (obj.CellEditingSubmitMode != JqGridCellEditingSubmitModes.Remote)
                    serializedObj.Add("cellsubmit", "clientArray");

                if (!String.IsNullOrWhiteSpace(obj.CellEditingUrl))
                    serializedObj.Add("cellurl", obj.CellEditingUrl);
            }

            serializedObj.Add("colModel", obj.ColumnsModels);
            serializedObj.Add("colNames", obj.ColumnsNames);

            if (!String.IsNullOrEmpty(obj.Caption))
            {
                serializedObj.Add("caption", obj.Caption);

                if (!obj.HiddenEnabled)
                    serializedObj.Add("hidegrid", false);
                else if (obj.Hidden)
                    serializedObj.Add("hiddengrid", true);
            }

            if (obj.UseDataString())
                serializedObj.Add("datastr", obj.DataString);
            else
                serializedObj.Add("url", obj.Url);

            if (obj.DataType != JqGridDataTypes.Xml)
                serializedObj.Add("datatype", obj.DataType.ToString().ToLower());

            if (obj.DynamicScrollingMode == JqGridDynamicScrollingModes.HoldAllRows)
                serializedObj.Add("scroll", true);
            else if (obj.DynamicScrollingMode == JqGridDynamicScrollingModes.HoldVisibleRows)
            {
                serializedObj.Add("scroll", 10);
                if (obj.DynamicScrollingTimeout != 200)
                    serializedObj.Add("scrollTimeout", obj.DynamicScrollingTimeout);
            }

            if (!String.IsNullOrWhiteSpace(obj.EditingUrl))
                serializedObj.Add("editurl", obj.EditingUrl);

            if (obj.FooterEnabled)
            {
                serializedObj.Add("footerrow", true);
                if (obj.UserDataOnFooter)
                    serializedObj.Add("userDataOnFooter", true);
            }

            if (obj.GroupingEnabled)
            {
                serializedObj.Add("grouping", true);

                if (obj.GroupingView != null)
                    serializedObj.Add("groupingView", obj.GroupingView);
            }

            if (obj.Height.HasValue)
                serializedObj.Add("height", obj.Height.Value);
            else
                serializedObj.Add("height", "100%");

            if (obj.MethodType != JqGridMethodTypes.Get)
                serializedObj.Add("mtype", "POST");

            if (obj.Pager)
                serializedObj.Add("pager", String.Format("#{0}Pager", obj.Id));

            if (obj.ParametersNames != null)
                serializedObj.Add("prmNames", obj.ParametersNames);

            serializedObj.Add("remapColumns", obj.ColumnsRemaping);

            if (obj.RowsNumber != 20)
                serializedObj.Add("rowNum", obj.RowsNumber);

            if (obj.ScrollOffset != 18)
                serializedObj.Add("scrollOffset", obj.ScrollOffset);

            if (!String.IsNullOrWhiteSpace(obj.SortingName))
                serializedObj.Add("sortname", obj.SortingName);

            if (obj.SortingOrder != JqGridSortingOrders.Asc)
                serializedObj.Add("sortorder", "desc");

            if (obj.SubgridEnabled)
            {
                serializedObj.Add("subGrid", true);
                if (obj.SubgridModel != null)
                    serializedObj.Add("subGridModel", obj.SubgridModel);

                if (!String.IsNullOrWhiteSpace(obj.SubgridUrl))
                    serializedObj.Add("subGridUrl", obj.SubgridUrl);

                if (obj.SubgridColumnWidth != 20)
                    serializedObj.Add("subGridWidth", obj.SubgridColumnWidth);
            }

            if (obj.TreeGridEnabled)
            {
                serializedObj.Add("treeGrid", true);

                if (obj.TreeGridModel != JqGridTreeGridModels.Nested)
                    serializedObj.Add("treeGridModel", "adjacency");

                if (!obj.ExpandColumnClick)
                    serializedObj.Add("ExpandColClick", false);

                if (!String.IsNullOrWhiteSpace(obj.ExpandColumn))
                    serializedObj.Add("ExpandColumn", obj.ExpandColumn);
            }

            if (obj.ViewRecords)
                serializedObj.Add("viewrecords", true);

            if (obj.Width.HasValue)
                serializedObj.Add("width", obj.Width.Value);
        }

        private static JqGridColumnModel DeserializeJqGridColumnModel(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            string name = GetStringFromSerializedObj(serializedObj, "name");
            if (!String.IsNullOrWhiteSpace(name))
            {
                JqGridColumnModel obj = new JqGridColumnModel(name);

                obj.Alignment = GetEnumFromSerializedObj<JqGridAlignments>(serializedObj, "align", obj.Alignment);
                obj.Classes = GetStringFromSerializedObj(serializedObj, "classes");
                obj.Editable = GetBooleanFromSerializedObj(serializedObj, "editable", false);
                obj.EditType = GetEnumFromSerializedObj<JqGridColumnEditTypes>(serializedObj, "edittype", JqGridColumnEditTypes.Text);

                if (serializedObj.ContainsKey("editoptions") && serializedObj["editoptions"] != null && serializedObj["editoptions"] is IDictionary<string, object>)
                    obj.EditOptions = DeserializeJqGridColumnEditOptions((IDictionary<string, object>)serializedObj["editoptions"], serializer);

                if (serializedObj.ContainsKey("editrules") && serializedObj["editrules"] != null && serializedObj["editrules"] is IDictionary<string, object>)
                    obj.EditRules = DeserializeJqGridColumnRules((IDictionary<string, object>)serializedObj["editrules"], serializer);

                obj.Fixed = GetBooleanFromSerializedObj(serializedObj, "fixed", false);
                obj.Hidden = GetBooleanFromSerializedObj(serializedObj, "hidden", false);

                if (serializedObj.ContainsKey("formatoptions") && serializedObj["formatoptions"] != null && serializedObj["formatoptions"] is IDictionary<string, object>)
                    obj.FormatterOptions = DeserializeJqGridColumnFormatterOptions((IDictionary<string, object>)serializedObj["formatoptions"], serializer);

                if (serializedObj.ContainsKey("formoptions") && serializedObj["formoptions"] != null && serializedObj["formoptions"] is IDictionary<string, object>)
                    obj.FormOptions = DeserializeJqGridColumnFormOptions((IDictionary<string, object>)serializedObj["formoptions"], serializer);

                obj.InitialSortingOrder = GetEnumFromSerializedObj<JqGridSortingOrders>(serializedObj, "firstsortorder", JqGridSortingOrders.Asc);
                obj.Formatter = GetStringFromSerializedObj(serializedObj, "formatter");
                obj.Resizable = GetBooleanFromSerializedObj(serializedObj, "resizable", true);
                obj.SummaryType = GetEnumFromSerializedObj<JqGridColumnSummaryTypes>(serializedObj, "summaryType");
                if (!obj.SummaryType.HasValue)
                {
                    obj.SummaryFunction = GetStringFromSerializedObj(serializedObj, "summaryType");
                    if (!String.IsNullOrWhiteSpace(obj.SummaryFunction))
                        obj.SummaryType = JqGridColumnSummaryTypes.Custom;
                }

                obj.SummaryTemplate = GetStringFromSerializedObj(serializedObj, "summaryTpl", "{0}");
                obj.Sortable = GetBooleanFromSerializedObj(serializedObj, "sortable", true);
                obj.Index = GetStringFromSerializedObj(serializedObj, "index");
                obj.Searchable = GetBooleanFromSerializedObj(serializedObj, "search", true);
                obj.SearchType = GetEnumFromSerializedObj<JqGridColumnSearchTypes>(serializedObj, "stype", JqGridColumnSearchTypes.Text);

                if (serializedObj.ContainsKey("searchoptions") && serializedObj["searchoptions"] != null && serializedObj["searchoptions"] is IDictionary<string, object>)
                    obj.SearchOptions = DeserializeJqGridColumnSearchOptions((IDictionary<string, object>)serializedObj["searchoptions"], serializer);

                if (serializedObj.ContainsKey("searchrules") && serializedObj["searchrules"] != null && serializedObj["searchrules"] is IDictionary<string, object>)
                    obj.SearchRules = DeserializeJqGridColumnRules((IDictionary<string, object>)serializedObj["searchrules"], serializer);

                obj.UnFormatter = GetStringFromSerializedObj(serializedObj, "unformat");
                obj.Width = GetInt32FromSerializedObj(serializedObj, "width", 150);

                return obj;
            }
            else
                return null;
        }

        private static void SerializeJqGridColumnModel(JqGridColumnModel obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("align", obj.Alignment.ToString().ToLower());

            if (!String.IsNullOrWhiteSpace(obj.Classes))
                serializedObj.Add("classes", obj.Classes);

            if (obj.Editable)
            {
                serializedObj.Add("editable", true);
                if (obj.EditType != JqGridColumnEditTypes.Text)
                    serializedObj.Add("edittype", obj.EditType.ToString().ToLower());

                if (obj.EditOptions != null)
                    serializedObj.Add("editoptions", obj.EditOptions);

                if (obj.EditRules != null)
                    serializedObj.Add("editrules", obj.EditRules);

                if (obj.FormOptions != null)
                    serializedObj.Add("formoptions", obj.FormOptions);
            }

            if (obj.Fixed)
                serializedObj.Add("fixed", true);

            if (obj.FormatterOptions != null)
                serializedObj.Add("formatoptions", obj.FormatterOptions);

            if (obj.InitialSortingOrder != JqGridSortingOrders.Asc)
                serializedObj.Add("firstsortorder", "desc");

            if (!String.IsNullOrWhiteSpace(obj.Formatter))
                serializedObj.Add("formatter", obj.Formatter);

            serializedObj.Add("hidden", obj.Hidden);

            if (!obj.Resizable)
                serializedObj.Add("resizable", false);

            if (obj.SummaryType.HasValue)
            {
                if (obj.SummaryType.Value != JqGridColumnSummaryTypes.Custom)
                    serializedObj.Add("summaryType", obj.SummaryType.Value.ToString().ToLower());
                else
                    serializedObj.Add("summaryType", obj.SummaryFunction);
            }

            if (obj.SummaryTemplate != "{0}")
                serializedObj.Add("summaryTpl", obj.SummaryTemplate);

            if (!obj.Sortable)
                serializedObj.Add("sortable", false);

            serializedObj.Add("index", obj.Index);

            if (obj.Searchable)
            {
                if (obj.SearchType != JqGridColumnSearchTypes.Text)
                    serializedObj.Add("stype", obj.SearchType.ToString().ToLower());
                    
                if (obj.SearchOptions != null)
                    serializedObj.Add("searchoptions", obj.SearchOptions);

                if (obj.SearchRules != null)
                    serializedObj.Add("searchrules", obj.SearchRules);
            }
            else
                serializedObj.Add("search", false);

            serializedObj.Add("name", obj.Name);

            if (!String.IsNullOrWhiteSpace(obj.UnFormatter))
                serializedObj.Add("unformat", obj.UnFormatter);

            if (obj.Width != 150)
                serializedObj.Add("width", obj.Width);
        }

        private static JqGridColumnEditOptions DeserializeJqGridColumnEditOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridColumnEditOptions obj = new JqGridColumnEditOptions();

            obj.CustomElementFunction = GetStringFromSerializedObj(serializedObj, "custom_element");
            serializedObj.Remove("custom_element");
            obj.CustomValueFunction = GetStringFromSerializedObj(serializedObj, "custom_value");
            serializedObj.Remove("custom_value");
            obj.DataUrl = GetStringFromSerializedObj(serializedObj, "dataUrl");
            serializedObj.Remove("dataUrl");
            obj.DefaultValue = GetStringFromSerializedObj(serializedObj, "defaultValue");
            serializedObj.Remove("defaultValue");
            obj.NullIfEmpty = GetBooleanFromSerializedObj(serializedObj, "NullIfEmpty", false);
            serializedObj.Remove("NullIfEmpty");
            obj.HtmlAttributes = serializedObj;

            return obj;
        }

        private static void SerializeJqGridColumnEditOptions(JqGridColumnEditOptions obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            if (!String.IsNullOrWhiteSpace(obj.CustomElementFunction))
                serializedObj.Add("custom_element", obj.CustomElementFunction);

            if (!String.IsNullOrWhiteSpace(obj.CustomValueFunction))
                serializedObj.Add("custom_value", obj.CustomValueFunction);

            SerializeJqGridColumnElementOptions(obj, serializer, ref serializedObj);

            if (obj.NullIfEmpty)
                serializedObj.Add("NullIfEmpty", true);

            if (obj.HtmlAttributes != null)
                foreach(KeyValuePair<string, object> htmlAttribute in obj.HtmlAttributes)
                    serializedObj.Add(htmlAttribute.Key, htmlAttribute.Value);
        }

        private static JqGridParametersNames DeserializeJqGridParametersNames(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridParametersNames obj = new JqGridParametersNames();

            obj.PageIndex = GetStringFromSerializedObj(serializedObj, "page", JqGridOptionsDefaults.PageIndex);
            obj.RecordsCount = GetStringFromSerializedObj(serializedObj, "rows", JqGridOptionsDefaults.RecordsCount);
            obj.SortingName = GetStringFromSerializedObj(serializedObj, "sort", JqGridOptionsDefaults.SortingName);
            obj.SortingOrder = GetStringFromSerializedObj(serializedObj, "order", JqGridOptionsDefaults.SortingOrder);
            obj.Searching = GetStringFromSerializedObj(serializedObj, "search", JqGridOptionsDefaults.Searching);
            obj.Id = GetStringFromSerializedObj(serializedObj, "id", JqGridOptionsDefaults.Id);
            obj.Operator = GetStringFromSerializedObj(serializedObj, "oper", JqGridOptionsDefaults.Operator);
            obj.EditOperator = GetStringFromSerializedObj(serializedObj, "editoper", JqGridOptionsDefaults.EditOperator);
            obj.AddOperator = GetStringFromSerializedObj(serializedObj, "addoper", JqGridOptionsDefaults.AddOperator);
            obj.DeleteOperator = GetStringFromSerializedObj(serializedObj, "deloper", JqGridOptionsDefaults.DeleteOperator);
            obj.SubgridId = GetStringFromSerializedObj(serializedObj, "subgridid", JqGridOptionsDefaults.SubgridId);
            obj.PagesCount = GetStringFromSerializedObj(serializedObj, "npage");
            obj.TotalRows = GetStringFromSerializedObj(serializedObj, "totalrows", JqGridOptionsDefaults.TotalRows);

            return obj;
        }

        private static void SerializeJqGridParametersNames(JqGridParametersNames obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            if (obj.PageIndex != JqGridOptionsDefaults.PageIndex)
                serializedObj.Add("page", obj.PageIndex);

            if (obj.RecordsCount != JqGridOptionsDefaults.RecordsCount)
                serializedObj.Add("rows", obj.RecordsCount);

            if (obj.SortingName != JqGridOptionsDefaults.SortingName)
                serializedObj.Add("sort", obj.SortingName);

            if (obj.SortingOrder != JqGridOptionsDefaults.SortingOrder)
                serializedObj.Add("order", obj.SortingOrder);

            if (obj.Searching != JqGridOptionsDefaults.Searching)
                serializedObj.Add("search", obj.Searching);

            if (obj.Id != JqGridOptionsDefaults.Id)
                serializedObj.Add("id", obj.Id);

            if (obj.Operator != JqGridOptionsDefaults.Operator)
                serializedObj.Add("oper", obj.Operator);

            if (obj.EditOperator != JqGridOptionsDefaults.EditOperator)
                serializedObj.Add("editoper", obj.EditOperator);

            if (obj.AddOperator != JqGridOptionsDefaults.AddOperator)
                serializedObj.Add("addoper", obj.AddOperator);

            if (obj.DeleteOperator != JqGridOptionsDefaults.DeleteOperator)
                serializedObj.Add("deloper", obj.DeleteOperator);

            if (obj.SubgridId != JqGridOptionsDefaults.SubgridId)
                serializedObj.Add("subgridid", obj.SubgridId);

            if (!String.IsNullOrWhiteSpace(obj.PagesCount))
                serializedObj.Add("npage", obj.PagesCount);

            if (obj.TotalRows != JqGridOptionsDefaults.TotalRows)
                serializedObj.Add("totalrows", obj.TotalRows);
        }

        private static JqGridGroupingView DeserializeJqGridGroupingView(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridGroupingView obj = new JqGridGroupingView();

            if (serializedObj.ContainsKey("groupField") && serializedObj["groupField"] is ArrayList)
                obj.Fields = (string[])((ArrayList)serializedObj["groupField"]).ToArray(typeof(String));

            if (serializedObj.ContainsKey("groupOrder") && serializedObj["groupOrder"] is ArrayList)
                obj.Orders = (JqGridSortingOrders[])((ArrayList)serializedObj["groupOrder"]).ToArray(typeof(JqGridSortingOrders));

            if (serializedObj.ContainsKey("groupText") && serializedObj["groupText"] is ArrayList)
                obj.Texts = (string[])((ArrayList)serializedObj["groupText"]).ToArray(typeof(String));

            if (serializedObj.ContainsKey("groupSummary") && serializedObj["groupSummary"] is ArrayList)
                obj.Summary = (bool[])((ArrayList)serializedObj["groupSummary"]).ToArray(typeof(Boolean));

            if (serializedObj.ContainsKey("groupColumnShow") && serializedObj["groupColumnShow"] is ArrayList)
                obj.ColumnShow = (bool[])((ArrayList)serializedObj["groupColumnShow"]).ToArray(typeof(Boolean));

            obj.SummaryOnHide = GetBooleanFromSerializedObj(serializedObj, "showSummaryOnHide", false);
            obj.DataSorted = GetBooleanFromSerializedObj(serializedObj, "groupDataSorted", false);
            obj.Collapse = GetBooleanFromSerializedObj(serializedObj, "groupCollapse", false);
            obj.PlusIcon = GetStringFromSerializedObj(serializedObj, "plusicon", JqGridOptionsDefaults.GroupingPlusIcon);
            obj.MinusIcon = GetStringFromSerializedObj(serializedObj, "minusicon", JqGridOptionsDefaults.GroupingMinusIcon);

            return obj;
        }

        private static void SerializeJqGridGroupingView(JqGridGroupingView obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
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
            JqGridColumnRules obj = new JqGridColumnRules();

            obj.Custom = GetBooleanFromSerializedObj(serializedObj, "custom", false);
            obj.CustomFunction = GetStringFromSerializedObj(serializedObj, "custom_func");
            obj.Date = GetBooleanFromSerializedObj(serializedObj, "date", false);
            obj.EditHidden = GetBooleanFromSerializedObj(serializedObj, "edithidden", false);
            obj.Email = GetBooleanFromSerializedObj(serializedObj, "email", false);
            obj.Integer = GetBooleanFromSerializedObj(serializedObj, "integer", false);
            obj.MaxValue = GetDoubleFromSerializedObj(serializedObj, "maxValue");
            obj.MinValue = GetDoubleFromSerializedObj(serializedObj, "minValue");
            obj.Number = GetBooleanFromSerializedObj(serializedObj, "number", false);
            obj.Required = GetBooleanFromSerializedObj(serializedObj, "required", false);
            obj.Time = GetBooleanFromSerializedObj(serializedObj, "time", false);
            obj.Url = GetBooleanFromSerializedObj(serializedObj, "url", false);

            return obj;
        }

        private static void SerializeJqGridColumnRules(JqGridColumnRules obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            if (obj.Custom)
                serializedObj.Add("custom", true);

            if (!String.IsNullOrWhiteSpace(obj.CustomFunction))
                serializedObj.Add("custom_func", obj.CustomFunction);

            if (obj.Date)
                serializedObj.Add("date", true);

            if (obj.EditHidden)
                serializedObj.Add("edithidden", true);

            if (obj.Email)
                serializedObj.Add("email", true);

            if (obj.Integer)
                serializedObj.Add("integer", true);

            if (obj.MaxValue.HasValue)
                serializedObj.Add("maxValue", obj.MaxValue.Value);

            if (obj.MinValue.HasValue)
                serializedObj.Add("minValue", obj.MinValue.Value);

            if (obj.Number)
                serializedObj.Add("number", true);

            if (obj.Required)
                serializedObj.Add("required", true);

            if (obj.Time)
                serializedObj.Add("time", true);

            if (obj.Url)
                serializedObj.Add("url", true);
        }

        private static JqGridColumnFormOptions DeserializeJqGridColumnFormOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridColumnFormOptions obj = new JqGridColumnFormOptions();

            obj.ColumnPosition = GetInt32FromSerializedObj(serializedObj, "colpos");
            obj.ElementPrefix = GetStringFromSerializedObj(serializedObj, "elmprefix");
            obj.ElementSuffix = GetStringFromSerializedObj(serializedObj, "elmsuffix");
            obj.Label = GetStringFromSerializedObj(serializedObj, "label");
            obj.RowPosition = GetInt32FromSerializedObj(serializedObj, "rowpos");
            
            return obj;
        }

        private static void SerializeJqGridColumnFormOptions(JqGridColumnFormOptions obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            if (obj.ColumnPosition.HasValue)
                serializedObj.Add("colpos", obj.ColumnPosition.Value);
            
            if (!String.IsNullOrWhiteSpace(obj.ElementPrefix))
                serializedObj.Add("elmprefix", obj.ElementPrefix);

            if (!String.IsNullOrWhiteSpace(obj.ElementSuffix))
                serializedObj.Add("elmsuffix", obj.ElementSuffix);

            if (!String.IsNullOrWhiteSpace(obj.Label))
                serializedObj.Add("label", obj.Label);

            if (obj.RowPosition.HasValue)
                serializedObj.Add("rowpos", obj.RowPosition.Value);
        }

        private static JqGridColumnFormatterOptions DeserializeJqGridColumnFormatterOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridColumnFormatterOptions obj = new JqGridColumnFormatterOptions();

            obj.AddParam = GetStringFromSerializedObj(serializedObj, "addParam");
            obj.BaseLinkUrl = GetStringFromSerializedObj(serializedObj, "baseLinkUrl");
            obj.DecimalPlaces = GetInt32FromSerializedObj(serializedObj, "decimalPlaces");
            obj.DecimalSeparator = GetStringFromSerializedObj(serializedObj, "decimalSeparator");
            obj.DefaulValue = GetStringFromSerializedObj(serializedObj, "defaulValue");
            obj.Disabled = GetBooleanFromSerializedObj(serializedObj, "disabled");
            obj.IdName = GetStringFromSerializedObj(serializedObj, "idName");
            obj.Prefix = GetStringFromSerializedObj(serializedObj, "prefix");
            obj.ShowAction = GetStringFromSerializedObj(serializedObj, "showAction");
            obj.SourceFormat = GetStringFromSerializedObj(serializedObj, "srcformat");
            obj.Suffix = GetStringFromSerializedObj(serializedObj, "suffix");
            obj.Target = GetStringFromSerializedObj(serializedObj, "target");
            obj.TargetFormat = GetStringFromSerializedObj(serializedObj, "newformat");
            obj.ThousandsSeparator = GetStringFromSerializedObj(serializedObj, "thousandsSeparator");

            return obj;
        }

        private static void SerializeJqGridColumnFormatterOptions(JqGridColumnFormatterOptions obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            if (!String.IsNullOrWhiteSpace(obj.AddParam))
                serializedObj.Add("addParam", obj.AddParam);

            if (!String.IsNullOrWhiteSpace(obj.BaseLinkUrl))
                serializedObj.Add("baseLinkUrl", obj.BaseLinkUrl);

            if (obj.DecimalPlaces.HasValue)
                serializedObj.Add("decimalPlaces", obj.DecimalPlaces);

            if (!String.IsNullOrWhiteSpace(obj.DecimalSeparator))
                serializedObj.Add("decimalSeparator", obj.DecimalSeparator);

            if (!String.IsNullOrWhiteSpace(obj.DefaulValue))
                serializedObj.Add("defaulValue", obj.DefaulValue);

            if (obj.Disabled.HasValue)
                serializedObj.Add("disabled", obj.Disabled);

            if (!String.IsNullOrWhiteSpace(obj.IdName))
                serializedObj.Add("idName", obj.IdName);

            if (!String.IsNullOrWhiteSpace(obj.Prefix))
                serializedObj.Add("prefix", obj.Prefix);

            if (!String.IsNullOrWhiteSpace(obj.ShowAction))
                serializedObj.Add("showAction", obj.ShowAction);

            if (!String.IsNullOrWhiteSpace(obj.SourceFormat))
                serializedObj.Add("srcformat", obj.SourceFormat);

            if (!String.IsNullOrWhiteSpace(obj.Suffix))
                serializedObj.Add("suffix", obj.Suffix);

            if (!String.IsNullOrWhiteSpace(obj.Target))
                serializedObj.Add("target", obj.Target);

            if (!String.IsNullOrWhiteSpace(obj.TargetFormat))
                serializedObj.Add("newformat", obj.TargetFormat);

            if (!String.IsNullOrWhiteSpace(obj.ThousandsSeparator))
                serializedObj.Add("thousandsSeparator", obj.ThousandsSeparator);            
        }

        private static void SerializeJqGridColumnElementOptions(JqGridColumnElementOptions obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            if (!String.IsNullOrWhiteSpace(obj.DataUrl))
                serializedObj.Add("dataUrl", obj.DataUrl);

            if (!String.IsNullOrWhiteSpace(obj.DefaultValue))
                serializedObj.Add("defaultValue", obj.DefaultValue);
        }

        private static JqGridColumnSearchOptions DeserializeJqGridColumnSearchOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridColumnSearchOptions obj = new JqGridColumnSearchOptions();

            obj.DataUrl = GetStringFromSerializedObj(serializedObj, "dataUrl");
            obj.DefaultValue = GetStringFromSerializedObj(serializedObj, "defaultValue");

            if (serializedObj.ContainsKey("attr") && serializedObj["attr"] != null && serializedObj["attr"] is IDictionary<string, object>)
                obj.HtmlAttributes = (IDictionary<string, object>)serializedObj["attr"];

            obj.SearchHidden = GetBooleanFromSerializedObj(serializedObj, "searchhidden", false);

            if (serializedObj.ContainsKey("sopt") && serializedObj["sopt"] is ArrayList)
            {
                JqGridSearchOperators? searchOperators = null;
                foreach (object innerSerializedObj in (ArrayList)serializedObj["sopt"])
                {
                    JqGridSearchOperators searchOperator = JqGridSearchOperators.Eq;
                    if (Enum.TryParse<JqGridSearchOperators>(innerSerializedObj.ToString(), true, out searchOperator))
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

        private static void SerializeJqGridColumnSearchOptions(JqGridColumnSearchOptions obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            SerializeJqGridColumnElementOptions(obj, serializer, ref serializedObj);

            if (obj.HtmlAttributes != null && obj.HtmlAttributes.Count > 0)
                serializedObj.Add("attr", obj.HtmlAttributes);

            if (obj.SearchHidden)
                serializedObj.Add("searchhidden", true);

            if (obj.SearchOperators != (JqGridSearchOperators)16383)
            {
                List<string> searchOperators = new List<string>();
                foreach (JqGridSearchOperators searchOperator in Enum.GetValues(typeof(JqGridSearchOperators)))
                {
                    if ((obj.SearchOperators & searchOperator) == searchOperator)
                        searchOperators.Add(Enum.GetName(typeof(JqGridSearchOperators), searchOperator).ToLower());
                }
                serializedObj.Add("sopt", searchOperators);
            }
        }

        private static JqGridSubgridModel DeserializeJqGridSubgridModel(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridSubgridModel obj = new JqGridSubgridModel();

            if (serializedObj.ContainsKey("name") && serializedObj["name"] is ArrayList)
            {
                foreach (object innerSerializedObj in (ArrayList)serializedObj["name"])
                    obj.ColumnsNames.Add(innerSerializedObj.ToString());
            }

            if (serializedObj.ContainsKey("align") && serializedObj["align"] is ArrayList)
            {
                foreach (object innerSerializedObj in (ArrayList)serializedObj["align"])
                {
                    JqGridAlignments alignment = JqGridAlignments.Left;
                    Enum.TryParse<JqGridAlignments>(innerSerializedObj.ToString(), true, out alignment);
                    obj.ColumnsAlignments.Add(alignment);
                }
            }

            if (serializedObj.ContainsKey("width") && serializedObj["width"] is ArrayList)
            {
                foreach (object innerSerializedObj in (ArrayList)serializedObj["width"])
                {
                    if (innerSerializedObj is Int32)
                        obj.ColumnsWidths.Add((int)innerSerializedObj);
                }
            }

            return obj;
        }

        private static void SerializeJqGridSubgridModel(JqGridSubgridModel obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("name", obj.ColumnsNames);

            List<string> alignments = new List<string>();
            foreach (JqGridAlignments alignment in obj.ColumnsAlignments)
                alignments.Add(alignment.ToString().ToLower());
            serializedObj.Add("align", alignments);

            serializedObj.Add("width", obj.ColumnsWidths);
        }

        private static void SerializeJqGridResponse(JqGridResponse obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("page", obj.PageIndex + 1);
            serializedObj.Add("records", obj.TotalRecordsCount);
            serializedObj.Add("rows", obj.Records);
            serializedObj.Add("total", obj.TotalPagesCount);

            if (obj.UserData != null)
                serializedObj.Add("userdata", obj.UserData);
        }

        private static void SerializeJqGridRecord(JqGridRecord obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            List<object> values = obj.Values;

            JqGridAdjacencyTreeRecord adjacencyTreeRecord = obj as JqGridAdjacencyTreeRecord;
            if (adjacencyTreeRecord != null)
            {
                values.Add(adjacencyTreeRecord.Level);
                values.Add(adjacencyTreeRecord.ParentId);
                values.Add(adjacencyTreeRecord.Leaf);
                values.Add(adjacencyTreeRecord.Expanded);
            }
            else
            {
                JqGridNestedSetTreeRecord nestedSetTreeRecord = obj as JqGridNestedSetTreeRecord;
                if (nestedSetTreeRecord != null)
                {
                    values.Add(nestedSetTreeRecord.Level);
                    values.Add(nestedSetTreeRecord.LeftField);
                    values.Add(nestedSetTreeRecord.RightField);
                    values.Add(nestedSetTreeRecord.Leaf);
                    values.Add(nestedSetTreeRecord.Expanded);
                }
            }

            serializedObj.Add("cell", values);
            serializedObj.Add("id", obj.Id);
        }

        private static JqGridRequestSearchingFilters DeserializeJqGridRequestSearchingFilters(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridRequestSearchingFilters obj = new JqGridRequestSearchingFilters();

            obj.GroupingOperator = GetEnumFromSerializedObj<JqGridSearchGroupingOperators>(serializedObj, "groupOp", JqGridSearchGroupingOperators.And);
            if (serializedObj.ContainsKey("rules") && serializedObj["rules"] is ArrayList)
            {
                foreach (object innerSerializedObj in (ArrayList)serializedObj["rules"])
                {
                    if (innerSerializedObj is IDictionary<string, object>)
                    {
                        JqGridRequestSearchingFilter searchingFilter = DeserializeJqGridRequestSearchingFilter((IDictionary<string, object>)innerSerializedObj, serializer);
                        obj.Filters.Add(searchingFilter);
                    }
                }
            }

            return obj;
        }

        private static JqGridRequestSearchingFilter DeserializeJqGridRequestSearchingFilter(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridRequestSearchingFilter obj = new JqGridRequestSearchingFilter();

            obj.SearchingName = GetStringFromSerializedObj(serializedObj, "field");
            obj.SearchingOperator = GetEnumFromSerializedObj<JqGridSearchOperators>(serializedObj, "op", JqGridSearchOperators.Eq);
            obj.SearchingValue = GetStringFromSerializedObj(serializedObj, "data");
            return obj;
        }

        private static bool? GetBooleanFromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && serializedObj[key] is Boolean)
                return (bool)serializedObj[key];
            else
                return null;
        }

        private static bool GetBooleanFromSerializedObj(IDictionary<string, object> serializedObj, string key, bool defaultValue)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && serializedObj[key] is Boolean)
                return (bool)serializedObj[key];
            else
                return defaultValue;
        }

        private static double? GetDoubleFromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            double value;
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && Double.TryParse(serializedObj[key].ToString(), out value))
                return value;
            else
                return null;
        }

        private static TEnum? GetEnumFromSerializedObj<TEnum>(IDictionary<string, object> serializedObj, string key) where TEnum : struct
        {
            TEnum value;
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && Enum.TryParse<TEnum>(serializedObj[key].ToString(), true, out value))
                return value;
            else
                return null;
        }

        private static TEnum GetEnumFromSerializedObj<TEnum>(IDictionary<string, object> serializedObj, string key, TEnum defaultValue) where TEnum : struct
        {
            TEnum value = defaultValue;
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && Enum.TryParse<TEnum>(serializedObj[key].ToString(), true, out value))
                return value;
            else
                return defaultValue;
        }

        private static int? GetInt32FromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && serializedObj[key] is Int32)
                return (int)serializedObj[key];
            else
                return null;
        }

        private static int GetInt32FromSerializedObj(IDictionary<string, object> serializedObj, string key, int defaultValue)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && serializedObj[key] is Int32)
                return (int)serializedObj[key];
            else
                return defaultValue;
        }

        private static string GetStringFromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null)
                return serializedObj[key].ToString();
            else
                return null;
        }

        private static string GetStringFromSerializedObj(IDictionary<string, object> serializedObj, string key, string defaultValue)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null)
                return serializedObj[key].ToString();
            else
                return defaultValue;
        }
        #endregion
    }
}
