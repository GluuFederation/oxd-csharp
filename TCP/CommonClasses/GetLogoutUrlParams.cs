using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Classes
{
    class GetLogoutUrlParams
    {

        [JsonProperty("oxd_id")]
        private String _oxdId;


        [JsonProperty("id_token_hint")]
        private String _idTokenHint;

        [JsonProperty("post_logout_redirect_uri")]
        private String _postLogoutRedirectUri;

        [JsonProperty("state")]
        private String _state;

        [JsonProperty("session_state")]
        private String _sessionState;

        public GetLogoutUrlParams()
        {
        }

        public String getPostLogoutRedirectUri()
        {
            return _postLogoutRedirectUri;
        }

        public void setPostLogoutRedirectUri(String postLogoutRedirectUri)
        {
            this._postLogoutRedirectUri = postLogoutRedirectUri;
        }

        public String getIdTokenHint()
        {
            return _idTokenHint;
        }

        public void setIdTokenHint(String idTokenHint)
        {
            this._idTokenHint = idTokenHint;
        }

        public String getOxdId()
        {
            return _oxdId;
        }

        public void setOxdId(String oxdId)
        {
            this._oxdId = oxdId;
        }

        public String getState()
        {
            return _state;
        }

        public void setState(String state)
        {
            this._state = state;
        }

        public String getSessionState()
        {
            return _sessionState;
        }

        public void setSessionState(String sessionState)
        {
            this._sessionState = sessionState;
        }

    }
}
