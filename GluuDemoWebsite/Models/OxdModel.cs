using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GluuDemoWebsite.Models
{
    public class OxdModel
    {
        [JsonProperty("oxdHost")]
        public string OxdHost { get; set; }

        [JsonProperty("oxdPort")]
        public int OxdPort { get; set; }

        [JsonProperty("redirectUrl")]
        public string RedirectUrl { get; set; }        

        [JsonProperty("oxdId")]
        public string OxdId { get; set; }

        [JsonProperty("postLogoutRedirectUrl")]
        public string PostLogoutRedirectUrl { get; set; }

        [JsonProperty("oxdEmail")]
        public string OxdEmail { get; set; }

        [JsonProperty("authUrl")]
        public string AuthUrl { get; set; }

        [JsonProperty("authCode")]
        public string AuthCode { get; set; }

        [JsonProperty("authState")]
        public string AuthState { get; set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}