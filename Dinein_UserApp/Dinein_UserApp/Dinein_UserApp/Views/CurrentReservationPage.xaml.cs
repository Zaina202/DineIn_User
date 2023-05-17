using Dinein_UserApp.ViewModels;
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
    public partial class CurrentReservationPage : ContentPage
    {
        public CurrentReservationPage()
        {
            InitializeComponent();
        }


        private void Button_Order(object sender, EventArgs e)
        {
            _ = Navigation.PushAsync(new BillPage());

        }

        private void Go_Reserve(object sender, EventArgs e)
        {
            _ = Navigation.PushAsync(new ReservationPage());

        }

    }
}

