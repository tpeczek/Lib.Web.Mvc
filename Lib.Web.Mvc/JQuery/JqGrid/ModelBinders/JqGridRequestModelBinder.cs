using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Lib.Web.Mvc.JQuery.JqGrid.ModelBinders
{
    internal class JqGridRequestModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException(nameof(controllerContext));
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            JqGridRequest model = new JqGridRequest();

            model.Searching = false;
            if (!string.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.Searching))
            {
                ValueProviderResult searchingResult = bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.Searching);
                if (searchingResult != null)
                    model.Searching = (bool)searchingResult.ConvertTo(typeof(bool));
            }

            model.SearchingFilter = null;
            model.SearchingFilters = null;
            if (model.Searching)
            {
                ValueProviderResult searchingFiltersResult = bindingContext.ValueProvider.GetValue("filters");
                if (!string.IsNullOrWhiteSpace(searchingFiltersResult?.AttemptedValue))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    serializer.RegisterConverters(new JavaScriptConverter[] { new Serialization.JqGridScriptConverter() });
                    model.SearchingFilters = serializer.Deserialize<JqGridRequestSearchingFilters>((string)searchingFiltersResult.ConvertTo(typeof(string)));
                }
                else
                {
                    string searchingName = string.Empty;
                    ValueProviderResult searchingNameResult = bindingContext.ValueProvider.GetValue("searchField");
                    if (searchingNameResult != null)
                        searchingName = (string)searchingNameResult.ConvertTo(typeof(string));

                    string searchingValue = string.Empty;
                    ValueProviderResult searchingValueResult = bindingContext.ValueProvider.GetValue("searchString");
                    if (searchingValueResult != null)
                        searchingValue = (string)searchingValueResult.ConvertTo(typeof(string));

                    JqGridSearchOperators searchingOperator = JqGridSearchOperators.Eq;
                    ValueProviderResult searchingOperatorResult = bindingContext.ValueProvider.GetValue("searchOper");
                    if (searchingOperatorResult != null)
                        Enum.TryParse((string)searchingOperatorResult.ConvertTo(typeof(string)), true, out searchingOperator);

                    if (!string.IsNullOrWhiteSpace(searchingName) && (!string.IsNullOrWhiteSpace(searchingValue) || ((searchingOperator & JqGridSearchOperators.NullOperators) != 0)))
                        model.SearchingFilter = new JqGridRequestSearchingFilter() { SearchingName = searchingName, SearchingOperator = searchingOperator, SearchingValue = searchingValue };
                }
            }

            if (!string.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.SortingName))
            {
                ValueProviderResult sortingName = bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.SortingName);
                if (!string.IsNullOrWhiteSpace(sortingName?.AttemptedValue))
                    model.SortingName = (string)sortingName.ConvertTo(typeof(string));
            }

            if (!string.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.SortingOrder))
            {
                ValueProviderResult sortingOrder = bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.SortingOrder);
                if (!string.IsNullOrWhiteSpace(sortingOrder?.AttemptedValue))
                    model.SortingOrder = (JqGridSortingOrders)sortingOrder.ConvertTo(typeof(JqGridSortingOrders));
            }

            if (!string.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.PageIndex))
                model.PageIndex = (int)bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.PageIndex).ConvertTo(typeof(int)) - 1;
            else
                model.PageIndex = 0;

            model.PagesCount = null;
            if (!string.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.PagesCount))
            {
                ValueProviderResult pagesCountValueResult = bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.PagesCount);
                if (pagesCountValueResult != null)
                    model.PagesCount = (int)pagesCountValueResult.ConvertTo(typeof(int));
            }

            if (!string.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.RecordsCount))
                model.RecordsCount = (int)bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.RecordsCount).ConvertTo(typeof(int));

            return model;
        }
    }
}
