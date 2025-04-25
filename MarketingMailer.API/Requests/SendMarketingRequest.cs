namespace MarketingMailer.API.Requests
{
    public class SendMarketingRequest
    {
        public List<string> Emails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
