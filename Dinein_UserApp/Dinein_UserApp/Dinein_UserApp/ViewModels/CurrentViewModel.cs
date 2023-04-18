using Dinein_UserApp.Models;
using Dinein_UserApp.Services;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{
    class CurrentViewModel : INotifyPropertyChanged
    {
        private ReservationModel selectedReservation;

        public ICommand EditReservationCommand { get; private set; }
        public ICommand CancelReservationCommand { get; private set; }

        public CurrentViewModel()
        {
            EditReservationCommand = new Command(OnEditReservation);
            CancelReservationCommand = new Command(OnCancelReservation);
        }
        public ReservationModel SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }

        private async void OnEditReservation()
        {
             EditReservationCommand = new Command(OnEditReservation);
            await Shell.Current.GoToAsync("//EditReservationPage");

        }

        private async void OnCancelReservation()
        {
            string userId = Application.Current.Properties["UID"] as string;

            var reservations = await new FirebaseClient(DataBase.FirebaseClient)
                .Child(nameof(ReservationModel))
                .OrderBy(nameof(ReservationModel.UserId))
                .EqualTo(userId)
                .OnceAsync<ReservationModel>();

            if (reservations.Any())
            {
                await new FirebaseClient(DataBase.FirebaseClient)
                    .Child(nameof(ReservationModel))
                    .Child(reservations.First().Key)
                    .DeleteAsync();

                await Shell.Current.GoToAsync("//CancelPage");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Reservation not found", "OK");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}