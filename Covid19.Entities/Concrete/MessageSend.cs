using Covid19.Entities.Abstract;
using System;

namespace Covid19.Entities.Concrete
{
    [Serializable]
    public class MessageSend : IEntity
    {
        public virtual int MessageSendId { get; set; }
        public virtual int ReferenceId { get; set; }
        public virtual int ReferenceType { get; set; }
        public virtual string Text { get; set; }
    }
}
