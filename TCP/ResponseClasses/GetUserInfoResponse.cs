using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.ResponseClasses
{
    /// <summary>
    /// Setting up userInfo response
    /// </summary>
    class GetUserInfoResponse
    {

        [JsonProperty("claims")]
        private dynamic _claims;

        public GetUserInfoResponse()
        {
        }

        public GetUserInfoResponse(dynamic obj)
        {
            this._claims = obj.claims;
        }

        public dynamic getClaims()
        {
            return _claims;
        }

        public void setClaims(dynamic claims)
        {
            this._claims = claims;
        }

    }
}
