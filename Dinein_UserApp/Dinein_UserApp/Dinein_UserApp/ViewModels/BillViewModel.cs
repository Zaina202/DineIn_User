using Dinein_UserApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dinein_UserApp.ViewModels
{
 
    class BillViewModel : INotifyPropertyChanged
    {
        public BillViewModel()
        {
            
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

        public event PropertyChangedEventHandler PropertyChanged;
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
