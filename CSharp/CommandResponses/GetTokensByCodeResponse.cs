using Newtonsoft.Json;
using System.Collections.Generic;

namespace oxdCSharp.CommandResponses
{
    /// <summary>
    /// Response for Get Token By Code Command
    /// </summary>
    public class GetTokensByCodeResponse
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Get Token By Code command's response Data
        /// </summary>
        [JsonProperty("data")]
        public GetTokensByCodeResponseData Data { get; set; }
    }

    /// <summary>
    /// Get Tokens By Code Response's data
    /// </summary>
    public class GetTokensByCodeResponseData
    {
        /// <summary>
        /// Access Token of the user
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Token Expiry 
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Refresh Token of the user
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// ID Token of the user
        /// </summary>
        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        /// <summary>
        /// ID Token Claims
        /// </summary>
        [JsonProperty("id_token_claims")]
        public IdTokenClaims IdTokenClaims { get; set; }
    }

    /// <summary>
    /// ID Token Claims
    /// </summary>
    public class IdTokenClaims
    {
        /// <summary>
        /// ISS
        /// </summary>
        [JsonProperty("iss")]
        public IList<string> Iss { get; set; }

        /// <summary>
        /// Sub
        /// </summary>
        [JsonProperty("sub")]
        public IList<string> Sub { get; set; }

        /// <summary>
        /// AUD
        /// </summary>
        [JsonProperty("aud")]
        public IList<string> Aud { get; set; }

        /// <summary>
        /// Nonce
        /// </summary>
        [JsonProperty("nonce")]
        public IList<string> Nonce { get; set; }

        /// <summary>
        /// EXP
        /// </summary>
        [JsonProperty("exp")]
        public IList<int> Exp { get; set; }

        /// <summary>
        /// IAT
        /// </summary>
        [JsonProperty("iat")]
        public IList<int> Iat { get; set; }

        /// <summary>
        /// At Hash
        /// </summary>
        [JsonProperty("at_hash")]
        public IList<string> AtHash { get; set; }
    }
}
