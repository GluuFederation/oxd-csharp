using Newtonsoft.Json;

namespace oxdCSharp.CommandResponses
{
    /// <summary>
    /// Response for Update Site Command
    /// </summary>
    public class UpdateSiteResponse
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Update Site command's response data
        /// </summary>
        [JsonProperty("data")]
        public UpdateSiteResponseData Data { get; set; }
    }

    /// <summary>
    /// Update Site Response's Data
    /// </summary>
    public class UpdateSiteResponseData
    {
        /// <summary>
        /// Registered OXD Id form the OP server
        /// </summary>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }
    }
}
