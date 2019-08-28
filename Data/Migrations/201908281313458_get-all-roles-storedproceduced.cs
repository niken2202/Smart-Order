namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class getallrolesstoredproceduced : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetAllRole",
              @"select * from ApplicationRoles");
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.GetAllRole");
        }
    }
}
