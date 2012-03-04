using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the sorting options for column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class JqGridColumnSortableAttribute : Attribute, IMetadataAware
    {
        #region Properties
        /// <summary>
        /// Gets the value defining if this column can be sorted.
        /// </summary>
        public bool Sortable { get; private set; }

        /// <summary>
        /// Gets or sets the type of the column for appropriate sorting when datatype is local.
        /// </summary>
        public JqGridColumnSortTypes SortType { get; set; }

        /// <summary>
        /// Gets or sets the custom sorting function when the SortType is set to JqGridColumnSortTypes.Function.
        /// </summary>
        public string SortFunction { get; set; }

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
            SortType = JqGridColumnSortTypes.Text;
            SortFunction = String.Empty;
            InitialSortingOrder = JqGridSortingOrders.Asc;
        }
        #endregion

        #region IMetadataAware
        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.SetColumnIndex(Index);
            metadata.SetColumnInitialSortingOrder(InitialSortingOrder);
            metadata.SetColumnSortable(Sortable);
            metadata.SetColumnSortType(SortType);
            metadata.SetColumnSortFunction(SortFunction);
        }
        #endregion
    }
}
