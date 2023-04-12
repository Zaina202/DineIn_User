using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dinein_UserApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_SignUp(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignupPage());
        }

        private void Reset_Password(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ResetPasswordPage());

        }
    }
}