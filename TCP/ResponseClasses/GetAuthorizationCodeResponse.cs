using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.ResponseClasses
{
    class GetAuthorizationCodeResponse
    {
        [JsonProperty("code")]
        private dynamic _code;

        public GetAuthorizationCodeResponse()
        {
        }

        public GetAuthorizationCodeResponse(dynamic obj)
        {
            this._code = obj.code;
        }

        public dynamic getCode()
        {
            return _code;
        }

        public void setCode(dynamic code)
        {
            this._code = code;
        }
    }
}
