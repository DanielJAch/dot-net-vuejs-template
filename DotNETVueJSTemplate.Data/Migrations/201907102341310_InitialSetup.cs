using System.Data.Entity.Migrations;

namespace DotNETVueJSTemplate.Data.Migrations
{
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExampleEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExampleProperty = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExampleEntities");
        }
    }
}
