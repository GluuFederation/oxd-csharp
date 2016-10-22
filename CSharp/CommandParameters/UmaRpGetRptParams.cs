using Newtonsoft.Json;

namespace oxdCSharp.CommandParameters
{
    /// <summary>
    /// Params for UMA RP - Get RPT Command
    /// </summary>
    public class UmaRpGetRptParams
    {
        /// <summary>
        /// Registered OXD Id.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        /// <summary>
        /// Indicates whether return new RPT, in general should be false, so oxd server can cache/reuse same RPT
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("force_new")]
        public bool ForceNew { get; set; }
    }
}
