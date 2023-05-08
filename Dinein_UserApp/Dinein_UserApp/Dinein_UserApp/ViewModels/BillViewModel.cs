using Dinein_UserApp.Models;
using Dinein_UserApp.Services;
using Dinein_UserApp.Views;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{
 
    class BillViewModel : INotifyPropertyChanged
    {
       


     
        private List<BillOrder> _orders;
        private List<Order> _OrderItems;
        private string _userId = Application.Current.Properties["UID"] as string;
        private DataBase _dataBase;

        public event PropertyChangedEventHandler PropertyChanged;

        public BillViewModel()
        {
            EditOrderCommand = new Command(OnEditOrder);
            CancelOrderCommand = new Command(OnCancelOrder);

            _dataBase = new DataBase();
           

        }
        private bool hasOrder;

        public bool HasOrder
        {
            get { return hasOrder; }
            set
            {
                hasOrder = value;
                OnPropertyChanged(nameof(HasOrder));
            }
        }
        private bool noOrder;

        public bool NoOrder
        {
            get { return noOrder; }
            set
            {
                noOrder = value;
                OnPropertyChanged(nameof(NoOrder));
            }
        }
        private async void OnCancelOrder(object obj)
        {
            string userId = Application.Current.Properties["UID"] as string;

            var reservations = await new FirebaseClient(DataBase.FirebaseClient)
                .Child(nameof(BillOrder))
                .OrderBy(nameof(BillOrder.UserId))
                .EqualTo(userId)
                .OnceAsync<BillOrder>();

            if (reservations.Any())
            {
                await new FirebaseClient(DataBase.FirebaseClient)
                    .Child(nameof(BillOrder))
                    .Child(reservations.First().Key)
                    .DeleteAsync();
                await Application.Current.MainPage.DisplayAlert("Info","your order sucssfully deleted", "OK");
                HasOrder = true;
                NoOrder = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Order not found", "OK");
                HasOrder = false;
                NoOrder = true;
            }
        }

        public ICommand EditOrderCommand { get; private set; }
        public ICommand CancelOrderCommand { get; private set; }

        private async void OnEditOrder()
        {
            string userId = Application.Current.Properties["UID"] as string;

            var reservations = await new FirebaseClient(DataBase.FirebaseClient)
                .Child(nameof(BillOrder))
                .OrderBy(nameof(BillOrder.UserId))
                .EqualTo(userId)
                .OnceAsync<BillOrder>();

            if (reservations.Any())
            {
                await new FirebaseClient(DataBase.FirebaseClient)
                    .Child(nameof(BillOrder))
                    .Child(reservations.First().Key)
                    .DeleteAsync();

                await Application.Current.MainPage.Navigation.PushAsync(new MenuPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Order not found", "OK");
            }
        }

        public List<BillOrder> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

       
        public List<Order> OrderItems
        {
            get { return _OrderItems; }
            set
            {
                _OrderItems = value;
                OnPropertyChanged(nameof(OrderItems));
            }
        }
        public async void LoadOrders(string userId)
        {
            Orders = await _dataBase.GetOrderById(userId);
            OrderItems = Orders.Select(el => el.OrderList).First();

        }

        private int _totalPrice;

        public int TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public BillViewModel(int totalPrice)
        {
            _dataBase = new DataBase();
            LoadOrders(_userId);
            TotalPrice = totalPrice;
        }
    }
}
