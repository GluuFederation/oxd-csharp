using Newtonsoft.Json;

namespace oxdCSharp.CommandParameters
{
    /// <summary>
    /// Params for Get Tokens By Code Command
    /// </summary>
    public class GetTokensByCodeParams
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
        /// <remarks><b>REQUIRED</b> Field (oxd-https-extension).</remarks>
        [JsonProperty("protection_access_token")]
        public string ProtectionAccessToken { get; set; }

        /// <summary>
        /// Code from OP redirect URL
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// State from OP redirect URL
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("state")]
        public string State { get; set; }
    }
}
