namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addforeignkeydishcategory : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Dish", "CategoryID");
            AddForeignKey("dbo.Dish", "CategoryID", "dbo.DishCategory", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dish", "CategoryID", "dbo.DishCategory");
            DropIndex("dbo.Dish", new[] { "CategoryID" });
        }
    }
}
