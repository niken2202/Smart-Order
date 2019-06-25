namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class revenueStatitis : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetRevenueStatistic",
                p => new
                {
                    fromDate = p.DateTime(),
                    toDate = p.DateTime()

                },
                @"
                select b.CreatedDate as Date, sum(bd.Price*bd.Amount) as BillTotal
                from Bill b 
                inner join BillDetails bd on b.ID = bd.BillID
                where b.CreatedDate >=@fromDate and b.CreatedDate <=@toDate
                group by b.CreatedDate"); ;
        }

        public override void Down()
        {
            DropStoredProcedure("dbo.GetRevenueStatistic");
        }
    }
}
