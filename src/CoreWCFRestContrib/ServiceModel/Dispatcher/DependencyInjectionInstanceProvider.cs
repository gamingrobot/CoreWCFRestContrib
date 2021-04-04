using System;
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Dispatcher;
using CoreWCFRestContrib.DependencyInjection;

namespace CoreWCFRestContrib.ServiceModel.Dispatcher
{
    public class DependencyInjectionInstanceProvider : IInstanceProvider
    {
        private readonly Type _serviceType;

        public DependencyInjectionInstanceProvider(Type serviceType)
        {
            _serviceType = serviceType;
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            OperationContext.Current.OperationCompleted += (s, e) => { 
                if (OperationContainer.Exists()) DependencyResolver.Current.ReleaseOperationContainer(OperationContainer.GetCurrent()); };
            return DependencyResolver.Current.GetOperationService<object>(OperationContainer.GetCurrent(), _serviceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance) { }
    }
}
