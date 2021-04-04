using System;
using CoreWCFRestContrib.Net.Http;

namespace CoreWCFRestContrib.Diagnostics
{
    public interface IWebLogHandler
    {
        void Write(Exception exception, RequestInformation information);
    }
}
