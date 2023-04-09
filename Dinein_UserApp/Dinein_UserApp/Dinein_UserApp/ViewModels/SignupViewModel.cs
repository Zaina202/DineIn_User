using Dinein_UserApp.Models;
using Firebase.Database;
using Firebase.Database.Query;
using MvvmHelpers;
using System;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {
        private string username;
        private string phoneNumber;
        private string password;
        private string confirmPassword;

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set => SetProperty(ref confirmPassword, value);
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // TODO: implement phone number validation logic
            // This can be done using regular expressions or third-party libraries

            return true; // For now, we will just assume that all phone numbers are valid
        }

        public Command SignUpCommand => new Command(async () =>
        {
            // check if password and confirm password match
            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            // create new user object
            var newUser = new User
            {
                Username = Username,
                PhoneNumber = PhoneNumber,
                Password = HashPassword(Password) // encrypt the password using a cryptographic hash function
            };

            // get firebase database reference
            var firebase = new FirebaseClient("https://dine-in-54308-default-rtdb.firebaseio.com/");

            // create new user node in database and set value to user object
            await firebase.Child("Users").PostAsync(newUser);

            // display success message
            await Application.Current.MainPage.DisplayAlert("Success", "User account created successfully!", "OK");
        });

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hashedString;
            }
        }
    }
}