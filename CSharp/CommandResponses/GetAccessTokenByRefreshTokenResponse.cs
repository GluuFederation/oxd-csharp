using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oxdCSharp.CommandResponses
{

    /// <summary>
    /// Response for Get Access token By Refresh token command
    /// </summary>
    public class GetAccessTokenByRefreshTokenResponse
    {

        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Authorize RPT command's response Data
        /// </summary>
        [JsonProperty("data")]
        public GetAccessTokenByRefreshTokenResponseData Data { get; set; }
    }

    /// <summary>
    /// Get Access token By Refresh token Response's Data
    /// </summary>
    public class GetAccessTokenByRefreshTokenResponseData
    {

        /// <summary>
        /// Access Token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Token expires_in
        /// </summary>
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        /// <summary>
        /// Refresh Token can be used as Access token
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
