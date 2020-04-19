using Covid19.Entities.Abstract;
using System;

namespace Covid19.Entities.Concrete
{
    [Serializable]
    public class Parameter : IEntity
    {
        public virtual int ParameterId { get; set; }
        public virtual int IntentId { get; set; }
        public virtual string ParameterName { get; set; }

    }
}
