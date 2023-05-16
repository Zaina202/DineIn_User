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
    class ResetPasswordViewModel 
    {
        private readonly DataBase dataBase;


        public ResetPasswordViewModel()
        {
            dataBase = new DataBase();
            ResetPasswordCommand = new Command(async () => await ResetPassword());
        }

        public string Email { get; set; }

        public Command ResetPasswordCommand { get; }

        private async Task ResetPassword()
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    await Application.Current.MainPage.DisplayAlert("warning", "Type Email", "Ok");
                    return;
                }
                bool result = await dataBase.ResetPassword(Email);
                if (result)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Password reset email sent", "Ok");
                    await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Password reset failed", "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", ex.Message);
            }
        }
    }
}