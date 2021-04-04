using System;
using System.Collections.Generic;
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using CoreWCFRestContrib.ServiceModel.Dispatcher;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class WebDispatchFormatterConfigurationBehavior : IServiceBehavior, IContractBehavior 
    {
        public WebDispatchFormatterConfigurationBehavior(
            Dictionary<string, Type> formatters,
            string defaultMimeType)
        {
            FormatterFactory = new WebFormatterFactory(formatters, defaultMimeType);
        }

        public WebFormatterFactory FormatterFactory { get; set; }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }
        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime) { }
        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime) { }
        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint) { }
    }
}
