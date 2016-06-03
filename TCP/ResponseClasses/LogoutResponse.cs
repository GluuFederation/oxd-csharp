using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.ResponseClasses
{
    class LogoutResponse
    {
        [JsonProperty("uri")]
        private String _uri;

        public LogoutResponse()
        {
        }

        public LogoutResponse(dynamic obj)
        {
            this._uri = obj.uri;
        }

        public String getUri()
        {
            return _uri;
        }

        public void setUri(String uri)
        {
            this._uri = uri;
        }
    }
}
