using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents subgrid model for jqGrid.
    /// </summary>
    /// <typeparam name="TModel">Type of model for grid</typeparam>
    public class JqGridSubgridModel<TModel> : JqGridSubgridModel
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridSubgridModel class.
        /// </summary>
        public JqGridSubgridModel()
            : base()
        {
            ModelMetadata modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(TModel));
            foreach (ModelMetadata propertyMetadata in modelMetadata.Properties.Where(p => p.ShowForDisplay && !p.IsComplexType))
            {
                IEnumerable<object> customAttributes = propertyMetadata.ContainerType.GetProperty(propertyMetadata.PropertyName).GetCustomAttributes(true).AsEnumerable();

                JqGridColumnLayoutAttribute columnLayoutAttribute = customAttributes.OfType<JqGridColumnLayoutAttribute>().FirstOrDefault();
                if (columnLayoutAttribute != null)
                {
                    ColumnsAlignments.Add(columnLayoutAttribute.Alignment);
                    ColumnsWidths.Add(columnLayoutAttribute.Width);
                }
                else
                {
                    ColumnsAlignments.Add(JqGridAlignments.Left);
                    ColumnsWidths.Add(150);
                }

                ColumnsNames.Add(propertyMetadata.GetDisplayName());
            }
        }
        #endregion
    }
}
