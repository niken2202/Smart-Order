namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class getRoleproceduce : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetRoleByUserID",

                 p => new
                 {
                     userid = p.String()
                 }
                ,
                @"select r.Name from ApplicationUserRoles  ur
inner join ApplicationRoles r on ur.RoleId = r.Id and ur.UserId = @userid");
          
        }

        public override void Down()
        {
            DropStoredProcedure("dbo.GetRoleByUserID");
        }
    }
}
