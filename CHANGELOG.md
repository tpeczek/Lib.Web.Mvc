## Lib.Web.Mvc 6.8.1
### Bug Fixes
- Fix for ETag generation in RangeFileResult on FIPS compliant environments
- Fix for different dates and months localization between Guriddo jqGrid and free jqGrid

## Lib.Web.Mvc 6.8.0
### Additions and Changes
- Added support for HTTP/2 Server Push (with Cache Digest) via PushPromiseAttribute and PushPromiseExtensions

## Lib.Web.Mvc 6.7.0
### Bug Fixes
- Fix for bug in DeserializeJqGridColumnSearchOptions and DeserializeJqGridColumnEditOptions (thanks to @glazkovalex)
### Additions and Changes
- Added support for Bootstrap in jqGrid via StyleUI property on JqGridOptions (thanks to @DH0)
- Added ClearSearch to JqGridColumnSearchOptions/JqGridColumnSearchableAttribute (thanks to @glazkovalex)
- Added BeforeProcessing to JqGridOptions and JqGridHelper (thanks to @NeoBelerophon)

## Lib.Web.Mvc 6.6.0
### Additions and Changes
- Added support for Content Security Policy Level 2 via ContentSecurityPolicyAttribute and ContentSecurityPolicyExtensions
- Added support for empty area names in JqGridColumnSearchableAttribute and JqGridColumnEditableAttribute

## Lib.Web.Mvc 6.5.0
### Additions and Changes
- Added HoverRows to JqGridOptions/JqGridHelper (thanks to @tascogin)

