using FluentValidation;
using Newtonsoft.Json;
using MarketingMailer.API.Responses;

namespace MarketingMailer.API.Services
{
    public abstract class HttpBaseProcessService<TService> : IHttpProcessService
    {
        private readonly IValidator<object>? _validator;
        protected readonly ILogger<TService> Logger;

        protected HttpBaseProcessService(
            ILogger<TService> logger,
            IValidator<object>? validator = null)
        {
            Logger = logger;
            _validator = validator;
        }

        protected abstract Task<HttpProcessResult> InternalProcess(object request);

        public async Task<HttpProcessResult> ProcessAsync(object request)
        {
            if (_validator != null)
            {
                var result = await _validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    var errors = result.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(x => x.ErrorMessage).ToList());

                    return new HttpProcessResult
                    {
                        Response = new ValidationErrorResponse
                        {
                            Successful = false,
                            ValidationErrors = errors
                        }
                    };
                }
            }

            try
            {
                return await InternalProcess(request);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unhandled error in {Service}", typeof(TService).Name);
                return new HttpProcessResult
                {
                    Response = new ValidationErrorResponse
                    {
                        Successful = false,
                        ValidationErrors = new Dictionary<string, List<string>>
                        {
                            ["Request"] = new List<string> { "An unexpected error occurred." }
                        }
                    }
                };
            }
        }

        public async Task<string> ProcessAndReturnJsonAsync(object request)
        {
            var result = await ProcessAsync(request);
            return JsonConvert.SerializeObject(
                result,
                new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                });
        }
    }
}
