using Dinein_UserApp.Models;
using Firebase.Database;
using Firebase.Database.Query;
using MvvmHelpers;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;


namespace Dinein_UserApp.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {
        private string username;
        private string email;
        private string phoneNumber;
        private string password;
        private string confirmPassword;

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
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
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            // use a regular expression to validate the phone number
            var regex = new Regex(@"^05\d([-]{0,1})\d{7}$");
            return regex.IsMatch(phoneNumber);// For now, we will just assume that all phone numbers are valid
        }
   
        public Command SignUpCommand => new Command(async () =>
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return;
            }

            // check if password and confirm password match
            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }
            if (!IsValidPhoneNumber(PhoneNumber))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid phone number.", "OK");
                return;
            }
            if (!IsValidEmail(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid email.", "OK");
                return;
            }

            // create new user object
            var newUser = new User
            {
                Username = Username,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Password = HashPassword(Password) // encrypt the password using a cryptographic hash function
            };
            // get firebase database reference
            var firebase = new FirebaseClient("https://dine-in-54308-default-rtdb.firebaseio.com/");

            // check if phone number is already used
            var existingUser = await firebase.Child("Users").OrderBy("PhoneNumber").EqualTo(PhoneNumber).OnceAsync<User>();
            if (existingUser.Count > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Phone number is already taken.", "OK");
                return;
            }
            // create new user node in database and set value to user object
            await firebase.Child("Users").PostAsync(newUser);

            // display success message
            await Application.Current.MainPage.DisplayAlert("Success", "User account created successfully!", "OK");
        });
        public bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(email);

            return match.Success;

        }

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