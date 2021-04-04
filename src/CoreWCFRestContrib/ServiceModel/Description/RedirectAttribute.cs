using System;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class RedirectAttribute : Attribute, IOperationBehavior 
    {
        readonly RedirectBehavior _behavior;

        public RedirectAttribute(string redirectUrlQuerystringParameter)
        {
            _behavior = new RedirectBehavior(redirectUrlQuerystringParameter);
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        { _behavior.ApplyDispatchBehavior(operationDescription, dispatchOperation); }
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        { _behavior.ApplyClientBehavior(operationDescription, clientOperation); }
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        { _behavior.AddBindingParameters(operationDescription, bindingParameters); }
        public void Validate(OperationDescription operationDescription)
        { _behavior.Validate(operationDescription); }
    }
}
