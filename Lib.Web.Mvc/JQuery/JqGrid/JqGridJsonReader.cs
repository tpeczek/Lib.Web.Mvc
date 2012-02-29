using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib.Web.Mvc.JQuery.JqGrid.Constants;

namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Class which represents JSON reader for jqGrid records.
    /// </summary>
    public class JqGridJsonRecordsReader
    {
        #region Fields
        private string _records;
        private string _recordValues;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of an array that contains the actual data.
        /// </summary>
        public string Records
        {
            get { return _records; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value");
                _records = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of an array that contains record values.
        /// </summary>
        public string RecordValues
        {
            get { return _recordValues; }
            set { _recordValues = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating if the information for the data in the row is repeatable.
        /// </summary>
        public bool RepeatItems { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridJsonRecordsReader class.
        /// </summary>
        public JqGridJsonRecordsReader()
        {
            Records = JqGridOptionsDefaults.ResponseRecords;
            RecordValues = JqGridOptionsDefaults.ResponseRecordValues;
            RepeatItems = true;
        }
        #endregion

        #region Methods
        internal virtual bool IsDefault()
        {
            return ((Records == JqGridOptionsDefaults.ResponseRecords) && (RecordValues == JqGridOptionsDefaults.ResponseRecordValues) && RepeatItems);
        }
        #endregion
    }

    /// <summary>
    /// Class which represents JSON reader for jqGrid.
    /// </summary>
    public class JqGridJsonReader : JqGridJsonRecordsReader, ICloneable
    {
        #region Fields
        private string _pageIndex;
        private string _recordId;
        private string _totalPagesCount;
        private string _totalRecordsCount;
        private string _userData;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of a field that contains the current page index.
        /// </summary>
        public string PageIndex
        {
            get { return _pageIndex; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value");
                _pageIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of a field that contains record identifier.
        /// </summary>
        public string RecordId
        {
            get { return _recordId; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value");
                _recordId = value;
            }
        }

        /// <summary>
        /// Gets the settings for jqGrid subgrid JSON reader.
        /// </summary>
        public JqGridJsonRecordsReader SubgridReader { get; private set; }

        /// <summary>
        /// Gets or sets the name of a field that contains total pages count.
        /// </summary>
        public string TotalPagesCount
        {
            get { return _totalPagesCount; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value");
                _totalPagesCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of a field that contains total records count.
        /// </summary>
        public string TotalRecordsCount
        {
            get { return _totalRecordsCount; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value");
                _totalRecordsCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of an array that contains custom data.
        /// </summary>
        public string UserData
        {
            get { return _userData; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value");
                _userData = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridJsonReader class.
        /// </summary>
        public JqGridJsonReader() : base()
        {
            PageIndex = JqGridOptionsDefaults.ResponsePageIndex;
            RecordId = JqGridOptionsDefaults.ResponseRecordId;
            SubgridReader = new JqGridJsonRecordsReader();
            TotalPagesCount = JqGridOptionsDefaults.ResponseTotalPagesCount;
            TotalRecordsCount = JqGridOptionsDefaults.ResponseTotalRecordsCount;
            UserData = JqGridOptionsDefaults.ResponseUserData;
        }
        #endregion

        #region Methods
        internal new bool IsDefault()
        {
            return (base.IsDefault() && (PageIndex == JqGridOptionsDefaults.ResponsePageIndex) && (RecordId == JqGridOptionsDefaults.ResponseRecordId) && (TotalPagesCount == JqGridOptionsDefaults.ResponseTotalPagesCount) && (TotalRecordsCount == JqGridOptionsDefaults.ResponseTotalRecordsCount) && (UserData == JqGridOptionsDefaults.ResponseUserData));
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Creates a new object that is a shallow copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a shallow copy of this instance.</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion
    }
}
