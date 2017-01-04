using System;
using System.Linq;
using System.Collections.Generic;

namespace Lib.Web.Mvc.Http
{
    /// <summary>
    /// The Cache Digest value
    /// </summary>
    public class CacheDigestValue
    {
        #region Constants
        internal const uint DefaultProbability = 128;

        private const int _countStartIndex = 0;
        private const int _countLength = 5;

        private const int _probabilityStartIndex = 5;
        private const int _probabilityLength = 5;
        
        private const int _hashesStartIndex = 10;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the count of URLs’ members, rounded to the nearest power of 2
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the probability of collision between hashes.
        /// </summary>
        public uint Probability { get; private set; }

        private HashSet<uint> Hashes { get; set; }

        private byte[] CachedByteArray { get; set; }
        #endregion

        #region Constructor
        private CacheDigestValue()
        { }
        #endregion

        #region Methods
        /// <summary>
        /// Creates new Cache Digest value from urls collection.
        /// </summary>
        /// <param name="urls">The collection of URLs with ETag. URLs should be converted to an ASCII string by percent-encoding as appropriate per RFC3986 and ETags should include both the weak indicator (if present) and double quotes.</param>
        /// <param name="probability">The probability of hashes collision.</param>
        /// <param name="validators">The value indicating that the validators should be included in the digest.</param>
        /// <returns>Cache Digest value.</returns>
        public static CacheDigestValue FromUrls(IDictionary<string, string> urls, uint probability = DefaultProbability, bool validators = false)
        {
            // Let N be the count of URLs’ members, rounded to the nearest power of 2 smaller than 2**32.
            int count = CalculateCount(urls?.Count ?? 0);

            try
            {
                ValidateProbability(probability);
            }
            catch (FormatException)
            {
                probability = DefaultProbability;
            }

            // Let hash-values be an empty array of integers.
            HashSet<uint> hashes = new HashSet<uint>();

            if (urls != null)
            {
                // For each (URL, ETag) in URLs, compute a hash value (Section 2.1.2) and append the result to hash-values.
                foreach (KeyValuePair<string, string> url in urls)
                {
                    hashes.Add(CacheDigestHashAlgorithm.ComputeHash(url.Key, count, probability, validators, url.Value));
                }
            }

            return new CacheDigestValue
            {
                Count = count,
                Probability = probability,
                Hashes = hashes
            };
        }

        /// <summary>
        /// Creates new Cache Digest value from byte array.
        /// </summary>
        /// <param name="digestBytes">The byte array.</param>
        /// <returns>Cache Digest value.</returns>
        public static CacheDigestValue FromByteArray(byte[] digestBytes)
        {
            // Transform the byte array to bits array.
            bool[] digestBitArray = GetDigestBitArray(digestBytes);

            // Read the first 5 bits of digest-value as an integer; let N be two raised to the power of that value.
            long count = (long)Math.Pow(2, ReadUInt32FromBitArray(digestBitArray, _countStartIndex, _countLength));
            ValidateCount(count);

            // Read the next 5 bits of digest-value as an integer; let P be two raised to the power of that value.
            int log2Probability = (int)ReadUInt32FromBitArray(digestBitArray, _probabilityStartIndex, _probabilityLength);
            long probability = (long)Math.Pow(2, log2Probability);
            ValidateProbability(probability);

            // Read the hashes.
            HashSet<uint> hashes = ReadHashesFromBitArray(digestBitArray, log2Probability, (uint)probability);

            if (CalculateCount(hashes.Count) != count)
            {
                throw new FormatException("The number of hashes stored by the digest is incorrect.");
            }

            return new CacheDigestValue
            {
                Count = (int)count,
                Probability = (uint)probability,
                Hashes = hashes,
                CachedByteArray = digestBytes
            };
        }

        /// <summary>
        /// Creates new Cache Digest value from Base64 encoded string.
        /// </summary>
        /// <param name="digestBase64String">The Base64 encoded string.</param>
        /// <returns>Cache Digest value.</returns>
        public static CacheDigestValue FromBase64String(string digestBase64String)
        {
            int neededBase64StringPadding = (digestBase64String.Length % 4);
            if (neededBase64StringPadding > 0)
            {
                digestBase64String += new string('=', 4 - neededBase64StringPadding);
            }

            byte[] digestBytes = Convert.FromBase64String(digestBase64String);

            return FromByteArray(digestBytes);
        }

