using Newtonsoft.Json;
using System;

namespace oxdCSharp.Responses
{
    [Obsolete("Not used. Get the Authorization Code using redirect URL from Gluu Server")]
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
