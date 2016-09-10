using Newtonsoft.Json;

namespace oxdCSharp.CommandResponses
{
    public class GetLogoutUriResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public GetLogoutUriResponseData Data { get; set; }
    }

    public class GetLogoutUriResponseData
    {
        [JsonProperty("uri")]
        public string LogoutUri { get; set; }
    }
}
