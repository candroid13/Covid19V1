using Covid19.Entities.Abstract;
using System;

namespace Covid19.Entities.Concrete
{
    [Serializable]
    public class Intent : IEntity
    {
        public virtual int IntentId { get; set; }
        public virtual string IntentName { get; set; }

    }
}
