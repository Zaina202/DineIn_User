using System;
using System.Collections.Generic;
using System.Text;

namespace Dinein_UserApp.Models
{
    public class BillOrder
    {
        public List<Order> OrderList { get; set; }
        public string BillOrderNo { get; set; }
        public string UserID { get; set; }
        public int OrderTotalPrice { get; set; }
        public string ReservationId { get; set; }
    }
}
