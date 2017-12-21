using Newtonsoft.Json;

namespace oxdCSharp.CommandParameters
{
    /// <summary>
    /// Params for Get User Info Command
    /// </summary>
    public class GetUserInfoParams
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
        /// Access Token
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
