using Newtonsoft.Json;

namespace oxdCSharp.CommandResponses
{
    /// <summary>
    /// Response for Get Logout URI Command
    /// </summary>
    public class GetLogoutUriResponse
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Get Logout URI command's response Data
        /// </summary>
        [JsonProperty("data")]
        public GetLogoutUriResponseData Data { get; set; }
    }

    /// <summary>
    /// Get Logout URI Response's data
    /// </summary>
    public class GetLogoutUriResponseData
    {
        /// <summary>
        /// Logout URI
        /// </summary>
        [JsonProperty("uri")]
        public string LogoutUri { get; set; }
    }
}
