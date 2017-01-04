using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lib.Web.Mvc.Http
{
    /// <summary>
    /// Represents the value of the Cache-Digest header.
    /// </summary>
    public class CacheDigestHeaderValue
    {
        #region Constants
        private const string ResetFlag = "RESET";
        private const string CompleteFlag = "COMPLETE";
        private const string ValidatorsFlag = "VALIDATORS";
        private const string StaleFlag = "STALE";
        private const string DigestValueSeparator = ";";
        #endregion

        #region Fields
        private Lazy<CacheDigestValue> _lazyDigestValue;
        #endregion

        #region Properties
        /// <summary>
        /// Indicates that any and all cache digests for the applicable origin held by the recipient MUST be considered invalid.
        /// </summary>
        public bool Reset { get; private set; }

        /// <summary>
        /// Indicates that the currently valid set of cache digests held by the server constitutes a complete representation of the cache’s state regarding that origin, for the type of cached response indicated by the Stale property.
        /// </summary>
        public bool Complete { get; private set; }

        /// <summary>
        /// Indicates that the validators are included in the digest.
        /// </summary>
        public bool Validators { get; private set; }

        /// <summary>
        /// Indicates that all cached responses represented in the digest are stale.
        /// </summary>
        public bool Stale { get; private set; }

        /// <summary>
        /// The digest value.
        /// </summary>
        public CacheDigestValue DigestValue { get { return _lazyDigestValue.Value; } }
        #endregion

        #region Constructor
        private CacheDigestHeaderValue()
        {
            Reset = false;
            Complete = false;
            Validators = false;
            Stale = false;

            _lazyDigestValue = new Lazy<CacheDigestValue>(() => CacheDigestValue.FromUrls(new Dictionary<string, string>()));
        }

        /// <summary>
        /// Initializes new instance of CacheDigestHeaderValue class.
        /// </summary>
        /// <param name="value">The value of the header.</param>
        public CacheDigestHeaderValue(string value)
            : this()
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                Reset = CheckFlagSet(value, ResetFlag);
                Complete = CheckFlagSet(value, CompleteFlag);
                Validators = CheckFlagSet(value, ValidatorsFlag);
                Stale = CheckFlagSet(value, StaleFlag);

                string digestBase64String = value.Substring(0, value.IndexOf(DigestValueSeparator));
                _lazyDigestValue = new Lazy<CacheDigestValue>(() => CacheDigestValue.FromBase64String(digestBase64String));
            }
        }

        /// <summary>
        /// Initializes new instance of CacheDigestHeaderValue class.
        /// </summary>
        /// <param name="digestValue">The digest value.</param>
        /// <param name="reset">Flag idicating that any and all cache digests for the applicable origin held by the recipient MUST be considered invalid.</param>
        /// <param name="validators">Flag indicating the validators are included in the digest.</param>
        /// <param name="stale">Flag indicating that all cached responses represented in the digest are stale.</param>
        public CacheDigestHeaderValue(CacheDigestValue digestValue, bool reset = false, bool validators = false, bool stale = false)
            : this()
        {
            Reset = reset;
            Complete = true;
            Validators = validators;
            Stale = stale;

            _lazyDigestValue = new Lazy<CacheDigestValue>(() => digestValue);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Queries the digest in order to determine whether there is a match in the digest.
        /// </summary>
        /// <param name="url">The URL of the resource, converted to an ASCII string by percent-encoding as appropriate per RFC3986.</param>
        /// <param name="entityTag">The ETag of the resource, including both the weak indicator (if present) and double quotes, as per RFC7232 Section 2.3.</param>
        /// <returns>True if there is a match in the digest, otherwise false.</returns>
        public bool QueryDigest(string url, string entityTag = null)
        {
            return DigestValue.Contains(url, Validators, entityTag);
        }

        private static bool CheckFlagSet(string value, string flag)
        {
            return (CultureInfo.InvariantCulture.CompareInfo.IndexOf(value, flag, CompareOptions.IgnoreCase) >= 0);
        }
        #endregion
    }
}
