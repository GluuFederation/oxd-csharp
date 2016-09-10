using Newtonsoft.Json;
using System.Collections.Generic;

namespace oxdCSharp.CommandResponses
{
    public class GetUserInfoResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public GetUserInfoResponseData Data { get; set; }
    }

    public class GetUserInfoResponseData
    {
        [JsonProperty("claims")]
        public GetUserInfoUserClaims UserClaims { get; set; }
    }

    public class GetUserInfoUserClaims
    {
        [JsonProperty("sub")]
        public IList<string> Sub { get; set; }

        [JsonProperty("name")]
        public IList<string> Name { get; set; }

        [JsonProperty("given_name")]
        public IList<string> GivenName { get; set; }

        [JsonProperty("family_name")]
        public IList<string> FamilyName { get; set; }

        [JsonProperty("preferred_username")]
        public IList<string> PreferredUsername { get; set; }

        [JsonProperty("email")]
        public IList<string> Email { get; set; }

        [JsonProperty("picture")]
        public IList<string> Picture { get; set; }
    }
}
