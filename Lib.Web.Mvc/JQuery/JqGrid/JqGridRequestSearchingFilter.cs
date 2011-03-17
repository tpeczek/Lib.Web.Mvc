using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents filter in request from jqGrid.
    /// </summary>
    public class JqGridRequestSearchingFilter
    {
        #region Properties
        /// <summary>
        /// Gets the searching column name.
        /// </summary>
        public string SearchingName { get; set; }

        /// <summary>
        /// Gets the searching value.
        /// </summary>
        public string SearchingValue { get; set; }

        /// <summary>
        /// Gets the searching operator.
        /// </summary>
        public JqGridSearchOperators SearchingOperator { get; set; }
        #endregion
    }
}
