using System;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the layout attributes of grid column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class JqGridColumnLayoutAttribute : Attribute, IMetadataAware
    {

        #region Fields
        private string _cellAttributes;
        private string _classes;
        private bool? _fixed;
        private bool? _frozen;
        private bool? _hideInDialog;
        private bool? _resizable;
        private bool? _title;
        private bool? _viewable;
        private int? _width;

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the alignment of the cell in the grid body layer.
        /// </summary>
        public JqGridAlignments Alignment { get; set; }

        internal bool IsCellAttriburesSetted { get; set; }

        /// <summary>
        /// Gets or sets the function which can add attributes to the cell during the creation of the data (dynamically).
        /// </summary>
        public string CellAttributes
        {
            get => _cellAttributes;
            set
            {
                _cellAttributes = value;
                IsCellAttriburesSetted = true;
            }
        }

        internal bool IsClassesSetted { get; private set; }
        /// <summary>
        /// Gets or sets additional CSS classes for the column (separated by space).
        /// </summary>
        public string Classes
        {
            get => _classes;
            set
            {
                _classes = value;
                IsClassesSetted = true;
            }
        }


        /// <summary>
        /// Gets or sets the value which defines if internal recalculation of the width of the column is disabled (default false).
        /// </summary>
        public bool Fixed
        {
            get => _fixed ?? false;
            set => _fixed = value;
        }


        /// <summary>
        /// Gets or sets the value indicating if column shouldn't scroll out of view when user is moving horizontally across the grid.
        /// </summary>
        public bool Frozen
        {
            get => _frozen ?? false;
            set => _frozen = value;
        }

        /// <summary>
        /// Gets or sets the value which defines if column will appear in the modal dialog where users can choose which columns to show or hide.
        /// </summary>
        public bool HideInDialog
        {
            get => _hideInDialog ?? false;
            set => _hideInDialog = value;
        }

        /// <summary>
        /// Gets or sets the value which defines if column can be resized (default true).
        /// </summary>
        public bool Resizable
        {
            get => _resizable ?? true;
            set => _resizable = value;
        }

        /// <summary>
        /// Gets or sets the value which defines if the title should be displayed in the column when user hovers a cell with the mouse.
        /// </summary>
        public bool Title
        {
            get => _title ?? true;
            set => _title = value;
        }

        /// <summary>
        /// Gets or sets the initial width in pixels of the column (default 150).
        /// </summary>
        public int Width
        {
            get => _width ?? 150;
            set => _width = value;
        }

        /// <summary>
        /// Gets or sets the value which defines if the column should appear in view form.
        /// </summary>
        public bool Viewable
        {
            get => _viewable ?? true;
            set => _viewable = value;
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnLayoutAttribute class.
        /// </summary>
        public JqGridColumnLayoutAttribute()
        {
            Alignment = JqGridAlignments.Default;
        }
        #endregion

        #region IMetadataAware
        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            if (Alignment != JqGridAlignments.Default) metadata.SetColumnAlignment(Alignment);
            metadata.SetColumnCellAttributes(new SettedString(IsCellAttriburesSetted, CellAttributes));
            metadata.SetColumnClasses(new SettedString(IsClassesSetted, Classes));
            metadata.SetColumnFixed(_fixed);
            metadata.SetColumnFrozen(_frozen);
            metadata.SetColumnHideInDialog(_hideInDialog);
            metadata.SetColumnResizable(_resizable);
            metadata.SetColumnTitle(_title);
            metadata.SetColumnWidth(_width);
        }
        #endregion
    }
}
