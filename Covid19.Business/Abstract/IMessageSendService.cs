using System.Collections.Generic;
using Covid19.Entities.Concrete;

namespace Covid19.Business.Abstract
{
    public interface IMessageSendService
    {
        List<MessageSend> GetAll();
        MessageSend GetById(int id);
    }
}
