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

        /// <summary>
        /// UmaRsProtect command's response Data
        /// </summary>
        [JsonProperty("data")]
        public UmaRsProtectResponseData Data { get; set; }
    }

    /// <summary>
    /// Uma RS Protect Response's Data
    /// </summary>
    public class UmaRsProtectResponseData
    {
        /// <summary>
        /// Registered OXD Id form the OP server
        /// </summary>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }
    }
}
