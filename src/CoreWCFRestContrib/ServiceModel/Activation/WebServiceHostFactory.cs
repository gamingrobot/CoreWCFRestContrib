using System;

namespace CoreWCFRestContrib.ServiceModel.Activation
{
    public class WebServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new Web.WebServiceHost(serviceType, baseAddresses);
        }
    }
}