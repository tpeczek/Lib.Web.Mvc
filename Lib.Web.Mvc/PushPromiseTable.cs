using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// The table of registered push promises.
    /// </summary>
    public class PushPromiseTable
    {
        #region Classes
        internal class Entry
        {
            #region Properties
            internal string ContentPath { get; private set; }

            internal string EntityTag { get; private set; }

            internal bool TrackInCacheDigest { get; private set; }

            internal string AbsoluteUrl { get; set; }
            #endregion

            #region Constructor
            internal Entry(string contentPath, string entityTag, bool trackInCacheDigest)
            {
                ContentPath = contentPath;
                EntityTag = entityTag;
                TrackInCacheDigest = trackInCacheDigest;
            }
            #endregion

            #region Methods
            public override int GetHashCode()
            {
                return ContentPath.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return ContentPath.Equals(obj);
            }
            #endregion
        }
        #endregion

        #region Constants
        /// <summary>
        /// Token representing mapping to any controller.
        /// </summary>
        public const string AnyController = "*";

        /// <summary>
        /// Token representing mapping to any action.
        /// </summary>
        public const string AnyAction = "*";
        #endregion

        #region Fields
        private readonly IDictionary<string, IDictionary<string, ICollection<Entry>>> _pushPromiseTable = new Dictionary<string, IDictionary<string, ICollection<Entry>>>();
        #endregion

        #region Methods
        /// <summary>
        /// Maps push promise to given action.
        /// </summary>
        /// <param name="controller">The controller name.</param>
        /// <param name="action">The action name.</param>
        /// <param name="contentPath">The virtual path of content to push.</param>
        /// <param name="etag">The ETag of content to push.</param>
        /// <param name="trackInCacheDigest">The flag indicating if the push is to be tracked in cookie based cache-digest implementation.</param>
        public void MapPushPromise(string controller, string action, string contentPath, string etag = null, bool trackInCacheDigest = true)
        {
            if (String.IsNullOrWhiteSpace(controller))
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (String.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (String.IsNullOrWhiteSpace(contentPath))
            {
                throw new ArgumentNullException(nameof(contentPath));
            }

            if (!_pushPromiseTable.ContainsKey(controller))
            {
                _pushPromiseTable.Add(controller, new Dictionary<string, ICollection<Entry>>());
            }

            if (!_pushPromiseTable[controller].ContainsKey(action))
            {
                _pushPromiseTable[controller].Add(action, new List<Entry>());
            }
            
            _pushPromiseTable[controller][action].Add(new Entry(contentPath, etag, trackInCacheDigest));
        }

        internal IEnumerable<Entry> GetPushPromiseEntries(string controller, string action)
        {
            HashSet<Entry> pushPromiseEntries = GetControllerPushPromiseEntries(null, controller, action);
            pushPromiseEntries = GetControllerPushPromiseEntries(pushPromiseEntries, AnyController, action);

            return pushPromiseEntries ?? Enumerable.Empty<Entry>();
        }

        private HashSet<Entry> GetControllerPushPromiseEntries(HashSet<Entry> pushPromiseEntries, string controller, string action)
        {
            if (_pushPromiseTable.ContainsKey(controller))
            {
                pushPromiseEntries = GetActionPushPromiseEntries(pushPromiseEntries, _pushPromiseTable[controller], action);
                pushPromiseEntries = GetActionPushPromiseEntries(pushPromiseEntries, _pushPromiseTable[controller], AnyAction);
            }

            return pushPromiseEntries;
        }

        private static HashSet<Entry> GetActionPushPromiseEntries(HashSet<Entry> pushPromiseEntries, IDictionary<string, ICollection<Entry>> controllerPushPromiseEntries, string action)
        {
            if (controllerPushPromiseEntries.ContainsKey(action))
            {
                if (pushPromiseEntries == null)
                {
                    pushPromiseEntries = new HashSet<Entry>(controllerPushPromiseEntries[action]);
                }
                else
                {
                    foreach (Entry pushPromiseEntry in controllerPushPromiseEntries[action])
                    {
                        if (!pushPromiseEntries.Contains(pushPromiseEntry))
                        {
                            pushPromiseEntries.Add(pushPromiseEntry);
                        }
                    }
                }
            }

            return pushPromiseEntries;
        }
        #endregion
    }
}
