namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_table : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Table");
            AddColumn("dbo.Table", "DeviceID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Table", new[] { "ID", "DeviceID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Table");
            DropColumn("dbo.Table", "DeviceID");
            AddPrimaryKey("dbo.Table", "ID");
        }
    }
}
