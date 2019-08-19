namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCombo_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Combo", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Combo", "CreatedDate");
        }
    }
}