        /// <summary>
        /// Checks if digest value contains given hash.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns>True if there is a match in the digest value, otherwise false.</returns>
        public bool Contains(uint hash)
        {
            return Hashes.Contains(hash);
        }

        /// <summary>
        /// Checks if digest value contains given URL.
        /// </summary>
        /// <param name="url">The URL, converted to an ASCII string by percent-encoding as appropriate per RFC3986.</param>
        /// <param name="validators">Flag indicating if digest value is expected to contain validators.</param>
        /// <param name="entityTag">The ETag, including both the weak indicator (if present) and double quotes, as per RFC7232 Section 2.3.</param>
        /// <returns>True if there is a match in the digest value, otherwise false.</returns>
        public bool Contains(string url, bool validators = false, string entityTag = null)
        {
            uint hash = CacheDigestHashAlgorithm.ComputeHash(url, Count, Probability, validators, entityTag);

            return Contains(hash);
        }

        /// <summary>
        /// Converts the Cache Digest value to byte array.
        /// </summary>
        /// <returns>Cache Digest value as byte array.</returns>
        public byte[] ToByteArray()
        {
            if (CachedByteArray == null)
            {
                // Let digest-value be an empty array of bits.
                IList<bool> digestBits = new List<bool>();

                // Write log base 2 of N to digest-value using 5 bits.
                digestBits = AppendUInt32ToBitList(digestBits, (uint)Math.Log(Count, 2), _countLength);

                // Write log base 2 of P to digest-value using 5 bits
                int log2Probability = (int)Math.Log(Probability, 2);
                digestBits = AppendUInt32ToBitList(digestBits, (uint)log2Probability, _probabilityLength);

                // Write the hashes.
                digestBits = AppendHashesToBitList(digestBits, Hashes, log2Probability, Probability);

                // If the length of digest-value is not a multiple of 8, pad it with 0s until it is.
                digestBits = PabBitList(digestBits);

                // Transform the bits list to byte array.
                CachedByteArray = GetDigestByteArray(digestBits);
            }

            return CachedByteArray;
        }

        /// <summary>
        /// Converts the Cache Digest value to Base64 encoded string.
        /// </summary>
        /// <returns>Cache Digest value as Base64 encoded string.</returns>
        public string ToBase64String()
        {
            return Convert.ToBase64String(ToByteArray()).TrimEnd('=');
        }

        private static int CalculateCount(int hashesCount)
        {
            return (hashesCount == 0) ? 1 : (int)Math.Pow(2, Math.Round(Math.Log(hashesCount, 2)));
        }

        private static void ValidateCount(long count)
        {
            if (count > Int32.MaxValue)
            {
                throw new FormatException("Count must be smaller than 2^31.");
            }
        }

        private static void ValidateProbability(long probability)
        {
            if (probability > UInt32.MaxValue)
            {
                throw new FormatException("Probability must be smaller than 2^32.");
            }

            if ((probability & (probability - 1)) != 0)
            {
                throw new FormatException("Probability must be a power of 2.");
            }
        }

        private static bool[] GetDigestBitArray(byte[] digestBytes)
        {
            bool[] digestBitArray = new bool[digestBytes.Length * 8];
            int digestBitArrayIndex = digestBitArray.Length - 1;

            for (int digestByteIndex = digestBytes.Length - 1; digestByteIndex >= 0; digestByteIndex--)
            {
                byte digestByte = digestBytes[digestByteIndex];
                for (int digestByteBitIndex = 0; digestByteBitIndex < 8; digestByteBitIndex++)
                {
                    digestBitArray[digestBitArrayIndex--] = ((digestByte % 2 == 0) ? false : true);
                    digestByte = (byte)(digestByte >> 1);
                }
            }

            return digestBitArray;
        }

