using Newtonsoft.Json;

namespace oxdCSharp.CommandResponses
{
    /// <summary>
    /// Response for Get Authorization URL Command
    /// </summary>
    public class GetAuthorizationUrlResponse
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Get Authorization URL command's response Data
        /// </summary>
        [JsonProperty("data")]
        public GetAuthorizationUrlResponseData Data { get; set; }
    }

    /// <summary>
    /// Get Authorization URL Response's Data
    /// </summary>
    public class GetAuthorizationUrlResponseData
    {
        /// <summary>
        /// Authorization URL
        /// </summary>
        [JsonProperty("authorization_url")]
        public string AuthorizationUrl { get; set; }
    }
}
