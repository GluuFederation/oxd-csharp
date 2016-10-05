using Newtonsoft.Json;

namespace oxdCSharp.CommandParameters
{
    /// <summary>
    /// Params for Get Logout URI Command
    /// </summary>
    public class GetLogoutUrlParams
    {
        /// <summary>
        /// Registered OXD Id.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        /// <summary>
        /// ID Token Hint. OXD Server will use last used ID Token
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("id_token_hint")]
        public string IdTokenHint { get; set; }

        /// <summary>
        /// Post Logout Redirect URI
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("post_logout_redirect_uri")]
        public string PostLogoutRedirectUri { get; set; }

        /// <summary>
        /// State
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Session State
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("session_state")]
        public string SessionState { get; set; }
    }
}
