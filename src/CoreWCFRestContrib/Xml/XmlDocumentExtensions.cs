using System.Xml;

namespace CoreWCFRestContrib.Xml
{
    public static class XmlDocumentExtensions
    {
        public static void SortAlphabetically<T>(this XmlDocument document) where T : XmlNode
        {
            document.DocumentElement.SortAlphabetically<T>(true);
        }
    }
}
