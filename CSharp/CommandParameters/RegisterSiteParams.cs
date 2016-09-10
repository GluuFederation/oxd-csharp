using Newtonsoft.Json;
using System.Collections.Generic;

namespace oxdCSharp.CommandParameters
{
    public class RegisterSiteParams
    {
        [JsonProperty("authorization_redirect_uri")]
        public string AuthorizationRedirectUri { get; set; }

        [JsonProperty("op_host")]
        public string OpHost { get; set; }

        [JsonProperty("post_logout_redirect_uri")]
        public string PostLogoutRedirectUri { get; set; }

        [JsonProperty("application_type")]
        public string ApplicationType { get; set; }

        [JsonProperty("response_types")]
        public IList<string> ResponseTypes { get; set; }

        [JsonProperty("grant_types")]
        public IList<string> GrantTypes { get; set; }

        [JsonProperty("scope")]
        public IList<string> Scope { get; set; }

        [JsonProperty("acr_values")]
        public IList<string> AcrValues { get; set; }

        [JsonProperty("client_name")]
        public string ClientName { get; set; }

        [JsonProperty("client_jwks_uri")]
        public string ClientJwksUri { get; set; }

        [JsonProperty("client_token_endpoint_auth_method")]
        public string ClientTokenEndpointAuthMethod { get; set; }

        [JsonProperty("client_request_uris")]
        public IList<string> ClientRequestUris { get; set; }

        [JsonProperty("client_logout_uris")]
        public IList<string> ClientLogoutUris { get; set; }

        [JsonProperty("client_sector_identifier_uri")]
        public IList<string> ClientSectorIdentifierUri { get; set; }

        [JsonProperty("contacts")]
        public IList<string> Contacts { get; set; }

        [JsonProperty("ui_locales")]
        public IList<string> UiLocales { get; set; }

        [JsonProperty("claims_locales")]
        public IList<string> ClaimsLocales { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
    }
}