using Newtonsoft.Json;

namespace oxdCSharp.UMA.CommandParameters
{
    /// <summary>
    /// Params for UMA RS Check Access command
    /// </summary>
    public class UmaRsCheckAccessParams
    {
        /// <summary>
        /// Registered OXD Id.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        /// <summary>
        /// RPT Token
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field. Can have blank value if absent (not send by RP)</remarks>
        [JsonProperty("rpt")]
        public string RPT { get; set; }

        /// <summary>
        /// Path of resource (e.g. http://rs.com/phones), /phones should be passed
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// Http method of RP request (GET, POST, PUT, DELETE)
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("http_method")]
        public string HttpMethod { get; set; }



        /// <summary>
        /// Protection Acccess Token.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("protection_access_token")]
        public string ProtectionAccessToken { get; set; }
    }
}
