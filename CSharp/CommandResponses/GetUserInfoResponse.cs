using Newtonsoft.Json;
using System.Collections.Generic;

namespace oxdCSharp.CommandResponses
{
    /// <summary>
    /// Response for Get User Info Command
    /// </summary>
    public class GetUserInfoResponse
    {
        /// <summary>
        /// Status of the commad execution
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Get User Info command's response Data
        /// </summary>
        [JsonProperty("data")]
        public GetUserInfoResponseData Data { get; set; }
    }

    /// <summary>
    /// Get User Info Response's data
    /// </summary>
    public class GetUserInfoResponseData
    {
        /// <summary>
        /// User Claims
        /// </summary>
        [JsonProperty("claims")]
        public Dictionary<string, List<string>> UserClaims = new Dictionary<string, List<string>>();

    }


}
