using Lib.Web.Mvc.JQuery.JqGrid.Constants;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents names for jqGrid request parameters.
    /// </summary>
    public class JqGridParametersNames
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name for page index parametr.
        /// </summary>
        public string PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the name for records count parametr.
        /// </summary>
        public string RecordsCount { get; set; }

        /// <summary>
        /// Gets or sets the name for sorting name parametr.
        /// </summary>
        public string SortingName { get; set; }

        /// <summary>
        /// Gets or sets the name for sorting order parametr.
        /// </summary>
        public string SortingOrder { get; set; }

        /// <summary>
        /// Gets or sets the name for searching parametr.
        /// </summary>
        public string Searching { get; set; }

        /// <summary>
        /// Gets or sets the name for id parametr.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name for operator parametr.
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Gets or sets the name for edit operator parametr.
        /// </summary>
        public string EditOperator { get; set; }

        /// <summary>
        /// Gets or sets the name for add operator parametr.
        /// </summary>
        public string AddOperator { get; set; }

        /// <summary>
        /// Gets or sets the name for delete operator parametr.
        /// </summary>
        public string DeleteOperator { get; set; }

        /// <summary>
        /// Gets or sets the name for subgrid id parametr.
        /// </summary>
        public string SubgridId { get; set; }

        /// <summary>
        /// Gets or sets the name for pages count parametr.
        /// </summary>
        public string PagesCount { get; set; }

        /// <summary>
        /// Gets or sets the name for total rows parametr.
        /// </summary>
        public string TotalRows { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridParametersNames class.
        /// </summary>
        public JqGridParametersNames()
        {
            PageIndex = JqGridOptionsDefaults.RequestPageIndex;
            RecordsCount = JqGridOptionsDefaults.RequestRecordsCount;
            SortingName = JqGridOptionsDefaults.RequestSortingName;
            SortingOrder = JqGridOptionsDefaults.RequestSortingOrder;
            Searching = JqGridOptionsDefaults.RequestSearching;
            Id = JqGridOptionsDefaults.RequestId;
            Operator = JqGridOptionsDefaults.RequestOperator;
            EditOperator = JqGridOptionsDefaults.RequestEditOperator;
            AddOperator = JqGridOptionsDefaults.RequestAddOperator;
            DeleteOperator = JqGridOptionsDefaults.RequestDeleteOperator;
            SubgridId = JqGridOptionsDefaults.RequestSubgridId;
            PagesCount = null;
            TotalRows = JqGridOptionsDefaults.RequestTotalRows;
        }
        #endregion

        #region Methods
        internal bool IsDefault()
        {
            return PageIndex == JqGridOptionsDefaults.RequestPageIndex
                   && RecordsCount == JqGridOptionsDefaults.RequestRecordsCount
                   && SortingName == JqGridOptionsDefaults.RequestSortingName
                   && SortingOrder == JqGridOptionsDefaults.RequestSortingOrder
                   && Searching == JqGridOptionsDefaults.RequestSearching
                   && Id == JqGridOptionsDefaults.RequestId
                   && Operator == JqGridOptionsDefaults.RequestOperator
                   && EditOperator == JqGridOptionsDefaults.RequestEditOperator
                   && AddOperator == JqGridOptionsDefaults.RequestAddOperator
                   && DeleteOperator == JqGridOptionsDefaults.RequestDeleteOperator
                   && SubgridId == JqGridOptionsDefaults.RequestSubgridId
                   && string.IsNullOrWhiteSpace(PagesCount)
                   && TotalRows == JqGridOptionsDefaults.RequestTotalRows;
        }

        internal bool IsGlobal => PageIndex == JqGridRequest.ParameterNames.PageIndex
                                  && RecordsCount == JqGridRequest.ParameterNames.RecordsCount
                                  && SortingName == JqGridRequest.ParameterNames.SortingName
                                  && SortingOrder == JqGridRequest.ParameterNames.SortingOrder
                                  && Searching == JqGridRequest.ParameterNames.Searching
                                  && Id == JqGridRequest.ParameterNames.Id
                                  && Operator == JqGridRequest.ParameterNames.Operator
                                  && EditOperator == JqGridRequest.ParameterNames.EditOperator
                                  && AddOperator == JqGridRequest.ParameterNames.AddOperator
                                  && DeleteOperator == JqGridRequest.ParameterNames.DeleteOperator
                                  && SubgridId == JqGridRequest.ParameterNames.SubgridId
                                  && PagesCount == JqGridRequest.ParameterNames.PagesCount
                                  && TotalRows == JqGridRequest.ParameterNames.TotalRows;
        #endregion
    }
}
