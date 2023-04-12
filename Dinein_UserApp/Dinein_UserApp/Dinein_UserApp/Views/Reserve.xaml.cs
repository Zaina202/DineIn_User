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
    public partial class Reserve : ContentPage
    {
        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public Reserve()
        {
            InitializeComponent();
            BindingContext = this;

        }
      
        private async void Start_Reserve(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

    }
}