using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Mvc;
using Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;

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
        /// Gets or sets the function which can add attributes to the cell during the creation of the data (dynamically).
        /// </summary>
        public string CellAttributes { get; set; }

        /// <summary>
        /// Gets or sets additional CSS classes for the column (separated by space).
        /// </summary>
        public string Classes { get; set; }

        /// <summary>
        /// Gets or sets the expected date format for this column in case of date validation (default ISO date). 
        /// </summary>
        public string DateFormat { get; set; }

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
        /// Gets or sets the value which defines if column shouldn't scroll out of view when user is moving horizontally across the grid.
        /// </summary>
        public bool Frozen { get; set; }

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
        /// Gets or sets the value which defines if this column will appear in the modal dialog where users can choose which columns to show or hide.
        /// </summary>
        public bool HideInDialog { get; set; }

        /// <summary>
        /// Gets or sets the sorting order for first column sorting.
        /// </summary>
        public JqGridSortingOrders InitialSortingOrder { get; set; }

        /// <summary>
        /// Gets or sets the JSON mapping for the column in the incoming JSON string.
        /// </summary>
        public string JsonMapping { get; set; }

        /// <summary>
        /// Defines if this column value should be used as unique row id (in case there is no id from the server). 
        /// </summary>
        public bool Key { get; set; }

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
        /// Gets or sets the type of the column for appropriate sorting when datatype is local.
        /// </summary>
        public JqGridColumnSortTypes SortType { get; set; }

        /// <summary>
        /// Gets or sets the custom sorting function when the SortType is set to JqGridColumnSortTypes.Function.
        /// </summary>
        public string SortFunction { get; set; }

        /// <summary>
        /// Gets or sets the index name for sorting and searching (default String.Empty)
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// Gets the unique name for the column.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the value which defines if the title should be displayed in the column when user hovers a cell with the mouse.
        /// </summary>
        public bool Title { get; set; }

        /// <summary>
        /// Gets or sets the custom function to "unformat" a value of the cell when used in editing or client-side sorting
        /// </summary>
        public string UnFormatter { get; set; }

        /// <summary>
        /// Gets or sets the initial width in pixels of the column (default 150).
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if the column should appear in view form.
        /// </summary>
        public bool Viewable { get; set; }

        /// <summary>
        /// Gets or sets the XML mapping for the column in the incomming XML file.
        /// </summary>
        public string XmlMapping { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnModel class.
        /// </summary>
        /// <param name="name">The unique name for the column.</param>
        public JqGridColumnModel(string name)
        {
            Alignment = JqGridAlignments.Left;
            CellAttributes = null;
            DateFormat = JqGridOptionsDefaults.DateFormat;
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
            HideInDialog = false;
            Index = String.Empty;
            JsonMapping = null;
            Key = false;
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
            SortType = JqGridColumnSortTypes.Text;
            SortFunction = String.Empty;
            Title = true;
            UnFormatter = String.Empty;
            Width = 150;
            Viewable = true;
            XmlMapping = null;
        }

        internal JqGridColumnModel(ModelMetadata propertyMetadata)
            : this(propertyMetadata.PropertyName)
        {
            IEnumerable<object> customAttributes  = propertyMetadata.ContainerType.GetProperty(propertyMetadata.PropertyName).GetCustomAttributes(true).AsEnumerable();

            TimestampAttribute timeStampAttribute = customAttributes.OfType<TimestampAttribute>().FirstOrDefault();
            if (timeStampAttribute != null)
            {
                Editable = true;
                Hidden = true;
            }
            else
            {
                RangeAttribute rangeAttribute = customAttributes.OfType<RangeAttribute>().FirstOrDefault();

                Alignment = propertyMetadata.GetColumnAlignment();
                CellAttributes = propertyMetadata.GetColumnCellAttributes();
                Classes = propertyMetadata.GetColumnClasses();
                DateFormat = propertyMetadata.GetColumnDateFormat();
                Fixed = propertyMetadata.GetColumnFixed();
                Frozen = propertyMetadata.GetColumnFrozen();
                Key = propertyMetadata.GetColumnKey();
                Resizable = propertyMetadata.GetColumnResizable();
                Title = propertyMetadata.GetColumnTitle();
                Width = propertyMetadata.GetColumnWidth();
                Viewable = propertyMetadata.GetColumnViewable();

                Editable = propertyMetadata.GetColumnEditable();
                EditOptions = propertyMetadata.GetColumnEditOptions();
                if (EditOptions != null)
                {
                    StringLengthAttribute stringLengthAttribute = customAttributes.OfType<StringLengthAttribute>().FirstOrDefault();
                    if (stringLengthAttribute != null)
                    {
                        if (EditOptions.HtmlAttributes == null)
                            EditOptions.HtmlAttributes = new Dictionary<string, object>();

                        if (EditOptions.HtmlAttributes.ContainsKey("maxlength"))
                            EditOptions.HtmlAttributes["maxlength"] = stringLengthAttribute.MaximumLength;
                        else
                            EditOptions.HtmlAttributes.Add("maxlength", stringLengthAttribute.MaximumLength);
                    }
                }
                EditRules = propertyMetadata.GetColumnEditRules();
                if (EditRules != null)
                {
                    RequiredAttribute requiredAttribute = customAttributes.OfType<RequiredAttribute>().FirstOrDefault();
                    if (requiredAttribute != null)
                        EditRules.Required = true;

                    if (rangeAttribute != null)
                    {
                        EditRules.MaxValue = Convert.ToDouble(rangeAttribute.Maximum);
                        EditRules.MinValue = Convert.ToDouble(rangeAttribute.Minimum);
                    }
                }
                EditType = propertyMetadata.GetColumnEditType();

                Formatter = propertyMetadata.GetColumnFormatter();
                FormatterOptions = propertyMetadata.GetColumnFormatterOptions();
                UnFormatter = propertyMetadata.GetColumnUnFormatter();

                FormOptions = propertyMetadata.GetColumnFormOptions();

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
                SortType = propertyMetadata.GetColumnSortType();
                SortFunction = propertyMetadata.GetColumnSortFunction();
                Index = propertyMetadata.GetColumnIndex();

                SummaryType = propertyMetadata.GetColumnSummaryType();
                SummaryTemplate = propertyMetadata.GetColumnSummaryTemplate();
                SummaryFunction = propertyMetadata.GetColumnSummaryFunction();

                if (!String.IsNullOrWhiteSpace(propertyMetadata.TemplateHint) && propertyMetadata.TemplateHint.Equals("HiddenInput"))
                    Hidden = true;
                else
                    Hidden = false;
                HideInDialog = propertyMetadata.GetColumnHideInDialog();
            }

            JsonMapping = propertyMetadata.GetColumnJsonMapping();
            XmlMapping = propertyMetadata.GetColumnXmlMapping();
        }
        #endregion
    }
}
