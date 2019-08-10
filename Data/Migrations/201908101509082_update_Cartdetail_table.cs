namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_Cartdetail_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartDetail", "Note", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CartDetail", "Note");
        }
    }
}
