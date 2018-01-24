using Newtonsoft.Json;
using oxdCSharp.Clients;
using oxdCSharp.CommandParameters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UMATestApi.Models
{
    public class ProtectionAccessToken
    {
        public static string oxdId = "";
        public static string clientId = "";
        public static string clientSecret = "";
        public static string opHost = "";


        public ProtectionAccessToken()
        {
            //Load oxd settings from oxd settings json file
            string readoxdconfigfile = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath(@ConfigurationManager.AppSettings["oxd_config"]));
            OxdSetting oxdsettings = JsonConvert.DeserializeObject<OxdSetting>(readoxdconfigfile);
            
            oxdId = oxdsettings.OxdId;
            clientId = oxdsettings.ClientId;
            clientSecret = oxdsettings.ClientSecret;
            opHost = oxdsettings.OpHost;
        }

        public string GetProtectionAccessToken(string OxdHost, int OxdPort)
        {
            var getClientAccessToken = new GetClientTokenClient();
            try
            {
                return getClientAccessToken.GetClientToken(OxdHost, OxdPort, getClientAccessTokenParams(clientId, clientSecret, oxdId, opHost)).Data.accessToken;
            }

            catch (NullReferenceException ex)
            {
                return null;
            }
        }

        public string GetProtectionAccessToken(string oxdtohttpurl)
        {
            var getClientAccessToken = new GetClientTokenClient();
            return getClientAccessToken.GetClientToken(oxdtohttpurl, getClientAccessTokenParams(clientId, clientSecret, oxdId, opHost)).Data.accessToken;
        }

        private GetClientTokenParams getClientAccessTokenParams(string clientid, string clientsecret, string oxd_id, string op_host)
        {
            var getClientAccessTokenParams = new GetClientTokenParams();

            getClientAccessTokenParams.clientId = clientid;
            getClientAccessTokenParams.clientSecret = clientsecret;
            getClientAccessTokenParams.opHost = op_host;

            return getClientAccessTokenParams;

        }
        
    }
}