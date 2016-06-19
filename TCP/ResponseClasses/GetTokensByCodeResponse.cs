using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.ResponseClasses
{
    /// <summary>
    /// Setting up Token by Code response
    /// </summary>
    class GetTokensByCodeResponse
    {

        [JsonProperty("access_token")]
        private dynamic _accessToken;

        [JsonProperty("expires_in")]
        private dynamic _expiresIn; // expiration time in seconds

        [JsonProperty("id_token")]
        private dynamic _idToken;

        [JsonProperty("refresh_token")]
        private dynamic _refreshToken;

        [JsonProperty("id_token_claims")]
        private dynamic _idTokenClaims;

        public GetTokensByCodeResponse(dynamic obj)
        {
            this._refreshToken = obj.refresh_token;
            this._accessToken = obj.access_token;
            this._expiresIn = obj.expires_in;
            this._idToken = obj.id_token;
            this._idTokenClaims = obj.id_token_claims; 
        }

        public dynamic getRefreshToken()
        {
            return _refreshToken;
        }

        public void setRefreshToken(dynamic refreshToken)
        {
            this._refreshToken = refreshToken;
        }

        public dynamic getAccessToken()
        {
            return _accessToken;
        }

        public void setAccessToken(dynamic accessToken)
        {
            this._accessToken = accessToken;
        }

        public dynamic getExpiresIn()
        {
            return _expiresIn;
        }

        public void setExpiresIn(dynamic expiresIn)
        {
            this._expiresIn = expiresIn;
        }

        public dynamic getIdToken()
        {
            return _idToken;
        }

        public void setIdToken(dynamic idToken)
        {
            this._idToken = idToken;
        }

        public dynamic getIdTokenClaims()
        {
            return _idTokenClaims;
        }

        public void setIdTokenClaims(dynamic idTokenClaims)
        {
            this._idTokenClaims = idTokenClaims;
        }
    }
}
