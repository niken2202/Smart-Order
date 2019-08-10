namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_top10_proceduced : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetTop10Dish",
                @"select TOP(10) *  from Dish d
where d.Status=1 
order by d.OrderCount DESC");
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.GetTop10Dish");
        }
    }
}
