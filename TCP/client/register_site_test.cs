using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.Classes;
using TCP.CommonClasses;
using TCP.ResponseClasses;

namespace TCP.client
{
    class register_site_test
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Register site using regular params
        /// </summary>
        /// <param name="host">Host address to connect</param>
        /// <param name="port">Port number</param>
        /// <param name="redirectUrl"></param>
        /// <param name="postLogoutRedirectUrl"></param>
        /// <param name="logoutUrl"></param>
        /// <returns></returns>
        public RegisterSiteResponse RegisterSite(String host, int port, String redirectUrl, String postLogoutRedirectUrl, String logoutUrl)
        {
            try
            { 
                CommandClient client = new CommandClient(host, port);
                RegisterSiteParams param = new RegisterSiteParams();
                param.SetAuthorizationRedirectUri("https://www.omsttech.com/wp-login.php?option=oxdOpenId");
                param.SetPostLogoutRedirectUri("https://www.omsttech.com/wp-login.php?action=logout&_wpnonce=a3c70643e9");
                param.SetApplicationType("web");
                param.SetRedirectUris(Lists.newArrayList(new string[] { "https://www.omsttech.com/wp-login.php?option=oxdOpenId" }));
                param.SetAcrValues(new List<string>());
                param.SetScope(Lists.newArrayList(new string[] { "openid", "profile", "email", "address", "clientinfo", "mobile_phone", "phone" }));
                param.SetContacts(Lists.newArrayList(new string[] { "hellochopra1@gmail.com" }));
                param.SetGrantType(Lists.newArrayList(new string[] { "authorization_code" }));
                param.SetResponseTypes(Lists.newArrayList(new string[] { "code" }));
                param.SetClientLogoutUri(Lists.newArrayList(new string[] { "https://www.omsttech.com/wp-login.php?action=logout&_wpnonce=a3c70643e9" }));

                //param.Op_host("https://ce-dev2.gluu.org");

                Command cmd = new Command(CommandType.register_site);
                cmd.setParamsObject(param);

                string commandresponse = client.send(cmd);
                RegisterSiteResponse response = new RegisterSiteResponse(JsonConvert.DeserializeObject<dynamic>(commandresponse).data);
                Assert.IsNotNull(response);
                Assert.IsTrue(!String.IsNullOrEmpty(response.getOxdId()));
                StoredValues._oxd_id = response.getOxdId();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.Debug(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Register site with minimal params
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public RegisterSiteResponse RegisterSite(string host, int port, string redirectUrl)
        {
            try
            {
                CommandClient client = new CommandClient(host, port);
                RegisterSiteParams param = new RegisterSiteParams();
                param.SetAuthorizationRedirectUri(redirectUrl);
                param.SetPostLogoutRedirectUri(redirectUrl);
                param.SetClientLogoutUri(Lists.newArrayList(new string[] { "" }));

                Command cmd = new Command(CommandType.register_site);
                cmd.setParamsObject(param);

                string commandresponse = client.send(cmd);
                RegisterSiteResponse response = new RegisterSiteResponse(JsonConvert.DeserializeObject<dynamic>(commandresponse).data);
                Assert.IsNotNull(response);
                Assert.IsTrue(!String.IsNullOrEmpty(response.getOxdId()));
                StoredValues._oxd_id = response.getOxdId();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.Debug(ex.Message);
                return null;
            }
        }

    }
}
