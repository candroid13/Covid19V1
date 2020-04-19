using Covid19.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Covid19.Entities.Concrete
{
    [Serializable]
    public class CovidParams : IEntity
    {
        public string success { get; set; }
        public List<Result> result { get; set; }

    }
    [Serializable]
    public partial class Result
    {
        public string country { get; set; }
        public string totalCases { get; set; }
        public string newCases { get; set; }
        public string totalDeaths { get; set; }
        public string newDeaths { get; set; }
        public string totalRecovered { get; set; }
        public string activeCases { get; set; }
    }
}
