using System;
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCFRestContrib.DependencyInjection;
using CoreWCFRestContrib.Reflection;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class DependencyInjectionAttribute : Attribute, IServiceBehavior
    {
        readonly DependencyInjectionBehavior _behavior;

        public DependencyInjectionAttribute(Type objectFactory)
        {
            if (!objectFactory.CastableAs<IDependencyResolver>())
                throw new Exception("objectFactory must implement IObjectFactory.");

            _behavior = new DependencyInjectionBehavior(objectFactory);
        }

        public DependencyInjectionBehavior BaseBehavior
        { get { return _behavior; } }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        { _behavior.AddBindingParameters(serviceDescription, serviceHostBase, endpoints, bindingParameters); }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        { _behavior.ApplyDispatchBehavior(serviceDescription, serviceHostBase); }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        { _behavior.Validate(serviceDescription, serviceHostBase); }
    }
}
