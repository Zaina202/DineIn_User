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
        public ObservableCollection<MenuItem> MenuItems { get; set; }


        public MenuPage()
        {
            InitializeComponent();

            MenuItems = new ObservableCollection<MenuItem>
        {
            new MenuItem { Name = "Item 1",Price="0", ImageUrl = "https://scontent.fjrs29-1.fna.fbcdn.net/v/t39.30808-6/336647745_769731694713211_1320182345920490310_n.jpg?_nc_cat=107&ccb=1-7&_nc_sid=730e14&_nc_ohc=E1z3Nhku7JgAX-Ynp_d&_nc_ht=scontent.fjrs29-1.fna&oh=00_AfAyqoitaSRg_kKj-Jn4nA2yV5EIOOFZ84Kfl6emu8DC5A&oe=642FD66F" },
            new MenuItem { Name = "Item 2",Price="0", ImageUrl = "https://scontent.fjrs29-1.fna.fbcdn.net/v/t39.30808-6/336647745_769731694713211_1320182345920490310_n.jpg?_nc_cat=107&ccb=1-7&_nc_sid=730e14&_nc_ohc=E1z3Nhku7JgAX-Ynp_d&_nc_ht=scontent.fjrs29-1.fna&oh=00_AfAyqoitaSRg_kKj-Jn4nA2yV5EIOOFZ84Kfl6emu8DC5A&oe=642FD66F" },
            new MenuItem { Name = "Item 3",Price="0", ImageUrl = "https://scontent.fjrs29-1.fna.fbcdn.net/v/t39.30808-6/336647745_769731694713211_1320182345920490310_n.jpg?_nc_cat=107&ccb=1-7&_nc_sid=730e14&_nc_ohc=E1z3Nhku7JgAX-Ynp_d&_nc_ht=scontent.fjrs29-1.fna&oh=00_AfAyqoitaSRg_kKj-Jn4nA2yV5EIOOFZ84Kfl6emu8DC5A&oe=642FD66F" },
            new MenuItem { Name = "Item 4",Price="0", ImageUrl = "https://scontent.fjrs29-1.fna.fbcdn.net/v/t39.30808-6/336647745_769731694713211_1320182345920490310_n.jpg?_nc_cat=107&ccb=1-7&_nc_sid=730e14&_nc_ohc=E1z3Nhku7JgAX-Ynp_d&_nc_ht=scontent.fjrs29-1.fna&oh=00_AfAyqoitaSRg_kKj-Jn4nA2yV5EIOOFZ84Kfl6emu8DC5A&oe=642FD66F" },
            new MenuItem { Name = "Item 5",Price="0", ImageUrl = "https://scontent.fjrs29-1.fna.fbcdn.net/v/t39.30808-6/336647745_769731694713211_1320182345920490310_n.jpg?_nc_cat=107&ccb=1-7&_nc_sid=730e14&_nc_ohc=E1z3Nhku7JgAX-Ynp_d&_nc_ht=scontent.fjrs29-1.fna&oh=00_AfAyqoitaSRg_kKj-Jn4nA2yV5EIOOFZ84Kfl6emu8DC5A&oe=642FD66F" },
        };
            ItemListView.ItemsSource = MenuItems;

        }
        public class MenuItem
        {
            public string Name { get; set; }
            public string ImageUrl { get; set; }
            public string Price { get; set; }

        }
    }
}