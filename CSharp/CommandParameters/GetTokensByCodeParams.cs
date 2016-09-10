using Newtonsoft.Json;

namespace oxdCSharp.CommandParameters
{
    public class GetTokensByCodeParams
    {
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
