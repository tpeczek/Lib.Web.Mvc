using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// jqGrid colModel options
    /// Description for every option: http://www.trirand.com/jqgridwiki/doku.php?id=wiki:colmodel_options
    /// Not all jqGrid colModel options are included in this class (it can be extended)
    /// </summary>
    [DataContract]
    public sealed class JqGridColumnModel
    {
        #region Properties
        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string align { get; set; }

        [DataMember]
        public bool sortable { get; set; }

        [DataMember]
        public int width { get; set; }

        [DataMember]
        public bool hidden { get; set; }

        [DataMember]
        public bool resizable { get; set; }
        #endregion

        #region Constructor
        public JqGridColumnModel()
        {
            width = 150;
            sortable = true;
            hidden = false;
            resizable = true;
        }
        #endregion
    }
}
