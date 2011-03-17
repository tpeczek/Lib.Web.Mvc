using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the layout attributes of grid column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class JqGridColumnLayoutAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Gets the alignment of the cell in the grid body layer.
        /// </summary>
        public JqGridAlignments Alignment { get; private set; }

        /// <summary>
        /// Gets or sets additional CSS classes for the column (separated by space).
        /// </summary>
        public string Classes { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if internal recalculation of the width of the column is disabled (default false).
        /// </summary>
        public bool Fixed { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if column can be resized (default true).
        /// </summary>
        public bool Resizable { get; set; }

        /// <summary>
        /// Gets or sets the initial width in pixels of the column (default 150).
        /// </summary>
        public int Width { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnLayoutAttribute class.
        /// </summary>
        /// <param name="alignment">The alignment of the cell in the grid body layer</param>
        public JqGridColumnLayoutAttribute(JqGridAlignments alignment)
        {
            Alignment = alignment;
            Fixed = false;
            Resizable = true;
            Width = 150;
        }
        #endregion
    }
}
