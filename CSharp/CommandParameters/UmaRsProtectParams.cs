using Newtonsoft.Json;
using System.Collections.Generic;

namespace oxdCSharp.UMA.CommandParameters
{
    /// <summary>
    /// Params for UMA RS Protect command
    /// </summary>
    public class UmaRsProtectParams
    {
        /// <summary>
        /// Registered OXD Id.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        /// <summary>
        /// Allow existing resource to overwrite or not
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("overwrite")]
        public bool Overwrite { get; set; }

        /// <summary>
        /// Resources to be protected
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("resources")]
        public IList<ProtectResource> ProtectResources { get; set; }

        /// <summary>
        /// Protection Acccess Token.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field (oxd-https-extension).</remarks>
        [JsonProperty("protection_access_token")]
        public string ProtectionAccessToken { get; set; }
    }

    /// <summary>
    /// Description of a resource in RS
    /// </summary>
    public class ProtectResource
    {
        /// <summary>
        /// Path of the resource URl
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// Conditions on how the resources should be protected
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("conditions")]
        public IList<ProtectCondition> ProtectConditions { get; set; }
    }

    /// <summary>
    /// Input format of Conditions on how the resource should be protected
    /// </summary>
    public class ProtectCondition
    {
        /// <summary>
        /// List of HTTP Methods supported in a condition
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("httpMethods")]
        public IList<string> HttpMethods { get; set; }

        /// <summary>
        /// List of Scopes in a condition
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("scopes")]
        public IList<string> Scopes { get; set; }

        /// <summary>
        /// List of Scopes protected with ticket in a condition
        /// </summary>
        /// <remarks>Optional Field.</remarks>
        [JsonProperty("ticketScopes", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> TicketScopes { get; set; }

        /// <summary>
        /// Scope expression for logical operations
        /// </summary>
        [JsonProperty("scope_expression")]
        public ScopeExpression ScopeExpressions { get; set; }
    }

    /// <summary>
    /// Scope expression for logical operations
    /// </summary>
    public class ScopeExpression
    {
        /// <summary>
        /// Logical operation rule
        /// </summary>
        [JsonProperty("rule")]
        public object Rule { get; set; }

        /// <summary>
        /// Data for logical operation
        /// </summary>
        [JsonProperty("data")]
        public IList<string> Data { get; set; }
    }
    
}
