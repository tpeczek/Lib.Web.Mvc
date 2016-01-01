using System.Linq;
using System.Web.Mvc;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// jqGrid options
    /// </summary>
    /// <typeparam name="TModel">Type of model for grid</typeparam>
    public sealed class JqGridOptions<TModel> : JqGridOptions
    {
        #region Properties
        internal ModelMetadata JqGridModelMetadata { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridOptions class.
        /// </summary>
        /// <param name="id">Identifier, which will be used for table (id='{0}'), pager div (id='{0}Pager'), filter grid div (id='{0}Search') and in JavaScript.</param>
        public JqGridOptions(string id)
            : base (id)
        {
            JqGridModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(TModel));
            foreach (ModelMetadata propertyMetadata in JqGridModelMetadata.Properties.Where(p => p.IsValidForColumn()))
            {
                JqGridColumnModel columnModel = new JqGridColumnModel(propertyMetadata);
                ColumnsModels.Add(columnModel);
                ColumnsNames.Add(propertyMetadata.GetDisplayName());
            }
        }
        #endregion
    }
}
