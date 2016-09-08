using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.ResponseClasses
{
    public class GetAuthorizationUrlResponseData
    {
        [JsonProperty("authorization_url")]
        public string AuthorizationUrl { get; set; }
    }

    public class GetAuthorizationUrlResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public GetAuthorizationUrlResponseData Data { get; set; }
    }
}
