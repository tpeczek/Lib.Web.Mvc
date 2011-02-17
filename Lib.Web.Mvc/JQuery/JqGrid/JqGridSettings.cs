using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Runtime.Serialization;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// jqGrid settings container
    /// Description for every option is available here:
    /// http://www.trirand.com/jqgridwiki/doku.php?id=wiki:options
    /// Not all jqGrid options are included in this class (it can be extended)
    /// </summary>
    [DataContract]
    public sealed class JqGridSettings
    {
        #region Properties
        [DataMember(EmitDefaultValue=false)]
        public string url { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string height { get; set; }

        [DataMember]
        public int rowNum { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string pager { get; set; }

        [DataMember]
        public bool pgbuttons { get; set; }

        [DataMember]
        public bool pginput { get; set; }

        [DataMember]
        public JqGridColumnModel[] colModel { get; set; }

        [DataMember]
        public string[] colNames { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string sortorder { get; set; }

		[DataMember(EmitDefaultValue = false)]
        public string sortname { get; set; }

		[DataMember(EmitDefaultValue = false)]
        public string datatype { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string mtype { get; set; }

        [DataMember]
        public bool viewrecords { get; set; }

		[DataMember]
        public int[] remapColumns { get; set; }
		
		[DataMember(EmitDefaultValue = false)]
        public string width { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; }
        #endregion

        #region Constructor
        public JqGridSettings()
        {
            sortorder = "asc";
            rowNum = 20;
            pgbuttons = true;
            pginput = true;
            viewrecords = false;
        }
        #endregion
    }
}
