namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBillDetail : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.BillDetails");
            AddColumn("dbo.BillDetails", "BillID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.BillDetails", "BillID");
            CreateIndex("dbo.BillDetails", "BillID");
            AddForeignKey("dbo.BillDetails", "BillID", "dbo.Bill", "ID");
            DropColumn("dbo.BillDetails", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BillDetails", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.BillDetails", "BillID", "dbo.Bill");
            DropIndex("dbo.BillDetails", new[] { "BillID" });
            DropPrimaryKey("dbo.BillDetails");
            DropColumn("dbo.BillDetails", "BillID");
            AddPrimaryKey("dbo.BillDetails", "ID");
        }
    }
}
