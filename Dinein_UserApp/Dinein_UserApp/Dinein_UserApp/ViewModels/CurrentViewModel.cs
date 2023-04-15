using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dinein_UserApp.ViewModels
{
    class CurrentViewModel : INotifyPropertyChanged
    {
        private int _editButtonCornerRadius = 20;
        public int EditButtonCornerRadius
        {
            get => _editButtonCornerRadius;
            set
            {
                _editButtonCornerRadius = value;
                OnPropertyChanged(nameof(EditButtonCornerRadius));
            }
        }

        private int _cancelButtonCornerRadius = 20;
        public int CancelButtonCornerRadius
        {
            get => _cancelButtonCornerRadius;
            set
            {
                _cancelButtonCornerRadius = value;
                OnPropertyChanged(nameof(CancelButtonCornerRadius));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}