using Newtonsoft.Json;
using oxdCSharp.Clients;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using oxdCSharp.UMA.CommandParameters;
using oxdCSharp.UMA.CommandResponses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using UMATestApi.Models;

namespace UMATestApi.Controllers
{
    public class UMAController : Controller
    {
        public static string httpresturl = "";
        public static string clientid = "";
        public static string clientsecret = "";
        public static string OXDType = "";
        public static string op_host = "";
        public static string oxd_id = "";
        public static string oxdHost = "";
        public static int oxdPort = 0;
        public static List<string> scope;
        public static List<string> grant_types;
        public static bool dynamic_registration = true;
        


        public ActionResult Loadoxdsettings()
        {

            //Load oxd settings from oxd settings json file
            string readoxdconfigfile = System.IO.File.ReadAllText(Server.MapPath(@ConfigurationManager.AppSettings["oxd_config"]));
            OxdSetting oxdsettings = JsonConvert.DeserializeObject<OxdSetting>(readoxdconfigfile);


            httpresturl = oxdsettings.HttpRestUrl;
            clientid = oxdsettings.ClientId;
            clientsecret = oxdsettings.ClientSecret;
            OXDType = oxdsettings.ConnectionType;
            op_host = oxdsettings.OpHost;
            oxd_id = oxdsettings.OxdId;
            oxdHost = oxdsettings.OxdHost;
            scope = oxdsettings.Scope.ToList();
            grant_types = oxdsettings.GrantTypes.ToList();

            
            if (!String.IsNullOrEmpty(oxdsettings.OxdPort.ToString()))
                oxdPort = oxdsettings.OxdPort;


            string displayjsonstring = JsonConvert.SerializeObject(oxdsettings, Formatting.None);


            return Json(displayjsonstring, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Setting()
        {
            return View();
        }

        /// <summary>
        ///It registers the client  
        /// </summary>
        /// <param name="oxd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetupClient(OxdModel oxd)
        {
            Loadoxdsettings();
            var setupClientInputParams = new SetupClientParams();
            var registerSiteClient = new SetupClientClient();

            //prepare input params for Setup client
            setupClientInputParams.AuthorizationRedirectUri = oxd.RedirectUrl;
            setupClientInputParams.OpHost = oxd.OpHost;
            setupClientInputParams.PostLogoutRedirectUri = oxd.PostLogoutRedirectUrl;
            setupClientInputParams.ClientName = oxd.ClientName;
            setupClientInputParams.Scope = scope;
            setupClientInputParams.GrantTypes = grant_types;
            //setupClientInputParams.ClientId = oxd.ClientId;
            //setupClientInputParams.ClientSecret = oxd.ClientSecret;
            setupClientInputParams.ClaimsRedirecturi = new List<string> { ConfigurationManager.AppSettings["ClaimsRedirectURI"] };

            var setupClientResponse = new SetupClientResponse();

            if (string.IsNullOrEmpty(oxd.OxdId))
            {
                //Setup Client using OXD Local
                if (oxd.ConnectionType == "local")
                    setupClientResponse = registerSiteClient.SetupClient(oxdHost, oxd.OxdPort, setupClientInputParams);


                //Setup Client using OXD Web
                if (oxd.ConnectionType == "web")
                    setupClientResponse = registerSiteClient.SetupClient(oxd.HttpRestUrl, setupClientInputParams);


                SetConfigValues(oxd, setupClientResponse);

                return Json(new { oxdId = setupClientResponse.Data.OxdId, clientId = setupClientResponse.Data.clientId, clientSecret = setupClientResponse.Data.clientSecret });

            }
            else
            {
                return Json(new { oxdId = oxd.OxdId, clientId = clientid, clientSecret = clientsecret });
            }
        }


        /// <summary>
        /// Write oxd settings into json file
        /// </summary>
        /// <param name="oxd">oxd models</param>
        /// <param name="setupClientResponse">oxd setup client response from oxd server</param>
        public void SetConfigValues(OxdModel oxd, SetupClientResponse setupClientResponse)
        {

            string configObjString = System.IO.File.ReadAllText(Server.MapPath(@ConfigurationManager.AppSettings["oxd_config"]));
            OxdSetting oxdsettings = JsonConvert.DeserializeObject<OxdSetting>(configObjString);
            oxdsettings.AuthUrl = oxd.RedirectUrl;
            oxdsettings.OpHost = oxd.OpHost;
            oxdsettings.ClientName = oxd.ClientName;
            oxdsettings.ConnectionType = oxd.ConnectionType;
            oxdsettings.OxdId = setupClientResponse.Data.OxdId;
            oxdsettings.ClientId = setupClientResponse.Data.clientId;
            oxdsettings.ClientSecret = setupClientResponse.Data.clientSecret;

            if (oxd.ConnectionType == "local")
                oxdsettings.OxdPort = oxd.OxdPort;
            else if (oxd.ConnectionType == "web")
                oxdsettings.HttpRestUrl = oxd.HttpRestUrl;
            


            string updatejsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(oxdsettings, Newtonsoft.Json.Formatting.None);
            System.IO.File.WriteAllText(Server.MapPath(@ConfigurationManager.AppSettings["oxd_config"]), updatejsonstring);
        }


        /// <summary>
        /// Deletes all the oxd setting values from the json file
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Deleteoxdsettings()
        {

            string deletesettingsjson = System.IO.File.ReadAllText(Server.MapPath(@ConfigurationManager.AppSettings["oxd_config"]));

            OxdSetting oxdsettings = JsonConvert.DeserializeObject<OxdSetting>(deletesettingsjson);
            oxdsettings.AuthUrl = "";
            oxdsettings.OpHost = "";
            oxdsettings.ConnectionType = "";
            oxdsettings.ClientId = "";
            oxdsettings.ClientSecret = "";
            oxdsettings.OxdId = "";
            oxdsettings.ClientName = "";
            oxdsettings.OxdPort = 0;
            oxdsettings.HttpRestUrl = "";
            
            string oxdsettingsJson = JsonConvert.SerializeObject(oxdsettings, Formatting.None);
            System.IO.File.WriteAllText(Server.MapPath(@ConfigurationManager.AppSettings["oxd_config"]), oxdsettingsJson);

            return View();

        }

        /// <summary>
        /// Protects the Resource
        /// </summary>
        /// <param name="oxdModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RSProtect(OxdModel oxd)
        {
            Loadoxdsettings();

            var protectResource = new RSProtectResource(oxd_id);

            string response = protectResource.ProtectResources(oxdHost, oxdPort, httpresturl, OXDType);

            return Json(new { Response = response });
        }
        
    }
}
