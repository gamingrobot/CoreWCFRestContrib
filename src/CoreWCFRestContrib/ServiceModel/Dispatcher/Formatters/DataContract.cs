using System;
using System.IO;
using System.Runtime.Serialization;

namespace CoreWCFRestContrib.ServiceModel.Dispatcher.Formatters
{
    public class DataContract : IWebFormatter 
    {
        public object Deserialize(WebFormatterDeserializationContext context, Type type)
        {
            if (context.ContentFormat == WebFormatterDeserializationContext.DeserializationFormat.Xml)
            {
                var serializer = new DataContractSerializer(type);
                return serializer.ReadObject(context.XmlReader);
            }
            throw new InvalidDataException("Data must be in xml format.");
        }

        public WebFormatterSerializationContext Serialize(object data, Type type)
        {
            return WebFormatterSerializationContext.CreateXmlSerialized(new DataContractSerializer(type));
        }
    }
}
