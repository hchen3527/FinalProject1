// 
// Generated code
// 

using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace EFGetStarted.AspNet5
{
    public partial class Unit_Conversion
    {
        public Unit_Conversion()
        {
        }
        
        // Properties
        public int Unit_Key1 { get; set; }
        public int Unit_Key2 { get; set; }
        public decimal? Conversion { get; set; }
        
        // Navigation Properties
        public virtual Unit Unit_Key1Navigation { get; set; }
        public virtual Unit Unit_Key2Navigation { get; set; }
    }
}
