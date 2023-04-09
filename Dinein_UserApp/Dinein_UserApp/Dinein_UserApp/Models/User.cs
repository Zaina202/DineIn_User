using System;
using System.Collections.Generic;
using System.Text;

namespace Dinein_UserApp.Models
{
    public class User
    {
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        private string confirmPassword { get; set; }
    }
}
