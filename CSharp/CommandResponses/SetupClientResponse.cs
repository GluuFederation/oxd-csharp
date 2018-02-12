using Newtonsoft.Json;

namespace oxdCSharp.CommandResponses
{
    /// <summary>
    /// Response for Register Site Command
    /// </summary>
    public class SetupClientResponse
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
        public SetupClientResponseData Data { get; set; }
    }

    /// <summary>
    /// Setup Client Response's Data (oxd-Id, ClientId, ClientScret, ..)
    /// </summary>
    public class SetupClientResponseData
    {
        /// <summary>
        /// Registered OXD Id form the OP server
        /// </summary>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        /// <summary>
        /// OpenID Provider
        /// </summary>
        [JsonProperty("op_host")]
        public string opHost { get; set; }

        /// <summary>
        /// Registered Client Id form the OP server
        /// </summary>
        [JsonProperty("client_id")]
        public string clientId { get; set; }

        /// <summary>
        ///Client secret form the OP server
        /// </summary>
        [JsonProperty("client_secret")]
        public string clientSecret { get; set; }
        
        /// <summary>
        ///Client access token form the OP server
        /// </summary>
        [JsonProperty("client_registration_access_token")]
        public string clientRegistrationAccessToken { get; set; }

        /// <summary>
        /// Client Registration Uri
        /// </summary>
        [JsonProperty("client_registration_client_uri")]
        public string clientRegistrationClientUri { get; set; }

        /// <summary>
        /// Client Issued time
        /// </summary>
        [JsonProperty("client_id_issued_at")]
        public long clientIdIssuedAt { get; set; }

        /// <summary>
        /// Client expire time
        /// </summary>
        [JsonProperty("client_secret_expires_at")]
        public long clientSecretExpiresAt { get; set; }

    }
}
