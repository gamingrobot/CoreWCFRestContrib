using System;
using System.Net;
using CoreWCF;
using CoreWCF.Channels;

namespace CoreWCFRestContrib.ServiceModel.Web
{
    /// <summary>
    /// Incomplete replacement of IncomingWebRequestContext for CoreWCF 
    /// </summary>
    public class IncomingWebRequestContext
    {
        private readonly OperationContext _operationContext;

        public IncomingWebRequestContext(OperationContext operationContext)
        {
            _operationContext = operationContext;
        }

        public WebHeaderCollection Headers => MessageProperty.Headers;

        public string Accept => MessageProperty.Headers[HttpRequestHeader.Accept];

        public string Method => MessageProperty.Method;

        public string UserAgent => MessageProperty.Headers[HttpRequestHeader.UserAgent];

        public string ContentType => MessageProperty.Headers[HttpRequestHeader.ContentType];

        private HttpRequestMessageProperty MessageProperty
        {
            get
            {
                if (!_operationContext.IncomingMessageProperties.ContainsKey(HttpRequestMessageProperty.Name))
                {
                    throw new InvalidOperationException("HttpRequestMessageProperty missing from OperationContext");
                }

                return _operationContext.IncomingMessageProperties[HttpRequestMessageProperty.Name] as
                    HttpRequestMessageProperty;
            }
        }
    }
}