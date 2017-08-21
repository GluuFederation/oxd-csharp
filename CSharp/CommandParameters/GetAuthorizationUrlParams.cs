using Newtonsoft.Json;
using System.Collections.Generic;

namespace oxdCSharp.CommandParameters
{
    /// <summary>
    /// Params for Get Authorization URL Command
    /// </summary>
    public class GetAuthorizationUrlParams
    {
        /// <summary>
        /// Registered OXD Id.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        /// <summary>
        /// Protection Acccess Token.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("protection_access_token")]
        public string ProtectionAccessToken { get; set; }


        /// <summary>
        /// Scope. 
        /// If value not set, by default, the command takes Scopes that was registered during Register Site Command.
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("scope")]
        public IList<string> Scope { get; set; }

        /// <summary>
        /// ACR Values.
        /// If value not set, the default is set to <b>basic</b>.
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("acr_values")]
        public IList<string> AcrValues { get; set; }

        /// <summary>
        /// Prompt.
        /// If value not set, this field is skipped.
        /// <b>prompt=login is REQUIRED</b> if you want to force alter current user session.
        /// <example>In case user is already logged in from SITE1 and SITE2 construsts authorization request and want to force alter current user session.</example>
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("prompt")]
        public string Prompt { get; set; }
    }
}
