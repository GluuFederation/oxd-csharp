using Newtonsoft.Json;
using System.Collections.Generic;

namespace oxdCSharp.CommandParameters
{
    /// <summary>
    /// Params for Update Site Command
    /// </summary>
    public class UpdateSiteParams
    {
        /// <summary>
        /// Registered OXD Id.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        /// <summary>
        /// Authorization Redirect URI
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("authorization_redirect_uri")]
        public string AuthorizationRedirectUri { get; set; }

        /// <summary>
        /// Post Logout Redirect Uri
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("post_logout_redirect_uri")]
        public string PostLogoutRedirectUri { get; set; }

        /// <summary>
        /// Client Logout URIs
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("client_logout_uris")]
        public IList<string> ClientLogoutUris { get; set; }

        /// <summary>
        /// Response Type
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("response_type")]
        public IList<string> ResponseType { get; set; }

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
        /// Client Secret Expires At. It can be used to extend client lifetime (milliseconds since 1970).
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("client_secret_expires_at")]
        public long ClientSecretExpiresAt { get; set; }

        /// <summary>
        /// Client JWKS URI
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
        /// Client Sector Identifier URI
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("client_sector_identifier_uri")]
        public string ClientSectorIdentifierUri { get; set; }

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
    }
}
