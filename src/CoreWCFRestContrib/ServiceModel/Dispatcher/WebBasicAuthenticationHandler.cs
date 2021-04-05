using System;
using System.Security.Principal;
using CoreWCF.IdentityModel.Selectors;
using CoreWCFRestContrib.DependencyInjection;
using CoreWCFRestContrib.Net.Http;
using CoreWCFRestContrib.ServiceModel.Web;
using CoreWCFRestContrib.ServiceModel.Web.Exceptions;

namespace CoreWCFRestContrib.ServiceModel.Dispatcher
{
    public class WebBasicAuthenticationHandler : IWebAuthenticationHandler 
    {
        public IIdentity Authenticate(
            IncomingWebRequestContext request, 
            OutgoingWebResponseContext response, 
            object[] parameters, 
            Type validatorType,
            bool secure,
            bool requiresTransportLayerSecurity,
            string source)
        {
            if (requiresTransportLayerSecurity && !secure)
                throw new BasicRequiresTransportSecurityException();
            var authentication = new BasicAuthentication(request.Headers);
            var validator = validatorType != null
                ? DependencyResolver.Current.GetOperationService<UserNamePasswordValidator>(OperationContainer.GetCurrent(), validatorType)
                : DependencyResolver.Current.GetOperationService<UserNamePasswordValidator>(OperationContainer.GetCurrent()).ThrowIfNull();
            if (!authentication.Authenticate(validator))
                throw new BasicUnauthorizedException(source);
            return new GenericIdentity(authentication.Username, "WebBasicAuthenticationHandler");
        }
    }
}
