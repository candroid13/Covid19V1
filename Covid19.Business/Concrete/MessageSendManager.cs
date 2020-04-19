using System;
using System.Collections.Generic;
using Covid19.Business.Abstract;
using Covid19.DataAccess.Abstract;
using Covid19.Entities.Concrete;

namespace Covid19.Business.Concrete
{
    public class MessageSendManager : IMessageSendService
    {
        private IMessageSendDal _messageSendDal;
        public MessageSendManager(IMessageSendDal messageSendDal)
        {
            _messageSendDal = messageSendDal;
        
        }
        public List<MessageSend> GetAll()
        {
            var messageSends = _messageSendDal.GetList();
            return messageSends;
        }
        public MessageSend GetById(int id)
        {
            return _messageSendDal.Get(p => p.MessageSendId == id);
        }
    }
}
