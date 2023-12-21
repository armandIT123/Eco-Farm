using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm;

public partial class HttpClientService
{
    public HttpClient GetPlatformSpecificClient()
    {
        var httpMessageHandler = GetPlatformSpecificMessageHandler();
        return new HttpClient(httpMessageHandler);
    }

    public partial HttpMessageHandler GetPlatformSpecificMessageHandler();
}



