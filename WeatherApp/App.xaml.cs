namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

#pragma warning disable CS0618 // Type or member is obsolete
            MainPage = new NavigationPage(new WelcomePage());
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}