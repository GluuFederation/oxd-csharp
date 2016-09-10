using Newtonsoft.Json;

namespace oxdCSharp.CommandResponses
{
    public class UpdateSiteResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
