namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcarttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TableID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
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
                    })
                .PrimaryKey(t => new { t.ID, t.CartID })
                .ForeignKey("dbo.Cart", t => t.CartID, cascadeDelete: true)
                .Index(t => t.CartID);
            
            AlterColumn("dbo.Table", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartDetail", "CartID", "dbo.Cart");
            DropIndex("dbo.CartDetail", new[] { "CartID" });
            AlterColumn("dbo.Table", "Status", c => c.Boolean(nullable: false));
            DropTable("dbo.CartDetail");
            DropTable("dbo.Cart");
        }
    }
}
