using System;
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class ServiceAuthenticationAttribute : Attribute, IServiceBehavior, IContractBehavior
    {
        readonly ServiceAuthenticationBehavior _behavior = new ServiceAuthenticationBehavior();

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        { _behavior.AddBindingParameters(serviceDescription, serviceHostBase, endpoints, bindingParameters); }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        { _behavior.ApplyDispatchBehavior(serviceDescription, serviceHostBase); }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        { _behavior.Validate(serviceDescription, serviceHostBase); }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        { _behavior.AddBindingParameters(contractDescription, endpoint, bindingParameters); }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        { _behavior.ApplyClientBehavior(contractDescription, endpoint, clientRuntime); }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        { _behavior.ApplyDispatchBehavior(contractDescription, endpoint, dispatchRuntime); }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        { _behavior.Validate(contractDescription, endpoint); }
    }
}
