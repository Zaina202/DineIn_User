using Dinein_UserApp.Services;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace Dinein_UserApp.ViewModels
{

    public class ProfileViewModel : INotifyPropertyChanged
    {
        private readonly FirebaseAuth _firebaseAuth;

        private DataBase dataBase;
        public ProfileViewModel()
        {
            dataBase = new DataBase();
            LoadUserData();

        }
        private List<Models.Users> _UserInfo;


        private async void LoadUserData()
        {

           
            var user = await dataBase.GetUser("Id");
         
            Name = user.Username;
            Email = user.Email;
            

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

    }
}
