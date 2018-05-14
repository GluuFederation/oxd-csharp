
using Newtonsoft.Json;
using System.Collections.Generic;

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

        /// <summary>
        /// Need Info Details
        /// </summary>
        [JsonProperty("details")]
        public ErrorDetails Details { get; set; }
    }

    /// <summary>
    /// Need Info Error Details
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Error
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        /// Ticket to Get the Claims
        /// </summary>
        [JsonProperty("ticket")]
        public string Ticket { get; set; }


        /// <summary>
        /// Required Claims
        /// </summary>
        [JsonProperty("required_claims")]
        public IList<RequiredClaim> RequiredClaims { get; set; }

        /// <summary>
        /// Redirect User
        /// </summary>
        [JsonProperty("redirect_user")]
        public string RedirectUser { get; set; }
        
    }

    /// <summary>
    /// Required Claims (Need_Info error)
    /// </summary>
    public class RequiredClaim
    {
        /// <summary>
        /// Claim Token Format
        /// </summary>
        [JsonProperty("claim_token_format")]
        public  IList<string> ClaimTokenFormat { get; set; }

        /// <summary>
        /// Claim Type
        /// </summary>
        [JsonProperty("claim_type")]
        public string  ClaimType { get; set; }

        /// <summary>
        /// Friendly Name
        /// </summary>
        [JsonProperty("friendly_name")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Issuer
        /// </summary>
        [JsonProperty("issuer")]
        public IList<string> Issuer { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
