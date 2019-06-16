namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editdatbase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bill", "Discount", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bill", "Discount", c => c.Int(nullable: false));
        }
    }
}
