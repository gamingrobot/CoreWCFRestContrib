using System;
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using CoreWCFRestContrib.Reflection;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class ErrorHandlerAttribute : Attribute, IServiceBehavior, IContractBehavior
    {
        readonly ErrorHandlerBehavior _behavior;

        public ErrorHandlerAttribute(Type errorHandler) : this(errorHandler, null, false) { }
        public ErrorHandlerAttribute(string unhandledErrorMessage, bool returnRawException) : 
            this(null, unhandledErrorMessage, returnRawException) { }

        public ErrorHandlerAttribute(
            Type errorHandler, 
            string unhandledErrorMessage,
            bool returnRawException)
        {
            if (errorHandler != null && !errorHandler.CastableAs<IErrorHandler>())
                throw new Exception("errorHandler must implement IErrorHandler.");

            _behavior = new ErrorHandlerBehavior(errorHandler, unhandledErrorMessage, returnRawException);
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        { _behavior.AddBindingParameters(contractDescription, endpoint, bindingParameters); }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        { _behavior.ApplyClientBehavior(contractDescription, endpoint, clientRuntime); }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        { _behavior.ApplyDispatchBehavior(contractDescription, endpoint, dispatchRuntime); }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        { _behavior.Validate(contractDescription, endpoint); }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        { _behavior.AddBindingParameters(serviceDescription, serviceHostBase, endpoints, bindingParameters); }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        { _behavior.ApplyDispatchBehavior(serviceDescription, serviceHostBase); }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        { _behavior.Validate(serviceDescription, serviceHostBase); }
    }
}
