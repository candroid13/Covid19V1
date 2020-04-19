using Covid19.Entities.Abstract;

namespace Covid19.Entities.ComplexTypes
{
    public class IntentParameter : IEntity
    {
        public virtual string ParameterName { get; set; }
    }
}
