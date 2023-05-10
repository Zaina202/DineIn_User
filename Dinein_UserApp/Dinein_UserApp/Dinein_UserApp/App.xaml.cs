using Dinein_UserApp.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dinein_UserApp
{
    public partial class App : Application
    {
        private bool isInternetConnected;

        public App()
        {
            InitializeComponent();

            // DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private async void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            isInternetConnected = e.NetworkAccess == NetworkAccess.Internet;
            if (!isInternetConnected)
            {
                await Application.Current.MainPage.DisplayAlert("No Internet", "Please check your internet connection and try again.", "OK");
                await Logout();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Internet Restored", "You are now connected to the internet.", "OK");
            }
        }
        private async Task Logout()
        {
            var loginPage = new LoginPage();
            await Current.MainPage.Navigation.PushAsync(loginPage);
        }
    }
}
