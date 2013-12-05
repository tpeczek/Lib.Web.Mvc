using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Lib.Web.Mvc.JQuery.JqGrid.ModelBinders
{
    internal class JqGridRequestModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");
            if (bindingContext == null)
                throw new ArgumentNullException("bindingContext");

            JqGridRequest model = new JqGridRequest();

            model.Searching = false;
            if (!String.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.Searching))
            {
                ValueProviderResult searchingResult = bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.Searching);
                if (searchingResult != null)
                    model.Searching = (bool)searchingResult.ConvertTo(typeof(Boolean));
            }

            model.SearchingFilter = null;
            model.SearchingFilters = null;
            if (model.Searching)
            {
                ValueProviderResult searchingFiltersResult = bindingContext.ValueProvider.GetValue("filters");
                if (searchingFiltersResult != null && !String.IsNullOrWhiteSpace(searchingFiltersResult.AttemptedValue))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    serializer.RegisterConverters(new JavaScriptConverter[] { new Lib.Web.Mvc.JQuery.JqGrid.Serialization.JqGridScriptConverter() });
                    model.SearchingFilters = serializer.Deserialize<JqGridRequestSearchingFilters>((string)searchingFiltersResult.ConvertTo(typeof(String)));
                }
                else
                {
                    string searchingName = String.Empty;
                    ValueProviderResult searchingNameResult = bindingContext.ValueProvider.GetValue("searchField");
                    if (searchingNameResult != null)
                        searchingName = (string)searchingNameResult.ConvertTo(typeof(String));

                    string searchingValue = String.Empty;
                    ValueProviderResult searchingValueResult = bindingContext.ValueProvider.GetValue("searchString");
                    if (searchingValueResult != null)
                        searchingValue = (string)searchingValueResult.ConvertTo(typeof(String));

                    JqGridSearchOperators searchingOperator = JqGridSearchOperators.Eq;
                    ValueProviderResult searchingOperatorResult = bindingContext.ValueProvider.GetValue("searchOper");
                    if (searchingOperatorResult != null)
                        Enum.TryParse<JqGridSearchOperators>((string)searchingOperatorResult.ConvertTo(typeof(String)), true, out searchingOperator);

                    if (!String.IsNullOrWhiteSpace(searchingName) && (!String.IsNullOrWhiteSpace(searchingValue) || ((searchingOperator & JqGridSearchOperators.NullOperators) != 0)))
                        model.SearchingFilter = new JqGridRequestSearchingFilter() { SearchingName = searchingName, SearchingOperator = searchingOperator, SearchingValue = searchingValue };
                }
            }

            if (!String.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.SortingName))
            {
                ValueProviderResult sortingName = bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.SortingName);
                if (sortingName != null && !String.IsNullOrWhiteSpace(sortingName.AttemptedValue))
                    model.SortingName = (string)sortingName.ConvertTo(typeof(String));
            }

            if (!String.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.SortingOrder))
            {
                ValueProviderResult sortingOrder = bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.SortingOrder);
                if (sortingOrder != null && !String.IsNullOrWhiteSpace(sortingOrder.AttemptedValue))
                    model.SortingOrder = (JqGridSortingOrders)sortingOrder.ConvertTo(typeof(JqGridSortingOrders));
            }

            if (!String.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.PageIndex))
                model.PageIndex = (int)bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.PageIndex).ConvertTo(typeof(Int32)) - 1;
            else
                model.PageIndex = 0;

            model.PagesCount = null;
            if (!String.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.PagesCount))
            {
                ValueProviderResult pagesCountValueResult = bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.PagesCount);
                if (pagesCountValueResult != null)
                    model.PagesCount = (int)pagesCountValueResult.ConvertTo(typeof(Int32));
            }

            if (!String.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.RecordsCount))
                model.RecordsCount = (int)bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.RecordsCount).ConvertTo(typeof(Int32));

            return model;
        }
    }
}
