using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System.Configuration;

namespace CSharp.client
{
    public class UpdateSiteRegistrationTest
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Method for updating the Current registration
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public UpdateSiteResponse UpdateSiteRegisteration(string host, int port)
        {
            try
            {
                //Prepare Update Site Params
                UpdateSiteParams updateSiteParams = new UpdateSiteParams();
                updateSiteParams.OxdId = StoredValues._oxd_id;
                updateSiteParams.AuthorizationRedirectUri = ConfigurationManager.AppSettings["AuthorizationRedirectUri"] ;
                updateSiteParams.PostLogoutRedirectUri = "";                
                updateSiteParams.AcrValues = new List<string>();
                updateSiteParams.ClientJwksUri = string.Empty;
                updateSiteParams.Contacts = new List<string> { ConfigurationManager.AppSettings["UserEmail"] };
                updateSiteParams.GrantTypes = new List<string> { "authorization_code" };
                updateSiteParams.ClientTokenEndpointAuthMethod = string.Empty;
                updateSiteParams.ClientLogoutUris = new List<string> { "http://www.omsttech.com/wp-login.php?action=logout&_wpnonce=a3c70643e9" };

                //Create Update Site command with its params
                Command cmdUpdateSite = new Command(CommandType.update_site_registration);
                cmdUpdateSite.setParamsObject(updateSiteParams);

                //Send request
                CommandClient client = new CommandClient(host, port);
                string commandresponse = client.send(cmdUpdateSite);

                //Process response
                UpdateSiteResponse response = JsonConvert.DeserializeObject<UpdateSiteResponse>(commandresponse);
                Assert.IsNotNull(response);
                Assert.AreEqual("ok", response.Status);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.Debug(ex.Message);
                return null;
            }
        }

        public UpdateSiteResponse UpdateSiteRegisteration(string host, int port, string oxdId, string newEmail, string postLogoutRedirectUri)
        {
            UpdateSiteParams updateSiteParams = new UpdateSiteParams();
            updateSiteParams.OxdId = oxdId;
            updateSiteParams.Contacts = new List<string> { newEmail };
            updateSiteParams.PostLogoutRedirectUri = postLogoutRedirectUri;

            //Create Update Site command with its params
            Command cmdUpdateSite = new Command(CommandType.update_site_registration);
            cmdUpdateSite.setParamsObject(updateSiteParams);

            //Send request
            CommandClient client = new CommandClient(host, port);
            string commandresponse = client.send(cmdUpdateSite);

            //Process response
            UpdateSiteResponse response = JsonConvert.DeserializeObject<UpdateSiteResponse>(commandresponse);
            Assert.IsNotNull(response);
            Assert.AreEqual("ok", response.Status);
            return response;
        }
    }
}
