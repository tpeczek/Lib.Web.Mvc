using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// Content Security Policy Level 2 inline execution modes
    /// </summary>
    public enum ContentSecurityPolicyInlineExecution
    {
        /// <summary>
        /// Refuse any inline execution
        /// </summary>
        Refuse,
        /// <summary>
        /// Allow all inline execution
        /// </summary>
        Unsafe,
        /// <summary>
        /// Use nonce mechanism
        /// </summary>
        Nonce,
        /// <summary>
        /// Use hash mechanism
        /// </summary>
        Hash
    }

    /// <summary>
    /// Action filter for defining Content Security Policy Level 2 policies
    /// </summary>
    public sealed class ContentSecurityPolicyAttribute : FilterAttribute, IActionFilter, IResultFilter
    {
        #region Constants
        internal const string ScriptInlineExecutionContextKey = "Lib.Web.Mvc.ContentSecurityPolicy.ScriptInlineExecution";
        internal const string NonceRandomContextKey = "Lib.Web.Mvc.ContentSecurityPolicy.NonceRandom";
        internal const string ScriptHashListBuilderContextKey = "Lib.Web.Mvc.ContentSecurityPolicy.ScriptHashListBuilder";

        private const string _scriptHashListPlaceholder = "<ScriptHashListPlaceholder>";

        private const string _contentSecurityPolicyHeader = "Content-Security-Policy";
        private const string _directivesDelimiter = ";";
        private const string _scriptDirective = "script-src";
        private const string _unsafeInlineSource = " 'unsafe-inline'";
        private const string _nonceSourceFormat = " 'nonce-{0}'";
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the source list for script-src directive.
        /// </summary>
        public string ScriptSource { get; set; }

        /// <summary>
        /// Gets or sets the inline execution mode for scripts
        /// </summary>
        public ContentSecurityPolicyInlineExecution ScriptInlineExecution { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes new instance of ContentSecurityPolicyAttribute
        /// </summary>
        public ContentSecurityPolicyAttribute()
        {
            ScriptInlineExecution = ContentSecurityPolicyInlineExecution.Refuse;
        }
        #endregion

        #region IActionFilter Members
        /// <summary>
        /// Called after the action method executes.
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        { }

        /// <summary>
        /// Called before an action method executes.
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            StringBuilder policyBuilder = new StringBuilder();

            if (!String.IsNullOrWhiteSpace(ScriptSource) || (ScriptInlineExecution != ContentSecurityPolicyInlineExecution.Refuse))
            {
                policyBuilder.Append(_scriptDirective);

                if (!String.IsNullOrWhiteSpace(ScriptSource))
                {
                    policyBuilder.AppendFormat(" {0}", ScriptSource);
                }

                filterContext.HttpContext.Items[ScriptInlineExecutionContextKey] = ScriptInlineExecution;
                switch (ScriptInlineExecution)
                {
                    case ContentSecurityPolicyInlineExecution.Unsafe:
                        policyBuilder.Append(_unsafeInlineSource);
                        break;
                    case ContentSecurityPolicyInlineExecution.Nonce:
                        string nonceRandom = Guid.NewGuid().ToString("N");
                        filterContext.HttpContext.Items[NonceRandomContextKey] = nonceRandom;
                        policyBuilder.AppendFormat(_nonceSourceFormat, nonceRandom);
                        break;
                    case ContentSecurityPolicyInlineExecution.Hash:
                        filterContext.HttpContext.Items[ScriptHashListBuilderContextKey] = new StringBuilder();
                        policyBuilder.Append(_scriptHashListPlaceholder);
                        break;
                    default:
                        break;
                }

                policyBuilder.Append(_directivesDelimiter);
            }

            if (policyBuilder.Length > 0)
            {
                filterContext.HttpContext.Response.AppendHeader(_contentSecurityPolicyHeader, policyBuilder.ToString());
            }
        }
        #endregion

        #region IResultFilter Members
        /// <summary>
        /// Called after an action result executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (ScriptInlineExecution == ContentSecurityPolicyInlineExecution.Hash)
            {
                filterContext.HttpContext.Response.Headers[_contentSecurityPolicyHeader] = filterContext.HttpContext.Response.Headers[_contentSecurityPolicyHeader].Replace(_scriptHashListPlaceholder, ((StringBuilder)filterContext.HttpContext.Items[ScriptHashListBuilderContextKey]).ToString());
            }
        }

        /// <summary>
        /// Called before an action result executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnResultExecuting(ResultExecutingContext filterContext)
        { }
        #endregion
    }
}
