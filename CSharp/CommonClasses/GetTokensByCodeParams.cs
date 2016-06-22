using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.CommonClasses
{
    /// <summary>
    /// Getting and Setting Tokens By Code Params
    /// </summary>
    class GetTokensByCodeParams
    {
        [JsonProperty("oxd_id")]
        private string _oxd_id { get; set; }
        [JsonProperty("code")]
        private string _code { get; set; }
        [JsonProperty("state")]
        private string _state { get; set; }

        [JsonIgnore]
        [JsonProperty("location")]
        private string _location { get; set; }
        [JsonProperty("scopes")]
        private List<string> _scopes { get; set; }

        public void SetOxdId(string val)
        {
            this._oxd_id = val;
        }
        public void SetCode(string val)
        {
            this._code = val;
        }
        public void SetState(string val)
        {
            this._state = val;
        }
        public void SetLocation(string val)
        {
            this._location = val;
        }
        public void SetScopes(List<string> val)
        {
            this._scopes = val;
        }
    }
}
