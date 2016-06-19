using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.CommonClasses
{
    /// <summary>
    /// Getting and Setting Authorization Code Params
    /// </summary>
    class GetAuthorizationCodeParams
    {
        [JsonProperty("oxd_id")]
        public string _oxd_id;

        [JsonProperty("username")]
        private string _username;

        [JsonProperty("password")] 
        private string _password;

        [JsonProperty("acr_values")]
        private List<string> _setAcrValues;
 
        public void SetOxdId(string val)
        {
            this._oxd_id = val;
        }

        public void SetAcrValues(List<string> val)
        {
            this._setAcrValues = val;
        }

        public void SetUserName(string val)
        {
            this._username = val;
        }

        public void SetPassword(string val)
        {
            this._password = val;
        }

        public string GetOxdId()
        {
            return _oxd_id;
        }

        public List<string> GetAcrValues()
        {
            return _setAcrValues;
        }

        public string GetUserName()
        {
            return _username;
        }

        public string SetPassword()
        {
            return _password;
        }
    }
}
