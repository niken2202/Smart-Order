namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditBill : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bill", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bill", "Content");
        }
    }
}
