namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeBillDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BillDetails", "BillID", "dbo.Bill");
            DropPrimaryKey("dbo.BillDetails");
            AddColumn("dbo.BillDetails", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BillDetails", "ID");
            AddForeignKey("dbo.BillDetails", "BillID", "dbo.Bill", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BillDetails", "BillID", "dbo.Bill");
            DropPrimaryKey("dbo.BillDetails");
            DropColumn("dbo.BillDetails", "ID");
            AddPrimaryKey("dbo.BillDetails", "BillID");
            AddForeignKey("dbo.BillDetails", "BillID", "dbo.Bill", "ID");
        }
    }
}
