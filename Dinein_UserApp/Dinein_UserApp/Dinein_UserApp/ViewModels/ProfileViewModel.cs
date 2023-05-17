using Dinein_UserApp.Services;
using Dinein_UserApp.Views;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{

    public class ProfileViewModel : INotifyPropertyChanged
    {
        public ICommand LogoutCommand { get; }
        private readonly DataBase database;
        private string id = (string)Application.Current.Properties["UID"];
        public ProfileViewModel()
        {
            database = new DataBase();
            _ = LoadData(id);
            LogoutCommand = new Command(async () => await Logout());
        }

        private async Task Logout()
        {
            bool confirmed = await Application.Current.MainPage.DisplayAlert("Logout", "Are you sure you want to log out?", "Yes", "No");

            if (confirmed)
            {
                try
                {
                    Application.Current.Properties.Remove("UID");
                    await Application.Current.SavePropertiesAsync();
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                    await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while logging out: {ex.Message}");
                }
            }
        }


        public async Task LoadData(string userId)
        {
            var user = await database.GetUserById(userId);

            if (user != null)
            {
                Name = user.UserName;
                Email = user.Email;
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;

                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string email;
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


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
