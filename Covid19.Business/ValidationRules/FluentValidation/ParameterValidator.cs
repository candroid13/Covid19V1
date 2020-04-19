using Covid19.Entities.Concrete;
using FluentValidation;

namespace Covid19.Business.ValidationRules.FluentValidation
{
    public class ParameterValidator : AbstractValidator<Parameter>
    {
        public ParameterValidator()
        {
            RuleFor(p => p.ParameterId).NotEmpty();
            RuleFor(p => p.IntentId).NotEmpty();
            RuleFor(p => p.ParameterName).NotEmpty();
        }
    }
}
