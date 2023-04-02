using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dinein_UserApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomPage : ContentPage
    {
        public WelcomPage()
        {
            InitializeComponent();
             StartTimer();
        }
        private async void StartTimer()
        {
            await Task.Delay(1000);
            await Navigation.PushAsync(new Home());
        }
    }
}