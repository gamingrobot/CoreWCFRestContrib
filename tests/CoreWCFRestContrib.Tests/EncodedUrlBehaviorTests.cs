﻿using System.Net;
using CoreWCF;

namespace CoreWCFRestContrib.Tests
{
    [TestFixture]
    public class EncodedUrlBehaviorTests
    {
        [ServiceContract]
        public class Service
        {            
            [WebGet(UriTemplate = "/{*value}")]
            [OperationContract]
            public string Method(string value)
            {
                return value;
            }
        }

        [Test]
        public void Should_Accept_Url_With_Encoded_Forwardslash()
        {
            using (var host = new Host<Service>("http://localhost:48645/"))
            {
                var response = host.Get("this%2fthat@someplace.com");

                response.StatusCode.ShouldEqual(HttpStatusCode.OK);
                response.Content.ShouldContain("this/that@someplace.com");
            }
        }

        [Test]
        public void Should_Accept_Url_With_Encoded_Backslash()
        {
            using (var host = new Host<Service>("http://localhost:48645/"))
            {
                var response = host.Get("this%5cthat@someplace.com");

                response.StatusCode.ShouldEqual(HttpStatusCode.OK);
                response.Content.ShouldContain(@"this/that@someplace.com");
            }
        }
    }
}