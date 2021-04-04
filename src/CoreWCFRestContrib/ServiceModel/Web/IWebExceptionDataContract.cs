using CoreWCFRestContrib.ServiceModel.Web.Exceptions;

namespace CoreWCFRestContrib.ServiceModel.Web
{
    public interface IWebExceptionDataContract
    {
        void Init(WebException exception);
    }
}
