using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    internal class JqGridColumnLabelOptions
    {
        #region Properties
        internal string Label { get; set; }

        internal string Class { get; set; }

        internal IDictionary<string, object> CssStyles { get; set; }

        internal IDictionary<string, object> HtmlAttributes { get; set; }
        #endregion

        #region Constructor
        internal JqGridColumnLabelOptions()
        {
            Label = String.Empty;
            Class = String.Empty;
            CssStyles = null;
            HtmlAttributes = null;
        }
        #endregion
    }
}
