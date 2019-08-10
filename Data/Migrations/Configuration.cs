namespace Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    internal sealed class Configuration : DbMigrationsConfiguration<Data.SmartOrderContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.SmartOrderContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SmartOrderContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new SmartOrderContext()));

            var user = new ApplicationUser()
            {
                UserName = "thang",
                Email = "hahaa.hahaha@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Le Trong Thang",

            };
            manager.Create(user, "123456");
            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("test");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });

        }
    }
}