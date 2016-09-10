using Newtonsoft.Json;

namespace oxdCSharp.CommandParameters
{
    public class GetLogoutUrlParams
    {
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        [JsonProperty("id_token_hint")]
        public string IdTokenHint { get; set; }

        [JsonProperty("post_logout_redirect_uri")]
        public string PostLogoutRedirectUri { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("session_state")]
        public string SessionState { get; set; }
    }
}
