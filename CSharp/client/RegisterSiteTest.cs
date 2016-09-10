using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using CSharp.CommonClasses;
using oxdCSharp.CommandResponses;
using System.Configuration;

namespace CSharp.client
{
    public class RegisterSiteTest
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
                //Prepare Register Site Command params
                RegisterSiteParams registerSiteParam = new RegisterSiteParams();
                registerSiteParam.AuthorizationRedirectUri = ConfigurationManager.AppSettings["AuthorizationRedirectUri"];
                registerSiteParam.OpHost = ConfigurationManager.AppSettings["GluuServerUrl"];                                
                registerSiteParam.PostLogoutRedirectUri = postLogoutRedirectUrl;
                registerSiteParam.ApplicationType = "web";
                registerSiteParam.Scope = new List<string> { "openid", "profile", "email", "address", "clientinfo", "mobile_phone", "phone" };
                registerSiteParam.Contacts = new List<string> { ConfigurationManager.AppSettings["UserEmail"] };
                registerSiteParam.GrantTypes = new List<string> { "authorization_code" };
                registerSiteParam.ResponseTypes = new List<string> { "code" };
                registerSiteParam.ClientLogoutUris = new List<string> { logoutUrl };
                registerSiteParam.ClientName = ConfigurationManager.AppSettings["OxdClientName"];

                //Prepare Register Site command using its params
                Command cmdRegisterSite = new Command(CommandType.register_site);
                cmdRegisterSite.setParamsObject(registerSiteParam);

                //Send request
                CommandClient client = new CommandClient(host, port);
                string commandresponse = client.send(cmdRegisterSite);

                //Process response
                RegisterSiteResponse response = JsonConvert.DeserializeObject<RegisterSiteResponse>(commandresponse);
                Assert.IsNotNull(response);
                Assert.IsTrue(!String.IsNullOrEmpty(response.Data.OxdId));
                StoredValues._oxd_id = response.Data.OxdId;

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
                //Prepare Register Site Params
                RegisterSiteParams registerSiteParam = new RegisterSiteParams();
                registerSiteParam.AuthorizationRedirectUri = redirectUrl;
                registerSiteParam.OpHost = ConfigurationManager.AppSettings["GluuServerUrl"];
                registerSiteParam.ClientName = ConfigurationManager.AppSettings["OxdClientName"];

                //Create Register Site command using its params
                Command cmd = new Command(CommandType.register_site);
                cmd.setParamsObject(registerSiteParam);

                CommandClient client = new CommandClient(host, port);
                string commandresponse = client.send(cmd);

                RegisterSiteResponse response = JsonConvert.DeserializeObject<RegisterSiteResponse>(commandresponse);
                Assert.IsNotNull(response);
                Assert.IsTrue(!String.IsNullOrEmpty(response.Data.OxdId));
                StoredValues._oxd_id = response.Data.OxdId;

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
