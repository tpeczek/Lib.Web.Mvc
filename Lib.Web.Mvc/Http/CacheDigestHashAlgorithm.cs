using System;
using System.Text;
using System.Security.Cryptography;

namespace Lib.Web.Mvc.Http
{
    internal static class CacheDigestHashAlgorithm
    {
        #region Constants
        private const int _hashValueLengthUpperBound = 32;
        #endregion

        #region Methods
        internal static uint ComputeHash(string url, int count, uint probability, bool validators = false, string entityTag = null)
        {
            int hashValueLength = (int)Math.Log(count * probability, 2);
            if (hashValueLength >= _hashValueLengthUpperBound)
            {
                throw new NotSupportedException("Only hash-values up to 31 bits are supported.");
            }

            // This assumes that URL is already converted to an ASCII string by percent-encoding as appropriate (RFC3986).
            string key = url;

            // If validators is true and ETag is not null.
            if (validators && !String.IsNullOrWhiteSpace(entityTag))
            {
                // Append ETag to key as an ASCII string.
                key += entityTag;
            }

            // Let hash-value be the SHA-256 message digest (RFC6234) of key, expressed as an integer.
            uint hashValue = BitConverter.ToUInt32(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(key)), 0);
            if (BitConverter.IsLittleEndian)
            {
                hashValue = (hashValue & 0x000000FFU) << 24 | (hashValue & 0x0000FF00U) << 8 | (hashValue & 0x00FF0000U) >> 8 | (hashValue & 0xFF000000U) >> 24;
            }

            // Truncate hash-value to log2(N*P) bits.
            return (hashValue >> (_hashValueLengthUpperBound - hashValueLength)) & (uint)((1 << hashValueLength) - 1);
        }
        #endregion
    }
}
