
using Newtonsoft.Json;

namespace oxdCSharp.CommandResponses
{
    /// <summary>
    /// Response for Get GAT Command
    /// </summary>
    public class GetGATResponse
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Get GAT command's response Data
        /// </summary>
        [JsonProperty("data")]
        public GetGATResponseData Data { get; set; }
    }

    /// <summary>
    /// Get GAT Response's data
    /// </summary>
    public class GetGATResponseData
    {
        /// <summary>
        /// RPT Token
        /// </summary>
        [JsonProperty("rpt")]
        public string Rpt { get; set; }
    }
}
