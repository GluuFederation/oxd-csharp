using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.ResponseClasses
{
    /// <summary>
    /// Setting up response for Authorization Code
    /// </summary>
    class GetAuthorizationCodeResponse
    {
        [JsonProperty("code")]
        private String _code;

        public GetAuthorizationCodeResponse()
        {
        }

        public GetAuthorizationCodeResponse(dynamic obj)
        {
            this._code = obj.code;
        }

        public String getCode()
        {
            return this._code;
        }

        public void setCode(dynamic code)
        {
            this._code = code;
        }
    }
}
