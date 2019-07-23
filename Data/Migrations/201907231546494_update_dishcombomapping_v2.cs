namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_dishcombomapping_v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Combo", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Combo", "Status");
        }
    }
}
