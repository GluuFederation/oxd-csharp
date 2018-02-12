using Newtonsoft.Json;
using System.Collections.Generic;

namespace oxdCSharp.UMA.CommandResponses
{
    /// <summary>
    /// Response for Introspect Rpt Command
    /// </summary>
    public class UmaIntrospectRptResponse
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Introspect Rpt command's response Data
        /// </summary>
        [JsonProperty("data")]
        public UmaIntrospectRptResponseData Data { get; set; }
    }

    /// <summary>
    /// Introspect Rpt Response's data
    /// </summary>
    public class UmaIntrospectRptResponseData
    {
        /// <summary>
        /// Active Status of token (True/False)
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; }

        /// <summary>
        /// Expiry Time of token (milliseconds since 1970).
        /// </summary>
        [JsonProperty("exp")]
        public long EXP { get; set; }

        /// <summary>
        /// Issued Time of token (milliseconds since 1970).
        /// </summary>
        [JsonProperty("iat")]
        public long IAT { get; set; }

        /// <summary>
        /// NBF
        /// </summary>
        [JsonProperty("nbf")]
        public string NBF { get; set; }

        /// <summary>
        /// Permissions
        /// </summary>
        [JsonProperty("permissions")]
        public IList<Permission> Permissions { get; set; }

        /// <summary>
        /// Registered Client Id form the OP server
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

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
    }

    /// <summary>
    /// Permissions
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Resource Id
        /// </summary>
        [JsonProperty("resource_id")]
        public string ResourceId { get; set; }

        /// <summary>
        /// Resource Scopes
        /// </summary>
        [JsonProperty("resource_scopes")]
        public IList<string> ResourceScopes { get; set; }

        /// <summary>
        /// Expiry Time
        /// </summary>
        [JsonProperty("exp")]
        public long EXP { get; set; }
    }
}
