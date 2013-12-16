using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// An interface representing helper class for generating jqGrid HMTL and JavaScript.
    /// </summary>
    public interface IJqGridHelper
    {
        #region Properties
        /// <summary>
        /// Gets the grid identifier.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the grid pager identifier.
        /// </summary>
        string PagerId { get; }

        /// <summary>
        /// Gets the filter grid (div) placeholder identifier.
        /// </summary>
        string FilterGridId { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the HTML that is used to render the table placeholder for the grid. 
        /// </summary>
        /// <returns>The HTML that represents the table placeholder for jqGrid</returns>
        MvcHtmlString GetTableHtml();

        /// <summary>
        /// Returns the HTML that is used to render the pager (div) placeholder for the grid. 
        /// </summary>
        /// <returns>The HTML that represents the pager (div) placeholder for jqGrid</returns>
        MvcHtmlString GetPagerHtml();

        /// <summary>
        /// Returns the HTML that is used to render the filter grid (div) placeholder for the grid. 
        /// </summary>
        /// <returns>The HTML that represents the filter grid (div) placeholder for jqGrid</returns>
        MvcHtmlString GetFilterGridHtml();

        /// <summary>
        /// Returns the HTML that is used to render the table placeholder for the grid with pager placeholder below it and filter grid (if enabled) placeholder above it.
        /// </summary>
        /// <returns>The HTML that represents the table placeholder for jqGrid with pager placeholder below i</returns>
        MvcHtmlString GetHtml();

        /// <summary>
        /// Return the JavaScript that is used to initialize jqGrid with given options.
        /// </summary>
        /// <returns>The JavaScript that initializes jqGrid with give options</returns>
        /// <exception cref="System.InvalidOperationException">
        /// <list type="bullet">
        /// <item><description>TreeGrid and data grouping are both enabled.</description></item>
        /// <item><description>Rows numbers and data grouping are both enabled.</description></item>
        /// <item><description>Dynamic scrolling and data grouping are both enabled.</description></item>
        /// <item><description>TreeGrid and GridView are both enabled.</description></item>
        /// <item><description>SubGrid and GridView are both enabled.</description></item>
        /// <item><description>AfterInsertRow event and GridView are both enabled.</description></item>
        /// </list> 
        /// </exception>
        MvcHtmlString GetJavaScript();
        #endregion
    }
}
