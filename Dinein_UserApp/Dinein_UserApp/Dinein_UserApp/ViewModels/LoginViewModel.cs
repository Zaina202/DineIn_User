using Dinein_UserApp.Services;
using Dinein_UserApp.Views;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly DataBase dataBase;
        private string email;
        private string password;

        public LoginViewModel()
        {
            dataBase = new DataBase();
            LogInCommand = new Command(async () => await LogIn());
            RetrieveStoredCredentials();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public Command LogInCommand { get; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task LogIn()
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Enter Email", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(Password))
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Enter password", "Ok");
                    return;
                }

                string token = await dataBase.SignIn(Email, Password);
                if (!string.IsNullOrEmpty(token))
                {
                    StoreCredentials();
                    Preferences.Set(token, true);
                    await Application.Current.MainPage.Navigation.PushAsync(new Home());
                }
                else
                {
                    await DisplayAlert("Log in", "Log in failed", "Ok");
                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_NOT_FOUND") || ex.Message.Contains("INVALID_PASSWORD"))
                {
                    await Application.Current.MainPage.DisplayAlert("Unauthorized", "Email not found or password incorrect", "Ok");
                }
                else
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        private async void RetrieveStoredCredentials()
        {
            Email = await SecureStorage.GetAsync("Email");
            Password = await SecureStorage.GetAsync("Password");
        }

        private async void StoreCredentials()
        {
            await SecureStorage.SetAsync("Email", Email);
            await SecureStorage.SetAsync("Password", Password);
        }
    }
}
