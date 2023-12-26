using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcoFarm;

public static class ServiceHelper
{
    public static IServiceProvider Services { get; private set; }

    public static void Initialize(IServiceProvider serviceProvider) =>
        Services = serviceProvider;

    public static T GetService<T>() => Services.GetService<T>();
}

internal class ServiceLink : IServiceLink
{
    private readonly IHttpClientFactory _clientFactory;

    public string localHostClient => "localHostClient";

    public string mainClient => throw new NotImplementedException();

    public ServiceLink(IHttpClientFactory clientFactory)
	{
        _clientFactory = clientFactory;

    }

    public ServiceLink()
    {

    }

    public async Task GetClient()
    {
        try
        {
            var client = _clientFactory.CreateClient(localHostClient);
            var data = await client.GetAsync("/WeatherForecast");
            
        }
        catch (Exception ex) { }
    }

    public async Task<List<Supplier>> GetSuppliers()
    {
        try
        {
            var client = _clientFactory.CreateClient(localHostClient);
            var data = await client.GetAsync("/Supplier");
            if(data.IsSuccessStatusCode)
            {
                var response = await data.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Supplier>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            return null;

        }
        catch (Exception ex) 
        {
            return null;
        }
    }
}
