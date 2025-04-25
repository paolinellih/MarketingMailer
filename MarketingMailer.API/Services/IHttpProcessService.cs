using MarketingMailer.API.Responses;

namespace MarketingMailer.API.Services
{
    public interface IHttpProcessService
    {
        Task<HttpProcessResult> ProcessAsync(object request);
        Task<string> ProcessAndReturnJsonAsync(object request);
    }
}
