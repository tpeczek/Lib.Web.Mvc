using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents options for form editing button icon.
    /// </summary>
    public class JqGridFormButtonIcon
    {
        #region Properties
        /// <summary>
        /// Gets or sets the value indicating if icon is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the icon position (relative to the text).
        /// </summary>
        public JqGridFormButtonIconPositions Position { get; set; }

        /// <summary>
        /// Gets or sets the jQuery UI icon class (ui-icon-*).
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Gets the predefined close icon.
        /// </summary>
        public static JqGridFormButtonIcon CloseIcon { get { return new JqGridFormButtonIcon() { Enabled = true, Position = JqGridFormButtonIconPositions.Left, Class = "ui-icon-close" }; } }

        /// <summary>
        /// Gets the predefined cancel icon.
        /// </summary>
        public static JqGridFormButtonIcon CancelIcon { get { return new JqGridFormButtonIcon() { Enabled = true, Position = JqGridFormButtonIconPositions.Left, Class = "ui-icon-cancel" }; } }

        /// <summary>
        /// Gets the predefined delete icon.
        /// </summary>
        public static JqGridFormButtonIcon DeleteIcon { get { return new JqGridFormButtonIcon() { Enabled = true, Position = JqGridFormButtonIconPositions.Left, Class = "ui-icon-delete" }; } }

        /// <summary>
        /// Gets the predefined save icon.
        /// </summary>
        public static JqGridFormButtonIcon SaveIcon { get { return new JqGridFormButtonIcon() { Enabled = true, Position = JqGridFormButtonIconPositions.Left, Class = "ui-icon-disk" }; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridFormButtonIcon class.
        /// </summary>
        public JqGridFormButtonIcon()
        { }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            JqGridFormButtonIcon objCasted = (JqGridFormButtonIcon)obj;
            return (Enabled == objCasted.Enabled) && (Position == objCasted.Position) && (Class == objCasted.Class);
        }
        #endregion
    }
}
