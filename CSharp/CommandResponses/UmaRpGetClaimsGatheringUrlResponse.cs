using Newtonsoft.Json;


namespace oxdCSharp.UMA.CommandResponses
{

    /// <summary>
    /// Response for UMA Get Claims Gathering URL command
    /// </summary>
    public class UmaRpGetClaimsGatheringUrlResponse
    {

        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Get Claims Gathering URL command's response Data
        /// </summary>
        [JsonProperty("data")]
        public UmaRpGetClaimsGatheringUrlResponseData Data { get; set; }
    }

    /// <summary>
    /// Get Claims Gathering URL Response's data
    /// </summary>
    public class UmaRpGetClaimsGatheringUrlResponseData
    {
        /// <summary>
        /// Claim Gathering URL
        /// </summary>
        [JsonProperty("url")]
        public string url { get; set; }

        /// <summary>
        /// State
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        /// Description of the error
        /// </summary>
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

    }
}
