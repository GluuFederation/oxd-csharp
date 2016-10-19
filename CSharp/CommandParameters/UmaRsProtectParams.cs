using Newtonsoft.Json;
using System.Collections.Generic;

namespace oxdCSharp.CommandParameters
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
        /// Resources to be protected
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("resources")]
        public IList<ProtectResource> ProtectResources { get; set; }
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
        [JsonProperty("ticketScopes")]
        public IList<string> TicketScopes { get; set; }
    }
}
