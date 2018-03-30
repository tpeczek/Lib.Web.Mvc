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

			var model = new JqGridRequest();

			model.Searching = false;
			if (!string.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.Searching))
			{
				var searchingResult = bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.Searching);
				if (searchingResult != null)
					model.Searching = (bool)searchingResult.ConvertTo(typeof(bool));
			}

			model.SearchingFilter = null;
			model.SearchingFilters = null;
			if (model.Searching)
			{
				var searchingFiltersResult = bindingContext.ValueProvider.GetValue("filters");
				if (!string.IsNullOrWhiteSpace(searchingFiltersResult?.AttemptedValue))
				{
					var serializer = new JavaScriptSerializer();
					serializer.RegisterConverters(new JavaScriptConverter[] { new Serialization.JqGridScriptConverter() });
					model.SearchingFilters = serializer.Deserialize<JqGridRequestSearchingFilters>((string)searchingFiltersResult.ConvertTo(typeof(string)));
				}
				else
				{
					var searchingName = string.Empty;
					var searchingNameResult = bindingContext.ValueProvider.GetValue("searchField");
					if (searchingNameResult != null)
						searchingName = (string)searchingNameResult.ConvertTo(typeof(string));

					var searchingValue = string.Empty;
					var searchingValueResult = bindingContext.ValueProvider.GetValue("searchString");
					if (searchingValueResult != null)
						searchingValue = (string)searchingValueResult.ConvertTo(typeof(string));

					var searchingOperator = JqGridSearchOperators.Eq;
					var searchingOperatorResult = bindingContext.ValueProvider.GetValue("searchOper");
					if (searchingOperatorResult != null)
						Enum.TryParse((string)searchingOperatorResult.ConvertTo(typeof(string)), true, out searchingOperator);

					if (!string.IsNullOrWhiteSpace(searchingName) && (!string.IsNullOrWhiteSpace(searchingValue) || ((searchingOperator & JqGridSearchOperators.NullOperators) != 0)))
						model.SearchingFilter = new JqGridRequestSearchingFilter() { SearchingName = searchingName, SearchingOperator = searchingOperator, SearchingValue = searchingValue };
				}
			}

			if (!string.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.SortingName))
			{
				var sortingName = bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.SortingName);
				if (!string.IsNullOrWhiteSpace(sortingName?.AttemptedValue))
					model.SortingName = (string)sortingName.ConvertTo(typeof(string));
			}

			if (!string.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.SortingOrder))
			{
				var sortingOrder = bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.SortingOrder);
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
				var pagesCountValueResult = bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.PagesCount);
				if (pagesCountValueResult != null)
					model.PagesCount = (int)pagesCountValueResult.ConvertTo(typeof(int));
			}

			if (!string.IsNullOrWhiteSpace(JqGridRequest.ParameterNames.RecordsCount))
				model.RecordsCount = (int)bindingContext.ValueProvider.GetValue(JqGridRequest.ParameterNames.RecordsCount).ConvertTo(typeof(int));

			return model;
		}
	}
}
