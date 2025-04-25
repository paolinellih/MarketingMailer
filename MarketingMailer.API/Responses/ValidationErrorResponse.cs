using System;
using System.Collections.Generic;

namespace MarketingMailer.API.Responses
{
    public class ValidationErrorResponse : BaseResponse
    {
        public Dictionary<string, List<string>> ValidationErrors { get; set; }
    }

    public class BaseResponse
    {
        public bool Successful { get; set; }
    }
}
