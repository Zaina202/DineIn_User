using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinein_UserApp.Views;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dinein_UserApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        // public ObservableCollection<MenuItem> MenuItems { get; set; }


        private Entry counterEntry;

        public MenuPage()
        {
            InitializeComponent();
            counterEntry = (Entry)FindByName("counterEntry");


        }


        private void Save_Order(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BillPage());

        }
        

        private void Plus(object sender, EventArgs e)
        {
            if (counterEntry != null)
            {
                int counter = int.Parse(counterEntry.Text);
                counter++;
                counterEntry.Text = counter.ToString();
            }
        }

        private void Minus(object sender, EventArgs e)
        {
            if (counterEntry != null)
            {
                int counter = int.Parse(counterEntry.Text);
                if (counter > 0)
                {
                    counter--;
                    counterEntry.Text = counter.ToString();
                }
            }
        }
    }
}