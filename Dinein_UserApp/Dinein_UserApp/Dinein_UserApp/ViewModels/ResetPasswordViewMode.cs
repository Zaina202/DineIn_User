using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{
    public class ResetPasswordViewMode : ContentPage
    {
        public ResetPasswordViewMode()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms!" }
                }
            };
        }
    }
}