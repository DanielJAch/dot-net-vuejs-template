using System.Data.Entity;
using DotNETVueJSTemplate.Model;

namespace DotNETVueJSTemplate.Data.EntityConfigurations
{
    public static class ExampleEntityConfiguration
    {
        public static DbModelBuilder ConfigureExampleEntity(this DbModelBuilder builder)
        {
            var table = builder.Entity<ExampleEntity>();

            table.HasKey(m => m.Id);

            return builder;
        }
    }
}
