using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Classes
{
    /// <summary>
    /// Setting up Command Type to send on Server
    /// </summary>
    public class CommandType
    {
        public static string register_site = "register_site";
        public static string update_site_registration = "update_site_registration";
        public static string get_authorization_url = "get_authorization_url";
        public static string get_tokens_by_code = "get_tokens_by_code";
        public static string get_user_info = "get_user_info";
        public static string get_logout_uri = "get_logout_uri";
        public static string get_authorization_code = "get_authorization_code";
    }
}
