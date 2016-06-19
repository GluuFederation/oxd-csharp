using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.ResponseClasses
{
    /// <summary>
    /// Setting up OXD from Site Update responce
    /// </summary>
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
            return this._oxdId;
        }

        public void setOxdId(String oxdId)
        {
            this._oxdId = oxdId;
        }
    }
}
