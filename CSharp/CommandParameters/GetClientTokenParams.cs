using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oxdCSharp.CommandParameters
{

    /// <summary>
    /// Params for Protection access token
    /// </summary>
   public class GetClientTokenParams
    {
        /// <summary>
        /// ClientID from OP Host provider
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("client_id")]
        public string clientId { get; set; }

        /// <summary>
        /// Client Secret from OP Host provider
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("client_secret")]
        public string clientSecret { get; set; }

        /// <summary>
        /// Op Host Provider url
        /// </summary>
        /// <remarks><b>REQUIRED</b> Field.</remarks>
        [JsonProperty("op_host")]
        public string opHost { get; set; }

        /// <summary>
        /// Path to discovery document
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("op_discovery_path")]
        public string opDiscoveryPath { get; set; }

        /// <summary>
        /// Scopes
        /// </summary>
        /// <remarks><b>OPTIONAL</b> Field.</remarks>
        [JsonProperty("scope")]
        public List<string> scope { get; set; }
    }
}
