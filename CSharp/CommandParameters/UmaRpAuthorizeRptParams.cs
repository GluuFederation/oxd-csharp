using Newtonsoft.Json;

namespace oxdCSharp.CommandParameters
{
    /// <summary>
    /// Params for Authorize RPT Command
    /// </summary>
    public class UmaRpAuthorizeRptParams
    {
        /// <summary>
        /// Registered OXD Id.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        /// <summary>
        /// RPT token
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("rpt")]
        public string RPT { get; set; }

        /// <summary>
        /// Ticket
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("ticket")]
        public string Ticket { get; set; }
    }
}
