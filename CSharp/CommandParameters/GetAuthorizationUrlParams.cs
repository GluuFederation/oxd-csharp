using Newtonsoft.Json;
using System.Collections.Generic;

namespace oxdCSharp.CommandParameters
{
    public class GetAuthorizationUrlParams
    {
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        [JsonProperty("scope")]
        public IList<string> Scope { get; set; }

        [JsonProperty("acr_values")]
        public IList<string> AcrValues { get; set; }

        [JsonProperty("prompt")]
        public string Prompt { get; set; }
    }
}
