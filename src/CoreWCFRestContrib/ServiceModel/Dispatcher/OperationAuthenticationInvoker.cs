using System;
using System.Threading.Tasks;
using CoreWCF;
using CoreWCF.Dispatcher;
using CoreWCFRestContrib.ServiceModel.Web;

namespace CoreWCFRestContrib.ServiceModel.Dispatcher
{
    public class OperationAuthenticationInvoker : Attribute, IOperationInvoker 
    {
        private readonly IOperationInvoker _invoker;
        private readonly IWebAuthenticationHandler _handler;
        private readonly Type _validatorType;
        private readonly bool _requiresTransportLayerSecurity;
        private readonly string _source;

        public OperationAuthenticationInvoker(
            IOperationInvoker invoker,
            IWebAuthenticationHandler handler,
            Type validatorType,
            bool requiresTransportLayerSecurity,
            string source)
        {
            _invoker = invoker.ThrowIfNull();
            _handler = handler.ThrowIfNull();
            _validatorType = validatorType;
            _requiresTransportLayerSecurity = requiresTransportLayerSecurity;
            _source = source;
        }

        public object[] AllocateInputs()
        { return _invoker.AllocateInputs(); }

        public ValueTask<(object returnValue, object[] outputs)> InvokeAsync(object instance, object[] inputs)
        {
            OperationContext.Current.ThrowIfNull().ReplacePrimaryIdentity(
                _handler.Authenticate(
                    WebOperationContext.Current.ThrowIfNull().IncomingRequest,
                    WebOperationContext.Current.ThrowIfNull().OutgoingResponse,
                    inputs,
                    _validatorType,
                    OperationContext.Current.ThrowIfNull().HasTransportLayerSecurity(),
                    _requiresTransportLayerSecurity,
                    _source));

            return _invoker.InvokeAsync(instance, inputs);
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, 
            AsyncCallback callback, object state)
        { throw new NotSupportedException(); }

        public object InvokeEnd(object instance, out object[] outputs, 
            IAsyncResult result)
        { throw new NotSupportedException(); }

        public bool IsSynchronous
        { get { return true; } }
    }
}
