namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstoredproceducegetrevenuebymoth : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetRevenueByMonth",
                p => new
                {
                    fromDate = p.DateTime(),
                    toDate = p.DateTime()
                },
            @"select Month(b.CreatedDate) Month,YEAR(b.CreatedDate) Year, sum(b.Total) as Revenue
    from Bill b where b.CreatedDate >=@fromDate and b.CreatedDate <=@toDate
    group by Month(b.CreatedDate) ,YEAR(b.CreatedDate)"

                );
        }

        public override void Down()
        {
            DropStoredProcedure("dbo.GetRevenueByMonth");
        }


    }
}
