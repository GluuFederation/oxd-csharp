using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMATestApi.Models
{
    public class OxdSetting
    {
        [JsonProperty("oxd_host")]
        public string OxdHost { get; set; }

        [JsonProperty("oxd_host_port")]
        public int OxdPort { get; set; }

        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        [JsonProperty("grant_types")]
        public IList<string> GrantTypes { get; set; }

        [JsonProperty("scope")]
        public IList<string> Scope { get; set; }

        [JsonProperty("acr_values")]
        public string AcrValues { get; set; }

        [JsonProperty("authorization_redirect_uri")]
        public string AuthUrl { get; set; }


        [JsonProperty("op_host")]
        public string OpHost { get; set; }

        [JsonProperty("connection_type")]
        public string ConnectionType { get; set; }

        [JsonProperty("client_name")]
        public string ClientName { get; set; }

        [JsonProperty("http_rest_url")]
        public string HttpRestUrl { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
        
    }
}