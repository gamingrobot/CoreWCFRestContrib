using CoreWCF;

namespace CoreWCFRestContrib.ServiceModel.Web
{
    /// <summary>
    /// Incomplete replacement of WebOperationContext for CoreWCF 
    /// </summary>
    internal class WebOperationContext : IExtension<OperationContext>
    {

        private readonly OperationContext _operationContext;

        private WebOperationContext(OperationContext operationContext)
        {
            _operationContext = operationContext;
            if (_operationContext.Extensions.Find<WebOperationContext>() == null)
            {
                _operationContext.Extensions.Add(this);
            }
        }

        public static WebOperationContext Current
        {
            get
            {
                if (OperationContext.Current == null)
                {
                    return null;
                }
                var webOperationContext = OperationContext.Current.Extensions.Find<WebOperationContext>();
                return webOperationContext ?? new WebOperationContext(OperationContext.Current);
            }
        }

        public IncomingWebRequestContext IncomingRequest => new IncomingWebRequestContext(_operationContext);

        public OutgoingWebResponseContext OutgoingResponse => new OutgoingWebResponseContext(_operationContext);

        public void Attach(OperationContext owner)
        {
        }

        public void Detach(OperationContext owner)
        {
        }
    }
}
