using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Web;

namespace Lib.Web.Mvc
{
    /// <summary>
    /// Action result for XML with optional XSL Transformation
    /// </summary>
    public class XmlActionResult: ActionResult
    {
        #region Fields
        private XmlDocument _xmlDocument;
        private XPathNavigator _xpathNavigator;
        private string _documentContent;
        private string _documentSource;
        private XslCompiledTransform _transform;
        private static XslCompiledTransform _transparentTransform;
        private string _transformSource;
        private XsltArgumentList _transformArgumentList;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the XmlDocument to write to the response.
        /// </summary>
        public XmlDocument Document
        {
            get { return _xmlDocument; }
            set
            {
                _xmlDocument = value;
                _documentContent = String.Empty;
                _documentSource = String.Empty;
                _xpathNavigator = null;
            }
        }

        /// <summary>
        /// Gets or sets a string that contains the XML document to write to the response.
        /// </summary>
        public string DocumentContent
        {
            get { return _documentContent; }
            set
            {
                _xmlDocument = null;
                _documentContent = value;
                _documentSource = String.Empty;
                _xpathNavigator = null;
            }
        }

        /// <summary>
        /// Gets or sets the path to an XML document to write to the response.
        /// </summary>
        public string DocumentSource
        {
            get { return _documentSource; }
            set
            {
                _xmlDocument = null;
                _documentContent = String.Empty;
                _documentSource = value;
                _xpathNavigator = null;
            }
        }

        /// <summary>
        /// Gets or sets the XslCompiledTransform object that formats the XML document before it is written to the response.
        /// </summary>
        public XslCompiledTransform Transform
        {
            get { return _transform; }
            set
            {
                _transform = value;
                _transformSource = String.Empty;
            }
        }

        /// <summary>
        /// Gets or sets a XsltArgumentList that contains a list of optional arguments passed to the style sheet and used during the XSL transformation.
        /// </summary>
        public XsltArgumentList TransformArgumentList
        {
            get { return _transformArgumentList; }
            set { _transformArgumentList = value; }
        }

        /// <summary>
        /// Gets or sets the path to an XSL Transformation style sheet that formats the XML document before it is written to the response.
        /// </summary>
        public string TransformSource
        {
            get { return _transformSource; }
            set
            {
                _transform = null;
                _transformSource = value;
            }
        }

        /// <summary>
        /// Gets or sets a cursor model for navigating and editing the XML data to write to the response.
        /// </summary>
        public XPathNavigator XPathNavigator
        {
            get { return _xpathNavigator; }
            set
            {
                _xmlDocument = null;
                _documentContent = String.Empty;
                _documentSource = String.Empty;
                _xpathNavigator = value;
            }
        }
        #endregion

        #region Constructor
        static XmlActionResult()
        {
            XmlTextReader copyTransformReader = new XmlTextReader(new StringReader("<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'><xsl:template match=\"/\"> <xsl:copy-of select=\".\"/> </xsl:template> </xsl:stylesheet>"));
            _transparentTransform = new XslCompiledTransform();
            _transparentTransform.Load(copyTransformReader);
        }
        #endregion

        #region Methods
        /// <summary>
        /// When called by the action invoker, renders the result of XML transformation to the response.
        /// </summary>
        /// <param name="context">The context that the result is executed in.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            XPathDocument xpathDocument = null;
            //Checking if we have been given XmlDocument or XPathNavigator directly
            if ((_xmlDocument == null) && (_xpathNavigator == null))
            {
                //Checking if we have document content
                if (!String.IsNullOrEmpty(_documentContent))
                {
                    StringReader documentReader = new StringReader(_documentContent);
                    xpathDocument = new XPathDocument(documentReader);
                }
                //Checking if we have path for document
                else if (!String.IsNullOrEmpty(_documentSource) && (_documentSource.Trim().Length != 0))
                {
                    //Checking if path is absolute or relative
                    if (!Path.IsPathRooted(_documentSource))
                        //Mapping the relative path
                        _documentSource = context.HttpContext.Server.MapPath(_documentSource);

                    //Loading XML from file into XPathDocument
                    using (FileStream documentStream = new FileStream(_documentSource, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        XmlTextReader documentReader = new XmlTextReader(documentStream);
                        xpathDocument = new XPathDocument(documentReader);
                    }
                }
            }

            //Checking if we have been given XslCompiledTransform directly, or do we have path for transform
            if ((_transform == null) && (!String.IsNullOrEmpty(_transformSource) && (_transformSource.Trim().Length != 0)))
            {
                //Checking if path is absolute or relative
                if (!Path.IsPathRooted(_transformSource))
                    //Mapping the relative path
                    _transformSource = context.HttpContext.Server.MapPath(_transformSource);

                //Loading XSLT from file into XslCompiledTransform
                using (FileStream transformStream = new FileStream(_transformSource, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlTextReader tranformReader = new XmlTextReader(transformStream);
                    _transform = new XslCompiledTransform();
                    _transform.Load(tranformReader);
                }
            }

            //Checking if we have XML in any form
            if (((_xmlDocument != null) || (xpathDocument != null)) || (_xpathNavigator != null))
            {
                context.HttpContext.Response.ContentType = "text/html";

                //Checking if we have XSLT
                if (_transform == null)
                    //If not, let's use transparent one
                    _transform = _transparentTransform;

                //Perform transformation based on form in which we have our XML
                if (_xmlDocument != null)
                    _transform.Transform((IXPathNavigable)_xmlDocument, _transformArgumentList, context.HttpContext.Response.Output);
                else if (_xpathNavigator != null)
                    _transform.Transform(_xpathNavigator, _transformArgumentList, context.HttpContext.Response.Output);
                else
                    _transform.Transform((IXPathNavigable)xpathDocument, _transformArgumentList, context.HttpContext.Response.Output);
            }
        }
        #endregion
    }
}
