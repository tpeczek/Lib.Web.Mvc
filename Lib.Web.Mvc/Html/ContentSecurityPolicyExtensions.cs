using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace Lib.Web.Mvc.Html
{
    /// <summary>
    /// Provides support for Content Security Policy Level 2 protected elements.
    /// </summary>
    public static class ContentSecurityPolicyExtensions
    {
        #region Methods
        /// <summary>
        /// Writes an opening script tag to the response, and sets attributes related to Content Security Policy
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <returns></returns>
        public static IDisposable BeginCspScript(this HtmlHelper htmlHelper)
        {
            return BeginCspScript(htmlHelper, null);
        }

        /// <summary>
        /// Writes an opening script tag to the response, and sets attributes related to Content Security Policy
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        /// <returns></returns>
        public static IDisposable BeginCspScript(this HtmlHelper htmlHelper, object htmlAttributes)
        {
            return new ContentSecurityPolicyScript(htmlHelper.ViewContext, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Writes an opening script tag to the response, and sets attributes related to Content Security Policy
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        /// <returns></returns>
        public static IDisposable BeginCspScript(this HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {
            return new ContentSecurityPolicyScript(htmlHelper.ViewContext, htmlAttributes);
        }
        #endregion

        #region Classes
        private class ContentSecurityPolicyScript : IDisposable
        {
            #region Fields
            private readonly ViewContext _viewContext;
            private readonly ContentSecurityPolicyInlineExecution _scriptInlineExecution;
            private readonly int _viewBuilderIndex;
            private readonly TagBuilder _scriptTag;
            #endregion

            #region Constants
            private const string _scriptTagName = "script";
            private const string _nonceAttribute = "nonce";
            private const string _sha256SourceFormat = " 'sha256-{0}'";
            #endregion

            #region Constructor
            public ContentSecurityPolicyScript(ViewContext context, IDictionary<string, object> htmlAttributes)
            {
                _viewContext = context;

                _scriptInlineExecution = (ContentSecurityPolicyInlineExecution)_viewContext.HttpContext.Items[ContentSecurityPolicyAttribute.ScriptInlineExecutionContextKey];

                _scriptTag = new TagBuilder(_scriptTagName);
                _scriptTag.MergeAttributes(htmlAttributes);
                if (_scriptInlineExecution == ContentSecurityPolicyInlineExecution.Nonce)
                {
                    _scriptTag.MergeAttribute(_nonceAttribute, (string)_viewContext.HttpContext.Items[ContentSecurityPolicyAttribute.NonceRandomContextKey]);
                }

                _viewContext.Writer.Write(_scriptTag.ToString(TagRenderMode.StartTag));

                if (_scriptInlineExecution == ContentSecurityPolicyInlineExecution.Hash)
                {
                    _viewBuilderIndex = ((StringWriter)_viewContext.Writer).GetStringBuilder().Length;
                }
            }
            #endregion

            #region IDisposable Members
            public void Dispose()
            {
                if (_scriptInlineExecution == ContentSecurityPolicyInlineExecution.Hash)
                {
                    StringBuilder viewBuilder = ((StringWriter)_viewContext.Writer).GetStringBuilder();
                    string scriptContent = viewBuilder.ToString(_viewBuilderIndex, viewBuilder.Length - _viewBuilderIndex).Replace("\r\n", "\n");
                    byte[] scriptHashBytes = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(scriptContent));
                    string scriptHash = Convert.ToBase64String(scriptHashBytes);
                    ((StringBuilder)_viewContext.HttpContext.Items[ContentSecurityPolicyAttribute.ScriptHashListBuilderContextKey]).AppendFormat(_sha256SourceFormat, scriptHash);
                }
                _viewContext.Writer.Write(_scriptTag.ToString(TagRenderMode.EndTag));
            }
            #endregion
        }
        #endregion
    }
}
