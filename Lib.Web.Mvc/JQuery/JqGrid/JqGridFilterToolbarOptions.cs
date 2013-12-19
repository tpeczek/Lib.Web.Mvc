using Lib.Web.Mvc.JQuery.JqGrid.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for jqGrid filter toolbar.
    /// </summary>
    public class JqGridFilterToolbarOptions : JqGridFilterOptions
    {
        #region Fields
        private JqGridSearchOperators _defaultSearchOperator;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the default search operator for all fields (it will be overriden by JqGridColumnSearchOptions.SearchOperators).
        /// </summary>
        public JqGridSearchOperators DefaultSearchOperator
        {
            get { return _defaultSearchOperator; }

            set
            {
                if (value == JqGridSearchOperators.EqualOrNotEqual || value == JqGridSearchOperators.NoTextOperators || value == JqGridSearchOperators.NullOperators || value == JqGridSearchOperators.TextOperators)
                    throw new ArgumentException("DefaultSearchOperator must be a simple operator", "DefaultSearchOperator");
                _defaultSearchOperator = value;
            }
        }

        /// <summary>
        /// Gets or sets the grouping operator for filters.
        /// </summary>
        public JqGridSearchGroupingOperators GroupingOperator { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if the user needs to press Enter key in text input to trigger search or if searching should triggered after typing any character.
        /// </summary>
        public bool SearchOnEnter { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if user is allowed to select operation when searching.
        /// </summary>
        public bool SearchOperators { get; set; }

        /// <summary>
        /// Gets or sets the tooltip for the operation button.
        /// </summary>
        public string OperandToolTip { get; set; }

        /// <summary>
        /// Gets or sets the short texts for search operators which are dispalyed to the user when a operation button is clicked.
        /// </summary>
        public Dictionary<JqGridSearchOperators, string> Operands { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if filters should be posted as JSON string (the same as in advanced searching) or as key:value pairs.
        /// </summary>
        public bool StringResult { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridJsonResult class.
        /// </summary>
        public JqGridFilterToolbarOptions()
        {
            DefaultSearchOperator = JqGridSearchOperators.Bw;
            GroupingOperator = JqGridSearchGroupingOperators.Or;
            SearchOnEnter = true;
            SearchOperators = false;
            OperandToolTip = JqGridFilterToolbarDefaults.OperandToolTip;
            Operands = null;
            StringResult = false;
        }
        #endregion
    }
}
