using Dinein_UserApp.Models;
using Dinein_UserApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{
    class ReservationViewModel : INotifyPropertyChanged
    {
        private int _hour = 0;
        private int _minute = 0;

        private string _time = "00:00";
        public string Time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    OnPropertyChanged(nameof(Time));

                    if (Time.Length == 5)
                    {
                        if (int.TryParse(Time.Substring(0, 2), out int hour) && int.TryParse(Time.Substring(3, 2), out int minute))
                        {
                            _hour = hour;
                            _minute = minute;
                        }
                    }
                }
            }
        }

        public ICommand MinusButtonCommand { get; }
        public ICommand PlusButtonCommand { get; }
        public ICommand ConfirmButtonCommand { get; }
        public ReservationViewModel()
        {

            MinusButtonCommand = new Command(OnMinusButtonClicked);
            PlusButtonCommand = new Command(OnPlusButtonClicked);
        }

        private void OnMinusButtonClicked()
        {
            if (_hour > 0 || _minute >= 30)
            {
                if (_minute >= 30)
                {
                    _minute -= 30;
                }
                else
                {
                    _hour--;
                    _minute += 30;
                }
            }

            Time = $"{_hour:D2}:{_minute:D2}";
        }

        private void OnPlusButtonClicked()
        {
            if (_hour < 23 || _minute < 30)
            {
                if (_minute < 30)
                {
                    _minute += 30;
                }
                else
                {
                    _hour++;
                    _minute -= 30;
                }
            }

            Time = $"{_hour:D2}:{_minute:D2}";
        }




        public ICommand ConfirmCommand => new Command(async () => await Confirm());

        public async Task Confirm()
        {
            if (string.IsNullOrEmpty(Time))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Please enter a Time! and Number of People", "Cancel");
            }

            else
            {
                ReservationModel reservationModel = new ReservationModel();
                reservationModel.TimePicker = Time;
                


                DataBase dataBase = new DataBase();
                var isSaved = await dataBase.Save(reservationModel);


                if (isSaved)
                {
                    await Application.Current.MainPage.DisplayAlert("Information", $"Your Reservation Time is: {Time}", "Ok");
                    Clear();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Your reservation failed ,Enter time and number of people ", "Ok");
                }
            }
        }

        public void Clear()
        {
            Time = string.Empty;
           
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
