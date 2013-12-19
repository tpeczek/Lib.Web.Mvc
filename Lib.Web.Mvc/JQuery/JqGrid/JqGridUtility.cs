using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data;
using System.Data.Linq;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    internal static class JqGridUtility
    {
        #region Methods
        internal static bool IsValidForColumn(this ModelMetadata metadata)
        {
            return
                metadata.ShowForDisplay
                && metadata.ModelType != typeof(EntityState)
                && (!metadata.IsComplexType || metadata.ModelType == typeof(Binary) || metadata.ModelType == typeof(byte[]));
        }
        #endregion
    }
}
