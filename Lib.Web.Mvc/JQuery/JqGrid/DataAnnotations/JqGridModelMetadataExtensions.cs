using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations
{
    internal static class JqGridModelMetadataExtensions
    {
        #region Fields
        private const string SummaryTypeKey = "JqGridColumnModel.SummaryType";
        private const string SummaryTemplateKey = "JqGridColumnModel.SummaryTemplate";
        private const string SummaryFunctionKey = "JqGridColumnModel.SummaryFunction";
        #endregion

        #region Properties
        internal static void SetColumnSummaryType(this ModelMetadata metadata, JqGridColumnSummaryTypes type)
        {
            metadata.AdditionalValues.Add(SummaryTypeKey, type);
        }

        internal static JqGridColumnSummaryTypes? GetColumnSummaryType(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(SummaryTypeKey))
                return (JqGridColumnSummaryTypes)metadata.AdditionalValues[SummaryTypeKey];
            else
                return null;
        }

        internal static void SetColumnSummaryTemplate(this ModelMetadata metadata, string template)
        {
            metadata.AdditionalValues.Add(SummaryTemplateKey, template);
        }

        internal static string GetColumnSummaryTemplate(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(SummaryTemplateKey))
                return (string)metadata.AdditionalValues[SummaryTemplateKey];
            else
                return null;
        }

        internal static void SetColumnSummaryFunction(this ModelMetadata metadata, string function)
        {
            metadata.AdditionalValues.Add(SummaryFunctionKey, function);
        }

        internal static string GetColumnSummaryFunction(this ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(SummaryFunctionKey))
                return (string)metadata.AdditionalValues[SummaryFunctionKey];
            else
                return null;
        }
        #endregion
    }
}
