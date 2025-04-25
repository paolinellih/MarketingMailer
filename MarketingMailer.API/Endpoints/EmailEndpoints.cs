using MarketingMailer.API.Requests;
using MarketingMailer.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketingMailer.API.Endpoints;

public static class EmailEndpoints
{
    private const string BaseUri = "api/Email";
    public static void AddMapEmailEndpoints(this WebApplication app)
    {
        app.MapPost($"{BaseUri}/send", 
        async (
            [FromBody] SendMarketingRequest request,
            [FromKeyedServices("MarketingMailerSendMarketingEmailService")] IHttpProcessService service) =>
                await service.ProcessAndReturnJsonAsync(request));

    }
}
