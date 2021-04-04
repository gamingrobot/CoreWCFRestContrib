using System;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using CoreWCFRestContrib.ServiceModel.Dispatcher;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class WebDispatchFormatterAttribute : Attribute, IOperationBehavior
    {
        private readonly WebDispatchFormatterBehavior _behavior;

        public WebDispatchFormatterAttribute() : this(WebDispatchFormatter.FormatterDirection.Both)
        {
        }

        public WebDispatchFormatterAttribute(WebDispatchFormatter.FormatterDirection direction)
        {
            _behavior = new WebDispatchFormatterBehavior(direction);
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            _behavior.ApplyDispatchBehavior(operationDescription, dispatchOperation);
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            _behavior.ApplyClientBehavior(operationDescription, clientOperation);
        }

        public void AddBindingParameters(OperationDescription operationDescription,
                                         BindingParameterCollection bindingParameters)
        {
            _behavior.AddBindingParameters(operationDescription, bindingParameters);
        }

        public void Validate(OperationDescription operationDescription)
        {
            _behavior.Validate(operationDescription);
        }
    }
}