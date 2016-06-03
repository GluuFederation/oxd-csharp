using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Classes
{
    class UpdateSiteParams
    {
        [JsonProperty("oxd_id")]
        private string _oxd_id { get; set; }

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

        [JsonProperty("client_token_endpoint_auth_method")]
        private string _client_token_endpoint_auth_method { get; set; }

        [JsonProperty("grant_types")]
        private List<string> _setGrantType { get; set; }

        [JsonProperty("client_request_uris")]
        private List<string> _client_request_uris { get; set; }

        [JsonProperty("contacts")]
        private List<string> _setContacts { get; set; }

        [JsonProperty("client_jwks_uri")]
        private string _client_jwks_uri { get; set; }

        public void SetOxdId(string val)
        {
            this._oxd_id = val;
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
        public void SetClientJwksUri(string val)
        {
            this._client_jwks_uri = val;
        }
        public void SetGrantType(List<string> val)
        {
            this._setGrantType = val;
        }
        public void SetClientTokenEndpointAuthMethod(string val)
        {
            this._client_token_endpoint_auth_method = val;
        }

        public void SetContacts(List<string> val)
        {
            this._setContacts = val;
        }
    }
}
