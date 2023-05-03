﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinein_UserApp.Services;
using Dinein_UserApp.ViewModels;
using Dinein_UserApp.Views;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dinein_UserApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        public MenuPage(string ResId)
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                var _reservationId = ResId;
                var viewModel = new MenuViewModel(_reservationId);
                Device.BeginInvokeOnMainThread(() =>
                {
                    BindingContext = viewModel;
                });
            });
        }



    }
}