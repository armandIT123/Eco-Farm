using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;

namespace EcoFarm.Platforms.Android
{
    internal class AndroidHttpMessageHandler : IPlatformHttpMessageHandler
    {
        public HttpMessageHandler GetHttpMessageHandler() => new AndroidMessageHandler
        {
            ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) 
                => certificate?.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None
        };
    }
}
