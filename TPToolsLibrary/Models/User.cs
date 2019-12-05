using System;
using System.Collections.Generic;
using System.Text;
using TPToolsLibrary.BrowserActions;

namespace TPToolsLibrary.Models
{
    public class User
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string OrgUnit { get; set; }
        public UserRole UserRole { get; set; }
        public bool SendEmail { get; set; }

    }
}
