using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Find_A_Restaurant.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int UserFirstName { get; set; }
        public int UserLastName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserPhoneNumber { get; set; }

    }
}