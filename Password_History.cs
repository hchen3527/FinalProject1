// 
// Generated code
// 

using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace EFGetStarted.AspNet5
{
    public partial class Password_History
    {
        public Password_History()
        {
        }
        
        // Properties
        public int Password_History_Key { get; set; }
        public string Password { get; set; }
        public int User_Key { get; set; }
        
        // Navigation Properties
        public virtual User User_KeyNavigation { get; set; }
    }
}
