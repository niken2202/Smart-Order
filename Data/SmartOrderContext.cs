using Microsoft.AspNet.Identity.EntityFramework;
using Model.Models;
using System.Data.Entity;

namespace Data
{
    public class SmartOrderContext : IdentityDbContext<ApplicationUser>
    {
        public SmartOrderContext() : base("SmartOrderConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Bill> Bills { set; get; }
        public DbSet<Combo> Combos { set; get; }
        public DbSet<Dish> Dishes { set; get; }
        public DbSet<DishBillMapping> DishBillMappings { set; get; }
        public DbSet<DishCategory> DishCategories { set; get; }
        public DbSet<DishMaterialMapping> DishMaterialMappings { set; get; }
        public DbSet<Error> Errors { set; get; }
        public DbSet<Material> Materials { set; get; }
        public DbSet<Table> Tables { set; get; }
        public DbSet<History> History { set; get; }
        public DbSet<DishComboMapping> DishComboMapping { set; get; }
        public DbSet<BillDetail> BillDetail { set; get; }
        public DbSet<PromotionCode> PromotionCode { set; get; }
        public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { set; get; }
        public static SmartOrderContext Create()
        {
            return new SmartOrderContext();
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new {i.UserId,i.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityUserClaim>().HasKey(i => new { i.UserId }).ToTable("ApplicationUserClaims");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
        }
    }
}