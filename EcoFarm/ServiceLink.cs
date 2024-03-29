﻿using Data;
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

internal interface IServiceLink
{
    string localHostClient { get; }
    string mainClient { get; }

    internal Task GetSuppliers();
    internal Task<IEnumerable<Product>> GetProducts(int supplierId);
    internal Task<IEnumerable<Review>> GetReviews(int supplierId);
    internal Task<SupplierAbout> GetSupplierDesciption(int supplierId);

    internal Task<string> RegisterUser(RegisterDTO registerDTO);
}

internal class ServiceLink : IServiceLink
{
    public delegate void DataUpdated();
    public static DataUpdated SuppliersUpdated;

    internal static List<Supplier>? Suppliers { get; private set; }

    private readonly IHttpClientFactory _clientFactory;

    public string localHostClient => "localHostClient";

    public string mainClient => throw new NotImplementedException();

    public ServiceLink(IHttpClientFactory clientFactory)
	{
        _clientFactory = clientFactory;
    }

    #region Supplier
    public async Task GetSuppliers()
    {
        try
        {
            var client = _clientFactory.CreateClient(localHostClient);
            var data = await client.GetAsync("/Supplier");
            if(data.IsSuccessStatusCode)
            {
                var response = await data.Content.ReadAsStringAsync();
                Suppliers = JsonSerializer.Deserialize<List<Supplier>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                SuppliersUpdated?.Invoke();
            }
        }
        catch (Exception ex) 
        {
            
        }
    }

    public async Task<SupplierAbout> GetSupplierDesciption(int supplierId)
    {
        if (!(supplierId > 0))
            return null;
        try
        {
            var client = _clientFactory.CreateClient(localHostClient);
            var data = await client.GetAsync($"/Supplier/GetDescription?id={supplierId}");
            if (data.IsSuccessStatusCode)
            {
                var response = await data.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SupplierAbout> (response, new JsonSerializerOptions
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

    public async Task<IEnumerable<Product>> GetProducts(int supplierId)
    {
        if (!(supplierId > 0))
            return null;
        try
        {
            var client = _clientFactory.CreateClient(localHostClient);
            var data = await client.GetAsync($"/Product?supplierId={supplierId}");
            if(data.IsSuccessStatusCode)
            {
                var response = await data.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<Product>>(response, new JsonSerializerOptions
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

    public async Task<IEnumerable<Review>> GetReviews(int supplierId)
    {
        try
        {
            var client = _clientFactory.CreateClient(localHostClient);
            var data = await client.GetAsync($"/Review?supplierId={supplierId}");
            if (data.IsSuccessStatusCode)
            {
                var response = await data.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<Review>>(response, new JsonSerializerOptions
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
    #endregion

    #region Order
    public async Task PlaceOrder(Order order)
    {
        try
        {
            var client = _clientFactory.CreateClient(localHostClient);
            var content = new StringContent(JsonSerializer.Serialize(order),Encoding.UTF8);
            var response = await client.PostAsync("/Order", content);
            if(response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
            }
        }
        catch(Exception ex)
        {
            // logger
        }
    }

    public async Task<IEnumerable<Order>> GetOrders(int userId)
    {
        try
        {
            var client = _clientFactory.CreateClient(localHostClient);
            var data = await client.GetAsync($"/Order?userId={userId}");
            if (data.IsSuccessStatusCode)
            {
                var response = await data.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<Order>>(response, new JsonSerializerOptions
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
    #endregion

    #region Login
    public async Task<string> RegisterUser(RegisterDTO registerDTO)
    {
        try
        {
            var client = _clientFactory.CreateClient(localHostClient);
            var content = new StringContent(JsonSerializer.Serialize(registerDTO));
            var response = await client.PostAsync("/User", content);
            string responseMessage = await response.Content.ReadAsStringAsync();
            return responseMessage;
        }
        catch(Exception ex)
        {
            return "Error";
        }
    }

    #endregion
}
