using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid Inline Navigator add action.
    /// </summary>
    public class JqGridInlineNavigatorAddActionOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the id for the new row
        /// </summary>
        public string RowId { get; set; }

        /// <summary>
        /// Gets or sets the initial values for the new row.
        /// </summary>
        public object InitData { get; set; }

        /// <summary>
        /// Gets or sets the new row position.
        /// </summary>
        public JqGridNewRowPositions Position { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if the DefaultValue from ColumnsModel should be used.
        /// </summary>
        public bool UseDefaultValues { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if formatters should be used.
        /// </summary>
        public bool UseFormatter { get; set; }

        /// <summary>
        /// Gets or sets edit options.
        /// </summary>
        public JqGridInlineNavigatorActionOptions Options { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridInlineNavigatorAddActionOptions class.
        /// </summary>
        public JqGridInlineNavigatorAddActionOptions()
        {
            RowId = JqGridNavigatorDefaults.NewRowId;
            Position = JqGridNewRowPositions.First;
            UseDefaultValues = false;
            UseFormatter = false;
            Options = null;
        }
        #endregion
    }
}
