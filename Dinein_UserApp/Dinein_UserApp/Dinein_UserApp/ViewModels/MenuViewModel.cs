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

        public MenuViewModel()
        {
            PlusCommand = new Command<Models.Menu>(OnPlusClicked);
            MinusCommand = new Command<Models.Menu>(OnMinusClicked);
            SaveOrderCommand = new Command(OnSaveOrderClicked);

           
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



        public Command PlusCommand { get; }
        public Command MinusCommand { get; }
        public Command SaveOrderCommand { get; }


        private void OnPlusClicked(Models.Menu selectedItem)
        {
            selectedItem.Quantity++;
            OnPropertyChanged(nameof(MenuItems));
        }

        private void OnMinusClicked(Models.Menu selectedItem)
        {
            if (selectedItem.Quantity > 0)
            {
                selectedItem.Quantity--;
                OnPropertyChanged(nameof(MenuItems));
            }
        }

        private async void OnSaveOrderClicked()
        {
            var order = new Order
            {
                MenuItemName = new List<string>(),
                MenuItemPrice = new List<int>(),
                Quantity = new List<int>()
            };

            foreach (var menuItem in MenuItems)
            {
                if (menuItem.Quantity > 0)
                {
                    order.MenuItemName.Add(menuItem.Name);
                    order.MenuItemPrice.Add(menuItem.Price);
                    order.Quantity.Add(menuItem.Quantity);
                }
            }

            await dataBase.OrderSave(order);
        }
    }
}
    


    

