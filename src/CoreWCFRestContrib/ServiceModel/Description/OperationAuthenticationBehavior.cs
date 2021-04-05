using System;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using CoreWCFRestContrib.ServiceModel.Dispatcher;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class OperationAuthenticationBehavior : IOperationBehavior 
    {
        public void ApplyDispatchBehavior(OperationDescription operationDescription, 
            DispatchOperation dispatchOperation)
        {
            var behavior =
                operationDescription.DeclaringContract.FindBehavior
                    <WebAuthenticationConfigurationBehavior,
                     WebAuthenticationConfigurationAttribute>(b => b.BaseBehavior) ??
                dispatchOperation.Parent.ChannelDispatcher.Host.Description.FindBehavior
                    <WebAuthenticationConfigurationBehavior,
                     WebAuthenticationConfigurationAttribute>(b => b.BaseBehavior);

            if (behavior == null)
                throw new InvalidOperationException(
                    "OperationAuthenticationConfigurationBehavior not applied to contract or service. This behavior is required to configure operation authentication.");

            dispatchOperation.Invoker = new OperationAuthenticationInvoker(
                dispatchOperation.Invoker,
                behavior.ThrowIfNull().AuthenticationHandler,
                behavior.UsernamePasswordValidatorType,
                behavior.RequireSecureTransport,
                behavior.Source);
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, 
            ClientOperation clientOperation) { }
        public void AddBindingParameters(OperationDescription operationDescription, 
            BindingParameterCollection bindingParameters) { }
        public void Validate(OperationDescription operationDescription) { }
    }
}
