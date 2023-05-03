using System;
using System.Collections.Generic;
using System.Text;

namespace Dinein_UserApp.Models
{
    public class OrderItem
    {
        public int OrderTotalPrice { get; set; }

        public string MenuItemName { get; set; }
        public int MenuItemPrice { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public int TotalPrice { get; set; }
        public string OrderId { get; set; }
        public string ReservationId { get; set; }
    }
}
