using System.Collections.Generic;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents subgrid model for jqGrid.
    /// </summary>
    public class JqGridSubgridModel
    {
        #region Properties
        /// <summary>
        /// Gets the list of columns names.
        /// </summary>
        public List<string> ColumnsNames { get; }

        /// <summary>
        /// Gets the list of columns alignments.
        /// </summary>
        public List<JqGridAlignments> ColumnsAlignments { get; }

        /// <summary>
        /// Gets the list of columns widths.
        /// </summary>
        public List<int> ColumnsWidths { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridSubgridModel class.
        /// </summary>
        public JqGridSubgridModel()
        {
            ColumnsNames = new List<string>();
            ColumnsAlignments = new List<JqGridAlignments>();
            ColumnsWidths = new List<int>();
        }
        #endregion
    }
}
