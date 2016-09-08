using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.ResponseClasses
{
    public class LogoutResponseData
    {
        [JsonProperty("uri")]
        public string LogoutUri { get; set; }
    }

    public class LogoutResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public LogoutResponseData Data { get; set; }
    }
}
