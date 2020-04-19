using Covid19.Entities.Concrete;
using System.Data.Entity.ModelConfiguration;

namespace Covid19.DataAccess.Concrete.EntityFramework.Mappings
{
    public class MessageSendMap : EntityTypeConfiguration<MessageSend>
    {
        public MessageSendMap()
        {
            ToTable(@"MessageSend", @"dbo");
            HasKey(x => x.MessageSendId);
            Property(x => x.ReferenceId).HasColumnName("ReferenceId");
            Property(x => x.ReferenceType).HasColumnName("ReferenceType");
            Property(x => x.Text).HasColumnName("Text");
        }
    }
}
