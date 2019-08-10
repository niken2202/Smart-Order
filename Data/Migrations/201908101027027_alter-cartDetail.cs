namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altercartDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartDetail", "ProID", c => c.Int(nullable: false));
            DropColumn("dbo.CartDetail", "CateID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartDetail", "CateID", c => c.Int(nullable: false));
            DropColumn("dbo.CartDetail", "ProID");
        }
    }
}
