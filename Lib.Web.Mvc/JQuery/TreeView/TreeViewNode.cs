using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib.Web.Mvc.JQuery.TreeView
{
    /// <summary>
    /// Class which represents node for jQuery TreeView plugin.
    /// </summary>
    public class TreeViewNode
    {
        #region Properties
        /// <summary>
        /// Gets or sets the node identifier
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the node text
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// Gets or sets whether node is expanded
        /// </summary>
        public bool expanded { get; set; }

        /// <summary>
        /// Gets or sets whether node has children
        /// </summary>
        public bool hasChildren { get; set; }

        /// <summary>
        /// Gets or sets additional CSS classes for node
        /// </summary>
        public string classes { get; set; }

        /// <summary>
        /// Gets or sets node childrens
        /// </summary>
        public TreeViewNode[] children { get; set; }
        #endregion
    }
}