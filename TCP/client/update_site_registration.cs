using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.Classes;
using TCP.CommonClasses;
using TCP.ResponseClasses;

namespace TCP.client
{
    class update_site_registration
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
                CommandClient client = new CommandClient(host, port);
                UpdateSiteParams param = new UpdateSiteParams();
                param.SetOxdId(StoredValues._oxd_id);
                param.SetAuthorizationRedirectUri("http://www.omsttech.com/wp-login.php");
                param.SetPostLogoutRedirectUri("http://www.omsttech.com/wp-login.php?action=logout&_wpnonce=a3c70643e9");
                param.SetApplicationType("web");
                param.SetRedirectUris(Lists.newArrayList(new string[] { "http://www.omsttech.com/wp-login.php" }));
                param.SetAcrValues(new List<string>());
                param.SetClientJwksUri("");
                param.SetContacts(Lists.newArrayList(new string[] { "hellochopra1@gmail.com" }));
                param.SetGrantType(Lists.newArrayList(new string[] { "authorization_code" }));
                param.SetClientTokenEndpointAuthMethod("");
                param.SetClientLogoutUri(Lists.newArrayList(new string[] { "http://www.omsttech.com/wp-login.php?action=logout&_wpnonce=a3c70643e9" }));

                Command cmd = new Command(CommandType.update_site_registration);
                cmd.setParamsObject(param);

                string commandresponse = client.send(cmd);
                UpdateSiteResponse response = new UpdateSiteResponse(JsonConvert.DeserializeObject<dynamic>(commandresponse).data);
                Assert.IsNotNull(response);
                Assert.IsTrue(!String.IsNullOrEmpty(response.getOxdId()));
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
