using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// Sends the contents of a binary file to the range response.
    /// </summary>
    public class RangeFileContentResult : RangeFileResult
    {
        #region Properties
        /// <summary>
        /// Gets the binary content to send to the response.
        /// </summary>
        public byte[] FileContents { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the RangeFileContentResult class.
        /// </summary>
        /// <param name="fileContents">The byte array to send to the response.</param>
        /// <param name="contentType">The content type to use for the response.</param>
        /// <param name="fileName">The file name to use for the response.</param>
        /// <param name="modificationDate">The file modification date to use for the response.</param>
        /**
         * <remarks>
         * The <paramref name="modificationDate"/> parameter is used internally while creating ETag and Last-Modified headers. Those headers might by used by client in order to verify that the same entity is being requested in separated partial requests and for caching purposes. Because of that it is important that the value passed to this parameter is consitant and reflects the actual state of entity during its entire lifetime.
         * </remarks>
         */
        public RangeFileContentResult(byte[] fileContents, string contentType, string fileName, DateTime modificationDate)
            : base(contentType, fileName, modificationDate, fileContents.Length)
        {
            if (fileContents == null)
                throw new ArgumentNullException("fileContents");

            FileContents = fileContents;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Writes the entire file to the response.
        /// </summary>
        /// <param name="response">The response from context within which the result is executed.</param>
        protected override void WriteEntireEntity(HttpResponseBase response)
        {
            response.OutputStream.Write(FileContents, 0, FileContents.Length);
        }

        /// <summary>
        /// Writes the file range to the response.
        /// </summary>
        /// <param name="response">The response from context within which the result is executed.</param>
        /// <param name="rangeStartIndex">Range start index</param>
        /// <param name="rangeEndIndex">Range end index</param>
        protected override void WriteEntityRange(HttpResponseBase response, long rangeStartIndex, long rangeEndIndex)
        {
            response.OutputStream.Write(FileContents, Convert.ToInt32(rangeStartIndex), Convert.ToInt32(rangeEndIndex - rangeStartIndex) + 1);
        }
        #endregion
    }
}
