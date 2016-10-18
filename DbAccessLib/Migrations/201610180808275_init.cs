namespace DbAccessLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        Street = c.String(),
                        Zip = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "PersonId", "dbo.People");
            DropIndex("dbo.Addresses", new[] { "PersonId" });
            DropTable("dbo.Addresses");
        }
    }
}
