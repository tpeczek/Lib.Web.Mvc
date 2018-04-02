﻿using System.Collections.Generic;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <inheritdoc />
    /// <summary>
    /// Class which represents options for jqGrid editable column
    /// </summary>
    public class JqGridColumnEditOptions : JqGridColumnElementOptions
    {

        #region Fields
        private string _customElementFunction;
        private string _customValueFunction;
        private string _postDataScript;
        private object _postData;

        #endregion

        #region Properties

        internal bool IsCustomElementFunctionSetted { get; private set; }
        /// <summary>
        /// Gets or sets the name of function which is used to create custom edit element
        /// </summary>
        public string CustomElementFunction
        {
            get => _customElementFunction;
            set
            {
                _customElementFunction = value;
                IsCustomElementFunctionSetted = true;
            }
        }

        internal bool IsCustomValueFunctionSetted { get; private set; }
        /// <summary>
        /// Gets or sets the name of function which should return the value from the custom element after the editing.
        /// </summary>
        public string CustomValueFunction
        {
            get => _customValueFunction;
            set
            {
                _customValueFunction = value;
                IsCustomValueFunctionSetted = true;
            }
        }

        /// <summary>z
        /// Gets or sets the value which defines if null value should be send to server if the field is empty.
        /// </summary>
        public bool? NullIfEmpty { get; set; }

        internal bool IsPostDataSetted { get; private set; }
        /// <summary>
        /// Gets or sets the additional data which will be added to the AJAX request to get the data for the select element (if EditType is JqGridColumnEditTypes.Select).
        /// </summary>
        public object PostData
        {
            get => _postData;
            set
            {
                _postData = value;
                IsPostDataSetted = true;
            }
        }

        internal bool IsPostDataScriptsSetted { get; private set; }
        /// <summary>
        /// Gets or sets the JavaScript which will dynamically generate the additional data which will be added to the AJAX request to get the data for the select element (if EditType is JqGridColumnEditTypes.Select). This property takes precedence over PostData.
        /// </summary>
        public string PostDataScript
        {
            get => _postDataScript;
            set
            {
                _postDataScript = value;
                IsPostDataScriptsSetted = true;
            }
        }

        /// <summary>
        /// Gets or sets the child property name if this element performs parent role in selects cascade.
        /// </summary>
        public string ChildName { get; set; }

        internal IList<JqGridColumnDataEvent> InternalDataEvents { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnEditOptions class.
        /// </summary>
        public JqGridColumnEditOptions()
        {
            DataEvents = null;
            HtmlAttributes = null;
        }
        #endregion

        #region Methods
        internal void ConfigureSelectsCascadeParent()
        {
            //Here be dragons
            StringBuilder changeDataEventBuilder = new StringBuilder();
            changeDataEventBuilder.Append("function() {{ var gridInstance = $('#{0}'); ");
            changeDataEventBuilder.AppendFormat("var parentElement = $(this); var inlineEditingChildElement = $('#' + parentElement.attr('id').substring(0, parentElement.attr('id').lastIndexOf('_' + parentElement.attr('name'))) + '_{0}'); var formEditingChildElement = $('#' + '{0}'); ", ChildName);
            changeDataEventBuilder.Append("var childColumnModel = null; var columnsModels = gridInstance.jqGrid ('getGridParam', 'colModel'); ");
            changeDataEventBuilder.AppendFormat("for (var i = 0; i < columnsModels.length; i++) {{{{ if (columnsModels[i].name === '{0}') {{{{ childColumnModel = columnsModels[i]; break; }}}} }}}} ", ChildName);
            changeDataEventBuilder.Append("var editOptions = $.extend({{}}, childColumnModel.editoptions || {{}} , {{ id: childColumnModel.name, name: childColumnModel.name }}); editOptions.postData = {{ parentVal: parentElement.val() }}; var childElement = null; ");
            changeDataEventBuilder.Append("if (inlineEditingChildElement.length === 1) {{ childElement = $.jgrid.createEl.call(gridInstance, childColumnModel.edittype, editOptions, '', true, $.extend({{}}, $.jgrid.ajaxOptions, gridInstance.jqGrid ('getGridParam', 'ajaxSelectOptions') || {{}})); $(childElement).addClass('editable'); inlineEditingChildElement.replaceWith(childElement); }} ");
            changeDataEventBuilder.Append("else if (formEditingChildElement.length === 1) {{ childElement = $.jgrid.createEl.call(gridInstance, childColumnModel.edittype, editOptions, '', false, $.extend({{}}, $.jgrid.ajaxOptions, gridInstance.jqGrid ('getGridParam', 'ajaxSelectOptions') || {{}})); $(childElement).addClass('FormElement ui-widget-content ui-corner-all'); formEditingChildElement.replaceWith(childElement); }} ");
            changeDataEventBuilder.Append("$.jgrid.bindEv.call(gridInstance, childElement, editOptions); $(childElement).change(); }}");

            if (InternalDataEvents == null)
                InternalDataEvents = new List<JqGridColumnDataEvent>();
            InternalDataEvents.Add(new JqGridColumnDataEvent("change", changeDataEventBuilder.ToString()));
        }
        #endregion
    }
}
