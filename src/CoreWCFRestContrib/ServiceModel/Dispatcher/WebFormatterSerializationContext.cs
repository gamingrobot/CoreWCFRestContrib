﻿using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace CoreWCFRestContrib.ServiceModel.Dispatcher
{
    public class WebFormatterSerializationContext
    {
        public enum SerializationFormat
        {
            Binary,
            Xml,
            Json
        }

        private WebFormatterSerializationContext(byte[] binaryData)
        {
            BinaryData = binaryData;
            ContentFormat = SerializationFormat.Binary;
        }

        private WebFormatterSerializationContext(XmlObjectSerializer xmlSerializer)
        {
            if (xmlSerializer is DataContractJsonSerializer)
                ContentFormat = SerializationFormat.Json;
            else
                ContentFormat = SerializationFormat.Xml;
            XmlSerializer = xmlSerializer;
        }

        public SerializationFormat ContentFormat { get; private set; }
        public byte[] BinaryData { get; private set; }
        public XmlObjectSerializer XmlSerializer { get; private set; }

        public static WebFormatterSerializationContext CreateXmlSerialized(XmlObjectSerializer xmlSerializer)
        {
            return new WebFormatterSerializationContext(xmlSerializer);
        }

        public static WebFormatterSerializationContext CreateBinary(byte[] binaryData)
        {
            return new WebFormatterSerializationContext(binaryData);
        }
    }
}
