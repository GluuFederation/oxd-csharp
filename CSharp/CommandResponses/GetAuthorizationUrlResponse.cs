using Newtonsoft.Json;

namespace oxdCSharp.CommandResponses
{
    public class GetAuthorizationUrlResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public GetAuthorizationUrlResponseData Data { get; set; }
    }

    public class GetAuthorizationUrlResponseData
    {
        [JsonProperty("authorization_url")]
        public string AuthorizationUrl { get; set; }
    }
}
