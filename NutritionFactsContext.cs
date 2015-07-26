// 
// Generated using Connection String: Server=CHONCHEN-WS01\SQLEXPRESS;Database=NutritionFacts;Trusted_Connection=True;
// 

using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace EFGetStarted.AspNet5
{
    public partial class NutritionFactsContext : DbContext
    {
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<Nutrition_History> Nutrition_History { get; set; }
        public virtual DbSet<Password_History> Password_History { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<Unit_Conversion> Unit_Conversion { get; set; }
        public virtual DbSet<Unit_Group> Unit_Group { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<User_Profile> User_Profile { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=CHONCHEN-WS01\SQLEXPRESS;Database=NutritionFacts;Trusted_Connection=True;");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>(entity =>
            {
                entity.Key(e => e.Food_Key);
                
                entity.Property(e => e.Food_Key)
                    .ForSqlServer().UseIdentity();
                
                entity.Property(e => e.Calcium)
                    .ForRelational().ColumnType("decimal(19, 8)");
                
                entity.Property(e => e.Calcium)
                    .ForRelational().DefaultValue(0m);
                
                entity.Property(e => e.Calories)
                    .ForRelational().ColumnType("decimal(19, 8)");
                
                entity.Property(e => e.Calories)
                    .ForRelational().DefaultValue(0m);
                
                entity.Property(e => e.Carbohydrate)
                    .ForRelational().ColumnType("decimal(19, 8)");
                
                entity.Property(e => e.Carbohydrate)
                    .ForRelational().DefaultValue(0m);
                
                entity.Property(e => e.Fiber)
                    .ForRelational().ColumnType("decimal(19, 8)");
                
                entity.Property(e => e.Fiber)
                    .ForRelational().DefaultValue(0m);
                
                entity.Property(e => e.Iron)
                    .ForRelational().ColumnType("decimal(19, 8)");
                
                entity.Property(e => e.Iron)
                    .ForRelational().DefaultValue(0m);
                
                entity.Property(e => e.Lipid)
                    .ForRelational().ColumnType("decimal(19, 8)");
                
                entity.Property(e => e.Lipid)
                    .ForRelational().DefaultValue(0m);
                
                entity.Property(e => e.Protein)
                    .ForRelational().ColumnType("decimal(19, 8)");
                
                entity.Property(e => e.Protein)
                    .ForRelational().DefaultValue(0m);
                
                entity.Property(e => e.Sugar)
                    .ForRelational().ColumnType("decimal(19, 8)");
                
                entity.Property(e => e.Sugar)
                    .ForRelational().DefaultValue(0m);
                
                entity.Property(e => e.Water)
                    .ForRelational().ColumnType("decimal(19, 8)");
                
                entity.Property(e => e.Water)
                    .ForRelational().DefaultValue(0m);
            });
            
            modelBuilder.Entity<Nutrition_History>(entity =>
            {
                entity.Key(e => new { e.Nutrition_History_Key, e.User_Key });
                
                entity.Property(e => e.Nutrition_History_Key)
                    .ForSqlServer().UseIdentity();
                
                entity.Property(e => e.Add_Date)
                    .ForRelational().DefaultExpression("dateadd(day,datediff(day,(0),getdate()),(0))");
                
                entity.Property(e => e.Quantity)
                    .ForRelational().ColumnType("decimal(19, 8)");
                
                entity.Property(e => e.Quantity)
                    .ForRelational().DefaultValue(0m);
            });
            
            modelBuilder.Entity<Password_History>(entity =>
            {
                entity.Key(e => e.Password_History_Key);
                
                entity.Property(e => e.Password_History_Key)
                    .ForSqlServer().UseIdentity();
            });
            
            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Key(e => e.Unit_Key);
                
                entity.Property(e => e.Unit_Key)
                    .ForSqlServer().UseIdentity();
            });
            
            modelBuilder.Entity<Unit_Conversion>(entity =>
            {
                entity.Key(e => new { e.Unit_Key1, e.Unit_Key2 });
                
                entity.Property(e => e.Conversion)
                    .ForRelational().ColumnType("decimal(19, 8)");
            });
            
            modelBuilder.Entity<Unit_Group>(entity =>
            {
                entity.Key(e => e.Unit_Group_Key);
                
                entity.Property(e => e.Unit_Group_Key)
                    .ForSqlServer().UseIdentity();
            });
            
            modelBuilder.Entity<User>(entity =>
            {
                entity.Key(e => e.User_Key);
                
                entity.Property(e => e.User_Key)
                    .ForSqlServer().UseIdentity();
            });
            
            modelBuilder.Entity<User_Profile>(entity =>
            {
                entity.Key(e => e.User_Key);
                
                entity.Property(e => e.Join_Date)
                    .ForRelational().DefaultExpression("dateadd(day,datediff(day,(0),getdate()),(0))");
            });
            
            modelBuilder.Entity<Food>(entity =>
            {
                entity.Reference<Unit>(d => d.Calcium_Unit_KeyNavigation).InverseCollection(p => p.Food).ForeignKey(d => d.Calcium_Unit_Key);
                
                entity.Reference<Unit>(d => d.Carbohydrate_Unit_KeyNavigation).InverseCollection(p => p.FoodNavigation).ForeignKey(d => d.Carbohydrate_Unit_Key);
                
                entity.Reference<Unit>(d => d.Fiber_Unit_KeyNavigation).InverseCollection(p => p.Food1).ForeignKey(d => d.Fiber_Unit_Key);
                
                entity.Reference<Unit>(d => d.Iron_Unit_KeyNavigation).InverseCollection(p => p.Food2).ForeignKey(d => d.Iron_Unit_Key);
                
                entity.Reference<Unit>(d => d.Lipid_Unit_KeyNavigation).InverseCollection(p => p.Food3).ForeignKey(d => d.Lipid_Unit_Key);
                
                entity.Reference<Unit>(d => d.Protein_Unit_KeyNavigation).InverseCollection(p => p.Food4).ForeignKey(d => d.Protein_Unit_Key);
                
                entity.Reference<Unit>(d => d.Sugar_Unit_KeyNavigation).InverseCollection(p => p.Food5).ForeignKey(d => d.Sugar_Unit_Key);
                
                entity.Reference<Unit>(d => d.Water_Unit_KeyNavigation).InverseCollection(p => p.Food6).ForeignKey(d => d.Water_Unit_Key);
            });
            
            modelBuilder.Entity<Nutrition_History>(entity =>
            {
                entity.Reference<Food>(d => d.Food_KeyNavigation).InverseCollection(p => p.Nutrition_History).ForeignKey(d => d.Food_Key);
                
                entity.Reference<Unit>(d => d.Unit_KeyNavigation).InverseCollection(p => p.Nutrition_History).ForeignKey(d => d.Unit_Key);
                
                entity.Reference<User>(d => d.User_KeyNavigation).InverseCollection(p => p.Nutrition_History).ForeignKey(d => d.User_Key);
            });
            
            modelBuilder.Entity<Password_History>(entity =>
            {
                entity.Reference<User>(d => d.User_KeyNavigation).InverseCollection(p => p.Password_History).ForeignKey(d => d.User_Key);
            });
            
            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Reference<Unit_Group>(d => d.Unit_Group_KeyNavigation).InverseCollection(p => p.Unit).ForeignKey(d => d.Unit_Group_Key);
            });
            
            modelBuilder.Entity<Unit_Conversion>(entity =>
            {
                entity.Reference<Unit>(d => d.Unit_Key1Navigation).InverseCollection(p => p.Unit_Conversion).ForeignKey(d => d.Unit_Key1);
                
                entity.Reference<Unit>(d => d.Unit_Key2Navigation).InverseCollection(p => p.Unit_ConversionNavigation).ForeignKey(d => d.Unit_Key2);
            });
            
            modelBuilder.Entity<User_Profile>(entity =>
            {
                entity.Reference<User>(d => d.User_KeyNavigation).InverseReference(p => p.User_Profile).ForeignKey<User_Profile>(d => d.User_Key);
            });
        }
    }
}
