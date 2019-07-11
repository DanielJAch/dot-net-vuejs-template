using System.Data.Entity.Migrations;

namespace DotNETVueJSTemplate.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DotNETVueJSTemplate.Data.ExampleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DotNETVueJSTemplate.Data.ExampleContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
