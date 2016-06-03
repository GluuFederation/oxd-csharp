using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.ResponseClasses
{
    class RegisterSiteResponse
    {

        [JsonProperty("oxd_id")]
        private dynamic _oxd_Id;

        public RegisterSiteResponse(dynamic obj)
        {
            this._oxd_Id = obj.oxd_id;
        }

        public String getOxdId()
        {
            return _oxd_Id;
        }

        public void setOxdId(dynamic oxdId)
        {
            this._oxd_Id = oxdId;
        }
    }
}
