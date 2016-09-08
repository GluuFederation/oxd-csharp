using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.ResponseClasses
{
    public class UserClaims
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

    public class GetUserInfoResponseData
    {
        [JsonProperty("claims")]
        public UserClaims UserClaims { get; set; }
    }

    public class GetUserInfoResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public GetUserInfoResponseData Data { get; set; }
    }
}
