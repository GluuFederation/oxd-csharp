using Newtonsoft.Json;
using System.Collections.Generic;

namespace oxdCSharp.CommandParameters
{
    /// <summary>
    /// Params for Get GAT Command
    /// </summary>
    public class GetGATParams
    {
        /// <summary>
        /// Registered OXD Id.
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("oxd_id")]
        public string OxdId { get; set; }

        /// <summary>
        /// Scopes
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field. RP should know required scopes in advance</remarks>
        [JsonProperty("scopes")]
        public IList<string> Scopes { get; set; }
    }
}
