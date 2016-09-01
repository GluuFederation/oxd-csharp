using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 
using System.Collections;
using CSharp.client;
using CSharp.ResponseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp
{
    class Program
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Main Program to test all Classes
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            try
            {

                ///Registering the new Site
                RegisterSiteTest testing = new RegisterSiteTest();
                RegisterSiteResponse response = testing.RegisterSite(IPAddress.Loopback.ToString(), 8099, "https://www.omsttech.com/wp-login.php?option=oxdOpenId");
                Console.WriteLine(response);

                ///updating the site
                UpdateSiteRegistrationTest updatetest = new UpdateSiteRegistrationTest();
                UpdateSiteResponse re = updatetest.UpdateSiteRegisteration(IPAddress.Loopback.ToString(), 8099);
                Console.WriteLine(re);

                /////Getting auth URL
                //get_authorization_url_test authUrltest = new get_authorization_url_test();
                //String authURL = authUrltest.GetAuthorizationURL("127.0.0.1", 8099);
                //Console.WriteLine(authURL);

                /////Get Token by code
                //get_tokens_by_code_test tokentest = new get_tokens_by_code_test();
                //GetTokensByCodeResponse res = tokentest.GetTokenByCode("127.0.0.1", 8099, "vikas1980", "vikas1980");
                //string accesstoken = res.getAccessToken();
                //Console.WriteLine(accesstoken);

                /////Getting User Info
                //get_user_info_test userinfo = new get_user_info_test();
                //GetUserInfoResponse userInfores = userinfo.GetUserInfo("127.0.0.1", 8099, accesstoken);
                //dynamic tes = userInfores.getClaims();
                //Console.WriteLine(userInfores.getClaims());

                /////Getting logout URL
                //get_logout_uri_test logoutURI = new get_logout_uri_test();
                //LogoutResponse logrep = logoutURI.GetLogoutURL("127.0.0.1", 8099);
                //Console.WriteLine(logrep);

                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.Debug(ex.Message);
            }
        }
    }
}
