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
    /// Content Security Policy Level 2 sandbox flags
    /// </summary>
    [Flags]
    public enum ContentSecurityPolicySandboxFlags
    {
        /// <summary>
        /// Set no sandbox flags
        /// </summary>
        None = 0,
        /// <summary>
        /// Set allow-forms sandbox flag
        /// </summary>
        AllowForms = 1,
        /// <summary>
        /// Set allow-pointer-lock sandbox flag
        /// </summary>
        AllowPointerLock = 2,
        /// <summary>
        /// Set allow-popups sandbox flag
        /// </summary>
        AllowPopups = 4,
        /// <summary>
        /// Set allow-same-origin sandbox flag
        /// </summary>
        AllowSameOrigin = 8,
        /// <summary>
        /// Set allow-scripts sandbox flag
        /// </summary>
        AllowScripts = 16,
        /// <summary>
        /// Set allow-top-navigation sandbox flag
        /// </summary>
        AllowTopNavigation = 32
    }

    /// <summary>
    /// Action filter for defining Content Security Policy Level 2 policies
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ContentSecurityPolicyAttribute : FilterAttribute, IActionFilter, IResultFilter
    {
        #region Constants
        internal const string ScriptDirective = "script-src";
        internal const string StyleDirective = "style-src";

        internal const string NonceRandomContextKey = "Lib.Web.Mvc.ContentSecurityPolicy.NonceRandom";

        private const string _contentSecurityPolicyHeader = "Content-Security-Policy";
        private const string _contentSecurityPolicyReportOnlyHeader = "Content-Security-Policy-Report-Only";
        private const string _directivesDelimiter = ";";
        private const string _baseDirectiveFormat = "base-uri {0};";
        private const string _defaultDirectiveFormat = "default-src {0};";
        private const string _childDirectiveFormat = "child-src {0};";
        private const string _connectDirectiveFormat = "connect-src {0};";
        private const string _fontDirectiveFormat = "font-src {0};";
        private const string _formDirectiveFormat = "form-action {0};";
        private const string _ancestorsDirectiveFormat = "frame-ancestors {0};";
        private const string _imageDirectiveFormat = "img-src {0};";
        private const string _mediaDirectiveFormat = "media-src {0};";
        private const string _objectDirectiveFormat = "object-src {0};";
        private const string _sandboxDirective = "sandbox";
        private const string _reportDirectiveFormat = "report-uri {0};";
        private const string _unsafeInlineSource = " 'unsafe-inline'";
        private const string _nonceSourceFormat = " 'nonce-{0}'";
        private const string _allowFormsSandboxFlag = " allow-forms";
        private const string _allowPointerLockSandboxFlag = " allow-pointer-lock";
        private const string _allowPopupsSandboxFlag = " allow-popups";
        private const string _allowSameOriginSandboxFlag = " allow-same-origin";
        private const string _allowScriptsSandboxFlag = " allow-scripts";
        private const string _allowTopNavigationSandboxFlag = " allow-top-navigation";
        #endregion

        #region Fields
        internal static IDictionary<string, string> InlineExecutionContextKeys = new Dictionary<string, string>
        {
            { ScriptDirective, "Lib.Web.Mvc.ContentSecurityPolicy.ScriptInlineExecution" },
            { StyleDirective, "Lib.Web.Mvc.ContentSecurityPolicy.StyleInlineExecution" }
        };

        internal static IDictionary<string, string> HashListBuilderContextKeys = new Dictionary<string, string>
        {
            { ScriptDirective, "Lib.Web.Mvc.ContentSecurityPolicy.ScriptHashListBuilder" },
            { StyleDirective, "Lib.Web.Mvc.ContentSecurityPolicy.StyleHashListBuilder" }
        };

        private static IDictionary<string, string> _hashListPlaceholders = new Dictionary<string, string>
        {
            { ScriptDirective, "<ScriptHashListPlaceholder>" },
            { StyleDirective, "<StyleHashListPlaceholder>" }
        };
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the list of URLs that can be used to specify the document base URL.
        /// </summary>
        public string BaseUri { get; set; }

        /// <summary>
        /// Gets or sets the default source list for directives which can fall back to the default sources.
        /// </summary>
        public string DefaultSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for child-src directive.
        /// </summary>
        public string ChildSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for connect-src directive.
        /// </summary>
        public string ConnectSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for font-src directive.
        /// </summary>
        public string FontSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for form-action directive.
        /// </summary>
        public string FormSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for frame-ancestors directive.
        /// </summary>
        public string AncestorsSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for img-src directive.
        /// </summary>
        public string ImageSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for media-src directive.
        /// </summary>
        public string MediaSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for object-src directive.
        /// </summary>
        public string ObjectSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for script-src directive.
        /// </summary>
        public string ScriptSources { get; set; }

        /// <summary>
        /// Gets or sets the inline execution mode for scripts
        /// </summary>
        public ContentSecurityPolicyInlineExecution ScriptInlineExecution { get; set; }

        /// <summary>
        /// Gets or sets the source list for style-src directive.
        /// </summary>
        public string StyleSources { get; set; }

        /// <summary>
        /// Gets or sets the inline execution mode for styles
        /// </summary>
        public ContentSecurityPolicyInlineExecution StyleInlineExecution { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if sandbox policy should be applied.
        /// </summary>
        public bool Sandbox { get; set; }

        /// <summary>
        /// Gets or sets the sandboxing flags (only used when Sandbox is true)
        /// </summary>
        public ContentSecurityPolicySandboxFlags SandboxFlags { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if this is report only policy.
        /// </summary>
        public bool ReportOnly { get; set; }

        /// <summary>
        /// Gets or sets the URL to which the user agent should send reports about policy violations
        /// </summary>
        public string ReportUri { get; set; }

        private string ContentSecurityPolicyHeader
        {
            get { return ReportOnly ? _contentSecurityPolicyReportOnlyHeader : _contentSecurityPolicyHeader; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes new instance of ContentSecurityPolicyAttribute
        /// </summary>
        public ContentSecurityPolicyAttribute()
        {
            ScriptInlineExecution = ContentSecurityPolicyInlineExecution.Refuse;
            StyleInlineExecution = ContentSecurityPolicyInlineExecution.Refuse;
            Sandbox = false;
            SandboxFlags = ContentSecurityPolicySandboxFlags.None;
            ReportOnly = false;
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
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            StringBuilder policyBuilder = new StringBuilder();

            AppendDirective(policyBuilder, _baseDirectiveFormat, BaseUri);
            AppendDirective(policyBuilder, _defaultDirectiveFormat, DefaultSources);
            AppendDirective(policyBuilder, _childDirectiveFormat, ChildSources);
            AppendDirective(policyBuilder, _connectDirectiveFormat, ConnectSources);
            AppendDirective(policyBuilder, _fontDirectiveFormat, FontSources);
            AppendDirective(policyBuilder, _formDirectiveFormat, FormSources);
            AppendDirective(policyBuilder, _ancestorsDirectiveFormat, AncestorsSources);
            AppendDirective(policyBuilder, _imageDirectiveFormat, ImageSources);
            AppendDirective(policyBuilder, _mediaDirectiveFormat, MediaSources);
            AppendDirective(policyBuilder, _objectDirectiveFormat, ObjectSources);
            AppendDirectiveWithInlineExecution(filterContext, policyBuilder, ScriptDirective, ScriptSources, ScriptInlineExecution);
            AppendDirectiveWithInlineExecution(filterContext, policyBuilder, StyleDirective, StyleSources, StyleInlineExecution);
            AppendSandboxDirective(policyBuilder);
            AppendDirective(policyBuilder, _reportDirectiveFormat, ReportUri);

            if (policyBuilder.Length > 0)
            {
                filterContext.HttpContext.Response.AppendHeader(ContentSecurityPolicyHeader, policyBuilder.ToString());
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
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            string contentSecurityPolicyHeaderValue = filterContext.HttpContext.Response.Headers[ContentSecurityPolicyHeader];

            if (!String.IsNullOrWhiteSpace(contentSecurityPolicyHeaderValue))
            {
                if (ScriptInlineExecution == ContentSecurityPolicyInlineExecution.Hash)
                {
                    contentSecurityPolicyHeaderValue = contentSecurityPolicyHeaderValue.Replace(_hashListPlaceholders[ScriptDirective], ((StringBuilder)filterContext.HttpContext.Items[HashListBuilderContextKeys[ScriptDirective]]).ToString());
                }

                if (StyleInlineExecution == ContentSecurityPolicyInlineExecution.Hash)
                {
                    contentSecurityPolicyHeaderValue = contentSecurityPolicyHeaderValue.Replace(_hashListPlaceholders[StyleDirective], ((StringBuilder)filterContext.HttpContext.Items[HashListBuilderContextKeys[StyleDirective]]).ToString());
                }

                filterContext.HttpContext.Response.Headers[ContentSecurityPolicyHeader] = contentSecurityPolicyHeaderValue;
            }
        }

        /// <summary>
        /// Called before an action result executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnResultExecuting(ResultExecutingContext filterContext)
        { }
        #endregion

        #region Private Methods
        private void AppendDirective(StringBuilder policyBuilder, string directiveFormat, string source)
        {
            if (!String.IsNullOrWhiteSpace(source))
            {
                policyBuilder.AppendFormat(directiveFormat, source);
            }
        }

        private void AppendDirectiveWithInlineExecution(ActionExecutingContext filterContext, StringBuilder policyBuilder, string directive, string source, ContentSecurityPolicyInlineExecution inlineExecution)
        {
            if (!String.IsNullOrWhiteSpace(source) || (inlineExecution != ContentSecurityPolicyInlineExecution.Refuse))
            {
                policyBuilder.Append(directive);

                if (!String.IsNullOrWhiteSpace(source))
                {
                    policyBuilder.AppendFormat(" {0}", source);
                }

                filterContext.HttpContext.Items[InlineExecutionContextKeys[directive]] = inlineExecution;
                switch (inlineExecution)
                {
                    case ContentSecurityPolicyInlineExecution.Unsafe:
                        policyBuilder.Append(_unsafeInlineSource);
                        break;
                    case ContentSecurityPolicyInlineExecution.Nonce:
                        string nonceRandom = GetNonceRandom(filterContext);
                        policyBuilder.AppendFormat(_nonceSourceFormat, nonceRandom);
                        break;
                    case ContentSecurityPolicyInlineExecution.Hash:
                        filterContext.HttpContext.Items[HashListBuilderContextKeys[directive]] = new StringBuilder();
                        policyBuilder.Append(_hashListPlaceholders[directive]);
                        break;
                    default:
                        break;
                }

                policyBuilder.Append(_directivesDelimiter);
            }
        }

        private string GetNonceRandom(ActionExecutingContext filterContext)
        {
            string nonceRandom;

            if (filterContext.HttpContext.Items.Contains(NonceRandomContextKey))
            {
                nonceRandom = (string)filterContext.HttpContext.Items[NonceRandomContextKey];
            }
            else
            {
                nonceRandom = Guid.NewGuid().ToString("N");
                filterContext.HttpContext.Items[NonceRandomContextKey] = nonceRandom;
            }

            return nonceRandom;
        }

        private void AppendSandboxDirective(StringBuilder policyBuilder)
        {
            if (Sandbox)
            {
                policyBuilder.Append(_sandboxDirective);

                if (SandboxFlags != ContentSecurityPolicySandboxFlags.None)
                {
                    AppendSandboxFlag(policyBuilder, ContentSecurityPolicySandboxFlags.AllowForms, _allowFormsSandboxFlag);
                    AppendSandboxFlag(policyBuilder, ContentSecurityPolicySandboxFlags.AllowPointerLock, _allowPointerLockSandboxFlag);
                    AppendSandboxFlag(policyBuilder, ContentSecurityPolicySandboxFlags.AllowPopups, _allowPopupsSandboxFlag);
                    AppendSandboxFlag(policyBuilder, ContentSecurityPolicySandboxFlags.AllowSameOrigin, _allowSameOriginSandboxFlag);
                    AppendSandboxFlag(policyBuilder, ContentSecurityPolicySandboxFlags.AllowScripts, _allowScriptsSandboxFlag);
                    AppendSandboxFlag(policyBuilder, ContentSecurityPolicySandboxFlags.AllowTopNavigation, _allowTopNavigationSandboxFlag);
                }

                policyBuilder.Append(_directivesDelimiter);
            }
        }

        private void AppendSandboxFlag(StringBuilder policyBuilder, ContentSecurityPolicySandboxFlags flag, string flagValue)
        {
            if (SandboxFlags.HasFlag(flag))
            {
                policyBuilder.Append(flagValue);
            }
        }
        #endregion
    }
}
