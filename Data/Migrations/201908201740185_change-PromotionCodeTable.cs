namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changePromotionCodeTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PromotionCode");
            AlterColumn("dbo.PromotionCode", "Code", c => c.String(nullable: false, maxLength: 256));
            AddPrimaryKey("dbo.PromotionCode", "Code");
            DropColumn("dbo.PromotionCode", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PromotionCode", "ID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.PromotionCode");
            AlterColumn("dbo.PromotionCode", "Code", c => c.String(maxLength: 256));
            AddPrimaryKey("dbo.PromotionCode", "ID");
        }
    }
}
