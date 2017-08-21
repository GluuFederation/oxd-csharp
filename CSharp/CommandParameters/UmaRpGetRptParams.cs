using Newtonsoft.Json;

namespace oxdCSharp.UMA.CommandParameters
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
        /// Ticket from  RS Check Access.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("ticket")]
        public string ticket { get; set; }



        /// <summary>
        /// Ticket from  RS Check Access.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("claim_token")]
        public string ClaimToken { get; set; }



        /// <summary>
        /// Ticket from  RS Check Access.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("claim_token_format")]
        public string ClaimTokenFormat { get; set; }


        /// <summary>
        /// Ticket from  RS Check Access.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("pct")]
        public string pct { get; set; }


        /// <summary>
        /// RPT- Request Party Token.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("access_token")]
        public string rpt { get; set; }

        /// <summary>
        /// scope
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("scope")]
        public string scope { get; set; }

        /// <summary>
        /// state returned from UMA_RP_GET_CLAIMS_GATHERING_URL
        /// </summary>
        /// <remarks><b>state</b> Field.</remarks>
        [JsonProperty("state")]
        public string state { get; set; }





        /// <summary>
        /// Protection Acccess Token.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("protection_access_token")]
        public string ProtectionAccessToken { get; set; }



        ///// <summary>
        ///// Indicates whether return new RPT, in general should be false, so oxd server can cache/reuse same RPT
        ///// </summary>
        ///// <remarks><b>REQUIRED</b> Field.</remarks>
        //[JsonProperty("force_new")]
        //public bool ForceNew { get; set; }
    }
}
