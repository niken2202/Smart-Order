namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecombotable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Combo", "Image", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Combo", "Image");
        }
    }
}
