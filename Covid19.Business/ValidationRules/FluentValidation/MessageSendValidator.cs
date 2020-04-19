using Covid19.Entities.Concrete;
using FluentValidation;

namespace Covid19.Business.ValidationRules.FluentValidation
{
    public class MessageSendValidator : AbstractValidator<MessageSend>
    {
        public MessageSendValidator()
        {
            RuleFor(p => p.MessageSendId).NotEmpty();
            RuleFor(p => p.ReferenceId).NotEmpty();
            RuleFor(p => p.ReferenceType).NotEmpty();
            RuleFor(p => p.Text).NotEmpty();
        }
    }
}
