using System;
using System.Collections.Generic;
using System.Text;

namespace Dinein_UserApp.Models
{
    public class Order
    {
        public string MenuItemName { get; set; }
        public int MenuItemPrice { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public string OrderId { get; set; }

    }
}
