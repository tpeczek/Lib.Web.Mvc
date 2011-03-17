using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents request from jqGrid.
    /// </summary>
    [ModelBinder(typeof(ModelBinders.JqGridRequestModelBinder))]
    public class JqGridRequest
    {
        #region Properties
        /// <summary>
        /// Gets the value indicating searching.
        /// </summary>
        public bool Searching { get; internal set; }

        /// <summary>
        /// Gets the searching filter (single searching). 
        /// </summary>
        public JqGridRequestSearchingFilter SearchingFilter { get; set; }

        /// <summary>
        /// Gets the searching filters (advanced searching or toolbar searching with JqGridFilterToolbarOptions.StringResult = true). 
        /// </summary>
        public JqGridRequestSearchingFilters SearchingFilters { get; set; }

        /// <summary>
        /// Gets the sorting column name.
        /// </summary>
        public string SortingName { get; internal set; }

        /// <summary>
        /// Gets the sorting order
        /// </summary>
        public JqGridSortingOrders SortingOrder { get; internal set; }

        /// <summary>
        /// Gets the index (zero based) of page to return
        /// </summary>
        public int PageIndex { get; internal set; }

        /// <summary>
        /// Gets the number of rows to return
        /// </summary>
        public int RecordsCount { get; internal set; }
        #endregion
    }
}
