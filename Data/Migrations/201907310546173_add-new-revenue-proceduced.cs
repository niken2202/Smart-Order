namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewrevenueproceduced : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetRevenueByMonth",
                p => new
                {
                    fromDate = p.DateTime(),
                    toDate = p.DateTime()
                },
            @"select Month(b.CreatedDate) Month,YEAR(b.CreatedDate) Year, sum(bd.Price*bd.Amount) as Revenue
    from Bill b 
    inner join BillDetails bd on b.ID = bd.BillID
    where b.CreatedDate >=@fromDate and b.CreatedDate <=@toDate
    group by Month(b.CreatedDate) ,YEAR(b.CreatedDate)"

                );
        }

        public override void Down()
        {
            DropStoredProcedure("dbo.GetRevenueByMonth");
        }

    }
}
