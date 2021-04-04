using System;
using System.Security.Principal;

namespace CoreWCFRestContrib.ServiceModel.Dispatcher
{
    public interface IWebAuthenticationHandler
    {
        IIdentity Authenticate(
            IncomingWebRequestContext request, 
            OutgoingWebResponseContext response, 
            object[] parameters,
            Type validatorType,
            bool secure,
            bool requiresTransportLayerSecurity,
            string source);
    }
}
