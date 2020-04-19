using Covid19.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Covid19.Entities.Concrete
{
    [Serializable]
    public class StateParams : IEntity
    {
        public int MissingParameterId { get; set; }
        public string MissingParameter { get; set; }
        public string MissingParameterMessage { get; set; }
        public List<string> MissingParameterData { get; set; }
        public bool IsMissingParameterCompleted { get; set; }
        public bool IsSuccess { get; set; }
        public string ResultMessage { get; set; }

    }
}
