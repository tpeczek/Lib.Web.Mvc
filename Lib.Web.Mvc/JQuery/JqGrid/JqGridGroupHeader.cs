using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid grouping header.
    /// </summary>
    public class JqGridGroupHeader
    {
        #region Properties
        /// <summary>
        /// Gets the name from colModel from which the grouping header begin, including the same field.
        /// </summary>
        public string StartColumn { get; private set; }

        /// <summary>
        /// Gets the number of columns which are included for this group.
        /// </summary>
        public int NumberOfColumns { get; private set; }

        /// <summary>
        /// Gets the text for this group (can contain HTML tags).
        /// </summary>
        public string Title { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridGroupHeader class.
        /// <param name="startColumn">The name from colModel from which the grouping header begin, including the same field.</param>
        /// <param name="numberOfColumns">The number of columns which are included for this group.</param>
        /// <param name="title">The text for this group (can contain HTML tags).</param>
        /// </summary>
        public JqGridGroupHeader(string startColumn, int numberOfColumns, string title = "")
        {
            if (String.IsNullOrWhiteSpace(startColumn))
                throw new ArgumentNullException("startColumn");
            StartColumn = startColumn;

            if (numberOfColumns <= 0)
                throw new ArgumentOutOfRangeException("numberOfColumns");
            NumberOfColumns = numberOfColumns;

            Title = title;
        }
        #endregion
    }
}
