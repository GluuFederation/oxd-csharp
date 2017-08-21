using Newtonsoft.Json;

namespace oxdCSharp.UMA.CommandParameters
{


   public class UmaRpGetClaimsGatheringUrlParams
    {


        /// <summary>
        /// Registered OXD Id.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        /// <summary>
        /// Ticket
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("ticket")]
        public string ticket { get; set; }

        /// <summary>
        /// ClaimsRedirectURI
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("claims_redirect_uri")]
        public string ClaimsRedirectURI { get; set; }


        /// <summary>
        /// ProtectionAccessToken (PAT)
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("protection_access_token")]
        public string ProtectionAccessToken { get; set; }

    }
}
