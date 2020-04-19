using Covid19.Entities.Concrete;
using System.Data.Entity.ModelConfiguration;


namespace Covid19.DataAccess.Concrete.EntityFramework.Mappings
{
    public class IntentMap : EntityTypeConfiguration<Intent>
    {
        public IntentMap()
        {
            ToTable(@"Intent", @"dbo");
            HasKey(x => x.IntentId);
            Property(x => x.IntentId).HasColumnName("IntentId");
            Property(x => x.IntentName).HasColumnName("IntentName");
        }
    }
}
