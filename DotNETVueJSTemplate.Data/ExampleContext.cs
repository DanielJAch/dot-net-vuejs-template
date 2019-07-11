using System.Configuration;
using System.Data.Entity;
using DotNETVueJSTemplate.Data.EntityConfigurations;
using DotNETVueJSTemplate.Model;

namespace DotNETVueJSTemplate.Data
{
    [DbConfigurationType(typeof(ExampleConfiguration))]
    public partial class ExampleContext : DbContext
    {
        private readonly bool _logAllSqlCalls;

        public ExampleContext() : this("ExampleDbContext")
        {
            bool.TryParse(ConfigurationManager.AppSettings["LogAllSqlCalls"], out this._logAllSqlCalls);
        }

        public ExampleContext(string nameOfConnectionString) : base(nameOfConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            bool.TryParse(ConfigurationManager.AppSettings["LogAllSqlCalls"], out this._logAllSqlCalls);
        }

        public virtual DbSet<ExampleEntity> ExampleEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureExampleEntity();
        }
    }
}
