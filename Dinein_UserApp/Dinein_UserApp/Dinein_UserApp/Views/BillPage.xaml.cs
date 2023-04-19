using Dinein_UserApp.Models;
using Dinein_UserApp.Services;
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
    public partial class BillPage : ContentPage
    {
        public BillPage()
        {
            InitializeComponent();

        }
        public BillPage(int totalPrice, List<Order> orders)
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                var dataBase = new DataBase();
                var _totalPrice = totalPrice;
                var viewModel = new BillViewModel(_totalPrice);
                Device.BeginInvokeOnMainThread(() =>
                {
                    BindingContext = viewModel;
                });
            });
        }

        private void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CurrentReservationPage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditOrderPage());

        }
    }
}