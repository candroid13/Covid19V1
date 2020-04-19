using Covid19.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Covid19.Entities.Concrete
{
    [Serializable]
    public class BotEntityDetail :IEntity
    {
        public string IntentName { get; set; }
        public int IntentId { get; set; }
        public List<EntityField> IntentEntities { get; set; }
        public StateParams StateParameters { get; set; }
        public string Message { get; set; }

    }
}
