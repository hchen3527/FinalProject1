// 
// Generated code
// 

using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace EFGetStarted.AspNet5
{
    public partial class Unit
    {
        public Unit()
        {
            Food = new HashSet<Food>();
            FoodNavigation = new HashSet<Food>();
            Food1 = new HashSet<Food>();
            Food2 = new HashSet<Food>();
            Food3 = new HashSet<Food>();
            Food4 = new HashSet<Food>();
            Food5 = new HashSet<Food>();
            Food6 = new HashSet<Food>();
            Nutrition_History = new HashSet<Nutrition_History>();
            Unit_Conversion = new HashSet<Unit_Conversion>();
            Unit_ConversionNavigation = new HashSet<Unit_Conversion>();
        }
        
        // Properties
        public int Unit_Key { get; set; }
        public int Unit_Group_Key { get; set; }
        public string Unit_Name { get; set; }
        
        // Navigation Properties
        public virtual ICollection<Food> Food { get; set; }
        public virtual ICollection<Food> FoodNavigation { get; set; }
        public virtual ICollection<Food> Food1 { get; set; }
        public virtual ICollection<Food> Food2 { get; set; }
        public virtual ICollection<Food> Food3 { get; set; }
        public virtual ICollection<Food> Food4 { get; set; }
        public virtual ICollection<Food> Food5 { get; set; }
        public virtual ICollection<Food> Food6 { get; set; }
        public virtual ICollection<Nutrition_History> Nutrition_History { get; set; }
        public virtual ICollection<Unit_Conversion> Unit_Conversion { get; set; }
        public virtual ICollection<Unit_Conversion> Unit_ConversionNavigation { get; set; }
        public virtual Unit_Group Unit_Group_KeyNavigation { get; set; }
    }
}
