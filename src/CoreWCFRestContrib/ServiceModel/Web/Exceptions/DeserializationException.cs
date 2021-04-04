﻿using System;
using System.Runtime.Serialization;
using System.Xml;

namespace CoreWCFRestContrib.ServiceModel.Web.Exceptions
{
    public class DeserializationException : WebException
    {
        public DeserializationException(Exception exception)
            : base(exception,
                System.Net.HttpStatusCode.BadRequest, 
                GetMessage(exception)) { }

        public DeserializationException(Exception exception, string message, params object[] args)
            : base(exception,
                System.Net.HttpStatusCode.BadRequest, 
                message,
                args) { }

        private static string GetMessage(Exception exception)
        {
            const string message = "The request body could not be deserialized. {0}";

            Exception messageException = FindException<XmlException>(exception);
            if (messageException != null)
                return string.Format(message, messageException.Message);

            messageException = FindException<SerializationException>(exception);
            return string.Format(message, messageException != null ? 
                messageException.Message : 
                "Please check the formatting of your request body.");
        }

        private static T FindException<T>(Exception exception) where T : Exception 
        {
            var currentException = exception;

            do
            {
                if (currentException.GetType() == typeof(T)) return (T)currentException;
                currentException = currentException.InnerException;
            } while (currentException != null);

            return null;
        }
    }
}
