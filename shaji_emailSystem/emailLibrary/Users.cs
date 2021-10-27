using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emailLibrary
{
    public class Users
    {
        public String UserName { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String SystemEmail { get; set; }

        public String AlternateEmail { get; set; }
        public String Avatar { get; set; }

        public int Status { get; set; }
        public String UserType { get; set; }
    }
}

