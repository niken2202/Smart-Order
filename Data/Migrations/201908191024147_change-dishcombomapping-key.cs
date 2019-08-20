namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedishcombomappingkey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DishComboMapping");
            AddColumn("dbo.DishComboMapping", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.DishComboMapping", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.DishComboMapping");
            DropColumn("dbo.DishComboMapping", "ID");
            AddPrimaryKey("dbo.DishComboMapping", new[] { "DishID", "ComboID" });
        }
    }
}