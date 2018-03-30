using System;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the sorting options for column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class JqGridColumnSortableAttribute : Attribute, IMetadataAware
    {
        #region Fields
        private string _sortFunction;
        private string _index;

        #endregion

        #region Properties
        /// <summary>
        /// Gets the value defining if this column can be sorted.
        /// </summary>
        public bool Sortable { get; }

        /// <summary>
        /// Gets or sets the type of the column for appropriate sorting when datatype is local.
        /// </summary>
        public JqGridColumnSortTypes SortType { get; set; }

        internal bool IsSortFunctionSetted { get; private set; }
        /// <summary>
        /// Gets or sets the custom sorting function when the SortType is set to JqGridColumnSortTypes.Function.
        /// </summary>
        public string SortFunction
        {
            get => _sortFunction;
            set
            {
                _sortFunction = value;
                IsSortFunctionSetted = true;
            }
        }

        internal bool IsIndexSetted { get; set; }
        /// <summary>
        /// Gets or sets the index name for sorting and searching.
        /// </summary>
        public string Index
        {
            get => _index;
            set
            {
                _index = value;
                IsIndexSetted = true;
            }
        }

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
        public JqGridColumnSortableAttribute(bool sortable = true)
        {
            Sortable = sortable;
            SortType = JqGridColumnSortTypes.Default;
            InitialSortingOrder = JqGridSortingOrders.Default;
        }
        #endregion

        #region IMetadataAware
        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.SetColumnIndex(new SettedString(IsIndexSetted, Index));
            metadata.SetColumnInitialSortingOrder(InitialSortingOrder);
            metadata.SetColumnSortable(Sortable);
            metadata.SetColumnSortType(SortType);
            metadata.SetColumnSortFunction(new SettedString(IsSortFunctionSetted, SortFunction));
        }
        #endregion
    }
}
