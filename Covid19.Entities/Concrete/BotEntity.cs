using Covid19.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Covid19.Entities.Concrete
{
    [Serializable]
    public class BotEntity : IEntity
    {
        public string ConversationId { get; set; }
        public string ChannelId { get; set; }
        public string From { get; set; }
        public BotEntityDetail BotEntityDetail { get; set; }
        public List<PartialEntities> LuisEntites { get; set; }
    }
}
