namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_cartDetail_Storedproceduce : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetAllCartDetail",
                "select cd.*, t.Name as TableName ,t.ID as TableID  from CartDetail cd " +
                "inner join Cart c on c.ID = cd.CartID" +
                " inner join[Table] t on t.ID = c.TableID"
                );
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.GetAllCartDetail");
        }
    }
}
