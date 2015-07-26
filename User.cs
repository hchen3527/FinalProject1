// 
// Generated code
// 

using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace EFGetStarted.AspNet5
{
    public partial class User
    {
        public User()
        {
            Nutrition_History = new HashSet<Nutrition_History>();
            Password_History = new HashSet<Password_History>();
        }
        
        // Properties
        public int User_Key { get; set; }
        public string Password { get; set; }
        public string User_Name { get; set; }
        
        // Navigation Properties
        public virtual ICollection<Nutrition_History> Nutrition_History { get; set; }
        public virtual ICollection<Password_History> Password_History { get; set; }
        public virtual User_Profile User_Profile { get; set; }
    }
}
