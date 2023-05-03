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
        private string _reservationId;

        public string ReservationID
        {
            get { return _reservationId; }
            set
            {
                _reservationId = value;
                OnPropertyChanged(nameof(ReservationID));
            }
        }
        public MenuViewModel()
        {

        }
        public MenuViewModel(string resId)
        {
            PlusCommand = new Command<Models.Menu>(OnPlusClicked);
            MinusCommand = new Command<Models.Menu>(OnMinusClicked);
            SaveOrderCommand = new Command(OnSaveOrderClicked);
            dataBase = new DataBase();
            LoadMenuItems();
            ReservationID = resId;
        }
        private async void OnSaveOrderClicked()
        {
            BillOrder billOrder = new BillOrder();
            billOrder.OrderList = new List<Order>();
            var totalPrice = 0;
           // var orderList = new List<Order>();
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
                       // UserId = userId,
                       // ReservationId= reservationId
                    };
                    //order.OrderId = odrerId;
                    totalPrice += order.TotalPrice;
                   billOrder.OrderList.Add(order);
                }
            }
            billOrder.UserId= userId;
            billOrder.BillOrderNo = orderId;
            billOrder.ReservationId = ReservationID;
            billOrder.OrderTotalPrice = totalPrice;
            await dataBase.OrderSave(billOrder);
            await Application.Current.MainPage.Navigation.PushAsync(new BillPage(totalPrice));
        }

    }
}
    


    

