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
            return BeginCspScript(htmlHelper, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Writes an opening script tag to the response, and sets attributes related to Content Security Policy
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        /// <returns></returns>
        public static IDisposable BeginCspScript(this HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {
            return new ContentSecurityPolicyInlineElement(htmlHelper.ViewContext, ContentSecurityPolicyInlineElement.ScriptTagName, htmlAttributes);
        }

        /// <summary>
        /// Writes an opening style tag to the response, and sets attributes related to Content Security Policy
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <returns></returns>
        public static IDisposable BeginCspStyle(this HtmlHelper htmlHelper)
        {
            return BeginCspStyle(htmlHelper, null);
        }

        /// <summary>
        /// Writes an opening style tag to the response, and sets attributes related to Content Security Policy
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        /// <returns></returns>
        public static IDisposable BeginCspStyle(this HtmlHelper htmlHelper, object htmlAttributes)
        {
            return BeginCspStyle(htmlHelper, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Writes an opening style tag to the response, and sets attributes related to Content Security Policy
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        /// <returns></returns>
        public static IDisposable BeginCspStyle(this HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {
            return new ContentSecurityPolicyInlineElement(htmlHelper.ViewContext, ContentSecurityPolicyInlineElement.StyleTagName, htmlAttributes);
        }
        #endregion

        #region Classes
        private class ContentSecurityPolicyInlineElement : IDisposable
        {
            #region Constants
            internal const string ScriptTagName = "script";
            internal const string StyleTagName = "style";

            private const string _nonceAttribute = "nonce";
            private const string _sha256SourceFormat = " 'sha256-{0}'";
            #endregion

            #region Fields
            private static IDictionary<string, string> _inlineExecutionContextKeys = new Dictionary<string, string>
            {
                { ScriptTagName, ContentSecurityPolicyAttribute.InlineExecutionContextKeys[ContentSecurityPolicyAttribute.ScriptDirective] },
                { StyleTagName, ContentSecurityPolicyAttribute.InlineExecutionContextKeys[ContentSecurityPolicyAttribute.StyleDirective] }
            };

            private static IDictionary<string, string> _hashListBuilderContextKeys = new Dictionary<string, string>
            {
                { ScriptTagName, ContentSecurityPolicyAttribute.HashListBuilderContextKeys[ContentSecurityPolicyAttribute.ScriptDirective] },
                { StyleTagName, ContentSecurityPolicyAttribute.HashListBuilderContextKeys[ContentSecurityPolicyAttribute.StyleDirective] }
            };

            private readonly ViewContext _viewContext;
            private readonly ContentSecurityPolicyInlineExecution _inlineExecution;
            private readonly int _viewBuilderIndex;
            private readonly TagBuilder _elementTag;
            #endregion

            #region Constructor
            public ContentSecurityPolicyInlineElement(ViewContext context, string elementTagName, IDictionary<string, object> htmlAttributes)
            {
                _viewContext = context;

                _inlineExecution = (ContentSecurityPolicyInlineExecution)_viewContext.HttpContext.Items[_inlineExecutionContextKeys[elementTagName]];

                _elementTag = new TagBuilder(elementTagName);
                _elementTag.MergeAttributes(htmlAttributes);
                if (_inlineExecution == ContentSecurityPolicyInlineExecution.Nonce)
                {
                    _elementTag.MergeAttribute(_nonceAttribute, (string)_viewContext.HttpContext.Items[ContentSecurityPolicyAttribute.NonceRandomContextKey]);
                }

                _viewContext.Writer.Write(_elementTag.ToString(TagRenderMode.StartTag));

                if (_inlineExecution == ContentSecurityPolicyInlineExecution.Hash)
                {
                    _viewBuilderIndex = ((StringWriter)_viewContext.Writer).GetStringBuilder().Length;
                }
            }
            #endregion

            #region IDisposable Members
            public void Dispose()
            {
                if (_inlineExecution == ContentSecurityPolicyInlineExecution.Hash)
                {
                    StringBuilder viewBuilder = ((StringWriter)_viewContext.Writer).GetStringBuilder();
                    string elementContent = viewBuilder.ToString(_viewBuilderIndex, viewBuilder.Length - _viewBuilderIndex).Replace("\r\n", "\n");
                    byte[] elementHashBytes = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(elementContent));
                    string elementHash = Convert.ToBase64String(elementHashBytes);
                    ((StringBuilder)_viewContext.HttpContext.Items[_hashListBuilderContextKeys[_elementTag.TagName]]).AppendFormat(_sha256SourceFormat, elementHash);
                }
                _viewContext.Writer.Write(_elementTag.ToString(TagRenderMode.EndTag));
            }
            #endregion
        }
        #endregion
    }
}