        private static byte[] GetDigestByteArray(IList<bool> digestBits)
        {
            byte[] digestByteArray = new byte[digestBits.Count / 8];
            int digestByteArrayIndex = 0;

            int digestBitIndex = 0;
            while (digestBitIndex < digestBits.Count)
            {
                for (int digestByteBitIndex = 0; digestByteBitIndex < 8; digestByteBitIndex++)
                {
                    digestByteArray[digestByteArrayIndex] <<= 1;
                    if (digestBits[(digestBitIndex++)])
                    {
                        digestByteArray[digestByteArrayIndex] |= 1;
                    };
                }

                digestByteArrayIndex++;
            }

            return digestByteArray;
        }

        private static uint ReadUInt32FromBitArray(bool[] bitArray, int starIndex, int length)
        {
            uint result = 0;

            for (int bitIndex = starIndex; bitIndex < (starIndex + length); bitIndex++)
            {
                result <<= 1;
                if (bitArray[bitIndex])
                {
                    result |= 1;
                }
            }

            return result;
        }

        private static IList<bool> AppendUInt32ToBitList(IList<bool> bitList, uint value, int countOfBitsToUse)
        {
            for (int bitToUseIndex = countOfBitsToUse - 1; bitToUseIndex >= 0; bitToUseIndex--)
            {
                bitList.Add((value & (1 << bitToUseIndex)) > 0);
            }

            return bitList;
        }

        private static HashSet<uint> ReadHashesFromBitArray(bool[] bitArray, int log2Probability, uint probability)
        {
            HashSet<uint> hashes = new HashSet<uint>();

            // Let C be -1.
            long c = -1;
            int hashesBitIndex = _hashesStartIndex;

            while (hashesBitIndex < bitArray.Length)
            {
                // Read ‘0’ bits until a ‘1’ bit is found; let Q bit the number of ‘0’ bits.
                uint q = 0;
                while ((hashesBitIndex < bitArray.Length) && !bitArray[hashesBitIndex])
                {
                    q++;
                    hashesBitIndex++;
                }

                if ((hashesBitIndex + log2Probability) < bitArray.Length)
                {
                    // Discard the ‘1’.
                    hashesBitIndex++;

                    // Read log2(P) bits after the ‘1’ as an integer. Let R be its value.
                    uint r = ReadUInt32FromBitArray(bitArray, hashesBitIndex, log2Probability);

                    // Let D be Q * P + R.
                    uint d = (q * probability) + r;

                    // Increment C by D + 1.
                    c = c + d + 1;

                    hashes.Add((uint)c);
                    hashesBitIndex += log2Probability;
                }
            }

            return hashes;
        }

        private static IList<bool> AppendHashesToBitList(IList<bool> bitList, HashSet<uint> hashes, int log2Probability, uint probability)
        {
            // Let C be -1.
            long c = -1;

            // Sort hash-values in ascending order and for each V in hash-values.
            foreach (uint hash in hashes.OrderBy(hash => hash))
            {
                // If V is equal to C, continue to the next V.
                if (hash != c)
                {
                    // Let D be the result of V - C - 1.
                    uint d = (uint)(hash - c - 1);

                    // Let Q be the integer result of D / P.
                    uint q = d / probability;

                    // Let R be the result of D modulo P.
                    uint r = d % probability;

                    // Write Q ‘0’ bits to digest-value.
                    for (uint i = 0; i < q; i++)
                    {
                        bitList.Add(false);
                    }

                    // Write 1 ‘1’ bit to digest-value.
                    bitList.Add(true);

                    // Write R to digest-value as binary, using log2(P) bits.
                    bitList = AppendUInt32ToBitList(bitList, r, log2Probability);

                    // Let C be V
                    c = hash;
                }
            }

            return bitList;
        }

        private static IList<bool> PabBitList(IList<bool> bitList)
        {
            int notPaddedBitsCount = bitList.Count % 8;
            if (notPaddedBitsCount != 0)
            {
                for (int i = 8 - notPaddedBitsCount; i > 0; i--)
                {
                    bitList.Add(false);
                }
            }

            return bitList;
        }
        #endregion
    }
}
