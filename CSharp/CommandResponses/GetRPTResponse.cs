
using Newtonsoft.Json;

namespace oxdCSharp.UMA.CommandResponses
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
        /// RPT Token as acces_token
        /// </summary>
        [JsonProperty("access_token")]
        public string Rpt { get; set; }


        /// <summary>
        /// token_type
        /// </summary>
        [JsonProperty("token_type")]
        public string token_type { get; set; }



        /// <summary>
        /// persisted claims token (PCT)
        /// </summary>
        [JsonProperty("pct")]
        public string pct { get; set; }

        /// <summary>
        /// upgraded
        /// </summary>
        [JsonProperty("updated")]
        public string upgraded { get; set; }

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
