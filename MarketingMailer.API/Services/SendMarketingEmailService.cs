using FluentValidation;
using MarketingMailer.API.Requests;
using MarketingMailer.API.Responses;
using MarketingMailer.Application.Interfaces;
using MarketingMailer.Application.Messaging;

namespace MarketingMailer.API.Services
{
    public class SendMarketingEmailService
        : HttpBaseProcessService<SendMarketingEmailService>
    {
        private readonly IEmailQueue _queue;

        public SendMarketingEmailService(
            ILogger<SendMarketingEmailService> logger,
            IEmailQueue queue,
            IValidator<SendMarketingRequest> validator)
            : base(logger, validator as IValidator<object>)
        {
            _queue = queue;
        }

        protected override Task<HttpProcessResult> InternalProcess(object request)
        {
            var req = request as SendMarketingRequest;

            _queue.Enqueue(new EmailSendRequest
            {
                Recipients = req!.Emails,
                Subject = req.Subject,
                Template = req.Body
            });

            return Task.FromResult(new HttpProcessResult
            {
                Response = Results.Accepted()
            });
        }
    }
}
