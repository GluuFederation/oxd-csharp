using Newtonsoft.Json;

namespace oxdCSharp.CommandResponses
{
    /// <summary>
    /// Response for Register Site Command
    /// </summary>
    public class RegisterSiteResponse
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Register Site command's response Data
        /// </summary>
        [JsonProperty("data")]
        public RegisterSiteResponseData Data { get; set; }  
    }

    /// <summary>
    /// Register Site Response's Data
    /// </summary>
    public class RegisterSiteResponseData
    {
        /// <summary>
        /// Registered OXD Id form the OP server
        /// </summary>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        /// <summary>
        /// OP Host. Useful in case op_host is set in oxd-default-site-config.json (and not provided as parameter)
        /// </summary>
        [JsonProperty("op_host")]
        public string OpHost { get; set; }
    }
}
