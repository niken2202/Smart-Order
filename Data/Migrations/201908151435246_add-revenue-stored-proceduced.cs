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
           @"select b.CreatedDate as  Date, sum(b.Total) as Revenue
    from Bill b 
    where b.CreatedDate >=@fromDate and b.CreatedDate <=@toDate
   group by b.CreatedDate"

               );
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.GetRevenueStatistic");
        }
    }
}
