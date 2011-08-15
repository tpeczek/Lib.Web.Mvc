using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents data event for jqGrid editable or searchable column
    /// </summary>
    public class JqGridColumnDataEvent
    {
        #region Properties
        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Gets the additional data for the event.
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// Gets the function which will be called on the event.
        /// </summary>
        public string Function { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnDataEvent class.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="function">The function which will be called on the event.</param>
        /// <param name="data">The additional (optional) data for the event.</param>
        public JqGridColumnDataEvent(string type, string function, object data = null)
        {
            if (String.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException("type");

            if (String.IsNullOrWhiteSpace(function))
                throw new ArgumentNullException("function");

            Type = type;
            Function = function;
            Data = data;
        }
        #endregion
    }
}
