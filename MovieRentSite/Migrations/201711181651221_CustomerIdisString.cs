namespace MovieRentSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerIdisString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rentals", "CustomerId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rentals", "CustomerId", c => c.Int(nullable: false));
        }
    }
}
