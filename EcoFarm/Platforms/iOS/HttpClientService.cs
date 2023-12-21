using System.Net.Security;

namespace EcoFarm;

public partial class HttpClientService
{
    public partial HttpMessageHandler GetPlatformSpecificMessageHandler()
    {
        var iosHandler = new NSUrlSessionHandler()
        {
            TrustOverrideForUrl = (nsUrlSessionHanderSender, url, secTrust) =>
            {
                return url.StartsWith("https://localhost");
            }
        };

        return iosHandler;
    }
}




