namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_dishcombomapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DishComboMapping", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DishComboMapping", "Amount");
        }
    }
}
