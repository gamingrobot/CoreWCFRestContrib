using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using CoreWCF;
using CoreWCF.IdentityModel.Policy;
using CoreWCF.Security;
using CoreWCFRestContrib.IdentityModel.Policy;

namespace CoreWCFRestContrib.ServiceModel
{
    public static class OperationContextExtensions
    {
        public static void ReplacePrimaryIdentity(this OperationContext context, IIdentity identity)
        {
            var incomingMessageProperties = context.IncomingMessageProperties;
            if (incomingMessageProperties != null)
            {
                var security = 
                    incomingMessageProperties.Security ?? 
                    (incomingMessageProperties.Security = new SecurityMessageProperty());

                var policies = security.ServiceSecurityContext.AuthorizationPolicies.ToList();
                policies.Add(new IdentityAuthorizationPolicy(identity));

                var authorizationContext =
                    AuthorizationContext.CreateDefaultAuthorizationContext(policies);

                security.ServiceSecurityContext = new ServiceSecurityContext(
                    authorizationContext,
                    new ReadOnlyCollection<IAuthorizationPolicy>(policies));
            }
        }

        public static bool HasTransportLayerSecurity(this OperationContext context)
        {
            return context.RequestContext.RequestMessage.Headers.To.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase);
        }
    }
}
