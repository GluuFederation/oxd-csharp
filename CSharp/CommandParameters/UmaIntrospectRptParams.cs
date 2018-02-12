

using Newtonsoft.Json;

namespace oxdCSharp.UMA.CommandParameters
{
    /// <summary>
    /// Params for UMA Introspect RPT command
    /// </summary>
    public class UmaIntrospectRptParams
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
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("rpt")]
        public string RPT { get; set; }
        
    }
}
