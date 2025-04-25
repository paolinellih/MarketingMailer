using MarketingMailer.Application.Interfaces;

namespace MarketingMailer.Infrastructure.Services;
public class EmailService : IEmailService
{
    public async Task SendMarketingEmailsAsync(string emailTemplate, List<string> recipients)
    {
        // Simulating email sending
        foreach (var email in recipients)
        {
            await Task.Delay(500); // Simulate time for sending email
            Console.WriteLine($"Email sent to: {email}");
        }
    }
}