// 
// Generated code
// 

using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace EFGetStarted.AspNet5
{
    public partial class User_Profile
    {
        public User_Profile()
        {
        }
        
        // Properties
        public int User_Key { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string First_Name { get; set; }
        public DateTime? Join_Date { get; set; }
        public string Last_Name { get; set; }
        public string Middle_Name { get; set; }
        public string State { get; set; }
        
        // Navigation Properties
        public virtual User User_KeyNavigation { get; set; }
    }
}
