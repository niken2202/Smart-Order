namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrevenuestoredproceduced : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetRevenueStatistic",
               p => new
               {
                   fromDate = p.DateTime(),
                   toDate = p.DateTime()
               },
           @"select cast(b.CreatedDate as Date) as Date,Sum(b.Total) as Revenue from Bill b
where cast(b.CreatedDate as Date) >= @fromDate and cast(b.CreatedDate as Date) <= @toDate

group by cast(b.CreatedDate as Date)"

               );
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.GetRevenueStatistic");
        }
    }
}
