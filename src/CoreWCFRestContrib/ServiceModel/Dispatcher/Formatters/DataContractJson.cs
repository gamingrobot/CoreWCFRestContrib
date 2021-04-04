﻿using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace CoreWCFRestContrib.ServiceModel.Dispatcher.Formatters
{
    public class DataContractJson : IWebFormatter
    {
        public object Deserialize(WebFormatterDeserializationContext context, Type type)
        {
            if (context.ContentFormat != WebFormatterDeserializationContext.DeserializationFormat.Xml)
                throw new InvalidDataException("Data must be in xml format.");
            var serializer = new DataContractJsonSerializer(type);
            return serializer.ReadObject(context.XmlReader);
        }

        public WebFormatterSerializationContext Serialize(object data, Type type)
        {
            return WebFormatterSerializationContext.CreateXmlSerialized(new DataContractJsonSerializer(type));
        }
    }
}
