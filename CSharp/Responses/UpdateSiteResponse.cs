using Newtonsoft.Json;

namespace oxdCSharp.Responses
{
    public class UpdateSiteResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
