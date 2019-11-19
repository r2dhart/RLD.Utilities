using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RLD.Utilities.Extensions
{
    /// <summary>
    /// LinqExtensions Class
    /// 
    /// Contains the extension methods I have developed to add onto the System.Linq library.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Converts an XDocument object to an XMLDocument object
        /// </summary>
        /// <param name="xDocument">The XDocument to be converted to XML.</param>
        /// <returns>An XmlDocument contianing the contents of the XDocument.</returns>
        /// Contributed by Russell Dehart
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                using (var xmlReader = xDocument.CreateReader())
                {
                    xmlDocument.Load(xmlReader);
                }
            }
            catch (XmlException ex)
            {
                throw new System.Exception("There is a load or parse error in the XML", ex);
            }


            return xmlDocument;
        }

        /// <summary>
        /// Converts an XDocument object to an XMLDocument object
        /// </summary>
        /// <param name="xElement">The XElement to be converted to XML.</param>
        /// <returns>An XmlDocument contianing the contents of the XElement.</returns>
        /// Contributed by Russell Dehart
        public static XmlDocument ToXmlDocument(this XElement xElement)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings xws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false };

            XmlDocument results;

            try
            {
                using (var xw = XmlWriter.Create(sb, xws))
                {
                    xElement.WriteTo(xw);
                }

                results = new XmlDocument();
                results.LoadXml(sb.ToString());
            }
            catch (XmlException)
            {
                throw;
            }
            catch (System.ArgumentNullException)
            {
                throw;
            }

            return results;
        }
    }
}
