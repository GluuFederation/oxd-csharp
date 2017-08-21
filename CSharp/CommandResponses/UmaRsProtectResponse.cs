using Newtonsoft.Json;

namespace oxdCSharp.UMA.CommandResponses
{
    /// <summary>
    /// Response for UMA RS Protect command
    /// </summary>
    public class UmaRsProtectResponse
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
