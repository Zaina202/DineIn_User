using Dinein_UserApp.Models;
using Dinein_UserApp.Services;
using Dinein_UserApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{

    class EditViewModel : INotifyPropertyChanged
    {
        private bool _isOneTwoChecked;
        private bool _isTwoThreeChecked;
        private bool _isFourChecked;
        private bool _isFiveChecked;
        private bool _isSixChecked;
        private int _selectedValue;
        private string selectedValue;
        private string note;
        private DataBase _dataBase;

        public ICommand EditButtonCommand { get; }
        public EditViewModel()
        {
            _dataBase = new DataBase();


        }

        private string _reservationTime;
        public string ReservationTime
        {
            get => _reservationTime;
            set
            {
                if (_reservationTime != value)
                {
                    _reservationTime = value;
                    OnPropertyChanged(nameof(ReservationTime));
                }
            }
        }

        private TimeSpan _selectedTime;
        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set
            {
                _selectedTime = value;
                ReservationTime = _selectedTime.ToString(@"hh\:mm");
                OnPropertyChanged(nameof(SelectedTime));
            }
        }



        public ICommand EditCommand => new Command(async () => await Edit());

        public async Task Edit()
        {

            if (string.IsNullOrEmpty(ReservationTime) || string.IsNullOrEmpty(selectedValue))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Please enter a Time ! or Number of People", "Cancel");
                return;
            }
            else
            {
                TimeSpan startTime = TimeSpan.FromHours(9);
                TimeSpan endTime = TimeSpan.FromHours(23);
                TimeSpan selectedTimeSpan = TimeSpan.Parse(ReservationTime);

                if (selectedTimeSpan < startTime || selectedTimeSpan > endTime)
                {
                    await Application.Current.MainPage.DisplayAlert("Time Outside Boundaries", "Please select a time between 9:00 AM and 11:00 PM.", "OK");
                    return;
                }
                else
                {
                    ReservationModel reservationModel = new ReservationModel
                    {
                        TimePicker = ReservationTime,
                        NumberOfPeople = selectedValue,
                        Note = note,
                        UserId = (string)Application.Current.Properties["UID"],
                        ReservationId = Guid.NewGuid().ToString()
                    };

                    try
                    {
                        var isSaved = await _dataBase.ReservationModelSave(reservationModel);
                        if (isSaved)
                        {
                            await Application.Current.MainPage.DisplayAlert("Information", $"Your Reservation Time is:( {ReservationTime} ) with ( {selectedValue} ) People", "Ok");
                            Clear();
                            await Application.Current.MainPage.Navigation.PushAsync(new CurrentReservationPage());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "Your reservation failed ,Enter time and number of people ", "Ok");
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }
        public void Clear()
        {
            ReservationTime = string.Empty;
            selectedValue = string.Empty;
            Note = string.Empty;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public bool IsOneTwoChecked
        {
            get => _isOneTwoChecked;
            set
            {
                if (_isOneTwoChecked != value)
                {
                    _isOneTwoChecked = value;
                    if (value)
                    {
                        selectedValue = "1-2";
                    }
                    OnPropertyChanged(nameof(IsOneTwoChecked));
                }
            }
        }


        public bool IsTwoThreeChecked
        {
            get => _isTwoThreeChecked;
            set
            {
                if (_isTwoThreeChecked != value)
                {
                    _isTwoThreeChecked = value;
                    if (value)
                    {
                        selectedValue = "3-4";
                    }
                    OnPropertyChanged(nameof(IsTwoThreeChecked));
                }
            }
        }

        public bool IsFourChecked
        {
            get => _isFourChecked;
            set
            {
                if (_isFourChecked != value)
                {
                    _isFourChecked = value;
                    if (value)
                    {
                        selectedValue = "5-6";
                    }
                    OnPropertyChanged(nameof(IsFourChecked));
                }
            }
        }

        public bool IsFiveChecked
        {
            get => _isFiveChecked;
            set
            {
                if (_isFiveChecked != value)
                {
                    _isFiveChecked = value;
                    if (value)
                    {
                        selectedValue = "7-10";
                    }
                    OnPropertyChanged(nameof(IsFiveChecked));


                }
            }
        }
        public bool IsSixChecked
        {
            get => _isSixChecked;
            set
            {
                if (_isSixChecked != value)
                {
                    _isSixChecked = value;
                    if (value)
                    {
                        selectedValue = "more than 10 people";
                    }
                    OnPropertyChanged(nameof(IsSixChecked));

                }
            }
        }



        public int SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                if (_selectedValue != value)
                {
                    _selectedValue = value;
                    OnPropertyChanged(nameof(SelectedValue));
                }
            }
        }
        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                OnPropertyChanged(nameof(Note));
            }
        }
    }
}
       
