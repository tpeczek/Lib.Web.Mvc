using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Web.Mvc;
using System.Web;
using System.Xml.Xsl;

namespace Lib.Web.Mvc.Html
{
    /// <summary>
    /// Provides support for rendering a XML.
    /// </summary>
    public static class RenderXmlExtensions
    {
        #region Fields
        private static XslCompiledTransform _transparentTransform;
        #endregion

        #region Constructor
        static RenderXmlExtensions()
        {
            XmlTextReader copyTransformReader = new XmlTextReader(new StringReader("<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'><xsl:template match=\"/\"> <xsl:copy-of select=\".\"/> </xsl:template> </xsl:stylesheet>"));
            _transparentTransform = new XslCompiledTransform();
            _transparentTransform.Load(copyTransformReader);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders the specified XML by using the specified HMTL helper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="xmlDocument">The XML document</param>
        /// <param name="transform">The XSLT transformation</param>
        public static void RenderXml(this HtmlHelper htmlHelper, XmlDocument xmlDocument, XslCompiledTransform transform)
        {
            RenderXmlInternal(htmlHelper, xmlDocument, null, String.Empty, transform, String.Empty, null);
        }

        /// <summary>
        /// Renders the specified XML by using the specified HMTL helper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="xmlDocument">The XML document</param>
        /// <param name="transform">The XSLT transformation</param>
        /// <param name="transformArgumentList">The XSLT transformation arguments list</param>
        public static void RenderXml(this HtmlHelper htmlHelper, XmlDocument xmlDocument, XslCompiledTransform transform, XsltArgumentList transformArgumentList)
        {
            RenderXmlInternal(htmlHelper, xmlDocument, null, String.Empty, transform, String.Empty, transformArgumentList);
        }

        /// <summary>
        /// Renders the specified XML by using the specified HMTL helper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="xmlDocument">The XML document</param>
        /// <param name="transformSource">The XSLT transformation source</param>
        public static void RenderXml(this HtmlHelper htmlHelper, XmlDocument xmlDocument, string transformSource)
        {
            RenderXmlInternal(htmlHelper, xmlDocument, null, String.Empty, null, transformSource, null);
        }

        /// <summary>
        /// Renders the specified XML by using the specified HMTL helper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="xmlDocument">The XML document</param>
        /// <param name="transformSource">The XSLT transformation source</param>
        /// <param name="transformArgumentList">The XSLT transformation arguments list</param>
        public static void RenderXml(this HtmlHelper htmlHelper, XmlDocument xmlDocument, string transformSource, XsltArgumentList transformArgumentList)
        {
            RenderXmlInternal(htmlHelper, xmlDocument, null, String.Empty, null, transformSource, transformArgumentList);
        }

        /// <summary>
        /// Renders the specified XML by using the specified HMTL helper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="xpathNavigator">The XML XPath navigator</param>
        /// <param name="transform">The XSLT transformation</param>
        public static void RenderXml(this HtmlHelper htmlHelper, XPathNavigator xpathNavigator, XslCompiledTransform transform)
        {
            RenderXmlInternal(htmlHelper, null, xpathNavigator, String.Empty, transform, String.Empty, null);
        }

        /// <summary>
        /// Renders the specified XML by using the specified HMTL helper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="xpathNavigator">The XML XPath navigator</param>
        /// <param name="transform">The XSLT transformation</param>
        /// <param name="transformArgumentList">The XSLT transformation arguments list</param>
        public static void RenderXml(this HtmlHelper htmlHelper, XPathNavigator xpathNavigator, XslCompiledTransform transform, XsltArgumentList transformArgumentList)
        {
            RenderXmlInternal(htmlHelper, null, xpathNavigator, String.Empty, transform, String.Empty, transformArgumentList);
        }

        /// <summary>
        /// Renders the specified XML by using the specified HMTL helper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="xpathNavigator">The XML XPath navigator</param>
        /// <param name="transformSource">The XSLT transformation source</param>
        public static void RenderXml(this HtmlHelper htmlHelper, XPathNavigator xpathNavigator, string transformSource)
        {
            RenderXmlInternal(htmlHelper, null, xpathNavigator, String.Empty, null, transformSource, null);
        }

        /// <summary>
        /// Renders the specified XML by using the specified HMTL helper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="xpathNavigator">The XML XPath navigator</param>
        /// <param name="transformSource">The XSLT transformation source</param>
        /// <param name="transformArgumentList">The XSLT transformation arguments list</param>
        public static void RenderXml(this HtmlHelper htmlHelper, XPathNavigator xpathNavigator, string transformSource, XsltArgumentList transformArgumentList)
        {
            RenderXmlInternal(htmlHelper, null, xpathNavigator, String.Empty, null, transformSource, transformArgumentList);
        }

        /// <summary>
        /// Renders the specified XML by using the specified HMTL helper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="documentSource">The XML path</param>
        /// <param name="transform">The XSLT transformation</param>
        public static void RenderXml(this HtmlHelper htmlHelper, string documentSource, XslCompiledTransform transform)
        {
            RenderXmlInternal(htmlHelper, null, null, documentSource, transform, String.Empty, null);
        }

        /// <summary>
        /// Renders the specified XML by using the specified HMTL helper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="documentSource">The XML path</param>
        /// <param name="transform">The XSLT transformation</param>
        /// <param name="transformArgumentList">The XSLT transformation arguments list</param>
        public static void RenderXml(this HtmlHelper htmlHelper, string documentSource, XslCompiledTransform transform, XsltArgumentList transformArgumentList)
        {
            RenderXmlInternal(htmlHelper, null, null, documentSource, transform, String.Empty, transformArgumentList);
        }

        /// <summary>
        /// Renders the specified XML by using the specified HMTL helper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="documentSource">The XML path</param>
        /// <param name="transformSource">The XSLT transformation source</param>
        public static void RenderXml(this HtmlHelper htmlHelper, string documentSource, string transformSource)
        {
            RenderXmlInternal(htmlHelper, null, null, documentSource, null, transformSource, null);
        }

        /// <summary>
        /// Renders the specified XML by using the specified HMTL helper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper</param>
        /// <param name="documentSource">The XML path</param>
        /// <param name="transformSource">The XSLT transformation source</param>
        /// <param name="transformArgumentList">The XSLT transformation arguments list</param>
        public static void RenderXml(this HtmlHelper htmlHelper, string documentSource, string transformSource, XsltArgumentList transformArgumentList)
        {
            RenderXmlInternal(htmlHelper, null, null, documentSource, null, transformSource, transformArgumentList);
        }

        private static void RenderXmlInternal(HtmlHelper htmlHelper, XmlDocument xmlDocument, XPathNavigator xpathNavigator, string documentSource, XslCompiledTransform transform, string transformSource, XsltArgumentList transformArgumentList)
        {
            XPathDocument xpathDocument = null;
            //Checking if we have been given XmlDocument or XPathNavigator directly, or do we have path for document
            if ((xmlDocument == null) && (xpathNavigator == null) && (!String.IsNullOrEmpty(documentSource) && (documentSource.Trim().Length != 0)))
            {
                //Checking if path is absolute or relative
                if (!Path.IsPathRooted(documentSource))
                    //Mapping the relative path
                    documentSource = htmlHelper.ViewContext.HttpContext.Server.MapPath(documentSource);
                
                //Loading XML from file into XPathDocument
                using (FileStream documentStream = new FileStream(documentSource, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlTextReader documentReader = new XmlTextReader(documentStream);
                    xpathDocument = new XPathDocument(documentReader);
                }
            }

            //Checking if we have been given XslCompiledTransform directly, or do we have path for transform
            if ((transform == null) && (!String.IsNullOrEmpty(transformSource) && (transformSource.Trim().Length != 0)))
            {
                //Checking if path is absolute or relative
                if (!Path.IsPathRooted(transformSource))
                    //Mapping the relative path
                    transformSource = htmlHelper.ViewContext.HttpContext.Server.MapPath(transformSource);

                //Loading XSLT from file into XslCompiledTransform
                using (FileStream transformStream = new FileStream(transformSource, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlTextReader tranformReader = new XmlTextReader(transformStream);
                    transform = new XslCompiledTransform();
                    transform.Load(tranformReader);
                }
            }

            //Checking if we have XML in any form
            if (((xmlDocument != null) || (xpathDocument != null)) || (xpathNavigator != null))
            {
                //Checking if we have XSLT
                if (transform == null)
                    //If not, let's use transparent one
                    transform = _transparentTransform;

                //Perform transformation based on form in which we have our XML
                if (xmlDocument != null)
                    transform.Transform((IXPathNavigable)xmlDocument, transformArgumentList, htmlHelper.ViewContext.Writer);
                else if (xpathNavigator != null)
                    transform.Transform(xpathNavigator, transformArgumentList, htmlHelper.ViewContext.Writer);
                else
                    transform.Transform((IXPathNavigable)xpathDocument, transformArgumentList, htmlHelper.ViewContext.Writer);
            }
        }
        #endregion
    }
}
