using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Mvc;
using Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// jqGrid column parameters description.
    /// </summary>
    public class JqGridColumnModel
    {
        #region Properties
        /// <summary>
        /// Gets or sets the alignment of the cell in the body layer (default JqGridAligns.Left)
        /// </summary>
        public JqGridAlignments Alignment { get; set; }

        /// <summary>
        /// Gets or sets additional CSS classes for the column (separated by space).
        /// </summary>
        public string Classes { get; set; }

        /// <summary>
        /// Gets or set the value defining if this column can be edited.
        /// </summary>
        public bool Editable { get; set; }

        /// <summary>
        /// Gets or sets the options for editable column.
        /// </summary>
        public JqGridColumnEditOptions EditOptions { get; set; }

        /// <summary>
        /// Gets or sets the rules for editable column.
        /// </summary>
        public JqGridColumnRules EditRules { get; set; }

        /// <summary>
        /// Gets or sets the type for editable column.
        /// </summary>
        public JqGridColumnEditTypes EditType { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if internal recalculation of the width of the column is disabled (default false).
        /// </summary>
        public bool Fixed { get; set; }

        /// <summary>
        /// Gets or sets the predefined formatter type ('' delimited string) or custom JavaScript formatting function name.
        /// </summary>
        public string Formatter { get; set; }

        /// <summary>
        /// Gets or sets the options for predefined formatter (every predefined formatter uses only a subset of all options), which are overwriting the defaults from the language file.
        /// </summary>
        public JqGridColumnFormatterOptions FormatterOptions { get; set; }

        /// <summary>
        /// Get or sets additional, used in form editing, options for editable column.
        /// </summary>
        public JqGridColumnFormOptions FormOptions { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if this column is hidden at initialization (default false).
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Gets or sets the sorting order for first column sorting.
        /// </summary>
        public JqGridSortingOrders InitialSortingOrder { get; set; }

        internal JqGridColumnLabelOptions LabelOptions { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if column can be resized (default true).
        /// </summary>
        public bool Resizable { get; set; }

        /// <summary>
        /// Gets or sets the value defining if this column can be searched.
        /// </summary>
        public bool Searchable { get; set; }

        /// <summary>
        /// Gets or sets the options for searchable column.
        /// </summary>
        public JqGridColumnSearchOptions SearchOptions { get; set; }

        /// <summary>
        /// Gets or sets the additional conditions for validating user input in search field.
        /// </summary>
        public JqGridColumnRules SearchRules { get; set; }

        /// <summary>
        /// Gets or sets the type of the search field for the column.
        /// </summary>
        public JqGridColumnSearchTypes SearchType { get; set; }

        /// <summary>
        /// Gets or sets the grouping summary type.
        /// </summary>
        public JqGridColumnSummaryTypes? SummaryType { get; set; }

        /// <summary>
        /// Gets or sets the grouping summary template.
        /// </summary>
        public string SummaryTemplate { get; set; }

        /// <summary>
        /// Gets or sets the grouping summary function for custom type.
        /// </summary>
        public string SummaryFunction { get; set; }

        /// <summary>
        /// Gets or sets the value defining if this column can be sorted.
        /// </summary>
        public bool Sortable { get; set; }

        /// <summary>
        /// Gets or sets the index name for sorting and searching (default String.Empty)
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// Gets the unique name for the column.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the custom function to "unformat" a value of the cell when used in editing or client-side sorting
        /// </summary>
        public string UnFormatter { get; set; }

        /// <summary>
        /// Gets or sets the initial width in pixels of the column (default 150).
        /// </summary>
        public int Width { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnModel class.
        /// </summary>
        /// <param name="name">The unique name for the column.</param>
        public JqGridColumnModel(string name)
        {
            Alignment = JqGridAlignments.Left;
            Editable = false;
            EditOptions = null;
            EditRules = null;
            EditType = JqGridColumnEditTypes.Text;
            Fixed = false;
            FormOptions = null;
            Formatter = String.Empty;
            FormatterOptions = null;
            InitialSortingOrder = JqGridSortingOrders.Asc;
            Hidden = false;
            Index = String.Empty;
            LabelOptions = null;
            Name = name;
            Resizable = true;
            Searchable = true;
            SearchOptions = null;
            SearchRules = null;
            SearchType = JqGridColumnSearchTypes.Text;
            SummaryType = null;
            SummaryTemplate = "{0}";
            SummaryFunction = null;
            Sortable = true;
            UnFormatter = String.Empty;
            Width = 150;
        }

        internal JqGridColumnModel(ModelMetadata propertyMetadata)
            : this(propertyMetadata.PropertyName)
        {
            IEnumerable<object> customAttributes  = propertyMetadata.ContainerType.GetProperty(propertyMetadata.PropertyName).GetCustomAttributes(true).AsEnumerable();
            RangeAttribute rangeAttribute = customAttributes.OfType<RangeAttribute>().FirstOrDefault();

            JqGridColumnLayoutAttribute columnLayoutAttribute = customAttributes.OfType<JqGridColumnLayoutAttribute>().FirstOrDefault();
            if (columnLayoutAttribute != null)
            {
                Alignment = columnLayoutAttribute.Alignment;
                Classes = columnLayoutAttribute.Classes;
                Fixed = columnLayoutAttribute.Fixed;
                Resizable = columnLayoutAttribute.Resizable;
                Width = columnLayoutAttribute.Width;
            }
            else
                Alignment = JqGridAlignments.Left;

            JqGridColumnFormatterAttribute columnFormatterAttribute = customAttributes.OfType<JqGridColumnFormatterAttribute>().FirstOrDefault();
            if (columnFormatterAttribute != null)
            {
                Formatter = columnFormatterAttribute.Formatter;
                FormatterOptions = columnFormatterAttribute.FormatterOptions;
                UnFormatter = columnFormatterAttribute.UnFormatter;
            }

            JqGridColumnEditableAttribute columnEditableAttribute = customAttributes.OfType<JqGridColumnEditableAttribute>().FirstOrDefault();
            if (columnEditableAttribute != null)
            {
                Editable = columnEditableAttribute.Editable;
                if (Editable)
                {
                    EditOptions = columnEditableAttribute.EditOptions;
                    EditRules = columnEditableAttribute.EditRules;
                    EditType = columnEditableAttribute.EditType;
                    FormOptions = columnEditableAttribute.FormOptions;

                    if ((propertyMetadata.ModelType == typeof(Int16)) || (propertyMetadata.ModelType == typeof(Int32)) || (propertyMetadata.ModelType == typeof(Int64)) || (propertyMetadata.ModelType == typeof(UInt16)) || (propertyMetadata.ModelType == typeof(UInt32)) || (propertyMetadata.ModelType == typeof(UInt32)))
                        EditRules.Integer = true;
                    else if ((propertyMetadata.ModelType == typeof(Decimal)) || (propertyMetadata.ModelType == typeof(Double)) || (propertyMetadata.ModelType == typeof(Single)))
                        EditRules.Number = true;

                    RequiredAttribute requiredAttribute = customAttributes.OfType<RequiredAttribute>().FirstOrDefault();
                    if (requiredAttribute != null)
                        EditRules.Required = true;

                    if (rangeAttribute != null)
                    {
                        EditRules.MaxValue = Convert.ToDouble(rangeAttribute.Maximum);
                        EditRules.MinValue = Convert.ToDouble(rangeAttribute.Minimum);
                    }

                    if (EditType == JqGridColumnEditTypes.Select)
                        EditOptions.DataUrl = columnEditableAttribute.DataUrl;

                    StringLengthAttribute stringLengthAttribute = customAttributes.OfType<StringLengthAttribute>().FirstOrDefault();
                    if (stringLengthAttribute != null)
                        EditOptions.MaximumLength = stringLengthAttribute.MaximumLength;
                }
            }

            LabelOptions = propertyMetadata.GetColumnLabelOptions();

            Searchable = propertyMetadata.GetColumnSearchable();
            SearchOptions = propertyMetadata.GetColumnSearchOptions();
            SearchRules = propertyMetadata.GetColumnSearchRules();
            if (SearchRules != null && rangeAttribute != null)
            {
                SearchRules.MaxValue = Convert.ToDouble(rangeAttribute.Maximum);
                SearchRules.MinValue = Convert.ToDouble(rangeAttribute.Minimum);
            }
            SearchType = propertyMetadata.GetColumnSearchType();

            InitialSortingOrder = propertyMetadata.GetColumnInitialSortingOrder();
            Sortable = propertyMetadata.GetColumnSortable();
            Index = propertyMetadata.GetColumnIndex();

            SummaryType = propertyMetadata.GetColumnSummaryType();
            SummaryTemplate = propertyMetadata.GetColumnSummaryTemplate();
            SummaryFunction = propertyMetadata.GetColumnSummaryFunction();

            if (!String.IsNullOrWhiteSpace(propertyMetadata.TemplateHint) && propertyMetadata.TemplateHint.Equals("HiddenInput"))
                Hidden = true;
            else
                Hidden = false;
        }
        #endregion
    }
}
