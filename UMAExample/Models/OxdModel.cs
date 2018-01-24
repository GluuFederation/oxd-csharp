using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMATestApi.Models
{
    public class OxdModel
    {
        [JsonProperty("oxd_host")]
        public string OxdHost { get; set; }

        [JsonProperty("oxd_host_port")]
        public int OxdPort { get; set; }

        [JsonProperty("redirectUrl")]
        public string RedirectUrl { get; set; }

        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        [JsonProperty("post_logout_redirect_uri")]
        public string PostLogoutRedirectUrl { get; set; }

        [JsonProperty("oxdEmail")]
        public string OxdEmail { get; set; }

        [JsonProperty("application_type")]
        public string ApplicationType { get; set; }

        [JsonProperty("response_types")]
        public IList<string> Responsetypes { get; set; }

        [JsonProperty("grant_types")]
        public IList<string> GrantTypes { get; set; }

        [JsonProperty("scope")]
        public IList<string> Scope { get; set; }

        [JsonProperty("acr_values")]
        public IList<string> AcrValues { get; set; }

        [JsonProperty("authorization_redirect_uri")]
        public string AuthUrl { get; set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("ophost")]
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