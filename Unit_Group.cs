// 
// Generated code
// 

using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace EFGetStarted.AspNet5
{
    public partial class Unit_Group
    {
        public Unit_Group()
        {
            Unit = new HashSet<Unit>();
        }
        
        // Properties
        public int Unit_Group_Key { get; set; }
        public string Unit_Group_Name { get; set; }
        
        // Navigation Properties
        public virtual ICollection<Unit> Unit { get; set; }
    }
}
