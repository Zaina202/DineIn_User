using Dinein_UserApp.Services;
using Dinein_UserApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private readonly DataBase dataBase;

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
            dataBase = new DataBase();
            LogInCommand = new Command(async () => await LogIn());
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public Command LogInCommand { get; }

        private async Task LogIn()
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    await Application.Current.MainPage.DisplayAlert("warning", "Enter Email", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(Password))
                {
                    await Application.Current.MainPage.DisplayAlert("warning", "Enter password", "Ok");
                    return;
                }
                string token = await dataBase.SignIn(Email, Password);
                if (!string.IsNullOrEmpty(token))
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new Home());
                }
                else
                {
                    await DisplayAlert("Log in", "log in failed", "ok");
                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_NOT_FOUND")|| ex.Message.Contains("INVALID_PASSWORD"))
                {
                    await Application.Current.MainPage.DisplayAlert("Unauthorrized", "email not found or password incorrect", "ok");

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "ok");

                }
            }
        }

        private async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
