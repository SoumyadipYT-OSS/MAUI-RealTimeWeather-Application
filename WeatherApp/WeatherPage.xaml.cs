using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using WeatherApp.Services;
using WeatherApp.Models;
using Microsoft.Maui.Devices.Sensors;

namespace WeatherApp {
    public partial class WeatherPage : ContentPage {
        public List<Models.List> WeatherList;
        private double latitude;
        private double longitude;

        public WeatherPage() {
            InitializeComponent();
            WeatherList = new List<Models.List>();
        }

        protected async override void OnAppearing() {
            base.OnAppearing();
            await GetLocation();
            await GetWeatherDataByLocation(latitude, longitude);
            var result = await ApiService.GetWeather(latitude, longitude);

            foreach (var i in result.list) {
                WeatherList.Add(i);
            }
            CvWeather.ItemsSource = WeatherList;

            if (result != null && result.city != null && result.list != null && result.list.Count > 0) {
                Dispatcher.Dispatch(() => {
                    LblCity.Text = result.city.name;
                    LblWeatherDescription.Text = result.list[0].weather[0].description;
                    LblTemperature.Text = result.list[0].main.roundtemparature + " ℃";
                    LblHumidity.Text = result.list[0].main.humidity + " %";
                    LblWind.Text = result.list[0].wind.speed + " km/h";
                    ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;
                });
            }
        }

        public async Task GetLocation() {
            var location = await Geolocation.GetLocationAsync();
            latitude = location.Latitude;
            longitude = location.Longitude;
        }

        private async void TapLocation_Tapped(object sender, EventArgs e) {
            await GetLocation();
            await GetWeatherDataByLocation(latitude, longitude);
        }

        public async Task GetWeatherDataByLocation(double latitude, double longitude) {
            var result = await ApiService.GetWeather(latitude, longitude);
            UpdateUI(result);
        }

        private async void ImageButton_Clicked(object sender, EventArgs e) {
            var response = await DisplayPromptAsync(title: "", message: "", placeholder: "Search weather by city", accept: "Search", cancel: "Cancel");
            if (response != null) {
                await GetWeatherDataByCity(response);
            }
        }

        public async Task GetWeatherDataByCity(string city) {
            try {
                var result = await ApiService.GetWeatherByCity(city);
                if (result != null && result.city != null && result.list != null && result.list.Count > 0) {
                    UpdateUI(result);
                } else {
                    await DisplayAlert("Error", "City not found. Please enter a valid city name.", "OK");
                }
            } catch (Exception ex) {
                await DisplayAlert("Error", "An error occurred while fetching weather data. Please try again.", "OK");
            }
        }

        public void UpdateUI(dynamic result) {
            WeatherList.Clear();
            foreach (var i in result.list) {
                WeatherList.Add(i);
            }
            CvWeather.ItemsSource = WeatherList;

            if (result != null && result.city != null && result.list != null && result.list.Count > 0) {
                Dispatcher.Dispatch(() => {
                    LblCity.Text = result.city.name;
                    LblWeatherDescription.Text = result.list[0].weather[0].description;
                    LblTemperature.Text = result.list[0].main.roundtemparature + " ℃";
                    LblHumidity.Text = result.list[0].main.humidity + " %";
                    LblWind.Text = result.list[0].wind.speed + " km/h";
                    ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;
                });
            }
        }
    }
}