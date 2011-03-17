using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the sorting options for column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class JqGridColumnSortableAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Gets the value defining if this column can be sorted.
        /// </summary>
        public bool Sortable { get; private set; }

        /// <summary>
        /// Gets or sets the index name for sorting and searching.
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the sorting order for first column sorting (default JqGridSortingOrders.Asc).
        /// </summary>
        public JqGridSortingOrders InitialSortingOrder { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnSortingNameAttribute class.
        /// </summary>
        /// <param name="sortable">If this column can be sorted</param>
        public JqGridColumnSortableAttribute(bool sortable)
        {
            Sortable = sortable;
            InitialSortingOrder = JqGridSortingOrders.Asc;
        }
        #endregion
    }
}
