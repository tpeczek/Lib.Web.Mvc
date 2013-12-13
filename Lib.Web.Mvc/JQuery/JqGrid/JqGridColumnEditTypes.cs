using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Defines available types of editable fields for jqGrid
    /// </summary>
    public enum JqGridColumnEditTypes
    {
        /// <summary>
        /// Input element of type text.
        /// </summary>
        Text,
        /// <summary>
        /// Input element of type textarea.
        /// </summary>
        TextArea,
        /// <summary>
        /// Select element
        /// </summary>
        Select,
        /// <summary>
        /// Input element of type checkbox
        /// </summary>
        CheckBox,
        /// <summary>
        /// Input element of type password
        /// </summary>
        Password,
        /// <summary>
        /// Input element of type button
        /// </summary>
        Button,
        /// <summary>
        /// Input element of type image
        /// </summary>
        Image,
        /// <summary>
        /// Input element of type file
        /// </summary>
        File,
        /// <summary>
        /// Custom editable element
        /// </summary>
        Custom,
        /// <summary>
        /// jQuery UI Autocomplete widget
        /// </summary>
        JQueryUIAutocomplete,
        /// <summary>
        /// jQuery UI Datepicker widget
        /// </summary>
        JQueryUIDatepicker,
        /// <summary>
        /// jQuery UI Spinner widget
        /// </summary>
        JQueryUISpinner,
        /// <summary>
        /// Select element performing parent role in selects cascade
        /// </summary>
        /// <remarks>
        /// This value is only valid in case of form and inline editing.
        /// </remarks>
        SelectsCascadeParent
    }
}
