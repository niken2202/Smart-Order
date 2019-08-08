namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(maxLength: 250),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ApplicationRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
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
                "dbo.Cart",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TableID = c.Int(nullable: false),
                        CartPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CartDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CartID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Image = c.String(nullable: false, maxLength: 256),
                        Status = c.Int(nullable: false),
                        CateID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cart", t => t.CartID, cascadeDelete: true)
                .Index(t => t.CartID);
            
            CreateTable(
                "dbo.Combo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Int(nullable: false),
                        Image = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
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
                        Amount = c.Int(nullable: false),
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
                "dbo.PromotionCode",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(nullable: false),
                        ExpiredDate = c.DateTime(nullable: false),
                        Discount = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Table",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DeviceID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID, t.DeviceID });
            
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
                "dbo.ApplicationUserClaims",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUserLogins",
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
            DropForeignKey("dbo.ApplicationUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserRoles", "IdentityRole_Id", "dbo.ApplicationRoles");
            DropForeignKey("dbo.DishMaterialMapping", "MaterialID", "dbo.Material");
            DropForeignKey("dbo.DishMaterialMapping", "DishID", "dbo.Dish");
            DropForeignKey("dbo.DishComboMapping", "DishID", "dbo.Dish");
            DropForeignKey("dbo.DishComboMapping", "ComboID", "dbo.Combo");
            DropForeignKey("dbo.DishBillMapping", "DishID", "dbo.Dish");
            DropForeignKey("dbo.DishBillMapping", "BillID", "dbo.Bill");
            DropForeignKey("dbo.CartDetail", "CartID", "dbo.Cart");
            DropForeignKey("dbo.BillDetails", "BillID", "dbo.Bill");
            DropIndex("dbo.ApplicationUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.DishMaterialMapping", new[] { "MaterialID" });
            DropIndex("dbo.DishMaterialMapping", new[] { "DishID" });
            DropIndex("dbo.DishComboMapping", new[] { "ComboID" });
            DropIndex("dbo.DishComboMapping", new[] { "DishID" });
            DropIndex("dbo.DishBillMapping", new[] { "BillID" });
            DropIndex("dbo.DishBillMapping", new[] { "DishID" });
            DropIndex("dbo.CartDetail", new[] { "CartID" });
            DropIndex("dbo.BillDetails", new[] { "BillID" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "IdentityRole_Id" });
            DropTable("dbo.ApplicationUserLogins");
            DropTable("dbo.ApplicationUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.Table");
            DropTable("dbo.PromotionCode");
            DropTable("dbo.History");
            DropTable("dbo.Errors");
            DropTable("dbo.Material");
            DropTable("dbo.DishMaterialMapping");
            DropTable("dbo.DishComboMapping");
            DropTable("dbo.DishCategory");
            DropTable("dbo.Dish");
            DropTable("dbo.DishBillMapping");
            DropTable("dbo.Combo");
            DropTable("dbo.CartDetail");
            DropTable("dbo.Cart");
            DropTable("dbo.Bill");
            DropTable("dbo.BillDetails");
            DropTable("dbo.ApplicationUserRoles");
            DropTable("dbo.ApplicationRoles");
        }
    }
}
