using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using oxdCSharp.UMA.CommandParameters;
using oxdCSharp.UMA.Clients;
using oxdCSharp.UMA.CommandResponses;
using Newtonsoft.Json;
using oxdCSharp.Clients;
using oxdCSharp.CommandParameters;
using System.Configuration;

namespace UMATestApi.Models
{
    public class RSCheckAccess
    {
        public static string oxdHost = "";
        public static int oxdPort = 0;
        public static string httpresturl = "";
        public static string OXDType = "";
        public static string oxdId = "";


        public RSCheckAccess()
        {
            //Load oxd settings from oxd settings json file
            string readoxdconfigfile = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath(@ConfigurationManager.AppSettings["oxd_config"]));
            OxdSetting oxdsettings = JsonConvert.DeserializeObject<OxdSetting>(readoxdconfigfile);

            oxdHost = oxdsettings.OxdHost;
            oxdPort = oxdsettings.OxdPort;
            httpresturl = oxdsettings.HttpRestUrl;
            OXDType = oxdsettings.ConnectionType;
            oxdId = oxdsettings.OxdId;
        }
        

        public UmaRsCheckAccessResponse CheckAccess(string rpt)
        {
            var checkAccessParams = new UmaRsCheckAccessParams();
            checkAccessParams.OxdId = oxdId;
            checkAccessParams.HttpMethod = "GET";
            checkAccessParams.Path = ConfigurationManager.AppSettings["resource_path"];

            if (rpt != null)
                checkAccessParams.RPT = rpt;

            var checkAccessClient = new UmaRsCheckAccessClient();
            var pat = new ProtectionAccessToken();


            var checkAccessResponse = new UmaRsCheckAccessResponse();

            if (OXDType == "local")
            {
                //Get protection access token
                //checkAccessParams.ProtectionAccessToken = pat.GetProtectionAccessToken(oxdHost, oxdPort);//Keep this line if protect_commands_with_access_token is set True for oxd-server
                //Check Access
                checkAccessResponse = checkAccessClient.CheckAccess(oxdHost, oxdPort, checkAccessParams);
            }

            if (OXDType == "web")
            {
                //Get protection access token
                checkAccessParams.ProtectionAccessToken = pat.GetProtectionAccessToken(httpresturl);
                //Check Access
                checkAccessResponse = checkAccessClient.CheckAccess(httpresturl, checkAccessParams);
            }

            return checkAccessResponse;

            throw new Exception("Error!! Check oxd Server log for error details.");
        }
    }
}