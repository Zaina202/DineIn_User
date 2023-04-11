using System;
using System.Collections.Generic;
using System.Text;

namespace Dinein_UserApp.Models
{
    public class Order
    {
        public List<string> MenuItemName { get; set; }
        public List<int> MenuItemPrice { get; set; }
        public List<int> Quantity { get; set; }
        public List<string> Image { get; set; }
        public int TotalPrice { get; set; }
        public string UserId { get; set; }


    }
}
