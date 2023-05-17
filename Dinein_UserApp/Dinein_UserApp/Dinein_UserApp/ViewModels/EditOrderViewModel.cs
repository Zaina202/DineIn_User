using Dinein_UserApp.Models;
using Dinein_UserApp.Services;
using Dinein_UserApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{
    class EditOrderViewModel : INotifyPropertyChanged
    {
        private DataBase dataBase;

        private List<Models.Menu> _menuItems;

        public List<Models.Menu> MenuItems
        {
            get { return _menuItems; }
            set
            {
                _menuItems = value;
                OnPropertyChanged(nameof(MenuItems));
            }
        }
        private string _reservationId;

        public string reservationId
        {
            get { return _reservationId; }
            set
            {
                _reservationId = value;
                OnPropertyChanged(nameof(reservationId));
            }
        }
        public EditOrderViewModel(string resId)
        {
            reservationId = resId;
        }
        public EditOrderViewModel()
        {
         
            SaveOrderCommand = new Command(OnSaveEditClicked);
            dataBase = new DataBase();
            LoadMenuItems();
        }

        private async void LoadMenuItems()
        {
            MenuItems = await dataBase.GetAll();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public Command SaveOrderCommand { get; }


      

        private async void OnSaveEditClicked()
        {
            var totalPrice = 0;
            var orderList = new List<Order>();
            var userId = (string)Application.Current.Properties["UID"];
            var odrerId = Guid.NewGuid().ToString();
            foreach (var menuItem in MenuItems)
            {
                if (menuItem.Quantity > 0)
                {
                    var order = new Order()
                    {
                        MenuItemName = menuItem.Name,
                        MenuItemPrice = menuItem.Price,
                        Quantity = menuItem.Quantity,
                        TotalPrice = menuItem.Quantity * menuItem.Price,
                       
                    };
                    order.OrderId = odrerId;
                    totalPrice += order.TotalPrice;
                    orderList.Add(order);
                }
            }

            await Application.Current.MainPage.Navigation.PushAsync(new BillPage());
        }

    }
}
