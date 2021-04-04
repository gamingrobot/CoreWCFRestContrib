﻿using System;

namespace CoreWCFRestContrib.ServiceModel.Web.Exceptions
{
    public class InternalServerException : WebException
    {
        public InternalServerException(Exception exception, string message, params object[] args)
            : base(exception,
                System.Net.HttpStatusCode.InternalServerError, 
                message,
                args) { }
    }
}
