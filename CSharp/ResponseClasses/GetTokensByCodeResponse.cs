using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.ResponseClasses
{
    public class IdTokenClaims
    {
        [JsonProperty("iss")]
        public IList<string> Iss { get; set; }

        [JsonProperty("sub")]
        public IList<string> Sub { get; set; }

        [JsonProperty("aud")]
        public IList<string> Aud { get; set; }

        [JsonProperty("nonce")]
        public IList<string> Nonce { get; set; }

        [JsonProperty("exp")]
        public IList<int> Exp { get; set; }

        [JsonProperty("iat")]
        public IList<int> Iat { get; set; }

        [JsonProperty("at_hash")]
        public IList<string> AtHash { get; set; }
    }

    public class GetTokensByCodeResponseData
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("id_token_claims")]
        public IdTokenClaims IdTokenClaims { get; set; }
    }

    public class GetTokensByCodeResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public GetTokensByCodeResponseData Data { get; set; }
    }
}
