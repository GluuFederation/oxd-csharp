using Newtonsoft.Json;

namespace oxdCSharp.CommandResponses
{
    /// <summary>
    /// Response for Remove Site Command
    /// </summary>
    public class RemoveSiteResponse
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Remove Site command's response Data
        /// </summary>
        [JsonProperty("data")]
        public RemoveSiteResponseData Data { get; set; }
    }

    /// <summary>
    /// Remove Site Response's Data
    /// </summary>
    public class RemoveSiteResponseData
    {
        /// <summary>
        /// Registered OXD Id form the OP server
        /// </summary>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }
    }
}
