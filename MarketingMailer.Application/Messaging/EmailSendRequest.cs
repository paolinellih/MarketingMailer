namespace MarketingMailer.Application.Messaging;
public class EmailSendRequest
{
    public required string Template { get; init; }
    public required string Subject { get; init; }
    public required List<string> Recipients { get; init; }
}