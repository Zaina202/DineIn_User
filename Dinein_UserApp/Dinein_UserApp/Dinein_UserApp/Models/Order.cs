using System;
using System.Collections.Generic;
using System.Text;

namespace Dinein_UserApp.Models
{
    public class Order
    {
        public String Id { get; set; }
        public List<string> MenuItemName { get; set; }
        public List<int> MenuItemPrice { get; set; }
        public List<int> Quantity { get; set; }
        public List<string> Image { get; set; }

    }
}
