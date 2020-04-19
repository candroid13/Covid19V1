using Covid19.Core.DataAccess.EntityFramework;
using Covid19.DataAccess.Abstract;
using Covid19.Entities.Concrete;

namespace Covid19.DataAccess.Concrete.EntityFramework
{
    public class EfMessageSendDal : EfEntityRepositoryBase<MessageSend, Covid19Context>, IMessageSendDal
    {
    }
}
