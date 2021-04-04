using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using CoreWCFRestContrib.DependencyInjection;
using CoreWCFRestContrib.ServiceModel.Dispatcher;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class DependencyInjectionBehavior : IServiceBehavior
    {
        public DependencyInjectionBehavior(Type type)
        {
            if (DependencyResolver.IsDefault()) DependencyResolver.SetResolver((IDependencyResolver)Activator.CreateInstance(type));
        }

        public void ApplyDispatchBehavior(
            ServiceDescription serviceDescription, 
            ServiceHostBase serviceHostBase)
        {
            foreach (var endpoint in serviceHostBase.ChannelDispatchers.
                                                        OfType<ChannelDispatcher>().
                                                        SelectMany(dispatcher => dispatcher.Endpoints))
                endpoint.DispatchRuntime.InstanceProvider = new DependencyInjectionInstanceProvider(serviceDescription.ServiceType);
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, 
                                         BindingParameterCollection bindingParameters) { }
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }
    }
}
