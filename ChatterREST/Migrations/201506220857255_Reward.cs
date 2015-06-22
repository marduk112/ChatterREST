namespace ChatterREST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reward : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rewards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rewards");
        }
    }
}
