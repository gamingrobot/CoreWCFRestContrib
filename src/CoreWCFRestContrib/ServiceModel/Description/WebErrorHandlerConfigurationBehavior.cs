using System;
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCFRestContrib.DependencyInjection;
using CoreWCFRestContrib.Diagnostics;
using CoreWCFRestContrib.ServiceModel.Web;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class WebErrorHandlerConfigurationBehavior : IServiceBehavior
    {
        private readonly Type _exceptionDataContract;

        public WebErrorHandlerConfigurationBehavior(
            Type logHandler,
            string unhandledErrorMessage,
            bool returnRawException,
            Type exceptionDataContract)
        {
            LogHandler = logHandler != null
                            ? DependencyResolver.Current.GetInfrastructureService<IWebLogHandler>(logHandler)
                            : DependencyResolver.Current.GetInfrastructureService<IWebLogHandler>();
            
            UnhandledErrorMessage = unhandledErrorMessage;
            ReturnRawException = returnRawException;
            _exceptionDataContract = exceptionDataContract;
        }

        public IWebLogHandler LogHandler { get; set; }
        public string UnhandledErrorMessage { get; set; }
        public bool ReturnRawException { get; set; }

        public IWebExceptionDataContract CreateExceptionDataContract()
        {
            return _exceptionDataContract != null
                        ? DependencyResolver.Current.GetInfrastructureService<IWebExceptionDataContract>(_exceptionDataContract)
                        : DependencyResolver.Current.GetInfrastructureService<IWebExceptionDataContract>();
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }
    }
}
