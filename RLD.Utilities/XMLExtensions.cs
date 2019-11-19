using System.Xml;
using System.Xml.Linq;

namespace RLD.Utilities.Extensions
{

    /// <summary>
    /// XmlExtensions Class
    /// 
    /// Contains the extension methods I have developed to add onto the System.XML library.
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// Converts an MLDocument object to an XDocument object
        /// </summary>
        /// <param name="xmlDocument">The XMLDocument to be converted.</param>
        /// <returns>An XDocument contianing the contents of the XmlDocument.</returns>
        /// Contributed by Russell Dehart
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                try
                {
                    nodeReader.MoveToContent();
                }
                catch (XmlException)
                {
                    throw;
                }
                catch (System.InvalidOperationException)
                {
                    throw;
                }

                return XDocument.Load(nodeReader);
            }
        }
    }
}
