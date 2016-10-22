using Newtonsoft.Json;

namespace oxdCSharp.CommandResponses
{
    /// <summary>
    /// Response for UMA RP Authorize RPT command
    /// </summary>
    public class UmaRpAuthorizeRptResponse
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Authorize RPT command's response Data
        /// </summary>
        [JsonProperty("data")]
        public UmaRpAuthorizeRptResponseData Data { get; set; }
    }

    /// <summary>
    /// Authorize RPT Response's data
    /// </summary>
    public class UmaRpAuthorizeRptResponseData
    {
        /// <summary>
        /// Authorize Error Code
        /// </summary>
        [JsonProperty("code")]
        public string AuthorizeErrorCode { get; set; }

        /// <summary>
        /// Authorize Error Description
        /// </summary>
        [JsonProperty("description")]
        public string AuthorizeErrorDescription { get; set; }
    }
}
