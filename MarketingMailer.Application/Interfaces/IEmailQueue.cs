using MarketingMailer.Application.Messaging;

namespace MarketingMailer.Application.Interfaces;

public interface IEmailQueue
{
    void Enqueue(EmailSendRequest request);
    ValueTask<EmailSendRequest> DequeueAsync(CancellationToken cancellationToken);
}