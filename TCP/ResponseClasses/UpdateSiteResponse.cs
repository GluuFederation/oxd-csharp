using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.ResponseClasses
{
    class UpdateSiteResponse
    {
        [JsonProperty("oxd_id")]
        private String _oxdId;

        public UpdateSiteResponse(dynamic obj)
        {
            this._oxdId = obj.oxd_id;
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
