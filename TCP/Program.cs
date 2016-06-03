using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TCP.Classes;
using System.Collections;
using TCP.client;
using TCP.ResponseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting; 

namespace TCP
{
    class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {

            try
            {
                
                
                //register_site_test testing = new register_site_test();
                //RegisterSiteResponse response = testing.register("127.0.0.1", 8099, "https://www.omsttech.com/wp-login.php?option=oxdOpenId");

                update_site_registration test = new update_site_registration();
                UpdateSiteResponse re = test.UpdateSiteRegisteration("127.0.0.1", 8099, "1c80f252-85a8-4280-b344-44786aba50f5");

                //get_authorization_url test = new get_authorization_url();
                //test.GetAuthorizationURL("127.0.0.1", "8099", "e90809a3-85a6-409e-b836-f59487a36965");

                //get_authorization_code test = new get_authorization_code();
                //test.GetAuthorizationCode("127.0.0.1", "8099", "e90809a3-85a6-409e-b836-f59487a36965", "vikas1980", "vikas1980");

                //get_tokens_by_code test = new get_tokens_by_code();
                //GetTokensByCodeResponse res = test.GetTokenByCode("127.0.0.1", 8099, "1c80f252-85a8-4280-b344-44786aba50f5", "vikas1980", "vikas1980");
                //string accesstoken = res.getAccessToken();

                //get_user_info userinfo = new get_user_info();
                //GetUserInfoResponse userInfores = userinfo.GetUserInfo("127.0.0.1", 8099, "1c80f252-85a8-4280-b344-44786aba50f5", accesstoken);
                //dynamic tes = userInfores.getClaims();

                //get_logout_uri logoutURI = new get_logout_uri();
                //LogoutResponse logrep = logoutURI.GetLogoutURL("127.0.0.1", 8099, "1c80f252-85a8-4280-b344-44786aba50f5");

            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);   
            }
        }
    }
}
