﻿using System;
using System.Net;

namespace CoreWCFRestContrib.Net.Http
{
    public class RequestInformation
    {
        public Uri Uri { get; set; }
        public string Method { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public string Accept { get; set; }
        public string UserAgent { get; set; }
        public WebHeaderCollection Headers { get; set; }
    }
}
