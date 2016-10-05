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
        public GetUserInfoUserClaims UserClaims { get; set; }
    }

    /// <summary>
    /// User Claims returned by Get User Info command
    /// </summary>
    public class GetUserInfoUserClaims
    {
        /// <summary>
        /// Sub
        /// </summary>
        [JsonProperty("sub")]
        public IList<string> Sub { get; set; }

        /// <summary>
        /// Name of the user
        /// </summary>
        [JsonProperty("name")]
        public IList<string> Name { get; set; }

        /// <summary>
        /// Given Name of the user
        /// </summary>
        [JsonProperty("given_name")]
        public IList<string> GivenName { get; set; }

        /// <summary>
        /// Family Name of the user
        /// </summary>
        [JsonProperty("family_name")]
        public IList<string> FamilyName { get; set; }

        /// <summary>
        /// Preferred User Name of the user
        /// </summary>
        [JsonProperty("preferred_username")]
        public IList<string> PreferredUsername { get; set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        [JsonProperty("email")]
        public IList<string> Email { get; set; }

        /// <summary>
        /// Picture of the user
        /// </summary>
        [JsonProperty("picture")]
        public IList<string> Picture { get; set; }
    }
}
