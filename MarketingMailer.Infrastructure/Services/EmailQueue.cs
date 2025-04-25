using System.Threading.Channels;
using MarketingMailer.Application.Interfaces;
using MarketingMailer.Application.Messaging;

namespace MarketingMailer.Infrastructure.Services;
public class EmailQueue : IEmailQueue
{
    private readonly Channel<EmailSendRequest> _queue;

    public EmailQueue()
    {
        // Unbounded channel allows unlimited number of messages
        _queue = Channel.CreateUnbounded<EmailSendRequest>(new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        });
    }

    public void Enqueue(EmailSendRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        _queue.Writer.TryWrite(request);
    }

    public async ValueTask<EmailSendRequest> DequeueAsync(CancellationToken cancellationToken)
    {
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}
