using Newtonsoft.Json;
using System.Collections.Generic;

namespace oxdCSharp.CommandResponses
{
    /// <summary>
    /// Response for Introspect Access Token Command
    /// </summary>
    public class IntrospectAccessTokenResponse
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Introspect Access Token command's response Data
        /// </summary>
        [JsonProperty("data")]
        public IntrospectAccessTokenResponseData Data { get; set; }
    }

    /// <summary>
    /// Introspect Access Token Response's Data
    /// </summary>
    public class IntrospectAccessTokenResponseData
    {
        /// <summary>
        /// Active Status of token (True/False)
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; }

        /// <summary>
        /// Scope
        /// </summary>
        [JsonProperty("scopes")]
        public IList<string> Scopes { get; set; }

        /// <summary>
        /// Registered Client Id form the OP server
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Resource owner who authorized the access token
        /// </summary>
        [JsonProperty("username")]
        public string UserName { get; set; }

        /// <summary>
        /// Type of the token
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// Expiry Time of token (milliseconds since 1970).
        /// </summary>
        [JsonProperty("exp")]
        public long? EXP { get; set; }

        /// <summary>
        /// Issued Time of token (milliseconds since 1970).
        /// </summary>
        [JsonProperty("iat")]
        public long? IAT { get; set; }

        /// <summary>
        /// Subject of the token
        /// </summary>
        [JsonProperty("sub")]
        public string Sub { get; set; }

        /// <summary>
        /// List of string identifiers representing the intended audience for this token
        /// </summary>
        [JsonProperty("aud")]
        public string Aud { get; set; }

        /// <summary>
        /// Issuer of the token
        /// </summary>
        [JsonProperty("iss")]
        public string Iss { get; set; }

        /// <summary>
        /// String identifier for the token
        /// </summary>
        [JsonProperty("jti")]
        public string JTI { get; set; }

        /// <summary>
        /// ACR Values
        /// </summary>
        [JsonProperty("acr_values")]
        public IList<string> AcrValues { get; set; }


    }
}
