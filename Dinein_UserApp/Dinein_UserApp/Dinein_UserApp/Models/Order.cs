using System;
using System.Collections.Generic;
using System.Text;

namespace Dinein_UserApp.Models
{
    public class Order
    {
        public int OrderTotalPrice { get; set; }

        public string ReservationId { get; set; }
        public List<OrderItem> OrderList { get; set; }
        public string BillOrderID { get; set; }


    }
}
