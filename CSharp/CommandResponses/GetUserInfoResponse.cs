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
    /// User claims list
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

        /// <summary>
        /// Birthdate of the user
        /// </summary>
        [JsonProperty("birthdate")]
        public IList<string> Birthdate { get; set; }

        /// <summary>
        /// Country where the user resides
        /// </summary>
        [JsonProperty("country")]
        public IList<string> Country { get; set; }

        /// <summary>
        /// True if the User's e-mail address has been verified; otherwise false
        /// </summary>
        [JsonProperty("email_verified")]
        public IList<string> EmailVerified { get; set; }


        /// <summary>
        /// Gender of the person, either male or female
        /// </summary>
        [JsonProperty("gender")]
        public IList<string> Gender { get; set; }

        /// <summary>
        /// IMAP Data
        /// </summary>
        [JsonProperty("imap_data")]
        public IList<string> IMAPData { get; set; }


        /// <summary>
        /// iname
        /// </summary>
        [JsonProperty("iname")]
        public IList<string> Iname { get; set; }

        /// <summary>
        /// XRI i-number, persistent non-reassignable identifier
        /// </summary>
        [JsonProperty("inum")]
        public IList<string> Inum { get; set; }

        /// <summary>
        /// Time when the User's information was last updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public IList<string> LastUpdated { get; set; }

        /// <summary>
        ///  End-User's locale, represented as a BCP47 (RFC5646) language tag.
        /// </summary>
        [JsonProperty("locale")]
        public IList<string> Locale { get; set; }

        /// <summary>
        /// Middle name(s) of the User.
        /// </summary>
        [JsonProperty("middle_name")]
        public IList<string> MiddleName { get; set; }

        /// <summary>
        ///  Casual name of the User that may or may not be the same as the given_name.
        /// </summary>
        [JsonProperty("nickname")]
        public IList<string> Nickname { get; set; }

        /// <summary>
        /// Organization name of the user
        /// </summary>
        [JsonProperty("o")]
        public IList<string> Organization { get; set; }

        /// <summary>
        /// True if the End-User's phone number has been verified; otherwise false
        /// </summary>
        [JsonProperty("phone_number_verified")]
        public IList<string> PhoneNumberVerified { get; set; }

        /// <summary>
        /// URL of the End-User's profile page. The contents of this Web page SHOULD be about the End-User.
        /// </summary>
        [JsonProperty("profile")]
        public IList<string> Profile { get; set; }

        /// <summary>
        /// String from time zone database representing the User's time zone. For example,Europe/Paris or America/Los_Angeles.
        /// </summary>
        [JsonProperty("zoneinfo")]
        public IList<string> ZoneInfo { get; set; }

        /// <summary>
        /// gluuPermission
        /// </summary>
        [JsonProperty("permission")]
        public IList<string> UserPermission { get; set; }

        /// <summary>
        /// A domain issued and managed identifier for the person.Subject - Identifier for the User at the Issuer.
        /// </summary>
        [JsonProperty("user_name")]
        public IList<string> UserName { get; set; }

        /// <summary>
        /// URL of the User's Web page or blog.
        /// </summary>
        [JsonProperty("website")]
        public IList<string> Website { get; set; }
    }


}
