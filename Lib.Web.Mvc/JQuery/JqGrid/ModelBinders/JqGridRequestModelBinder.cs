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
            ValueProviderResult searchingResult = bindingContext.ValueProvider.GetValue("_search");
            if (searchingResult != null)
                model.Searching = (bool)searchingResult.ConvertTo(typeof(Boolean));

            model.SearchingFilter = null;
            model.SearchingFilters = null;
            if (model.Searching)
            {
                ValueProviderResult searchingFiltersResult = bindingContext.ValueProvider.GetValue("filters");
                if (searchingFiltersResult != null)
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

                    if (!String.IsNullOrWhiteSpace(searchingName) && !String.IsNullOrWhiteSpace(searchingValue))
                        model.SearchingFilter = new JqGridRequestSearchingFilter() { SearchingName = searchingName, SearchingOperator = searchingOperator, SearchingValue = searchingValue };
                }
            }

            model.SortingName = (string)bindingContext.ValueProvider.GetValue("sidx").ConvertTo(typeof(String));
            model.SortingOrder = (JqGridSortingOrders)bindingContext.ValueProvider.GetValue("sord").ConvertTo(typeof(JqGridSortingOrders));
            model.PageIndex = (int)bindingContext.ValueProvider.GetValue("page").ConvertTo(typeof(Int32)) - 1;

            ValueProviderResult pagesCountValueResult = bindingContext.ValueProvider.GetValue("npage");
            if (pagesCountValueResult != null)
                model.PagesCount = (int)pagesCountValueResult.ConvertTo(typeof(Int32));
            else
                model.PagesCount = null;
            
                model.RecordsCount = (int)bindingContext.ValueProvider.GetValue("rows").ConvertTo(typeof(Int32));

            return model;
        }
    }
}
