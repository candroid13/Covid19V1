using Covid19.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Covid19.Entities.Concrete
{
    [Serializable]
    public class LuisResult : IEntity
    {
        public string Query { get; set; }
        public PartialTopScoringIntent TopScoringIntent { get; set; }
        public List<PartialIntents> Intents { get; set; }
        public List<PartialEntities> Entities { get; set; }
        public PartialSentimentAnalysis SentimentAnalysis { get; set; }
    }
    [Serializable]
    public partial class PartialTopScoringIntent
    {
        public string Intent { get; set; }
        public double Score { get; set; }
    }
    [Serializable]
    public partial class PartialIntents
    {
        public string Intent { get; set; }
        public double Score { get; set; }
    }
    [Serializable]
    public partial class PartialEntities
    {
        public string Entity { get; set; }
        public string Type { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public double Score { get; set; }
    }
    [Serializable]
    public partial class PartialSentimentAnalysis
    {
        public double Score { get; set; }
    }
}
