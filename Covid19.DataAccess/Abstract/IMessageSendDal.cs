using Covid19.Core.DataAccess;
using Covid19.Entities.Concrete;

namespace Covid19.DataAccess.Abstract
{
    public interface IMessageSendDal : IEntityRepository<MessageSend>
    {
    }
}
