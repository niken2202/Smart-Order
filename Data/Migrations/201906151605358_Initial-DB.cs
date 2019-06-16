namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bill",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Voucher = c.String(maxLength: 256),
                        CustomerName = c.String(nullable: false, maxLength: 256),
                        TableID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Discount = c.Int(nullable: false),
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
                        CreatedDate = c.Int(nullable: false),
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
                "dbo.Table",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DishMaterialMapping", "MaterialID", "dbo.Material");
            DropForeignKey("dbo.DishMaterialMapping", "DishID", "dbo.Dish");
            DropForeignKey("dbo.DishComboMapping", "DishID", "dbo.Dish");
            DropForeignKey("dbo.DishComboMapping", "ComboID", "dbo.Combo");
            DropForeignKey("dbo.DishBillMapping", "DishID", "dbo.Dish");
            DropForeignKey("dbo.DishBillMapping", "BillID", "dbo.Bill");
            DropIndex("dbo.DishMaterialMapping", new[] { "MaterialID" });
            DropIndex("dbo.DishMaterialMapping", new[] { "DishID" });
            DropIndex("dbo.DishComboMapping", new[] { "ComboID" });
            DropIndex("dbo.DishComboMapping", new[] { "DishID" });
            DropIndex("dbo.DishBillMapping", new[] { "BillID" });
            DropIndex("dbo.DishBillMapping", new[] { "DishID" });
            DropTable("dbo.Table");
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
        }
    }
}
