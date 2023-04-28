using Dinein_UserApp.Services;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{

    public class ProfileViewModel : INotifyPropertyChanged
    {
        private readonly DataBase database;
        private string id = (string)Application.Current.Properties["UID"];
        public ProfileViewModel()
        {
            database = new DataBase();
            _ = LoadData(id);
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
