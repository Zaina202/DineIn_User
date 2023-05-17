using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Dinein_UserApp.Models;
using Dinein_UserApp.Services;
using Dinein_UserApp.Views;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
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

            dataBase = new DataBase();
            LoadCurrentReservation();
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
                await Application.Current.MainPage.Navigation.PushAsync(new EditReservationPage());

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Reservation not found", "OK");
            }

        }

        private async void OnCancelReservation()
        {
            string userId = Application.Current.Properties["UID"] as string;

            await dataBase.DeleteOrderAsync(userId);
            await dataBase.DeleteReservationAsync(userId);
            await Application.Current.MainPage.Navigation.PushAsync(new CancelPage());

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
                    return "00:00";
                }
            }
        }


        public string NumPeople
        {
            get { return reservation?.NumberOfPeople; }
        }


        private bool hasReservation;

        public bool HasReservation
        {
            get { return hasReservation; }
            set
            {
                hasReservation = value;
                OnPropertyChanged(nameof(HasReservation));
            }
        }
        private bool noReservation;

        public bool NoReservation
        {
            get { return noReservation; }
            set
            {
                noReservation = value;
                OnPropertyChanged(nameof(NoReservation));
            }
        }

        private async void LoadCurrentReservation()
        {
            var userId = (string)Application.Current.Properties["UID"];
            reservation = await dataBase.GetCurrentReservation(userId);
            if (reservation != null)
            {
                HasReservation = true;
                NoReservation = false;
                OnPropertyChanged(nameof(Time));
                OnPropertyChanged(nameof(NumPeople));
            }
            else
            {
                HasReservation = false;
                NoReservation = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}