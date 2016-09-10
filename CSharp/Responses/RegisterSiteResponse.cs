using Newtonsoft.Json;

namespace oxdCSharp.Responses
{
    public class RegisterSiteResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public RegisterSiteResponseData Data { get; set; }  
    }

    public class RegisterSiteResponseData
    {
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }
    }
}
