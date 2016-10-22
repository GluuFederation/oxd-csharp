
using Newtonsoft.Json;

namespace oxdCSharp.CommandResponses
{
    /// <summary>
    /// Response for Get GAT or Get RPT Commands
    /// </summary>
    public class GetRPTResponse
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Get RPT command's response Data
        /// </summary>
        [JsonProperty("data")]
        public GetRPTResponseData Data { get; set; }
    }

    /// <summary>
    /// Get RPT Response's data
    /// </summary>
    public class GetRPTResponseData
    {
        /// <summary>
        /// RPT Token
        /// </summary>
        [JsonProperty("rpt")]
        public string Rpt { get; set; }
    }
}
