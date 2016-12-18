using System.Collections.Generic;
using System.Linq;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// The table of registered push promises.
    /// </summary>
    public class PushPromiseTable
    {
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
        private readonly IDictionary<string, IDictionary<string, ICollection<string>>> _pushPromiseTable = new Dictionary<string, IDictionary<string, ICollection<string>>>();
        #endregion

        #region Methods
        /// <summary>
        /// Maps push promise to given action.
        /// </summary>
        /// <param name="controller">The controller name.</param>
        /// <param name="action">The action name.</param>
        /// <param name="contentPath">The virtual path of content to push.</param>
        public void MapPushPromise(string controller, string action, string contentPath)
        {
            if (!_pushPromiseTable.ContainsKey(controller))
            {
                _pushPromiseTable.Add(controller, new Dictionary<string, ICollection<string>>());
            }

            if (!_pushPromiseTable[controller].ContainsKey(action))
            {
                _pushPromiseTable[controller].Add(action, new List<string>());
            }

            _pushPromiseTable[controller][action].Add(contentPath);
        }

        internal IEnumerable<string> GetPushPromiseContentPaths(string controller, string action)
        {
            HashSet<string> pushPromiseContentPaths = GetControllerPromiseContentPaths(null, controller, action);
            pushPromiseContentPaths = GetControllerPromiseContentPaths(pushPromiseContentPaths, AnyController, action);

            return pushPromiseContentPaths ?? Enumerable.Empty<string>();
        }

        private HashSet<string> GetControllerPromiseContentPaths(HashSet<string> pushPromiseContentPaths, string controller, string action)
        {
            if (_pushPromiseTable.ContainsKey(controller))
            {
                pushPromiseContentPaths = GetActionPromiseContentPaths(pushPromiseContentPaths, _pushPromiseTable[controller], action);
                pushPromiseContentPaths = GetActionPromiseContentPaths(pushPromiseContentPaths, _pushPromiseTable[controller], AnyAction);
            }

            return pushPromiseContentPaths;
        }

        private static HashSet<string> GetActionPromiseContentPaths(HashSet<string> pushPromiseContentPaths, IDictionary<string, ICollection<string>> controllerPushPromiseContentPaths, string action)
        {
            if (controllerPushPromiseContentPaths.ContainsKey(action))
            {
                if (pushPromiseContentPaths == null)
                {
                    pushPromiseContentPaths = new HashSet<string>(controllerPushPromiseContentPaths[action]);
                }
                else
                {
                    foreach (string pushPromiseContentPath in controllerPushPromiseContentPaths[action])
                    {
                        if (!pushPromiseContentPaths.Contains(pushPromiseContentPath))
                        {
                            pushPromiseContentPaths.Add(pushPromiseContentPath);
                        }
                    }
                }
            }

            return pushPromiseContentPaths;
        }
        #endregion
    }
}
