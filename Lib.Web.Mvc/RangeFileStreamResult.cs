using System;
using System.IO;
using System.Web;
using System.Buffers;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// Sends binary content to the range response by using a Stream instance.
    /// </summary>
    public class RangeFileStreamResult : RangeFileResult
    {
        #region Fields
        private const int _defaultWriteBufferSize = 81920;
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
            FileStream = fileStream ?? throw new ArgumentNullException("fileStream"); ;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Writes the entire file to the response.
        /// </summary>
        /// <param name="response">The response from context within which the result is executed.</param>
        protected override void WriteEntireEntity(HttpResponseBase response)
        {
            WriteFileStream(response, 0, FileStream.Length);
        }

        /// <summary>
        /// Writes the file range to the response.
        /// </summary>
        /// <param name="response">The response from context within which the result is executed.</param>
        /// <param name="rangeStartIndex">Range start index</param>
        /// <param name="rangeEndIndex">Range end index</param>
        protected override void WriteEntityRange(HttpResponseBase response, long rangeStartIndex, long rangeEndIndex)
        {
            WriteFileStream(response, rangeStartIndex, (rangeEndIndex - rangeStartIndex) + 1);
        }

        private void WriteFileStream(HttpResponseBase response, long offset, long length)
        {
            using (FileStream)
            {
                int writeBufferSize = (int)Math.Min(_defaultWriteBufferSize, length);

                byte[] writeBuffer = ArrayPool<byte>.Shared.Rent(writeBufferSize);
                try
                {
                    int read;
                    long remaining = length;
                    
                    FileStream.Seek(offset, SeekOrigin.Begin);
                    while ((remaining > 0) && (read = FileStream.Read(writeBuffer, 0, writeBuffer.Length)) != 0)
                    {
                        response.OutputStream.Write(writeBuffer, 0, read);
                        response.Flush();

                        remaining -= read;
                    }
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(writeBuffer);
                }
            }
        }
        #endregion
    }
}
