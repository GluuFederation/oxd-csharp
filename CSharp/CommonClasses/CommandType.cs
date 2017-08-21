namespace oxdCSharp.CommonClasses
{


    public static class CommandType
    {

        //OpenID Connect Commands
        public static string register_site = "register_site";
        public static string setup_client = "setup_client";
        public static string update_site_registration = "update_site_registration";
        public static string get_client_token = "get_client_token";
        public static string get_authorization_url = "get_authorization_url";
        public static string get_tokens_by_code = "get_tokens_by_code";
        public static string get_user_info = "get_user_info";
        public static string get_logout_uri = "get_logout_uri";
        public static string get_access_token_by_refresh_token = "get_access_token_by_refresh_token";

        //UMA Commands
        public static string uma_rs_protect = "uma_rs_protect";
        public static string uma_rs_check_access = "uma_rs_check_access";
        public static string uma_rp_get_rpt = "uma_rp_get_rpt";
        public static string uma_rp_get_claims_gathering_url = "uma_rp_get_claims_gathering_url";
        
        public static string uma_rp_authorize_rpt = "uma_rp_authorize_rpt";
        public static string uma_rp_get_gat = "uma_rp_get_gat";        
        public static string get_authorization_code = "get_authorization_code";

        


    }

    public static class RestCommandType
    {
        //OpenID Connect Commands
        public static string register_site = "register-site";
        public static string setup_client = "setup-client";
        public static string update_site_registration = "update-site";
        public static string get_client_token = "get-client-token";
        public static string get_authorization_url = "get-authorization-url";
        public static string get_tokens_by_code = "get-tokens-by-code";
        public static string get_user_info = "get-user-info";
        public static string get_logout_uri = "get-logout-uri";

        // UMA Commands
        public static string uma_rs_protect = "uma-rs-protect";
        public static string uma_rs_check_access = "uma-rs-check-access";
        public static string uma_rp_get_rpt = "uma-rp-get-rpt";
        public static string uma_rp_get_claims_gathering_url = "uma-rp-get-claims-gathering-url";

    }

   
    
}
