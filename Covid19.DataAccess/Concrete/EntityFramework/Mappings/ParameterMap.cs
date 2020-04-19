using Covid19.Entities.Concrete;
using System.Data.Entity.ModelConfiguration;

namespace Covid19.DataAccess.Concrete.EntityFramework.Mappings
{
    public class ParameterMap : EntityTypeConfiguration<Parameter>
    {
        public ParameterMap()
        {
            ToTable(@"Parameter", @"dbo");
            HasKey(x => x.ParameterId);

            Property(x => x.ParameterId).HasColumnName("ParameterId");
            Property(x => x.IntentId).HasColumnName("IntentId");
            Property(x => x.ParameterName).HasColumnName("ParameterName");
        }
    }
}
