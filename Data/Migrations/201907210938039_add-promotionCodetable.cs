namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpromotionCodetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PromotionCode",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(nullable: false),
                        ExpiredDate = c.DateTime(nullable: false),
                        Discount = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PromotionCode");
        }
    }
}
