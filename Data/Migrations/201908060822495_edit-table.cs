namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edittable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Table");
            AlterColumn("dbo.Table", "DeviceID", c => c.String());
            AddPrimaryKey("dbo.Table", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Table");
            AlterColumn("dbo.Table", "DeviceID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Table", new[] { "ID", "DeviceID" });
        }
    }
}
