using Dinein_UserApp.Models;
using Dinein_UserApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Dinein_UserApp.Views;
namespace Dinein_UserApp.ViewModels
{
    class MenuViewModel : INotifyPropertyChanged
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

        public MenuViewModel()
        {
            SaveOrderCommand = new Command(OnSaveOrderClicked);
            dataBase = new DataBase();
            LoadMenuItems();
        }

        private bool isSavingOrder = false; 

        private async void OnSaveOrderClicked()
        {
            if (isSavingOrder)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Order saving is already in progress.", "OK");
                return;
            }

            var userId = (string)Application.Current.Properties["UID"];

            bool hasExistingOrder = await dataBase.HasExistingOrder(userId);
            if (hasExistingOrder)
            {
                await Application.Current.MainPage.DisplayAlert("Existing Order", "You already have an existing order. Please complete or cancel your existing order before placing a new one.", "OK");
                return;
            }

            isSavingOrder = true; 

            BillOrder billOrder = new BillOrder();
            billOrder.OrderList = new List<Order>();
            var totalPrice = 0;
            var orderId = Guid.NewGuid().ToString();
            bool hasOrder = false;

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
                    totalPrice += order.TotalPrice;
                    billOrder.OrderList.Add(order);
                    hasOrder = true;
                }
            }

            if (!hasOrder)
            {
                await Application.Current.MainPage.DisplayAlert("No Items Selected", "Please select at least one item to place an order.", "OK");
                isSavingOrder = false; 
                return;
            }

            billOrder.UserId = userId;
            billOrder.BillOrderNo = orderId;
            billOrder.OrderTotalPrice = totalPrice;

            bool orderSaved = await dataBase.OrderSave(billOrder);
            if (orderSaved)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new BillPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Failed to save the order.", "OK");
            }

            isSavingOrder = false; 
        }


    }
}
    


    

