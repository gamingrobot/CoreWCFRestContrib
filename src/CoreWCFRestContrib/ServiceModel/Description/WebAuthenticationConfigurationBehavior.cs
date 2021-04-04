using System;
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using CoreWCFRestContrib.DependencyInjection;
using CoreWCFRestContrib.ServiceModel.Dispatcher;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class WebAuthenticationConfigurationBehavior : IServiceBehavior, IContractBehavior
    {
        public WebAuthenticationConfigurationBehavior(
            Type authenticationHandler,
            Type usernamePasswordValidator,
            bool requireSecureTransport,
            string source)
        {
            AuthenticationHandler = authenticationHandler != null
                ? DependencyResolver.Current.GetInfrastructureService<IWebAuthenticationHandler>(authenticationHandler)
                : DependencyResolver.Current.GetInfrastructureService<IWebAuthenticationHandler>().ThrowIfNull();

            UsernamePasswordValidatorType = usernamePasswordValidator;

            Source = source;
            RequireSecureTransport = requireSecureTransport;
        }

        public IWebAuthenticationHandler AuthenticationHandler { get; private set; }
        public Type UsernamePasswordValidatorType { get; private set; }
        public bool RequireSecureTransport { get; set; }
        public string Source { get; set; }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }
        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime) { }
        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime) { }
        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint) { }
    }
}
