using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Classes
{
    class GetUserInfoParams
    {
        [JsonProperty("oxd_id")]
        private String _oxdId;

        [JsonProperty("access_token")]
        private String _accessToken;

        public GetUserInfoParams()
        {
        }

        public String getAccessToken()
        {
            return _accessToken;
        }

        public void setAccessToken(String accessToken)
        {
            this._accessToken = accessToken;
        }

        public String getOxdId()
        {
            return _oxdId;
        }

        public void setOxdId(String oxdId)
        {
            this._oxdId = oxdId;
        }
    }
}
