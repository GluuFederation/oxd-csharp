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
            checkAccessParams.Path = "/values";
            //checkAccessParams.Path = "/GetAll";

            if (rpt != null)
                checkAccessParams.RPT = rpt;

            var checkAccessClient = new UmaRsCheckAccessClient();
            var pat = new ProtectionAccessToken();


            var checkAccessResponse = new UmaRsCheckAccessResponse();

            //For OXD Local
            if (OXDType == "local")
            {
                //Get protection access token
                checkAccessParams.ProtectionAccessToken = pat.GetProtectionAccessToken(oxdHost, oxdPort);
                //Check Access
                checkAccessResponse = checkAccessClient.CheckAccess(oxdHost, oxdPort, checkAccessParams);
            }

            //For OXD Web
            if (OXDType == "web")
            {
                //Get protection access token
                checkAccessParams.ProtectionAccessToken = pat.GetProtectionAccessToken(httpresturl);
                //Check Access
                checkAccessResponse = checkAccessClient.CheckAccess(httpresturl, checkAccessParams);
            }

            return checkAccessResponse;

            throw new Exception("Error!! Check OXD Server log for error details.");
        }
    }
}