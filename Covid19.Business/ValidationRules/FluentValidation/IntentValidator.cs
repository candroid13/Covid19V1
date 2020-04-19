using Covid19.Entities.Concrete;
using FluentValidation;

namespace Covid19.Business.ValidationRules.FluentValidation
{
    public class IntentValidator : AbstractValidator<Intent>
    {
        public IntentValidator()
        {
            RuleFor(p => p.IntentId).NotEmpty();
            RuleFor(p => p.IntentName).NotEmpty();
        }
    }
}
