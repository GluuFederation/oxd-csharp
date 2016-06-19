using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.CommonClasses
{
    /// <summary>
    /// Getting and Setting Register Site Params
    /// </summary>
    class RegisterSiteParams
    {
        [JsonIgnore]
        [JsonProperty("op_host")]
        public string _op_host { get; set; }
        [JsonProperty("authorization_redirect_uri")]
        private string _setAuthorizationRedirectUri { get; set; }
         [JsonProperty("post_logout_redirect_uri")]
        private string _setPostLogoutRedirectUri { get; set; }
         [JsonProperty("client_logout_uris")]
         private List<string> _setClientLogoutUri { get; set; }
         [JsonProperty("application_type")]
         private string _setApplicationType { get; set; }
         [JsonProperty("redirect_uris")]
         private List<string> _setRedirectUris { get; set; }
         [JsonProperty("acr_values")]
         private List<string> _setAcrValues { get; set; }
         [JsonProperty("scope")]
         private List<string> _setScope { get; set; }
         [JsonProperty("grant_types")]
         private List<string> _setGrantType { get; set; }
         [JsonProperty("response_types")]
         private List<string> _setResponseTypes { get; set; }
         [JsonProperty("contacts")]
         private List<string> _setContacts { get; set; }

         public void Op_host(string val)
         {
             this._op_host = val;
         }
        public void SetAuthorizationRedirectUri(string val)
        {
            this._setAuthorizationRedirectUri = val;
        }
        public void SetPostLogoutRedirectUri(string val)
        {
            this._setPostLogoutRedirectUri = val;
        }
        public void SetClientLogoutUri(List<string> val)
        {
            this._setClientLogoutUri = val;
        }
        public void SetApplicationType(string val)
        {
            this._setApplicationType = val;
        }
        public void SetRedirectUris(List<string> val)
        {
            this._setRedirectUris = val;
        }
        public void SetAcrValues(List<string> val)
        {
            this._setAcrValues = val;
        }
        public void SetScope(List<string> val)
        {
            this._setScope = val;
        }
        public void SetGrantType(List<string> val)
        {
            this._setGrantType = val;
        }
        public void SetResponseTypes(List<string> val)
        {
            this._setResponseTypes = val;
        }

        public void SetContacts(List<string> val)
        {
            this._setContacts = val;
        }

        //public override string ToString()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("RegisterSiteParams");
        //    sb.Append("{acrValues=").Append(_setAcrValues);
        //    //sb.Append(", opHost='").Append(_op_host).Append('\'');
        //    sb.Append(", authorizationRedirectUri='").Append(_setAuthorizationRedirectUri).Append('\'');
        //    sb.Append(", applicationType='").Append(_setApplicationType).Append('\'');
        //    sb.Append(", redirectUris=").Append(_setRedirectUris);
        //    sb.Append(", responseTypes=").Append(_setResponseTypes);
        //    //sb.append(", clientId='").append(clientId).append('\'');
        //    //sb.append(", clientSecret='").append(clientSecret).append('\'');
        //   // sb.append(", sectorIdentifierUri='").append(clientSectorIdentifierUri).append('\'');
        //    sb.Append(", scope=").Append(_setScope);
        //    //sb.append(", uiLocales=").append(uiLocales);
        //    //sb.append(", claimsLocales=").append(claimsLocales);
        //    sb.Append(", grantType=").Append(_setGrantType);
        //    sb.Append(", contacts=").Append(_setContacts);
        //    sb.Append('}');
        //    return base.ToString();
        //}

    }
}
