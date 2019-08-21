namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterbilltable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bill", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bill", "Total", c => c.Double(nullable: false));
        }
    }
}
