using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GluuDemoWebsite.Models
{
    public class RegistrationType
    {


        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("issuer")]
        public string issuer { get; set; }

        [JsonProperty("authorization_endpoint")]
        public string authorization_endpoint { get; set; }

        [JsonProperty("token_endpoint")]
        public string token_endpoint { get; set; }

        [JsonProperty("userinfo_endpoint")]
        public string userinfo_endpoint { get; set; }

        [JsonProperty("clientinfo_endpoint")]
        public string clientinfo_endpoint { get; set; }
        [JsonProperty("check_session_iframe")]
        public string check_session_iframe { get; set; }

        /// <summary>
        /// Register Site command's response Data
        /// </summary>
        [JsonProperty("registration_endpoint")]
        public string registration_endpoint { get; set; }
    }
}