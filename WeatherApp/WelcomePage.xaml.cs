using Microsoft.Maui.Controls;

namespace WeatherApp {
    public partial class WelcomePage : ContentPage {
        public WelcomePage() {
            InitializeComponent();
        }

        private async void OnGetStartedClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new WeatherPage());
        }
    }
}
