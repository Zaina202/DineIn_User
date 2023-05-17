//using Dinein_UserApp.ViewModels;
using Dinein_UserApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dinein_UserApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Home), typeof(Home));
            Routing.RegisterRoute(nameof(CurrentReservationPage), typeof(CurrentReservationPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        }

    }
}
