using Covid19.Entities.Abstract;

namespace Covid19.Entities.ComplexTypes
{
    public class ParameterMessage : IEntity
    {
        public virtual string Text { get; set; }
    }
}
