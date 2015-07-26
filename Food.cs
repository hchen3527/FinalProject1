// 
// Generated code
// 

using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace EFGetStarted.AspNet5
{
    public partial class Food
    {
        public Food()
        {
            Nutrition_History = new HashSet<Nutrition_History>();
        }
        
        // Properties
        public int Food_Key { get; set; }
        public decimal? Calcium { get; set; }
        public int Calcium_Unit_Key { get; set; }
        public decimal? Calories { get; set; }
        public decimal? Carbohydrate { get; set; }
        public int Carbohydrate_Unit_Key { get; set; }
        public decimal? Fiber { get; set; }
        public int Fiber_Unit_Key { get; set; }
        public decimal? Iron { get; set; }
        public int Iron_Unit_Key { get; set; }
        public decimal? Lipid { get; set; }
        public int Lipid_Unit_Key { get; set; }
        public string Name { get; set; }
        public decimal? Protein { get; set; }
        public int Protein_Unit_Key { get; set; }
        public decimal? Sugar { get; set; }
        public int Sugar_Unit_Key { get; set; }
        public decimal? Water { get; set; }
        public int Water_Unit_Key { get; set; }
        
        // Navigation Properties
        public virtual ICollection<Nutrition_History> Nutrition_History { get; set; }
        public virtual Unit Calcium_Unit_KeyNavigation { get; set; }
        public virtual Unit Carbohydrate_Unit_KeyNavigation { get; set; }
        public virtual Unit Fiber_Unit_KeyNavigation { get; set; }
        public virtual Unit Iron_Unit_KeyNavigation { get; set; }
        public virtual Unit Lipid_Unit_KeyNavigation { get; set; }
        public virtual Unit Protein_Unit_KeyNavigation { get; set; }
        public virtual Unit Sugar_Unit_KeyNavigation { get; set; }
        public virtual Unit Water_Unit_KeyNavigation { get; set; }
    }
}
