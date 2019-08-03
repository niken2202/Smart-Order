namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillDetails",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    BillID = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 256),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Amount = c.Int(nullable: false),
                    Description = c.String(),
                    Image = c.String(maxLength: 256),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Bill", t => t.BillID, cascadeDelete: true)
                .Index(t => t.BillID);

            CreateTable(
                "dbo.Bill",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Voucher = c.String(maxLength: 256),
                    CustomerName = c.String(nullable: false, maxLength: 256),
                    Content = c.String(nullable: false),
                    TableID = c.Int(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(),
                    Discount = c.Int(),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Combo",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Description = c.String(),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Amount = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.DishBillMapping",
                c => new
                {
                    DishID = c.Int(nullable: false),
                    BillID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.DishID, t.BillID })
                .ForeignKey("dbo.Bill", t => t.BillID, cascadeDelete: true)
                .ForeignKey("dbo.Dish", t => t.DishID, cascadeDelete: true)
                .Index(t => t.DishID)
                .Index(t => t.BillID);

            CreateTable(
                "dbo.Dish",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Amount = c.Int(nullable: false),
                    Description = c.String(),
                    Image = c.String(maxLength: 256),
                    OrderCount = c.Int(),
                    CategoryID = c.Int(nullable: false),
                    Status = c.Int(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.DishCategory",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    CreatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.DishComboMapping",
                c => new
                {
                    DishID = c.Int(nullable: false),
                    ComboID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.DishID, t.ComboID })
                .ForeignKey("dbo.Combo", t => t.ComboID, cascadeDelete: true)
                .ForeignKey("dbo.Dish", t => t.DishID, cascadeDelete: true)
                .Index(t => t.DishID)
                .Index(t => t.ComboID);

            CreateTable(
                "dbo.DishMaterialMapping",
                c => new
                {
                    DishID = c.Int(nullable: false),
                    MaterialID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.DishID, t.MaterialID })
                .ForeignKey("dbo.Dish", t => t.DishID, cascadeDelete: true)
                .ForeignKey("dbo.Material", t => t.MaterialID, cascadeDelete: true)
                .Index(t => t.DishID)
                .Index(t => t.MaterialID);

            CreateTable(
                "dbo.Material",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Amount = c.Int(nullable: false),
                    Unit = c.String(maxLength: 50),
                    CreatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Errors",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Message = c.String(),
                    StackTrace = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.History",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    TaskName = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.IdentityRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                    IdentityRole_Id = c.String(maxLength: 128),
                    ApplicationUser_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);

            CreateTable(
                "dbo.Table",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    FullName = c.String(maxLength: 256),
                    Address = c.String(maxLength: 256),
                    BirthDay = c.DateTime(),
                    Email = c.String(),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                    ApplicationUser_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);

            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    LoginProvider = c.String(),
                    ProviderKey = c.String(),
                    ApplicationUser_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.DishMaterialMapping", "MaterialID", "dbo.Material");
            DropForeignKey("dbo.DishMaterialMapping", "DishID", "dbo.Dish");
            DropForeignKey("dbo.DishComboMapping", "DishID", "dbo.Dish");
            DropForeignKey("dbo.DishComboMapping", "ComboID", "dbo.Combo");
            DropForeignKey("dbo.DishBillMapping", "DishID", "dbo.Dish");
            DropForeignKey("dbo.DishBillMapping", "BillID", "dbo.Bill");
            DropForeignKey("dbo.BillDetails", "BillID", "dbo.Bill");
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.DishMaterialMapping", new[] { "MaterialID" });
            DropIndex("dbo.DishMaterialMapping", new[] { "DishID" });
            DropIndex("dbo.DishComboMapping", new[] { "ComboID" });
            DropIndex("dbo.DishComboMapping", new[] { "DishID" });
            DropIndex("dbo.DishBillMapping", new[] { "BillID" });
            DropIndex("dbo.DishBillMapping", new[] { "DishID" });
            DropIndex("dbo.BillDetails", new[] { "BillID" });
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.Table");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.History");
            DropTable("dbo.Errors");
            DropTable("dbo.Material");
            DropTable("dbo.DishMaterialMapping");
            DropTable("dbo.DishComboMapping");
            DropTable("dbo.DishCategory");
            DropTable("dbo.Dish");
            DropTable("dbo.DishBillMapping");
            DropTable("dbo.Combo");
            DropTable("dbo.Bill");
            DropTable("dbo.BillDetails");
        }

    }
}
