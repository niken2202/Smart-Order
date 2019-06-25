namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteTotal : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BillDetails", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BillDetails", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
