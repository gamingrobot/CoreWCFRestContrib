using System.Net;
using CoreWCF;
using CoreWCF.Channels;

namespace CoreWCFRestContrib.ServiceModel.Web
{
    /// <summary>
    /// Incomplete replacement of OutgoingWebResponseContext for CoreWCF 
    /// </summary>
    public class OutgoingWebResponseContext
    {
        private readonly OperationContext _operationContext;

        public OutgoingWebResponseContext(OperationContext operationContext)
        {
            _operationContext = operationContext;
        }

        public WebHeaderCollection Headers => MessageProperty.Headers;

        public HttpStatusCode StatusCode
        {
            get => MessageProperty.StatusCode;
            set => MessageProperty.StatusCode = value;
        }

        public string StatusDescription
        {
            get => MessageProperty.StatusDescription;
            set => MessageProperty.StatusDescription = value;
        }

        public string ContentType
        {
            get => MessageProperty.Headers[HttpResponseHeader.ContentType];
            set => MessageProperty.Headers[HttpResponseHeader.ContentType] = value;
        }

        public bool SuppressEntityBody
        {
            get => MessageProperty.SuppressEntityBody;
            set => MessageProperty.SuppressEntityBody = value;
        }

        public string Location
        {
            get => MessageProperty.Headers[HttpResponseHeader.Location];
            set => MessageProperty.Headers[HttpResponseHeader.Location] = value;
        }

        private HttpResponseMessageProperty MessageProperty
        {
            get
            {
                if (!_operationContext.OutgoingMessageProperties.ContainsKey(HttpResponseMessageProperty.Name))
                {
                    _operationContext.OutgoingMessageProperties.Add(HttpResponseMessageProperty.Name,
                        new HttpResponseMessageProperty());
                }

                return _operationContext.OutgoingMessageProperties[HttpResponseMessageProperty.Name] as
                    HttpResponseMessageProperty;
            }
        }
	}
}