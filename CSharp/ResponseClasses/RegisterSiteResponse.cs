using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.ResponseClasses
{
    public class RegisterSiteResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public RegisterSiteResponseData Data { get; set; }  
    }

    public class RegisterSiteResponseData
    {
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }
    }
}
