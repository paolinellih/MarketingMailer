using MarketingMailer.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MarketingMailer.Infrastructure.Services;
public class EmailBackgroundService : BackgroundService
{
    private readonly IEmailQueue _emailQueue;
    private readonly IEmailService _emailService;
    private readonly ILogger<EmailBackgroundService> _logger;

    public EmailBackgroundService(
        IEmailQueue emailQueue,
        IEmailService emailService,
        ILogger<EmailBackgroundService> logger)
    {
        _emailQueue = emailQueue;
        _emailService = emailService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Email background service started.");

        var retryDelay = TimeSpan.FromSeconds(2);
        var maxDelay = TimeSpan.FromMinutes(1);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var request = await _emailQueue.DequeueAsync(stoppingToken);
                await _emailService.SendMarketingEmailsAsync(request.Template, request.Recipients);
                _logger.LogInformation("Email batch sent successfully.");

                retryDelay = TimeSpan.FromSeconds(2); // Reset delay after success
            }
            catch (OperationCanceledException)
            {
                // Expected when the service is stopping
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email batch.");
                
                _logger.LogInformation("Retrying in {Delay} seconds...", retryDelay.TotalSeconds);
                await Task.Delay(retryDelay, stoppingToken);
                // Increase delay for next retry (double it)
                retryDelay = TimeSpan.FromSeconds(Math.Min(retryDelay.TotalSeconds * 2, maxDelay.TotalSeconds));
            }
        }

        _logger.LogInformation("Email background service is stopping.");
    }
}