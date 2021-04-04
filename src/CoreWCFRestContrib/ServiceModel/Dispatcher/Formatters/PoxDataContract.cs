﻿using System;
using System.IO;
using CoreWCFRestContrib.Runtime.Serialization;
using CoreWCFRestContrib.Xml;

namespace CoreWCFRestContrib.ServiceModel.Dispatcher.Formatters
{
    public class PoxDataContract : IWebFormatter 
    {
        public object Deserialize(WebFormatterDeserializationContext context, Type type)
        {
            if (context.ContentFormat == WebFormatterDeserializationContext.DeserializationFormat.Xml)
            {
                return CreateSerializer(type).ReadObject(context.XmlReader);
            }
            throw new InvalidDataException("Data must be in xml format.");
        }

        public WebFormatterSerializationContext Serialize(object data, Type type)
        {
            return WebFormatterSerializationContext.CreateXmlSerialized(CreateSerializer(type));
        }

        private static WrappedDataContractSerializer CreateSerializer(Type type)
        {
            return new WrappedDataContractSerializer(type,
                        r => new PoxXmlReader(r),
                        w => new PoxXmlWriter(w),
                        false);
        }
    }
}
