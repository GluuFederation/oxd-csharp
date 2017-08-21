using Newtonsoft.Json;

namespace oxdCSharp.UMA.CommandResponses
{
    /// <summary>
    /// Response for UMA RS Check Access command
    /// </summary>
    public class UmaRsCheckAccessResponse
    {
        /// <summary>
        /// Status of the command execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Uma Rs Check Access command's response data
        /// </summary>
        [JsonProperty("data")]
        public UmaRsCheckAccessResponseData Data { get; set; }
    }

    /// <summary>
    /// Response Data for UMA RS Check Access command
    /// </summary>
    public class UmaRsCheckAccessResponseData
    {
        /// <summary>
        /// Status of Access of a resource
        /// </summary>
        [JsonProperty("access")]
        public string Access { get; set; }

        /// <summary>
        /// WWW Authentication Header
        /// </summary>
        [JsonProperty("www-authenticate_header")]
        public string WwwAuthenticateHeader { get; set; }

        /// <summary>
        /// Ticket
        /// </summary>
        [JsonProperty("ticket")]
        public string Ticket { get; set; }

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