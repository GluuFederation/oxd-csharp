using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.CommonClasses
{
    /// <summary>
    /// Getting and Setting Authorization Url Params
    /// </summary>
    class GetAuthorizationUrlParams
    {
        [JsonProperty("oxd_id")]
        private string _oxd_id { get; set; }

        [JsonProperty("acr_values")]
        private List<string> _setAcrValues { get; set; }

        [JsonIgnore]
        [JsonProperty("authorization_url")]
        private List<string> _authorization_url { get; set; }

        

        public void SetOxdId(string val)
        {
            this._oxd_id = val;
        }

        public void SetAcrValues(List<string> val)
        {
            this._setAcrValues = val;
        }
    }
}
