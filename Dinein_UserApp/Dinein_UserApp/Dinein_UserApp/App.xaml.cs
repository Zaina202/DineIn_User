//using Dinein_UserApp.Services;
using Dinein_UserApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dinein_UserApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

           // DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
    }
}
