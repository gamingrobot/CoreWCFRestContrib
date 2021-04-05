﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using CoreWCFRestContrib.DependencyInjection;
using CoreWCFRestContrib.Net.Http;
using CoreWCFRestContrib.ServiceModel.Web;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class ErrorHandlerBehavior : IServiceBehavior, IContractBehavior
    {
        public const string HttpRequestInformationProperty = "HttpRequestInformation";

        private readonly Type _errorHandler;

        public ErrorHandlerBehavior(Type type, string unhandledErrorMessage, bool returnRawException)
        {
            _errorHandler = type;
            UnhandledErrorMessage = unhandledErrorMessage;
            ReturnRawException = returnRawException;
        }

        public string UnhandledErrorMessage { get; set; }
        public bool ReturnRawException { get; set; }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var errorHandler = GetErrorHandler();
            if (errorHandler == null) return;

            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                if (!dispatcher.ErrorHandlers.Contains(errorHandler)) dispatcher.ErrorHandlers.Add(errorHandler);

                foreach (var endpoint in dispatcher.Endpoints.
                                Where(endpoint => endpoint.DispatchRuntime.MessageInspectors.
                                    FirstOrDefault(i => i.GetType() == typeof (HttpRequestInformationInspector)) == null))
                    endpoint.DispatchRuntime.MessageInspectors.Add(new HttpRequestInformationInspector());
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime) 
        {
            var errorHandler = GetErrorHandler();
            if (errorHandler == null) return;

            if (!dispatchRuntime.EndpointDispatcher.ChannelDispatcher.ErrorHandlers.Contains(errorHandler))
                dispatchRuntime.EndpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(errorHandler);

            foreach (var endpointDispatcher in dispatchRuntime.EndpointDispatcher.ChannelDispatcher.Endpoints.
                            Where(endpointDispatcher => endpointDispatcher.DispatchRuntime.MessageInspectors.
                                FirstOrDefault(i => i.GetType() == typeof(HttpRequestInformationInspector)) == null))
                endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new HttpRequestInformationInspector());
        }
        
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }
        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime) { }
        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint) { }

        private IErrorHandler GetErrorHandler()
        {
            return _errorHandler != null
                        ? DependencyResolver.Current.GetInfrastructureService<IErrorHandler>(_errorHandler)
                        : DependencyResolver.Current.GetInfrastructureService<IErrorHandler>();
        }

        private class HttpRequestInformationInspector : IDispatchMessageInspector   
        {
            public object AfterReceiveRequest(ref Message request, 
                IClientChannel channel, InstanceContext instanceContext)
            {
                if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(
                        HttpRequestInformationProperty)) return null;

                var info = new RequestInformation();

                var contentLengthHeader =
                    WebOperationContext.Current.IncomingRequest.Headers[HttpRequestHeader.ContentLength];

                long contentLength;
                if (!string.IsNullOrEmpty(contentLengthHeader))
                    long.TryParse(contentLengthHeader, out contentLength);
                else
                    contentLength = -1;

                info.ContentLength = contentLength;
                info.Uri = OperationContext.Current.IncomingMessageHeaders.To;
                info.Method = WebOperationContext.Current.IncomingRequest.Method;
                info.ContentType = WebOperationContext.Current.IncomingRequest.ContentType;
                info.Accept = WebOperationContext.Current.IncomingRequest.Accept;
                info.UserAgent = WebOperationContext.Current.IncomingRequest.UserAgent;
                info.Headers = WebOperationContext.Current.IncomingRequest.Headers;

                OperationContext.Current.OutgoingMessageProperties.Add(
                    HttpRequestInformationProperty, info);
                return null;
            }

            public void BeforeSendReply(ref Message reply, object correlationState) { }
        }
    }
}
