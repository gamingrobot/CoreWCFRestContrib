﻿using System;
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using CoreWCFRestContrib.ServiceModel.Dispatcher;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class ServiceAuthenticationBehavior : IServiceBehavior, IContractBehavior 
    {
        public class ServiceAuthenticationConfigurationMissingException : Exception
        {
            public ServiceAuthenticationConfigurationMissingException() : 
                base("ServiceAuthenticationConfigurationBehavior not applied to contract or service. This behavior is required to configure service authentication.") {}
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var behavior = 
                serviceHostBase.Description.FindBehavior
                        <WebAuthenticationConfigurationBehavior,
                        WebAuthenticationConfigurationAttribute>(b => b.BaseBehavior);

            if (behavior == null)
                throw new ServiceAuthenticationConfigurationMissingException();

            var authenticationHandler = behavior.AuthenticationHandler;
            var usernamePasswordValidator = behavior.UsernamePasswordValidatorType;

            foreach (ChannelDispatcher dispatcher in 
                serviceHostBase.ChannelDispatchers)
                foreach (var endpoint in dispatcher.Endpoints)
                    endpoint.DispatchRuntime.MessageInspectors.Add(
                        new ServiceAuthenticationInspector(
                            authenticationHandler,
                            usernamePasswordValidator,
                            behavior.RequireSecureTransport,
                            behavior.Source));
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            var behavior = 
                dispatchRuntime.ChannelDispatcher.Host.Description.FindBehavior
                        <WebAuthenticationConfigurationBehavior,
                         WebAuthenticationConfigurationAttribute>(b => b.BaseBehavior);
            
            if (behavior == null)
                behavior = contractDescription.FindBehavior
                        <WebAuthenticationConfigurationBehavior,
                         WebAuthenticationConfigurationAttribute>(b => b.BaseBehavior);

            if (behavior == null)
                throw new ServiceAuthenticationConfigurationMissingException();

            foreach (var endpointDispatcher in dispatchRuntime.ChannelDispatcher.Endpoints)
                endpointDispatcher.DispatchRuntime.MessageInspectors.Add(
                    new ServiceAuthenticationInspector(
                        behavior.ThrowIfNull().AuthenticationHandler,
                        behavior.UsernamePasswordValidatorType,
                        behavior.RequireSecureTransport,
                        behavior.Source));
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint) { }
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }
        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime) { }
    }
}
