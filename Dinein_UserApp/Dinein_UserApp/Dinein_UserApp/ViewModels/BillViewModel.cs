using Dinein_UserApp.Models;
using Dinein_UserApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinein_UserApp.ViewModels
{
 
    class BillViewModel : INotifyPropertyChanged
    {
        public BillViewModel()
        {
            
        }

        private List<BillOrder> _orders;
        private List<Order> _OrderItems;

        private DataBase _dataBase;

        public event PropertyChangedEventHandler PropertyChanged;
      

       
        public BillViewModel(string userId)
        {
            _dataBase = new DataBase();


            _ = LoadOrders(userId);
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
        public async Task LoadOrders(string userId)
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
            TotalPrice = totalPrice;
        }
    }
}