## Lib.Web.Mvc 6.4.2
### Bug Fixes
- Fix for suffix-byte-range-spec support in RangeFileResult (Work Item #13267)
- Fix for JqGridOptions.ColumnsRemaping rendering (Work Item #13152)

## Lib.Web.Mvc 6.4.1
### Bug Fixes
- Fix for Last-Modified header in RangeFileResult (Work Item #13128)

## Lib.Web.Mvc 6.4.0
### Additions and Changes
- Added RecreateForm to JqGridNavigatorViewActionOptions (Work Item #13017)
- Added IsInTheSameGroupCallbacks and FormatDisplayFieldCallbacks to JqGridGroupingView (Work Item #13018)
- Add support for choosing search operators in toolbar searching (Work Item #13019)
- Added MultiSort to JqGridOptions/JqGridHelper (Work Item #13020)
- Added HeaderTitles to JqGridOptions/JqGridHelper (Work Item #13027)
- Upgrade to ASP.NET MVC 4 and added NuGet package dependencies (Work Item #13046)

## Lib.Web.Mvc 6.3.4
### Bug Fixes
- Fix for JqGridColumnEditableAttribute.PostDataScript rendering (Work Item #13093)

## Lib.Web.Mvc 6.3.3
### Bug Fixes
- Fix for System.Web.HttpException in RangeFileResult when range request is being canceled (Work Item #13084)

## Lib.Web.Mvc 6.3.2
### Bug Fixes
- Fix for "Operation could destabilize the runtime" error on .Net Framework 4.5 without KB2748645 (Work Item #13052)
- Fix for JqGridInlineNavigatorActionOptions.ExtraParamScript rendering (Work Item #13053)

## Lib.Web.Mvc 6.3.1
### Bug Fixes
- Fix for Nu (Is null) and Nn (Is not null) operators deserialization in single and toolbar searching (Work Item #13030)
Additions and Changes
- Added NullOperators value to JqGridSearchOperators

## Lib.Web.Mvc 6.3.0
### Additions and Changes 
- Added RowAttributes to JqGridOptions/JqGridHelper (Work Item #12880)
- Added OnInitGrid to JqGridOptions/JqGridHelper (Work Item #12883)
- Added DataWidth to JqGridNavigatorActionOptions (Work Item #12882)
- Changed GetValuesAsList and GetValuesAsDictionary methods of JqGridRecord/JqGridRecord<TModel> visibility to protected internal (Work Item #12879)
- Added PostData and PostDataScript to JqGridColumnEditOptions/JqGridColumnEditableAttribute (Work Item #12881)

## Lib.Web.Mvc 6.2.1
### Bug Fixes
- Fix for built-in formatters support in configuration import/export functionality

## Lib.Web.Mvc 6.2.0
### Bug Fixes
- Fix for JqGridInlineNavigatorAddActionOptions.InitData rendering
Additions and Changes 
- Added support for System.Byte[] and System.Data.Linq.Binary types in models - values are being send as Base64 strings (Work Item #12511)
- Added support for TimespanAttribute - columns with this attribute are hidden but will be posted while editing (Work Item #12511)
- Added support for templates in advanced and advanced with gropus searching through JqGridNavigatorSearchActionOptions.Templates property (Work Item #12540)
- Added support for providing values for EditType = JqGridColumnEditTypes.Select and SearchType = JqGridColumnSearchTypes by setting type and method which will be called at runtime (Work Item #11950)
- Added IgnoreCase to JqGridOptions/JqGridHelper (Work Item #12711)
- Added jQuery UI integrations (Work Item #12812)

## Lib.Web.Mvc 6.1.0
### Additions and Changes 
- Added HideInDialog property to JqGridColumnLayoutAttribute and JqGridColumnModel (Work Item #12390)
- Added ExtraParamScript property to JqGridInlineNavigatorActionOptions
- Added ExtraDataScript property to JqGridNavigatorModifyActionOptions
- Added Nu (Is null) and Nn (Is not null) values to JqGridSearchOperators (Work Item #12478)
- Added Sortable to JqGridOptions/JqGridHelper
- Added GridView to JqGridOptions/JqGridHelper (Work Item #12485)
- Added some jqGrid limitations checks
- Added support for MethodType and RestoreAfterError in inlineEditingOptions parameter of JqGridHelper.AddActionsColumn method (Work Item #11984)

## Lib.Web.Mvc 6.0.1
### Bug Fixes
- Fix for "Subgrid as Grid" scenario URL issue (Work Item #12078)

## Lib.Web.Mvc 6.0.0
### Bug Fixes
- Fix for bug in Toolbar and Custom searching options rendering.
- Fix for JqGridColumnElementAttribute trying to resolve DataUrl while there is no current HttpContext (Work Item #11799)
Additions and Changes
- Removed SubgridOptions from JqGridOptions/JqGridHelper, added subgridHelper parameter to JqGridHelper constructor for "Subgrid as Grid" scenario support (Work Item #11763)
- Added SortType and SortFunction properties to JqGridColumnSortableAttribute and JqGridColumnModel (Work Item #11798)
- Added JqGridSearchOperators.EqualOrNotEqual, JqGridSearchOperators.TextOperators and JqGridSearchOperators.NoTextOperators (Work Item #11710)
- Added AcceptAjaxRequestAttribute, AjaxRequestAttribute, NoAjaxRequestAttribute
- Added EmptyRecords to JqGridOptions/JqGridHelper (Work Item #11884)
- Added AfterRedraw, OnReset, OnSearch, ShowOnLoad, SearchOperator, ErrorCheck and Layer to JqGridNavigatorSearchActionOptions (Work Item #11883)

## Lib.Web.Mvc 5.0.2
### Bug Fixes
- Fix for JqGridHelper.AddActionsColumn to make the column not sortable, viewable and searchable (Work Item #11864)

## Lib.Web.Mvc 5.0.1
### Bug Fixes
- Fix for JqGridResponse.Reader (proper cloning of JqGridResponse.JsonReader)
- Fix for JqGridRequestModelBinder deserializing JqGridRequest.SearchingFilter (Work Item #11833)

## Lib.Web.Mvc 5.0.0
### Additions and Changes
- Refactored form editing related API (Work Item #11685, Work Item #11712)
- Added AddActionsColumn method to JqGridHelper (Work Item #11711)
- Added SubgridOptions to JqGridOptions/JqGridHelper for "Subgrid as Grid" scenario support (Work Item #11536)
- Added PostData and PostDataScript to JqGridOptions/JqGridHelper (Work Item #11540)

## Lib.Web.Mvc 4.4.0
### Bug Fixes
- Fix for JqGridColumnElementAttribute.DataEvents related bug (Work Item #11646)
Additions and Changes
- Added JqGridGroupHeader class and SetGroupHeaders method to JqGridHelper (Work Item #11537)
- Added Frozen property to JqGridColumnLayoutAttribute/JqGridColumnModel and SetFrozenColumns method to JqGridHelper (Work Item #11538)
- Added JqGridInlineNavigatorOptions, JqGridInlineNavigatorActionOptions and JqGridInlineNavigatorAddActionOptions classes as well as InlineNavigator method to JqGridHelper (Work Item #11539)

## Lib.Web.Mvc 4.3.1
### Bug Fixes
- Fix for JqGridColumnEditableAttribute.DateFormat related bug
- Fix for DisplayAttribute.Order support

## Lib.Web.Mvc 4.3.0
### Additions and Changes
- Added CellAttributes, Title and Viewable properties to JqGridColumnLayoutAttribute/JqGridColumnModel (Work Item #11441)
- Added DateFormat property to JqGridColumnElementAttribute/JqGridColumnModel
- Added JqGridColumnMappingAttribute as well as JsonMapping, Key and XmlMapping properties to JqGridColumnModel
- Added TopPager to JqGridOptions/JqGridHelper, Pager property to JqGridNavigatorOptions and CloneToTop property to JqGridNavigatorControlOptions
- Added MultiKey, MultiBoxOnly, MultiSelect and MultiSelectWidth to JqGridOptions/JqGridHelper (Work Item #11427)
- Added RowsNumbers and RowsNumbersWidth to JqGridOptions/JqGridHelper (Work Item #11427)

## Lib.Web.Mvc 4.2.0
### Bug Fixes
- Fix for bug in JqGridResponse (missing groups when using advanced searching with AdvancedSearchingWithGroups = true) (Work Item #11409)
Additions and Changes
- Added AltClass, AltRows, AutoEncode, CellLayout, DeepEmpty, Direction, LoadOnce and ShrinkToFit to JqGridOptions/JqGridHelper (Work Item #11403, Work Item #11408)
- Added Id, PagerId and FilterGridId properties to JqGridHelper (Work Item #11404)
- Added support for setting editoptions.value and searchoptions.value as string (Work Item #11410)

## Lib.Web.Mvc 4.1.1
### Bug Fixes
- Fix for minValue and maxValue rules rendering

## Lib.Web.Mvc 4.1.0
### Additions and Changes
- Extended JqGridHelper support for standard jqGrid events 
- Extended JqGridHelper support for jqGrid subgrid related events 
- Extended JqGridHelper support for jqGrid cell editing related events 
- Added RangeFileResult, RangeFilePathResult, RangeFileStreamResult, RangeFileContentResult

## Lib.Web.Mvc 4.0.0
### Bug Fixes
- Fix for JqGridHelper (JqGridColumnEditOptions rendering)
- Fix for JqGridHelper (JqGridColumnFormatterOptions rendering)

### Additions and Changes
- Refactored JqGridColumnSortableAttribute
- Refactored JqGridColumnLayoutAttribute
- Refactored JqGridColumnFormatterAttribute
- Extended JqGridColumnEditOptions and JqGridColumnEditableAttribute (extended support for editing configuration)
- JqGridRequest properties setters visibility has been changed to public (Work Item #11224)
- Added DataUrlRouteValues to JqGridColumnSearchableAttribute and JqGridColumnEditableAttribute
  which (when overridden in derived class) may provide additional values for select element request route (Work Item #11238)
- Added JqGridRequest.ParameterNames
- Added JqGridJsonReader, JqGridOptions.JsonReader, JqGridResponse.JsonReader and JqGridResponse.Reader (Work Item #11202)
- Added JqGridOptions.RowsList (Work Item #11288)

## Lib.Web.Mvc 3.4
### Bug Fixes
- Fix for JqGridHelper (JqGridColumnRules rendering)
- Fix for JqGridHelper (JqGridColumnSearchOptions rendering)

### Additions and Changes
- Added JqGridOptions.FooterEnabled and JqGridOptions.UserDataOnFooter properties (support for the footer)
- Added JqGridHelper.SetFooterData method (support for the footer)
- Added JqGridResponse.UserData (support for the footer)
- Added JqGridColumnDataEvent class (extended support for searching configuration)
- Extended JqGridColumnSearchOptions (extended support for searching configuration)
- Added JqGridColumnModel.SearchRules (extended support for searching configuration)
- Added JqGridOptions.AutoWidth (Work Item #11186)
- Added JqGridColumnLabelAttribute (Work Item #11176)

## Lib.Web.Mvc 3.3
### Bug Fixes
- Fix for JqGridHelper (JqGridOptions rendering)

### Additions and Changes
- Added JqGridOptions.Hidden and JqGridOptions.HiddenEnabled properties (support for the caption layer)
- Added JqGridDynamicScrollingModes, JqGridParametersNames, JqGridOptions.DynamicScrollingMode, JqGridOptions.DynamicScrollingTimeout and JqGridOptions.ParametersNames (support for the dynamic scrolling)
- Added JqGridGroupingView, JqGridColumnSummaryTypes, JqGridColumnSummaryAttribute, JqGridColumnModel.SummaryType, JqGridColumnModel.SummaryTemplate, JqGridColumnModel.SummaryFunction, JqGridOptions.GroupingEnabled, JqGridOptions.GroupingView (support for grouping)

## Lib.Web.Mvc 3.2
### Bug Fixes
- Fix for JqGridHelper (JqGridNavigatorOptions rendering)
- Fix for JqGridHelper (JqGridNavigatorActionOptions rendering)
- Fix for JqGridHelper (JqGridNavigatorSearchActionOptions rendering)

### Additions and Changes
- Added JqGridOptions.Caption property (thanks to Robin Riem)
- Added JqGridOptions.GridComplete property
- Added JqGridOptions.LoadError property
- Added JqGridOptions.LoadComplete property
- Added JqGridNavigatorActionOptions.AfterSubmit property (thanks to Robin Riem)
- Added JqGridHelper.AddNavigatorButton method and JqGridNavigatorButtonOptions class (support for jqGrid navButtonAdd method)
- Added JqGridHelper.AddNavigatorSeparator method and JqGridNavigatorSeparatorOptions class (support for jqGrid navSeparatorAdd method)

## Lib.Web.Mvc 3.1
### Bug Fixes
- Fix for JqGridColumnEditableAttribute (Setting FormOptions properties)
- Fix for JqGridColumnModel (Getting FormOptions from Metadata)
- Fix for JqGridColumnModel (Getting Index from Metadata)
- Fix for JqGridHelper (JqGridNavigatorActionOptions rendering)

### Additions and Changes
- Added JqGridNavigatorActionOptions.OnInitializeForm
- Added JqGridNavigatorActionOptions.OnBeforeShowForm
- Added JqGridNavigatorActionOptions.OnAfterShowForm
