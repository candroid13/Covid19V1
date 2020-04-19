using Covid19.DataAccess.Concrete.EntityFramework.Mappings;
using Covid19.Entities.Concrete;
using System.Data.Entity;

namespace Covid19.DataAccess.Concrete.EntityFramework
{
    public class Covid19Context : DbContext
    {
        public Covid19Context()
        {
            Database.SetInitializer<Covid19Context>(null);
        }
        public DbSet<Intent> Intent { get; set; }
        public DbSet<Parameter> Parameter { get; set; }
        public DbSet<MessageSend> MessageSend { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IntentMap());
            modelBuilder.Configurations.Add(new ParameterMap());
            modelBuilder.Configurations.Add(new MessageSendMap());
        }
    }
}
