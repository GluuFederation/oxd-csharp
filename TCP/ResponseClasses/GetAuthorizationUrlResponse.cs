using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.ResponseClasses
{
    class GetAuthorizationUrlResponse
    {
        [JsonProperty("authorization_url")]
        private dynamic _authorizationUrl;

        public GetAuthorizationUrlResponse()
        {
        }

        public GetAuthorizationUrlResponse(dynamic obj)
        {
            this._authorizationUrl = obj.authorization_url;
        }

        public dynamic getAuthorizationUrl()
        {
            return _authorizationUrl;
        }

        public void setAuthorizationUrl(dynamic authorizationUrl)
        {
            this._authorizationUrl = authorizationUrl;
        }
    }
}
