using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// Sends binary content to the range response by using a Stream instance.
    /// </summary>
    public class RangeFileStreamResult : RangeFileResult
    {
        #region Fields
        private const int _bufferSize = 0x1000;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the stream that will be sent to the response.
        /// </summary>
        public Stream FileStream { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the RangeFileStreamResult class.
        /// </summary>
        /// <param name="fileStream">The stream to send to the response.</param>
        /// <param name="contentType">The content type to use for the response.</param>
        /// <param name="fileName">The file name to use for the response.</param>
        /// <param name="modificationDate">The file modification date to use for the response.</param>
        /**
         * <remarks>
         * The <paramref name="modificationDate"/> parameter is used internally while creating ETag and Last-Modified headers. Those headers might by used by client in order to verify that the same entity is being requested in separated partial requests and for caching purposes. Because of that it is important that the value passed to this parameter is consitant and reflects the actual state of entity during its entire lifetime.
         * </remarks>
         */
        public RangeFileStreamResult(Stream fileStream, string contentType, string fileName, DateTime modificationDate)
            : base(contentType, fileName, modificationDate, fileStream.Length)
        {
            if (fileStream == null)
                throw new ArgumentNullException("fileStream");

            FileStream = fileStream;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Writes the entire file to the response.
        /// </summary>
        /// <param name="response">The response from context within which the result is executed.</param>
        protected override void WriteEntireEntity(HttpResponseBase response)
        {
            using (FileStream)
            {
                byte[] buffer = new byte[_bufferSize];

                while (true)
                {
                    int bytesRead = FileStream.Read(buffer, 0, _bufferSize);
                    if (bytesRead == 0)
                        break;

                    response.OutputStream.Write(buffer, 0, bytesRead);
                }
            }
        }

        /// <summary>
        /// Writes the file range to the response.
        /// </summary>
        /// <param name="response">The response from context within which the result is executed.</param>
        /// <param name="rangeStartIndex">Range start index</param>
        /// <param name="rangeEndIndex">Range end index</param>
        protected override void WriteEntityRange(HttpResponseBase response, long rangeStartIndex, long rangeEndIndex)
        {
            using (FileStream)
            {
                FileStream.Seek(rangeStartIndex, SeekOrigin.Begin);

                int bytesRemaining = Convert.ToInt32(rangeEndIndex - rangeStartIndex) + 1;
                byte[] buffer = new byte[_bufferSize];

                while (bytesRemaining > 0)
                {
                    int bytesRead = FileStream.Read(buffer, 0, _bufferSize < bytesRemaining ? _bufferSize : bytesRemaining);
                    response.OutputStream.Write(buffer, 0, bytesRead);
                    bytesRemaining -= bytesRead;
                }
            }
        }
        #endregion
    }
}
