using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.ResponseClasses
{
    /// <summary>
    /// Setting up Authorization Url
    /// </summary>
    class GetAuthorizationUrlResponse
    {
        [JsonProperty("authorization_url")]
        private String _authorizationUrl;

        public GetAuthorizationUrlResponse()
        {
        }

        public GetAuthorizationUrlResponse(dynamic obj)
        {
            this._authorizationUrl = obj.authorization_url;
        }

        public String getAuthorizationUrl()
        {
            return this._authorizationUrl;
        }

        public void setAuthorizationUrl(dynamic authorizationUrl)
        {
            this._authorizationUrl = authorizationUrl;
        }
    }
}
