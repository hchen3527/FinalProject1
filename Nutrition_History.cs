// 
// Generated code
// 

using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace EFGetStarted.AspNet5
{
    public partial class Nutrition_History
    {
        public Nutrition_History()
        {
        }
        
        // Properties
        public int Nutrition_History_Key { get; set; }
        public int User_Key { get; set; }
        public DateTime? Add_Date { get; set; }
        public int Food_Key { get; set; }
        public decimal? Quantity { get; set; }
        public int Unit_Key { get; set; }
        
        // Navigation Properties
        public virtual Food Food_KeyNavigation { get; set; }
        public virtual Unit Unit_KeyNavigation { get; set; }
        public virtual User User_KeyNavigation { get; set; }
    }
}
