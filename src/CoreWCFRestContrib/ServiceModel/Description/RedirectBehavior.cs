using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using CoreWCFRestContrib.ServiceModel.Dispatcher;

namespace CoreWCFRestContrib.ServiceModel.Description
{
    public class RedirectBehavior : IOperationBehavior 
    {
        private readonly string _redirectUrlQuerystringParameter;

        public RedirectBehavior(string redirectUrlQuerystringParameter)
        {
            _redirectUrlQuerystringParameter = redirectUrlQuerystringParameter;
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, 
            DispatchOperation dispatchOperation)
        {
             dispatchOperation.ParameterInspectors.Add( 
                 new RedirectInspector(_redirectUrlQuerystringParameter));
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, 
            ClientOperation clientOperation) { }
        public void AddBindingParameters(OperationDescription operationDescription, 
            BindingParameterCollection bindingParameters) { }
        public void Validate(OperationDescription operationDescription) { }
    }
}
