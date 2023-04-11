using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dinein_UserApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
        public Home()
        {
            InitializeComponent();
            BindingContext = this;

        }


        private void Start_Reserve(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ReservationPage());
            Navigation.PushAsync(new SignupPage());

        }
    }
}