using Newtonsoft.Json;
using System.Collections.Generic;

namespace oxdCSharp.CommandParameters
{
    /// <summary>
    /// Params for Register Site Command
    /// </summary>
    public class RegisterSiteParams
    {
        /// <summary>
        /// Uri to Redirect for Authorization
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field</remarks>
        [JsonProperty("authorization_redirect_uri")]
        public string AuthorizationRedirectUri { get; set; }

        /// <summary>
        /// Url that must points to a valid OpenID Connect Provider that supports client registration like Gluu Server.
        /// Ex. https://idp.example.org
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field. If missing, must be present in <b>oxd-default-site-config.json</b> file in Oxd Server Configs.</remarks>
        [JsonProperty("op_host")]
        public string OpHost { get; set; }

        /// <summary>
        /// Post Logout Redirect URI
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("post_logout_redirect_uri")]
        public string PostLogoutRedirectUri { get; set; }

        /// <summary>
        /// Application Type
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("application_type")]
        public string ApplicationType { get; set; }

        /// <summary>
        /// Response Types
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("response_types")]
        public IList<string> ResponseTypes { get; set; }

        /// <summary>
        /// Grant Types
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("grant_types")]
        public IList<string> GrantTypes { get; set; }

        /// <summary>
        /// Scope
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("scope")]
        public IList<string> Scope { get; set; }

        /// <summary>
        /// ACR Values
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("acr_values")]
        public IList<string> AcrValues { get; set; }

        /// <summary>
        /// Client Name
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("client_name")]
        public string ClientName { get; set; }

        /// <summary>
        /// Client JWKS Uri
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("client_jwks_uri")]
        public string ClientJwksUri { get; set; }

        /// <summary>
        /// Client Token Endpoint Auth Method
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("client_token_endpoint_auth_method")]
        public string ClientTokenEndpointAuthMethod { get; set; }

        /// <summary>
        /// Client Request URIs
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("client_request_uris")]
        public IList<string> ClientRequestUris { get; set; }

        /// <summary>
        /// Client Front Channel Logout URIs
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("client_frontchannel_logout_uris")]
        public IList<string> ClientFrontChannelLogoutUris { get; set; }

        /// <summary>
        /// Client Sector Identifier URIs
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("client_sector_identifier_uri")]
        public IList<string> ClientSectorIdentifierUri { get; set; }

        /// <summary>
        /// Contacts
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("contacts")]
        public IList<string> Contacts { get; set; }

        /// <summary>
        /// UI Locales
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("ui_locales")]
        public IList<string> UiLocales { get; set; }

        /// <summary>
        /// Claims Locales
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("claims_locales")]
        public IList<string> ClaimsLocales { get; set; }

        /// <summary>
        /// Client ID.
        /// If value presents, Ignores all other parameters and Skips new client registration forcing to use existing client.
        /// <b>ClientSecret</b> is REQUIRED if this parameter is set
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Client Secret.
        /// Must be used together with ClientId.
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>
        /// oxd_rp_programming_language.
        /// Programming language: For example Csharp
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("oxd_rp_programming_language")]
        internal string ProgrammingLanguage { get; set; }


        /// <summary>
        /// Claims Redirect URI.
        /// Must be used together with ClientId.
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("claims_redirect_uri")]
        public string ClaimsRedirecturi { get; set; }

    }
}