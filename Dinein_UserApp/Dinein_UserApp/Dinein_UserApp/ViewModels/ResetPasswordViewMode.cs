using Dinein_UserApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{
    class ResetPasswordViewModel : INotifyPropertyChanged
    {
        private readonly DataBase dataBase;

        public event PropertyChangedEventHandler PropertyChanged;

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
                    await Shell.Current.GoToAsync("//LoginPage");

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Password reset failed", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}