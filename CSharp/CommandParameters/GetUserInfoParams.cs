using Newtonsoft.Json;

namespace oxdCSharp.CommandParameters
{
    public class GetUserInfoParams
    {
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
