using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Models;


namespace WeatherApp.Services
{
    public static class ApiService
    {
        public static async Task<Root> GetWeather(double latitude, double longitude) {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=ee3f5390f325c571f832c0aae24d3a71", latitude, longitude));
#pragma warning disable CS8603
            return JsonConvert.DeserializeObject<Root>(response);
#pragma warning restore CS8603
        }



        public static async Task<Root> GetWeatherByCity(string city) {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=ee3f5390f325c571f832c0aae24d3a71", city));
#pragma warning disable CS8603
            return JsonConvert.DeserializeObject<Root>(response);
#pragma warning restore CS8603
        }
    }
}