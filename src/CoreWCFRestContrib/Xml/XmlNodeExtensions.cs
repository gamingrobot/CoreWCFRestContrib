using System.Collections;
using System.Linq;
using System.Xml;

namespace CoreWCFRestContrib.Xml
{
    public static class XmlNodeExtensions
    {
        public static void SortAlphabetically<T>(this XmlNode node, bool includeChildren) where T : XmlNode
        {
            IEnumerable nodes = node.ChildNodes.OfType<T>().OrderBy(n => n.Name);
            foreach (XmlElement child in nodes)
            {
                node.AppendChild(child);
                if (includeChildren && node.HasChildNodes) SortAlphabetically<T>(child, true);
            }
        }
    }
}
