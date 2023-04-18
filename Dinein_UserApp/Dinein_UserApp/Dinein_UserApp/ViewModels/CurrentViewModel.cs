using Dinein_UserApp.Models;
using Dinein_UserApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

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

        private DataBase dataBase;
        private ReservationModel reservation;
        public string Time
        {
            get
            {
                if (reservation != null)
                {
                    return $"{reservation.TimePicker}";
                }
                else
                {
                    return "No current reservation";
                }
            }
        }


        public string NumPeople
        {
            get { return reservation?.NumberOfPeople; }
        }


        public CurrentViewModel()
        {
            dataBase = new DataBase();
            LoadCurrentReservation();
        }

        private async void LoadCurrentReservation()
        {
            var userId = (string)Application.Current.Properties["UID"];
            reservation = await dataBase.GetCurrentReservation(userId);
            if (reservation != null)
            {
                OnPropertyChanged(nameof(Time));
                OnPropertyChanged(nameof(NumPeople));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}