using Java.Net;
using Javax.Net.Ssl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;
using Object = Java.Lang.Object;

namespace EcoFarm;

public partial class HttpClientService
{
    public partial HttpMessageHandler GetPlatformSpecificMessageHandler()
    {
        var androidHttpHandler = new AndroidMessageHandler()
        {
            ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyError) =>
            {
                if(certificate?.Issuer == "CN=localhost" || sslPolicyError == SslPolicyErrors.None)
                    return true;
                return false;
            }
        };
        return androidHttpHandler;
    }

    // just for >net 6 and below. Not neccesaary
    class LocalHostAndroidMessageHandler : AndroidMessageHandler
    {
        protected override IHostnameVerifier? GetSSLHostnameVerifier(HttpsURLConnection connection) => new LocalHostNameVerifier();
    }

    class LocalHostNameVerifier : Object, IHostnameVerifier
    {
        public bool Verify(string? hostname, ISSLSession? session)
        {
            if (HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session) || hostname == "10.0.2.2")
                return true;
            return false;
        }
    }
}




