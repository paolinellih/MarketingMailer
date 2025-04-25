using FluentValidation;
using MarketingMailer.API.Requests;

namespace MarketingMailer.API.Validators
{
    public class SendMarketingRequestValidator : AbstractValidator<SendMarketingRequest>
    {
        public SendMarketingRequestValidator()
        {
            RuleFor(x => x.Emails).NotEmpty().WithMessage("Emails are required.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Subject is required.");
            RuleFor(x => x.Body).NotEmpty().WithMessage("Body is required.");
        }
    }
}
