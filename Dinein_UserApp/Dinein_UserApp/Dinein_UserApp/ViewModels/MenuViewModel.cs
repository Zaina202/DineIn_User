﻿using Dinein_UserApp.Models;
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

        private async void OnSaveOrderClicked()
        {
            BillOrder billOrder = new BillOrder();
            billOrder.OrderList = new List<Order>();
            var totalPrice = 0;
            var userId = (string)Application.Current.Properties["UID"];
            var orderId= Guid.NewGuid().ToString();
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
                }
            }
            billOrder.UserId= userId;
            billOrder.BillOrderNo = orderId;
            billOrder.OrderTotalPrice = totalPrice;
            await dataBase.OrderSave(billOrder);
            await Application.Current.MainPage.Navigation.PushAsync(new BillPage());
        }

    }
}
    


    

