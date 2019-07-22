namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBillStoredProceduced : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetBillByRange",
                p => new
                {
                    fromDate = p.DateTime(),
                    toDate = p.DateTime()
                },
                @"select * from Bill where Bill.CreatedDate>=@fromDate and Bill.CreatedDate<= @toDate"
); 
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.GetBillByRange");
        }
    }
}
