
namespace MarketingMailer.Application.Interfaces;
public interface IEmailService
{
    Task SendMarketingEmailsAsync(string emailTemplate, List<string> recipients);
}
