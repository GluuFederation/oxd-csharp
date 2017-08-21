using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oxdCSharp.CommandResponses
{
    public class GetClientTokenResponse
    {

        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Get Client Token response Data
        /// </summary>
        [JsonProperty("data")]
        public GetClientTokenResponseData Data { get; set; }
        

    }
    public class GetClientTokenResponseData
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
         [JsonProperty("access_token")]
        public string accessToken { get; set; }
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("expires_in")]
        public int expiresIn { get; set; }
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("refresh_token")]
        public string refreshToken { get; set; }

        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("scope")]
        public string scope { get; set; }

    }
}
