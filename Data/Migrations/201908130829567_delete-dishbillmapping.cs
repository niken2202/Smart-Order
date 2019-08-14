namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedishbillmapping : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DishBillMapping", "BillID", "dbo.Bill");
            DropForeignKey("dbo.DishBillMapping", "DishID", "dbo.Dish");
            DropIndex("dbo.DishBillMapping", new[] { "DishID" });
            DropIndex("dbo.DishBillMapping", new[] { "BillID" });
            DropTable("dbo.DishBillMapping");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DishBillMapping",
                c => new
                    {
                        DishID = c.Int(nullable: false),
                        BillID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DishID, t.BillID });
            
            CreateIndex("dbo.DishBillMapping", "BillID");
            CreateIndex("dbo.DishBillMapping", "DishID");
            AddForeignKey("dbo.DishBillMapping", "DishID", "dbo.Dish", "ID", cascadeDelete: true);
            AddForeignKey("dbo.DishBillMapping", "BillID", "dbo.Bill", "ID", cascadeDelete: true);
        }
    }
}
