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
            Dictionary<string, object> serializedObj = new Dictionary<string, object>();

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
                obj.AltClass = GetStringFromSerializedObj(serializedObj, "altclass", JqGridOptionsDefaults.AltClass);
                obj.AltRows = GetBooleanFromSerializedObj(serializedObj, "altRows", false);
                obj.AutoEncode = GetBooleanFromSerializedObj(serializedObj, "autoencode", false);
                obj.AutoWidth = GetBooleanFromSerializedObj(serializedObj, "autowidth", false);
                obj.Caption = GetStringFromSerializedObj(serializedObj, "caption");
                obj.CellLayout = GetInt32FromSerializedObj(serializedObj, "cellLayout", JqGridOptionsDefaults.CellLayout);
                obj.CellEditingEnabled = GetBooleanFromSerializedObj(serializedObj, "cellEdit", false);
                obj.CellEditingSubmitMode = GetEnumFromSerializedObj(serializedObj, "cellsubmit", JqGridCellEditingSubmitModes.Remote);
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

                obj.DataString = GetStringFromSerializedObj(serializedObj, "datastr");
                obj.DataType = GetEnumFromSerializedObj(serializedObj, "datatype", JqGridDataTypes.Xml);
                obj.DeepEmpty = GetBooleanFromSerializedObj(serializedObj, "deepempty", false);
                obj.Direction = GetEnumFromSerializedObj(serializedObj, "direction", JqGridLanguageDirections.Ltr);
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
                obj.EmptyRecords = GetStringFromSerializedObj(serializedObj, "emptyrecords", JqGridOptionsDefaults.EmptyRecords);
                obj.ExpandColumnClick = GetBooleanFromSerializedObj(serializedObj, "ExpandColClick", true);
                obj.ExpandColumn = GetStringFromSerializedObj(serializedObj, "ExpandColumn");
                obj.FooterEnabled = GetBooleanFromSerializedObj(serializedObj, "footerrow", false);
                obj.UserDataOnFooter = GetBooleanFromSerializedObj(serializedObj, "userDataOnFooter", false);
                obj.GridView = GetBooleanFromSerializedObj(serializedObj, "gridview", false);

                obj.GroupingEnabled = GetBooleanFromSerializedObj(serializedObj, "grouping", false);
                if (serializedObj.ContainsKey("groupingView") && serializedObj["groupingView"] is IDictionary<string, object>)
                    obj.GroupingView = DeserializeJqGridGroupingView((IDictionary<string, object>)serializedObj["groupingView"], serializer);

                obj.Height = GetInt32FromSerializedObj(serializedObj, "height");
                obj.Hidden = GetBooleanFromSerializedObj(serializedObj, "hiddengrid", false);
                obj.HiddenEnabled  = GetBooleanFromSerializedObj(serializedObj, "hidegrid", true);
                obj.HoverRows = GetBooleanFromSerializedObj(serializedObj, "hoverrows", true);
                obj.IgnoreCase = GetBooleanFromSerializedObj(serializedObj, "ignoreCase", false);
                obj.LoadOnce = GetBooleanFromSerializedObj(serializedObj, "loadonce", false);
                obj.MethodType = GetEnumFromSerializedObj(serializedObj, "mtype", JqGridMethodTypes.Get);
                obj.MultiKey = GetEnumFromSerializedObj<JqGridMultiKeys>(serializedObj, "multikey");
                obj.MultiBoxOnly = GetBooleanFromSerializedObj(serializedObj, "multiboxonly", false);
                obj.MultiSelect = GetBooleanFromSerializedObj(serializedObj, "multiselect", false);
                obj.MultiSelectWidth = GetInt32FromSerializedObj(serializedObj, "multiselectWidth", 20);
                obj.MultiSort = GetBooleanFromSerializedObj(serializedObj, "multiSort", false);
                
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
                obj.RowsNumber = GetInt32FromSerializedObj(serializedObj, "rowNum", 20);
                obj.RowsNumbers = GetBooleanFromSerializedObj(serializedObj, "rownumbers", false);
                obj.RowsNumbersWidth = GetInt32FromSerializedObj(serializedObj, "rownumWidth", 25);
                obj.ShrinkToFit = GetBooleanFromSerializedObj(serializedObj, "shrinkToFit", true);
                obj.ScrollOffset = GetInt32FromSerializedObj(serializedObj, "scrollOffset", 18);
                obj.Sortable = GetBooleanFromSerializedObj(serializedObj, "sortable", false);
                obj.SortingName = GetStringFromSerializedObj(serializedObj, "sortname");
                obj.SortingOrder = GetEnumFromSerializedObj(serializedObj, "sortorder", JqGridSortingOrders.Asc);
                obj.StyleUI = GetEnumFromSerializedObj(serializedObj, "styleUI", JqGridStyleUIOptions.jQueryUI);
                obj.SubgridEnabled = GetBooleanFromSerializedObj(serializedObj, "subGrid", false);

                if (serializedObj.ContainsKey("subGridModel") && serializedObj["subGridModel"] != null && serializedObj["subGridModel"] is IDictionary<string, object>)
                    obj.SubgridModel = DeserializeJqGridSubgridModel((IDictionary<string, object>)serializedObj["subGridModel"], serializer);

                obj.SubgridUrl = GetStringFromSerializedObj(serializedObj, "subGridUrl");
                obj.SubgridColumnWidth = GetInt32FromSerializedObj(serializedObj, "subGridWidth", 20);
                obj.TopPager = GetBooleanFromSerializedObj(serializedObj, "toppager", false);
                obj.TreeGridEnabled = GetBooleanFromSerializedObj(serializedObj, "treeGrid", false);
                obj.TreeGridModel = GetEnumFromSerializedObj(serializedObj, "treeGridModel", JqGridTreeGridModels.Nested);
                obj.Url = GetStringFromSerializedObj(serializedObj, "url");
                obj.ViewRecords = GetBooleanFromSerializedObj(serializedObj, "viewrecords", false);
                obj.Width = GetInt32FromSerializedObj(serializedObj, "width");

                return obj;
            }
            else
                return null;
        }

        private static void SerializeJqGridOptions(JqGridOptions obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("id", obj.Id);

            if (obj.AltClass != JqGridOptionsDefaults.AltClass)
                serializedObj.Add("altclass", obj.AltClass);

            if (obj.AltRows)
                serializedObj.Add("altRows", true);

            if (obj.AutoEncode)
                serializedObj.Add("autoencode", true);

            if (obj.AutoWidth)
                serializedObj.Add("autowidth", true);

            if (obj.CellLayout != JqGridOptionsDefaults.CellLayout)
                serializedObj.Add("cellLayout", obj.CellLayout);

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

            if (obj.DeepEmpty)
                serializedObj.Add("deepempty", true);

            if (obj.Direction != JqGridLanguageDirections.Ltr)
                serializedObj.Add("direction", "rtl");

            if (obj.DynamicScrollingMode == JqGridDynamicScrollingModes.HoldAllRows)
                serializedObj.Add("scroll", true);
            else if (obj.DynamicScrollingMode == JqGridDynamicScrollingModes.HoldVisibleRows)
            {
                serializedObj.Add("scroll", 10);
                if (obj.DynamicScrollingTimeout != 200)
                    serializedObj.Add("scrollTimeout", obj.DynamicScrollingTimeout);
            }

            if (obj.EmptyRecords != JqGridOptionsDefaults.EmptyRecords)
                serializedObj.Add("emptyrecords", obj.EmptyRecords);

            if (!String.IsNullOrWhiteSpace(obj.EditingUrl))
                serializedObj.Add("editurl", obj.EditingUrl);

            if (obj.FooterEnabled)
            {
                serializedObj.Add("footerrow", true);
                if (obj.UserDataOnFooter)
                    serializedObj.Add("userDataOnFooter", true);
            }

            if (obj.GridView)
                serializedObj.Add("gridview", true);

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

            if (!obj.HoverRows)
                serializedObj.Add("hoverrows", false);

            if (obj.IgnoreCase)
                serializedObj.Add("ignoreCase", true);

            if (obj.LoadOnce)
                serializedObj.Add("loadonce", true);

            if (obj.MethodType != JqGridMethodTypes.Get)
                serializedObj.Add("mtype", "POST");

            if (obj.MultiKey.HasValue)
                serializedObj.Add("multikey", String.Format("{0}Key", obj.MultiKey.Value.ToString().ToLower()));

            if (obj.MultiBoxOnly)
                serializedObj.Add("multiboxonly", true);

            if (obj.MultiSelect)
                serializedObj.Add("multiselect", true);

            if (obj.MultiSelectWidth != 20)
                serializedObj.Add("multiselectWidth", obj.MultiSelectWidth);

            if (obj.MultiSort)
                serializedObj.Add("multiSort", true);

            if (obj.Pager)
                serializedObj.Add("pager", String.Format("#{0}Pager", obj.Id));

            if (obj.JsonReader != null && !obj.JsonReader.IsDefault())
                serializedObj.Add("jsonReader", obj.JsonReader);

            if (obj.ParametersNames != null && !obj.ParametersNames.IsDefault())
                serializedObj.Add("prmNames", obj.ParametersNames);

            serializedObj.Add("remapColumns", obj.ColumnsRemaping);

            if (obj.RowsList != null && obj.RowsList.Count > 0)
                serializedObj.Add("rowList", obj.RowsList);

            if (obj.RowsNumber != 20)
                serializedObj.Add("rowNum", obj.RowsNumber);

            if (obj.RowsNumbers)
                serializedObj.Add("rownumbers", true);

            if (obj.RowsNumbersWidth != 25)
                serializedObj.Add("rownumWidth", obj.RowsNumbersWidth);

            if (!obj.ShrinkToFit)
                serializedObj.Add("shrinkToFit", false);

            if (obj.ScrollOffset != 18)
                serializedObj.Add("scrollOffset", obj.ScrollOffset);

            if (obj.Sortable)
                serializedObj.Add("sortable", true);

            if (!String.IsNullOrWhiteSpace(obj.SortingName))
                serializedObj.Add("sortname", obj.SortingName);

            if (obj.SortingOrder != JqGridSortingOrders.Asc)
                serializedObj.Add("sortorder", "desc");

            if (obj.StyleUI != JqGridStyleUIOptions.jQueryUI)
                serializedObj.Add("styleUI", obj.StyleUI.ToString());

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

            if (obj.TopPager)
                serializedObj.Add("toppager", true);

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

                obj.Alignment = GetEnumFromSerializedObj(serializedObj, "align", obj.Alignment);
                obj.DateFormat = GetStringFromSerializedObj(serializedObj, "datefmt", JqGridOptionsDefaults.DateFormat);
                obj.Classes = GetStringFromSerializedObj(serializedObj, "classes");
                obj.Editable = GetBooleanFromSerializedObj(serializedObj, "editable", false);
                obj.EditType = GetEnumFromSerializedObj(serializedObj, "edittype", JqGridColumnEditTypes.Text);

                if (serializedObj.ContainsKey("editoptions") && serializedObj["editoptions"] != null && serializedObj["editoptions"] is IDictionary<string, object>)
                    obj.EditOptions = DeserializeJqGridColumnEditOptions((IDictionary<string, object>)serializedObj["editoptions"], serializer);

                if (serializedObj.ContainsKey("editrules") && serializedObj["editrules"] != null && serializedObj["editrules"] is IDictionary<string, object>)
                    obj.EditRules = DeserializeJqGridColumnRules((IDictionary<string, object>)serializedObj["editrules"], serializer);

                obj.Fixed = GetBooleanFromSerializedObj(serializedObj, "fixed", false);
                obj.Frozen = GetBooleanFromSerializedObj(serializedObj, "frozen", false);
                obj.Hidden = GetBooleanFromSerializedObj(serializedObj, "hidden", false);
                obj.HideInDialog = GetBooleanFromSerializedObj(serializedObj, "hidedlg", false);

                obj.Formatter = GetStringFromSerializedObj(serializedObj, "formatter");
                if (!String.IsNullOrWhiteSpace(obj.Formatter))
                    obj.Formatter = '\'' + obj.Formatter + '\'';

                if (serializedObj.ContainsKey("formatoptions") && serializedObj["formatoptions"] != null && serializedObj["formatoptions"] is IDictionary<string, object>)
                    obj.FormatterOptions = DeserializeJqGridColumnFormatterOptions((IDictionary<string, object>)serializedObj["formatoptions"], serializer);
                //obj.UnFormatter = GetStringFromSerializedObj(serializedObj, "unformat");

                if (serializedObj.ContainsKey("formoptions") && serializedObj["formoptions"] != null && serializedObj["formoptions"] is IDictionary<string, object>)
                    obj.FormOptions = DeserializeJqGridColumnFormOptions((IDictionary<string, object>)serializedObj["formoptions"], serializer);

                obj.InitialSortingOrder = GetEnumFromSerializedObj(serializedObj, "firstsortorder", JqGridSortingOrders.Asc);
                obj.JsonMapping = GetStringFromSerializedObj(serializedObj, "jsonmap");
                obj.Key = GetBooleanFromSerializedObj(serializedObj, "key", false);
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
                obj.SortType = GetEnumFromSerializedObj(serializedObj, "sorttype", JqGridColumnSortTypes.Text);
                obj.Index = GetStringFromSerializedObj(serializedObj, "index");
                obj.Searchable = GetBooleanFromSerializedObj(serializedObj, "search", true);
                obj.SearchType = GetEnumFromSerializedObj(serializedObj, "stype", JqGridColumnSearchTypes.Text);

                if (serializedObj.ContainsKey("searchoptions") && serializedObj["searchoptions"] != null && serializedObj["searchoptions"] is IDictionary<string, object>)
                    obj.SearchOptions = DeserializeJqGridColumnSearchOptions((IDictionary<string, object>)serializedObj["searchoptions"], serializer);

                if (serializedObj.ContainsKey("searchrules") && serializedObj["searchrules"] != null && serializedObj["searchrules"] is IDictionary<string, object>)
                    obj.SearchRules = DeserializeJqGridColumnRules((IDictionary<string, object>)serializedObj["searchrules"], serializer);

                obj.Title = GetBooleanFromSerializedObj(serializedObj, "title", true);
                obj.Width = GetInt32FromSerializedObj(serializedObj, "width", 150);
                obj.Viewable = GetBooleanFromSerializedObj(serializedObj, "viewable", true);
                obj.XmlMapping = GetStringFromSerializedObj(serializedObj, "xmlmap");

                return obj;
            }
            else
                return null;
        }

        private static void SerializeJqGridColumnModel(JqGridColumnModel obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("align", obj.Alignment.ToString().ToLower());

            if (!String.IsNullOrWhiteSpace(obj.Classes))
                serializedObj.Add("classes", obj.Classes);

            if (obj.DateFormat != JqGridOptionsDefaults.DateFormat)
                serializedObj.Add("datefmt", obj.DateFormat);

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

            if (obj.Frozen)
                serializedObj.Add("frozen", true);

            if (obj.InitialSortingOrder != JqGridSortingOrders.Asc)
                serializedObj.Add("firstsortorder", "desc");

            if (!String.IsNullOrWhiteSpace(obj.Formatter) && obj.Formatter[0] == '\'' && obj.Formatter[obj.Formatter.Length - 1] == '\'')
            {
                serializedObj.Add("formatter", obj.Formatter.Substring(1, obj.Formatter.Length - 2));

                if (obj.FormatterOptions != null)
                    serializedObj.Add("formatoptions", obj.FormatterOptions);
            }

            //if (!String.IsNullOrWhiteSpace(obj.UnFormatter))
            //    serializedObj.Add("unformat", obj.UnFormatter);

            if (obj.Hidden)
                serializedObj.Add("hidden", true);

            if (obj.HideInDialog) 
                serializedObj.Add("hidedlg", true);

            if (!String.IsNullOrWhiteSpace(obj.JsonMapping))
                serializedObj.Add("jsonmap", obj.JsonMapping);

            if (obj.Key)
                serializedObj.Add("key", true);

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

            if ((obj.SortType != JqGridColumnSortTypes.Text) && (obj.SortType != JqGridColumnSortTypes.Function))
                serializedObj.Add("sorttype", obj.SortType.ToString().ToLower());

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

            if (!obj.Title)
                serializedObj.Add("title", false);

            if (obj.Width != 150)
                serializedObj.Add("width", obj.Width);

            if (!obj.Viewable)
                serializedObj.Add("viewable", false);

            if (!String.IsNullOrWhiteSpace(obj.XmlMapping))
                serializedObj.Add("xmlmap", obj.XmlMapping);
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

            if (serializedObj.ContainsKey("value") && serializedObj["value"] != null && serializedObj["value"] is IDictionary<string, object>)
                obj.ValueDictionary = ((IDictionary<string, object>)serializedObj["value"]).ToDictionary(k => k.Key, v => v.Value.ToString());
            else
                obj.Value = GetStringFromSerializedObj(serializedObj, "value");
            serializedObj.Remove("value");

            obj.NullIfEmpty = GetBooleanFromSerializedObj(serializedObj, "NullIfEmpty", false);
            serializedObj.Remove("NullIfEmpty");
            obj.HtmlAttributes = serializedObj;

            return obj;
        }

        private static void SerializeJqGridColumnEditOptions(JqGridColumnEditOptions obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            if (!String.IsNullOrWhiteSpace(obj.CustomElementFunction))
                serializedObj.Add("custom_element", obj.CustomElementFunction);

            if (!String.IsNullOrWhiteSpace(obj.CustomValueFunction))
                serializedObj.Add("custom_value", obj.CustomValueFunction);

            SerializeJqGridColumnElementOptions(obj, serializer, serializedObj);

            if (obj.NullIfEmpty)
                serializedObj.Add("NullIfEmpty", true);

            if (obj.HtmlAttributes != null)
                foreach(KeyValuePair<string, object> htmlAttribute in obj.HtmlAttributes)
                    serializedObj.Add(htmlAttribute.Key, htmlAttribute.Value);
        }

        private static JqGridJsonReader DeserializeJqGridJsonReader(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridJsonReader obj = new JqGridJsonReader();

            obj.PageIndex = GetStringFromSerializedObj(serializedObj, "page", JqGridOptionsDefaults.ResponsePageIndex);
            obj.RecordId = GetStringFromSerializedObj(serializedObj, "id", JqGridOptionsDefaults.ResponseRecordId);
            obj.Records = GetStringFromSerializedObj(serializedObj, "root", JqGridOptionsDefaults.ResponseRecords);
            obj.RecordValues = GetStringFromSerializedObj(serializedObj, "cell", JqGridOptionsDefaults.ResponseRecordValues);
            obj.RepeatItems = GetBooleanFromSerializedObj(serializedObj, "repeatitems", true);
            obj.TotalPagesCount = GetStringFromSerializedObj(serializedObj, "total", JqGridOptionsDefaults.ResponseTotalPagesCount);
            obj.TotalRecordsCount = GetStringFromSerializedObj(serializedObj, "records", JqGridOptionsDefaults.ResponseTotalRecordsCount);
            obj.UserData = GetStringFromSerializedObj(serializedObj, "userdata", JqGridOptionsDefaults.ResponseUserData);

            if (serializedObj.ContainsKey("subgrid") && serializedObj["subgrid"] is IDictionary<string, object>)
            {
                IDictionary<string, object> serializedSubgrid = (IDictionary<string, object>)serializedObj["subgrid"];
                obj.SubgridReader.Records = GetStringFromSerializedObj(serializedSubgrid, "root", JqGridOptionsDefaults.ResponseRecords);
                obj.SubgridReader.RecordValues = GetStringFromSerializedObj(serializedSubgrid, "cell", JqGridOptionsDefaults.ResponseRecordValues);
                obj.SubgridReader.RepeatItems = GetBooleanFromSerializedObj(serializedSubgrid, "repeatitems", true);
            }

            return obj;
        }

        private static void SerializeJqGridJsonReader(JqGridJsonReader obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            if (obj.Records != JqGridOptionsDefaults.ResponseRecords)
                serializedObj.Add("root", obj.Records);

            if (obj.PageIndex != JqGridOptionsDefaults.ResponsePageIndex)
                serializedObj.Add("page", obj.PageIndex);

            if (obj.TotalPagesCount != JqGridOptionsDefaults.ResponseTotalPagesCount)
                serializedObj.Add("total", obj.TotalPagesCount);

            if (obj.TotalRecordsCount != JqGridOptionsDefaults.ResponseTotalRecordsCount)
                serializedObj.Add("records", obj.TotalRecordsCount);

            if (!obj.RepeatItems)
                serializedObj.Add("repeatitems", false);

            if (obj.RecordValues != JqGridOptionsDefaults.ResponseRecordValues)
                serializedObj.Add("cell", obj.RecordValues);

            if (obj.RecordId != JqGridOptionsDefaults.ResponseRecordId)
                serializedObj.Add("id", obj.RecordId);

            if (obj.UserData != JqGridOptionsDefaults.ResponseUserData)
                serializedObj.Add("userdata", obj.UserData);

            if (!obj.SubgridReader.IsDefault())
            {
                Dictionary<string, object> serializedSubgrid = new Dictionary<string, object>();

                if (obj.SubgridReader.Records != JqGridOptionsDefaults.ResponseRecords)
                    serializedSubgrid.Add("root", obj.SubgridReader.Records);

                if (!obj.SubgridReader.RepeatItems)
                    serializedSubgrid.Add("repeatitems", false);

                if (obj.SubgridReader.RecordValues != JqGridOptionsDefaults.ResponseRecordValues)
                    serializedSubgrid.Add("cell", obj.SubgridReader.RecordValues);

                serializedObj.Add("subgrid", serializedSubgrid);
            }
        }

        private static JqGridParametersNames DeserializeJqGridParametersNames(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridParametersNames obj = new JqGridParametersNames();

            obj.PageIndex = GetStringFromSerializedObj(serializedObj, "page", JqGridOptionsDefaults.RequestPageIndex);
            obj.RecordsCount = GetStringFromSerializedObj(serializedObj, "rows", JqGridOptionsDefaults.RequestRecordsCount);
            obj.SortingName = GetStringFromSerializedObj(serializedObj, "sort", JqGridOptionsDefaults.RequestSortingName);
            obj.SortingOrder = GetStringFromSerializedObj(serializedObj, "order", JqGridOptionsDefaults.RequestSortingOrder);
            obj.Searching = GetStringFromSerializedObj(serializedObj, "search", JqGridOptionsDefaults.RequestSearching);
            obj.Id = GetStringFromSerializedObj(serializedObj, "id", JqGridOptionsDefaults.RequestId);
            obj.Operator = GetStringFromSerializedObj(serializedObj, "oper", JqGridOptionsDefaults.RequestOperator);
            obj.EditOperator = GetStringFromSerializedObj(serializedObj, "editoper", JqGridOptionsDefaults.RequestEditOperator);
            obj.AddOperator = GetStringFromSerializedObj(serializedObj, "addoper", JqGridOptionsDefaults.RequestAddOperator);
            obj.DeleteOperator = GetStringFromSerializedObj(serializedObj, "deloper", JqGridOptionsDefaults.RequestDeleteOperator);
            obj.SubgridId = GetStringFromSerializedObj(serializedObj, "subgridid", JqGridOptionsDefaults.RequestSubgridId);
            obj.PagesCount = GetStringFromSerializedObj(serializedObj, "npage");
            obj.TotalRows = GetStringFromSerializedObj(serializedObj, "totalrows", JqGridOptionsDefaults.RequestTotalRows);

            return obj;
        }

        private static void SerializeJqGridParametersNames(JqGridParametersNames obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            if (obj.PageIndex != JqGridOptionsDefaults.RequestPageIndex)
                serializedObj.Add("page", obj.PageIndex);

            if (obj.RecordsCount != JqGridOptionsDefaults.RequestRecordsCount)
                serializedObj.Add("rows", obj.RecordsCount);

            if (obj.SortingName != JqGridOptionsDefaults.RequestSortingName)
                serializedObj.Add("sort", obj.SortingName);

            if (obj.SortingOrder != JqGridOptionsDefaults.RequestSortingOrder)
                serializedObj.Add("order", obj.SortingOrder);

            if (obj.Searching != JqGridOptionsDefaults.RequestSearching)
                serializedObj.Add("search", obj.Searching);

            if (obj.Id != JqGridOptionsDefaults.RequestId)
                serializedObj.Add("id", obj.Id);

            if (obj.Operator != JqGridOptionsDefaults.RequestOperator)
                serializedObj.Add("oper", obj.Operator);

            if (obj.EditOperator != JqGridOptionsDefaults.RequestEditOperator)
                serializedObj.Add("editoper", obj.EditOperator);

            if (obj.AddOperator != JqGridOptionsDefaults.RequestAddOperator)
                serializedObj.Add("addoper", obj.AddOperator);

            if (obj.DeleteOperator != JqGridOptionsDefaults.RequestDeleteOperator)
                serializedObj.Add("deloper", obj.DeleteOperator);

            if (obj.SubgridId != JqGridOptionsDefaults.RequestSubgridId)
                serializedObj.Add("subgridid", obj.SubgridId);

            if (!String.IsNullOrWhiteSpace(obj.PagesCount))
                serializedObj.Add("npage", obj.PagesCount);

            if (obj.TotalRows != JqGridOptionsDefaults.RequestTotalRows)
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

        private static void SerializeJqGridColumnRules(JqGridColumnRules obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
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

        private static void SerializeJqGridColumnFormOptions(JqGridColumnFormOptions obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
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

            obj.AddParam = GetStringFromSerializedObj(serializedObj, "addParam", String.Empty);
            obj.BaseLinkUrl = GetStringFromSerializedObj(serializedObj, "baseLinkUrl", String.Empty);
            obj.DecimalPlaces = GetInt32FromSerializedObj(serializedObj, "decimalPlaces", 0);
            obj.DecimalSeparator = GetStringFromSerializedObj(serializedObj, "decimalSeparator", String.Empty);
            obj.DefaultValue = GetStringFromSerializedObj(serializedObj, "defaultValue", String.Empty);
            obj.Disabled = GetBooleanFromSerializedObj(serializedObj, "disabled", true);
            obj.IdName = GetStringFromSerializedObj(serializedObj, "idName", String.Empty);
            obj.OutputFormat = GetStringFromSerializedObj(serializedObj, "srcformat", String.Empty);
            obj.Prefix = GetStringFromSerializedObj(serializedObj, "prefix", String.Empty);
            obj.ShowAction = GetStringFromSerializedObj(serializedObj, "showAction", String.Empty);
            obj.SourceFormat = GetStringFromSerializedObj(serializedObj, "newformat", String.Empty);
            obj.Suffix = GetStringFromSerializedObj(serializedObj, "suffix", String.Empty);
            obj.Target = GetStringFromSerializedObj(serializedObj, "target", String.Empty);
            obj.ThousandsSeparator = GetStringFromSerializedObj(serializedObj, "thousandsSeparator", String.Empty);

            return obj;
        }

        private static void SerializeJqGridColumnFormatterOptions(JqGridColumnFormatterOptions obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("addParam", obj.AddParam);
            serializedObj.Add("baseLinkUrl", obj.BaseLinkUrl);
            serializedObj.Add("decimalPlaces", obj.DecimalPlaces);
            serializedObj.Add("decimalSeparator", obj.DecimalSeparator);
            serializedObj.Add("defaultValue", obj.DefaultValue);
            serializedObj.Add("disabled", obj.Disabled);
            serializedObj.Add("idName", obj.IdName);
            serializedObj.Add("srcformat", obj.OutputFormat);
            serializedObj.Add("prefix", obj.Prefix);
            serializedObj.Add("showAction", obj.ShowAction);
            serializedObj.Add("newformat", obj.SourceFormat);
            serializedObj.Add("suffix", obj.Suffix);
            serializedObj.Add("target", obj.Target);
            serializedObj.Add("thousandsSeparator", obj.ThousandsSeparator);
        }

        private static void SerializeJqGridColumnElementOptions(JqGridColumnElementOptions obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            if (!String.IsNullOrWhiteSpace(obj.DataUrl))
                serializedObj.Add("dataUrl", obj.DataUrl);

            if (!String.IsNullOrWhiteSpace(obj.DefaultValue))
                serializedObj.Add("defaultValue", obj.DefaultValue);

            if (!String.IsNullOrWhiteSpace(obj.Value))
                serializedObj.Add("value", obj.Value);
            else if (obj.ValueDictionary != null)
                serializedObj.Add("value", obj.ValueDictionary);
        }

        private static JqGridColumnSearchOptions DeserializeJqGridColumnSearchOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridColumnSearchOptions obj = new JqGridColumnSearchOptions();

            obj.ClearSearch = GetBooleanFromSerializedObj(serializedObj, "clearSearch", true);
            obj.DataUrl = GetStringFromSerializedObj(serializedObj, "dataUrl");
            obj.DefaultValue = GetStringFromSerializedObj(serializedObj, "defaultValue");

            if (serializedObj.ContainsKey("value") && serializedObj["value"] != null && serializedObj["value"] is IDictionary<string, object>)
                obj.ValueDictionary = ((IDictionary<string, object>)serializedObj["value"]).ToDictionary(k => k.Key, v => v.Value.ToString());
            else
                obj.Value = GetStringFromSerializedObj(serializedObj, "value");

            if (serializedObj.ContainsKey("attr") && serializedObj["attr"] != null && serializedObj["attr"] is IDictionary<string, object>)
                obj.HtmlAttributes = (IDictionary<string, object>)serializedObj["attr"];

            obj.SearchHidden = GetBooleanFromSerializedObj(serializedObj, "searchhidden", false);

            if (serializedObj.ContainsKey("sopt") && serializedObj["sopt"] is ArrayList)
            {
                JqGridSearchOperators? searchOperators = null;
                foreach (object innerSerializedObj in (ArrayList)serializedObj["sopt"])
                {
                    JqGridSearchOperators searchOperator = JqGridSearchOperators.Eq;
                    if (Enum.TryParse(innerSerializedObj.ToString(), true, out searchOperator))
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

            if (!obj.ClearSearch)
                serializedObj.Add("clearSearch", false);

            if (obj.SearchHidden)
                serializedObj.Add("searchhidden", true);

            if (obj.SearchOperators != (JqGridSearchOperators)32768)
            {
                List<string> searchOperators = new List<string>();
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
                    Enum.TryParse(innerSerializedObj.ToString(), true, out alignment);
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

        private static void SerializeJqGridSubgridModel(JqGridSubgridModel obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("name", obj.ColumnsNames);

            List<string> alignments = new List<string>();
            foreach (JqGridAlignments alignment in obj.ColumnsAlignments)
                alignments.Add(alignment.ToString().ToLower());
            serializedObj.Add("align", alignments);

            serializedObj.Add("width", obj.ColumnsWidths);
        }

        private static void SerializeJqGridResponse(JqGridResponse obj, JavaScriptSerializer serializer, Dictionary<string, object> serializedObj)
        {
            JqGridJsonReader jsonReader = (obj.Reader == null) ? JqGridResponse.JsonReader : obj.Reader;

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
            int recordIdIndex = 0;
            bool isRecordIndexInt = Int32.TryParse(jsonReader.RecordId, out recordIdIndex);
            bool isRecordValuesEmpty = String.IsNullOrWhiteSpace(jsonReader.RecordValues);
            bool repeatItems = isSubgridResponse ? jsonReader.SubgridReader.RepeatItems : jsonReader.RepeatItems;

            if (!isSubgridResponse)
            {
                if (repeatItems && isRecordValuesEmpty && !isRecordIndexInt)
                    throw new InvalidOperationException("JqGridJsonReader.RecordId must be a number when JqGridJsonReader.RepeatItems is set to true and JqGridJsonReader.RecordValues is set to empty string.");

                if (repeatItems && !isRecordValuesEmpty && isRecordIndexInt)
                    throw new InvalidOperationException("JqGridJsonReader.RecordValues can't be an empty string when JqGridJsonReader.RepeatItems is set to true and JqGridJsonReader.RecordId is a number.");
            }

            List<object> serializedObjs = new List<object>();

            foreach (JqGridRecord obj in objs)
            {
                JqGridAdjacencyTreeRecord adjacencyTreeRecord = obj as JqGridAdjacencyTreeRecord;
                JqGridNestedSetTreeRecord nestedSetTreeRecord = obj as JqGridNestedSetTreeRecord;
                if (repeatItems)
                {
                    List<object> values = obj.Values;

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
                        Dictionary<string, object> serializedObj = new Dictionary<string, object>();
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

                    Dictionary<string, object> serializedObj = obj.GetValuesAsDictionary();

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
            if (serializedObj.ContainsKey("groups") && serializedObj["groups"] is ArrayList)
            {
                foreach (object innerSerializedObj in (ArrayList)serializedObj["groups"])
                {
                    if (innerSerializedObj is IDictionary<string, object>)
                    {
                        JqGridRequestSearchingFilters searchingFilters = DeserializeJqGridRequestSearchingFilters((IDictionary<string, object>)innerSerializedObj, serializer);
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
            JqGridRequestSearchingFilter obj = new JqGridRequestSearchingFilter();

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

        private static List<int> GetInt32ArrayFromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            List<int> array = null;

            if (serializedObj.ContainsKey(key) && serializedObj[key] is ArrayList)
            {
                array = new List<int>();
                ArrayList serializedArray = (ArrayList)serializedObj[key];
                foreach (object serializedItem in serializedArray)
                {
                    if (serializedItem is Int32)
                        array.Add((int)serializedItem);
                }
            }

            return array;
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
