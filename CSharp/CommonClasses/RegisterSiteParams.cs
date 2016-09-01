using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSharp.CommonClasses
{
    /// <summary>
    /// Register Site Commad Params
    /// </summary>
    class RegisterSiteParams
    {
        /// <summary>
        /// Authorization Redirect URI
        /// </summary>
        /// <remarks>Required Json Property</remarks>
        [JsonProperty("authorization_redirect_uri", Required = Required.Always)]
        public string AuthorizationRedirectUri { get; set; }

        /// <summary>
        /// OP Host
        /// </summary>
        /// <remarks>Optional Json Property (But if missing, must be present in oxd-default-site-config.json file of OXD Server)</remarks>
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

        /// <summary>
        /// Client ID. It is optional Json property. If specified, it ignores all other parameters and skips new client registration.
        /// It forces to use existing client with this Client ID and Client Secret
        /// </summary>
        /// <remarks>Client Secret is required if this parameter is set</remarks>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Client Secret
        /// </summary>
        /// <remarks>It is requirede Json property only when the Client ID property is set</remarks>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
    }
}