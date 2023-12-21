﻿using Data;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace EcoFarm;

public partial class MainPage : ContentPage
{
    List<Supplier> suppliers;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;

        suppliers = new()
        {
            new Supplier()
            {
                Name = "Test",
                Image = "picture.png",
                Rating = 3.5,

            },
            new Supplier()
            {
                Name = "Test 2",
                Image = "picture.png",
                Rating = 5
            },
            new Supplier()
            {
                Name= "Test 3",
                Image = "picture.png",
                Rating = 4.5
            }
        };
        OnPropertyChanged(nameof(Suppliers));
    }

    public List<Supplier> Suppliers => suppliers;

    private async void Button_Clicked(object sender, EventArgs e)
    {

        CallHttps();
    }

    async void CallHttps()
    {
        try
        {
            var httpCLient = new HttpClientService().GetPlatformSpecificClient();
            string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7184" : "https://localhost:7184";
            var response = await httpCLient.GetAsync(baseUrl + "/WeatherForecast");


            var data = await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex) { }
    }

    async void CallHttp()
    {
        var httpCLient = new HttpClient();
        string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5236" : "http://localhost:5236";
        var response = await httpCLient.GetAsync(baseUrl + ":5236/WeatherForecast");


        var data = await response.Content.ReadAsStringAsync();
    }
}
