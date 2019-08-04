namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class add_historyProceduced : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetHistoryByRange",
                p => new
                {
                    fromDate = p.DateTime(),
                    toDate = p.DateTime()
                },
                @"select * from History where History.CreatedDate>=@fromDate and History.CreatedDate<= @toDate");
        }

        public override void Down()
        {
            DropStoredProcedure("dbo.GetHistoryByRange");
        }
    }
}
