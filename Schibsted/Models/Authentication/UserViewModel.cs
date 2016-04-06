using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schibsted.Models.Authentication
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}