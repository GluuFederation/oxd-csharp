using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oxdCSharp.CommandParameters
{

    /// <summary>
    /// Params to get Access token from Refresh token
    /// </summary>
    public class GetAccessTokenByRefreshTokenParams
    {
        /// <summary>
        /// Registered OXD Id.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }


        /// <summary>
        /// Refresh Token from get_tokens_by_code command.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }


        /// <summary>
        /// Scope 
        /// </summary>
        /// <remarks><b>Optional</b> Field.</remarks>
        [JsonProperty("scope")]
        public List<string> scope { get; set; }

        /// <summary>
        /// Protection Acccess Token.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field (oxd-https-extension).</remarks>
        [JsonProperty("protection_access_token")]
        public string ProtectionAccessToken { get; set; }
    }
}
