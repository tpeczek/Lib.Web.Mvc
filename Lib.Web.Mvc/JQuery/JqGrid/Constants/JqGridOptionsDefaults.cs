using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid.Constants
{
    /// <summary>
    /// Contains default values for jqGrid options
    /// </summary>
    public static class JqGridOptionsDefaults
    {
        /// <summary>
        /// The name of an array that contains the actual data.
        /// </summary>
        public const string ResponseRecords = "rows";

        /// <summary>
        /// The name for page index parametr.
        /// </summary>
        public const string ResponsePageIndex = "page";

        /// <summary>
        /// The name of a field that contains total pages count.
        /// </summary>
        public const string ResponseTotalPagesCount = "total";

        /// <summary>
        /// The name of a field that contains total records count.
        /// </summary>
        public const string ResponseTotalRecordsCount = "records";

        /// <summary>
        /// The name of an array that contains custom data.
        /// </summary>
        public const string ResponseUserData = "userdata";

        /// <summary>
        /// The name of a field that contains record identifier.
        /// </summary>
        public const string ResponseRecordId = "id";

        /// <summary>
        /// The name of an array that contains record values.
        /// </summary>
        public const string ResponseRecordValues = "cell";

        /// <summary>
        /// The name for page index parametr.
        /// </summary>
        public const string RequestPageIndex = "page";

        /// <summary>
        /// The name for records count parametr.
        /// </summary>
        public const string RequestRecordsCount = "rows";

        /// <summary>
        /// The name for sorting name parametr.
        /// </summary>
        public const string RequestSortingName = "sidx";

        /// <summary>
        /// The name for sorting order parametr.
        /// </summary>
        public const string RequestSortingOrder = "sord";

        /// <summary>
        /// The name for searching parametr.
        /// </summary>
        public const string RequestSearching = "_search";

        /// <summary>
        /// The name for id parametr.
        /// </summary>
        public const string RequestId = "id";

        /// <summary>
        /// The name for operator parametr.
        /// </summary>
        public const string RequestOperator = "oper";

        /// <summary>
        /// The name for edit operator parametr.
        /// </summary>
        public const string RequestEditOperator = "edit";

        /// <summary>
        /// The name for add operator parametr.
        /// </summary>
        public const string RequestAddOperator = "add";

        /// <summary>
        /// The name for delete operator parametr.
        /// </summary>
        public const string RequestDeleteOperator = "del";

        /// <summary>
        /// The name for subgrid id parametr.
        /// </summary>
        public const string RequestSubgridId = "id";

        /// <summary>
        /// The name for total rows parametr.
        /// </summary>
        public const string RequestTotalRows = "totalrows";

        /// <summary>
        /// The icon (form UI theme images) that will be used if the group is collapsed.
        /// </summary>
        public const string GroupingPlusIcon = "ui-icon-circlesmall-plus";

        /// <summary>
        /// The icon (form UI theme images) that will be used if the group is expanded.
        /// </summary>
        public const string GroupingMinusIcon = "ui-icon-circlesmall-minus";

        /// <summary>
        /// The decimal places for formatter.
        /// </summary>
        public const int FormatterDecimalPlaces = 2;

        /// <summary>
        /// The decimal separator for formatter.
        /// </summary>
        public const string FormatterDecimalSeparator = ".";

        /// <summary>
        /// The date source format for formatter.
        /// </summary>
        public const string FormatterSourceFormat = "Y-m-d";

        /// <summary>
        /// The date output format for formatter.
        /// </summary>
        public const string FormatterOutputFormat = "n/j/Y";

        /// <summary>
        /// The thousands separator for formatter.
        /// </summary>
        public const string FormatterThousandsSeparator = " ";

        /// <summary>
        /// The first parameter that is added after the ShowAction.
        /// </summary>
        public const string FormatterIdName = "id";

        /// <summary>
        /// The default value for integer formatter.
        /// </summary>
        public const string IntegerFormatterDefaultValue = "0";

        /// <summary>
        /// The default value for number formatter.
        /// </summary>
        public const string NumberFormatterDefaultValue = "0.00";

        /// <summary>
        /// The default value for currency formatter.
        /// </summary>
        public const string CurrencyFormatterDefaultValue = "0.00";

        /// <summary>
        /// The class that is used for alternate rows.
        /// </summary>
        public const string  AltClass = "ui-priority-secondary";

        /// <summary>
        /// The padding plus border width of the cell.
        /// </summary>
        public const int CellLayout = 5;

        /// <summary>
        /// The ISO date format.
        /// </summary>
        public const string DateFormat = "Y-m-d";

        /// <summary>
        /// The information to be displayed when the returned (or the current) number of records is zero.
        /// </summary>
        public const string EmptyRecords = "No records to view";
    }
}
