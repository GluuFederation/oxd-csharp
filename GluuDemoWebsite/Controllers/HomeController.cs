using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using GluuDemoWebsite.Models;
using CSharp.CommonClasses;
using oxdCSharp.CommandResponses;
using oxdCSharp.Clients;
using oxdCSharp.CommandParameters;
using System.Configuration;
using Newtonsoft.Json;
using System.Net.Http;

using System.Threading.Tasks;
using oxdCSharp.UMA.CommandResponses;
using oxdCSharp.UMA.CommandParameters;
using oxdCSharp.UMA.Clients;
using oxdCSharp.UMA.UMA.Clients;

namespace GluuDemoWebsite.Controllers
{
    public class HomeController : Controller
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

        /// <summary>
        /// Read oxd setting from json file and binds all the properties to global variables
        /// </summary>
        /// <returns></returns>

        [HttpGet]
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

            bool.TryParse(oxdsettings.DynamicRegistration.ToString(), out dynamic_registration);

            if (!String.IsNullOrEmpty(oxdsettings.OxdPort.ToString()))
                oxdPort = oxdsettings.OxdPort;


            string displayjsonstring = JsonConvert.SerializeObject(oxdsettings,Formatting.None);


            return Json(displayjsonstring, JsonRequestBehavior.AllowGet);

        }

        #region Main OXD Actions

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
           // setupClientInputParams.ClientId = oxd.ClientId;
           // setupClientInputParams.ClientSecret = oxd.ClientSecret;

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
            oxdsettings.PostLogoutRedirectUrl = oxd.PostLogoutRedirectUrl;
            oxdsettings.ClientName = oxd.ClientName;
            oxdsettings.ConnectionType = oxd.ConnectionType;
            oxdsettings.OxdId= setupClientResponse.Data.OxdId;
            oxdsettings.ClientId = setupClientResponse.Data.clientId;
            oxdsettings.ClientSecret = setupClientResponse.Data.clientSecret;

            if (oxd.ConnectionType == "local")
                oxdsettings.OxdPort = oxd.OxdPort;
            else if (oxd.ConnectionType == "web")
                oxdsettings.HttpRestUrl = oxd.HttpRestUrl;

            oxdsettings.DynamicRegistration = (isDynamicRegistration(oxd.OpHost) == true) ? true : false;


            string updatejsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(oxdsettings, Newtonsoft.Json.Formatting.None);
            System.IO.File.WriteAllText(Server.MapPath(@ConfigurationManager.AppSettings["oxd_config"]), updatejsonstring);
        }
        /// <summary>
        /// update oxd server and settings json file
        /// </summary>
        /// <param name="oxd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(OxdModel oxd)
        {
            Loadoxdsettings();

            string updateStatus = "ok";

            if (dynamic_registration)
            {
                var updateSiteInputParams = new UpdateSiteParams();
                var updateSiteClient = new UpdateSiteRegistrationClient();

                updateSiteInputParams.AuthorizationRedirectUri = oxd.RedirectUrl;
                updateSiteInputParams.PostLogoutRedirectUri = oxd.PostLogoutRedirectUrl;
                updateSiteInputParams.ClientName = oxd.ClientName;

                updateSiteInputParams.OxdId = oxd.OxdId;
                updateSiteInputParams.Contacts = new List<string> { oxd.OxdEmail };
                updateSiteInputParams.PostLogoutRedirectUri = oxd.PostLogoutRedirectUrl;
                updateSiteInputParams.GrantTypes = grant_types;
                //updateSiteInputParams.Scope = scope;


                var updateSiteResponse = new UpdateSiteResponse();

                //Update Client using  OXD Local
                if (oxd.ConnectionType == "local")
                {
                    //Get ProtectionAccessToken
                    updateSiteInputParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxd.OxdPort);
                    // Update Site response
                    updateSiteResponse = updateSiteClient.UpdateSiteRegistration(oxdHost, oxd.OxdPort, updateSiteInputParams);
                }

                //Update Client using OXD Web
                if (oxd.ConnectionType == "web")
                {
                    updateSiteInputParams.ProtectionAccessToken = GetProtectionAccessToken(oxd.HttpRestUrl);
                    updateSiteResponse = updateSiteClient.UpdateSiteRegistration(oxd.HttpRestUrl, updateSiteInputParams);
                }


                updateStatus = updateSiteResponse.Status;
            }

            Updateoxdsettingfile(oxd);


            //Process Response
            return Json(new { status = updateStatus });

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
            oxdsettings.PostLogoutRedirectUrl = "";
            oxdsettings.OpHost = "";
            oxdsettings.ConnectionType = "";
            oxdsettings.ClientId = "";
            oxdsettings.ClientSecret = "";
            oxdsettings.OxdId = "";
            oxdsettings.ClientName = "";
            oxdsettings.OxdPort = 0;
            oxdsettings.HttpRestUrl = "";
            oxdsettings.ApplicationType = "";
           
       
            oxdsettings.DynamicRegistration = false;


            string oxdsettingsJson = JsonConvert.SerializeObject(oxdsettings, Formatting.None);
            System.IO.File.WriteAllText(Server.MapPath(@ConfigurationManager.AppSettings["oxd_config"]), oxdsettingsJson);

            return View();

        }


        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns the Authorization URL for Login
        /// </summary>
        /// <param name="oxd"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult Index(OxdModel oxd)
        {
            Loadoxdsettings();
            var getAuthUrlInputParams = new GetAuthorizationUrlParams();
            var getAuthUrlClient = new GetAuthorizationUrlClient();

            //prepare input params for Getting Auth URL from a site
            getAuthUrlInputParams.OxdId = oxd_id;
           
           getAuthUrlInputParams.custom_parameters.Add("param1", "value1");
            getAuthUrlInputParams.custom_parameters.Add("param2", "value2");
            var getAuthUrlResponse = new GetAuthorizationUrlResponse();

            //Get Authorization url using OXD Local
            if (OXDType == "local")
            {
                //Get ProtectionAccessToken
                if (dynamic_registration)
                    getAuthUrlInputParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);
                //Get Auth URL
                getAuthUrlResponse = getAuthUrlClient.GetAuthorizationURL(oxdHost, oxdPort, getAuthUrlInputParams);
            }

            //Get Authorization url using OXD Web
            if (OXDType == "web")
            {
                if (dynamic_registration)
                    getAuthUrlInputParams.ProtectionAccessToken = GetProtectionAccessToken(httpresturl);

                getAuthUrlResponse = getAuthUrlClient.GetAuthorizationURL(httpresturl, getAuthUrlInputParams);
            }

            //Process Response
            return Json(new { authUrl = getAuthUrlResponse.Data.AuthorizationUrl });
        }


        public ActionResult GetUserInfo()
        {
            string code = Request.Params["code"];
            string state = Request.Params["state"];
            if (code==null && state==null)
            {
                return RedirectToAction("Index");
            }

            OxdModel oxd = new OxdModel();

            #region GetProtectionAccessToken
            string protectionAccessToken = "";

            if (dynamic_registration)
            {
                //For OXD Server
                if (OXDType == "local")
                {
                    //Get ProtectionAccessToken
                    protectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);
                }

                //For OXD Web
                if (OXDType == "web")
                {
                    protectionAccessToken = GetProtectionAccessToken(httpresturl);
                }
            }
            #endregion


            #region Get Tokens by Code

            var getTokenByCodeInputParams = new GetTokensByCodeParams();
            var getTokenByCodeClient = new GetTokensByCodeClient();

            //prepare input params for Getting Tokens from a site
            getTokenByCodeInputParams.OxdId = oxd_id;
            getTokenByCodeInputParams.Code = code;
            getTokenByCodeInputParams.State = state;
            getTokenByCodeInputParams.ProtectionAccessToken = protectionAccessToken;

            var getTokensByCodeResponse = new GetTokensByCodeResponse();

            //For OXD Server
            if (OXDType == "local")
            {
                getTokensByCodeResponse = getTokenByCodeClient.GetTokensByCode(oxdHost, oxdPort, getTokenByCodeInputParams);
            }

            //For OXD Web
            if (OXDType == "web")
            {
                getTokensByCodeResponse = getTokenByCodeClient.GetTokensByCode(httpresturl, getTokenByCodeInputParams);
            }


            var accessToken = getTokensByCodeResponse.Data.AccessToken;
            var refreshToken = getTokensByCodeResponse.Data.RefreshToken;
            #endregion


            #region Get Access Token By Refresh Token
            if (dynamic_registration)
            {
                var getAccessTokenByRefreshTokenInputParams = new GetAccessTokenByRefreshTokenParams();
                var getAccessTokenByRefreshTokenClient = new GetAccessTokenByRefreshTokenClient();

                //prepare input params for Getting Tokens from a site
                getAccessTokenByRefreshTokenInputParams.OxdId = oxd_id;
                getAccessTokenByRefreshTokenInputParams.RefreshToken = refreshToken;
                getAccessTokenByRefreshTokenInputParams.ProtectionAccessToken = protectionAccessToken;


                var getAccessTokenByRefreshTokenResponse = new GetAccessTokenByRefreshTokenResponse();

                //For OXD Server
                if (OXDType == "local")
                {
                    getAccessTokenByRefreshTokenResponse = getAccessTokenByRefreshTokenClient.GetAccessTokenByRefreshToken(oxdHost, oxdPort, getAccessTokenByRefreshTokenInputParams);
                }

                //For OXD Web
                if (OXDType == "web")
                {
                    getAccessTokenByRefreshTokenResponse = getAccessTokenByRefreshTokenClient.GetAccessTokenByRefreshToken(httpresturl, getAccessTokenByRefreshTokenInputParams);
                }


                accessToken = getAccessTokenByRefreshTokenResponse.Data.AccessToken;
                refreshToken = getAccessTokenByRefreshTokenResponse.Data.RefreshToken;
            }
            #endregion


            #region Get User Info

            var getUserInfoInputParams = new GetUserInfoParams();
            var getUserInfoClient = new GetUserInfoClient();

            //prepare input params for Getting User Info from a site
            getUserInfoInputParams.OxdId = oxd_id;
            getUserInfoInputParams.AccessToken = accessToken;
            getUserInfoInputParams.ProtectionAccessToken = protectionAccessToken;


            var getUserInfoResponse = new GetUserInfoResponse();

            //For OXD Server
            if (OXDType == "local")
            {
                getUserInfoResponse = getUserInfoClient.GetUserInfo(oxdHost, oxdPort, getUserInfoInputParams);
            }

            //For OXD Web
            if (OXDType == "web")
            {
                getUserInfoResponse = getUserInfoClient.GetUserInfo(httpresturl, getUserInfoInputParams);
            }


            //Process response          
            Session["userName"] = getUserInfoResponse.Data.UserClaims.Name.First();
            Session["userEmail"] = getUserInfoResponse.Data.UserClaims.Email == null ? string.Empty : getUserInfoResponse.Data.UserClaims.Email.FirstOrDefault();
            #endregion

            return RedirectToAction("UserInfo");
        }

        public ActionResult UserInfo()
        {
            if (Session["userName"] == null)
                return RedirectToAction("Index");

            return View();
        }
        /// <summary>
        /// Returns the logout URL
        /// </summary>
        /// <returns></returns>

        public ActionResult Logout()
        {
            var getLogoutUriInputParams = new GetLogoutUrlParams();
            var getLogoutUriClient = new GetLogoutUriClient();

            //prepare input params for Getting Logout URI from a site
            getLogoutUriInputParams.OxdId = oxd_id;

            var getLogoutUriResponse = new GetLogoutUriResponse();

            //For OXD Local
            if (OXDType == "local")
            {
                //Get ProtectionAccessToken
                if (dynamic_registration)
                    getLogoutUriInputParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);
                //Get Logout Url
                getLogoutUriResponse = getLogoutUriClient.GetLogoutURL(oxdHost, oxdPort, getLogoutUriInputParams);
            }

            //For OXD Web
            if (OXDType == "web")
            {
                //Get ProtectionAccessToken
                if (dynamic_registration)
                    getLogoutUriInputParams.ProtectionAccessToken = GetProtectionAccessToken(httpresturl);
                //Get Logout Url
                getLogoutUriResponse = getLogoutUriClient.GetLogoutURL(httpresturl, getLogoutUriInputParams);
            }


            Session.Clear();
            //Process response
            return Json(new { LogoutUri = getLogoutUriResponse.Data.LogoutUri });

        }
        /// <summary>
        /// Checks for the registration_endpoint of the ophost
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public ActionResult CheckRegistrationEndPoint()
        {
            string ophostUiVal = Request["ophostId"].ToString();

            bool registration_type = isDynamicRegistration(ophostUiVal);

            return Json(registration_type);
            //return registration_type;
        }

        private Boolean isDynamicRegistration(string opHost)
        {
            try
            {
                opHost += "/.well-known/openid-configuration";

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(opHost);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                string result = sr.ReadToEnd();
                var obj = JsonConvert.DeserializeObject<RegistrationType>(result);
                var registration_endpoint = obj.registration_endpoint;
                sr.Close();
                myResponse.Close();
                if (registration_endpoint == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            { return true; }
        }

        #endregion


        #region UMA related methods

        public ActionResult UMA()
        {
            Loadoxdsettings();
            return View();
        }

        [HttpPost]
        public ActionResult ProtectResources()
        {
            var protectParams = new UmaRsProtectParams();
            var protectClient = new UmaRsProtectClient();

            //prepare input params for Protect Resource
            protectParams.OxdId = oxd_id;
            protectParams.ProtectResources = new List<ProtectResource>
            {
                new ProtectResource
                {
                    Path = "/scim",
                    ProtectConditions = new List<ProtectCondition>
                    {
                        new ProtectCondition
                        {
                            HttpMethods = new List<string> { "GET" },
                            Scopes = new List<string> { "https://scim-test.gluu.org/identity/seam/resource/restv1/scim/vas1" },
                            TicketScopes = new List<string> { "https://scim-test.gluu.org/identity/seam/resource/restv1/scim/vas1" }
                        }
                    }
                }
            };

            var protectResponse = new UmaRsProtectResponse();

            //For OXD Local
            if (OXDType == "local")
            {
                //Get protection access token
                protectParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);
                protectResponse = protectClient.ProtectResources(oxdHost, oxdPort, protectParams);
            }

            //For OXD Web
            if (OXDType == "web")
            {
                //Get protection access token
                protectParams.ProtectionAccessToken = GetProtectionAccessToken(httpresturl);
                protectResponse = protectClient.ProtectResources(httpresturl, protectParams);
            }

            //process response
            if (protectResponse.Status.ToLower().Equals("ok"))
            {
                return Json(new { Response = protectResponse.Status });
            }

            throw new Exception("Procteting Resource is not successful. Check OXD Server log for error details.");
        }

        [HttpPost]
        public ActionResult CheckAccess()
        {
            var checkAccessParams = new UmaRsCheckAccessParams();
            var checkAccessClient = new UmaRsCheckAccessClient();

            string rpt = "";
            string path = "/scim";
            string httpMethod = "GET";

            if (Session["rpt"] != null)
                rpt = Session["rpt"].ToString();

            //prepare input params for Check Access
            checkAccessParams.OxdId = oxd_id;
            checkAccessParams.RPT = rpt;
            checkAccessParams.Path = path;
            checkAccessParams.HttpMethod = httpMethod;


            var checkAccessResponse = new UmaRsCheckAccessResponse();

            //For OXD Local
            if (OXDType == "local")
            {
                //Get protection access token
                checkAccessParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);
                //Check Access
                checkAccessResponse = checkAccessClient.CheckAccess(oxdHost, oxdPort, checkAccessParams);
            }

            //For OXD Web
            if (OXDType == "web")
            {
                //Get protection access token
                checkAccessParams.ProtectionAccessToken = GetProtectionAccessToken(httpresturl);
                //Check Access
                checkAccessResponse = checkAccessClient.CheckAccess(httpresturl, checkAccessParams);
            }

            if (checkAccessResponse.Status.ToLower().Equals("ok"))
            {
                Session["uma_ticket"] = checkAccessResponse.Data.Ticket;

                return Json(new { Response = JsonConvert.SerializeObject(checkAccessResponse.Data) });
            }

            throw new Exception("Error!! Check OXD Server log for error details.");
        }

        [HttpPost]
        public ActionResult ObtainRpt()
        {
            var getRptParams = new UmaRpGetRptParams();
            var getRptClient = new UmaRpGetRptClient();

            string ticket = "";
            if (Session["uma_ticket"] != null)
                ticket = Session["uma_ticket"].ToString();

            //prepare input params for Protect Resource
            getRptParams.OxdId = oxd_id;
            getRptParams.ticket = ticket;


            var getRptResponse = new GetRPTResponse();

            //For OXD Local
            if (OXDType == "local")
            {
                //Get protection access token
                getRptParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);
                //Get RPT
                getRptResponse = getRptClient.GetRPT(oxdHost, oxdPort, getRptParams);
            }

            //For OXD Web
            if (OXDType == "web")
            {
                //Get protection access token
                getRptParams.ProtectionAccessToken = GetProtectionAccessToken(httpresturl);
                //Get RPT
                getRptResponse = getRptClient.GetRPT(httpresturl, getRptParams);
            }

            //process response
            if (getRptResponse.Status.ToLower().Equals("ok"))
            {
                Session["rpt"] = getRptResponse.Data.Rpt;
                return Json(new { Response = JsonConvert.SerializeObject(getRptResponse.Data) });
            }

            throw new Exception("Obtaining RPT is not successful. Check OXD Server log for error details.");
        }

        [HttpPost]
        public ActionResult GetClaimsGatheringUrl()
        {
            var getClaimsGatheringUrlParams = new UmaRpGetClaimsGatheringUrlParams();
            var getClaimsGatheringUrlClient = new UmaRpGetClaimsGatheringUrlClient();

            string ticket = "";
            if (Session["uma_ticket"] != null)
                ticket = Session["uma_ticket"].ToString();

            //prepare input params for Check Access
            getClaimsGatheringUrlParams.OxdId = oxd_id;
            getClaimsGatheringUrlParams.Ticket = ticket;
            getClaimsGatheringUrlParams.ClaimsRedirectURI = "https://client.example.com";


            var getClaimsGatheringUrlResponse = new UmaRpGetClaimsGatheringUrlResponse();

            //For OXD Local
            if (OXDType == "local")
            {
                //Get protection access token
                getClaimsGatheringUrlParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);
                //Authorize RPT
                getClaimsGatheringUrlResponse = getClaimsGatheringUrlClient.GetClaimsGatheringUrl(oxdHost, oxdPort, getClaimsGatheringUrlParams);
            }

            //For OXD Web
            if (OXDType == "web")
            {
                //Get protection access token
                getClaimsGatheringUrlParams.ProtectionAccessToken = GetProtectionAccessToken(httpresturl);
                //Authorize RPT
                getClaimsGatheringUrlResponse = getClaimsGatheringUrlClient.GetClaimsGatheringUrl(httpresturl, getClaimsGatheringUrlParams);
            }

            //process response
            return Json(new { Response = JsonConvert.SerializeObject(getClaimsGatheringUrlResponse.Data) });
        }

        #endregion


        #region Protection_Access_Token
        private string GetProtectionAccessToken(string OxdHost, int OxdPort)
        {
            var getClientAccessToken = new GetClientTokenClient();
            try
            {
                return getClientAccessToken.GetClientToken(OxdHost, OxdPort, getClientAccessTokenParams()).Data.accessToken;
            }

            catch (NullReferenceException ex)
            {
                return null;
            }
        }

        private string GetProtectionAccessToken(string oxdtohttpurl)
        {
            var getClientAccessToken = new GetClientTokenClient();
            return getClientAccessToken.GetClientToken(oxdtohttpurl, getClientAccessTokenParams()).Data.accessToken;
        }

        private GetClientTokenParams getClientAccessTokenParams()
        {
            var getClientAccessTokenParams = new GetClientTokenParams();

            getClientAccessTokenParams.clientId = clientid;
            getClientAccessTokenParams.clientSecret = clientsecret;
            getClientAccessTokenParams.OxdId = oxd_id;
            getClientAccessTokenParams.opHost = op_host;

            return getClientAccessTokenParams;

        }

        /// <summary>
        /// updates oxd settings value in the json file
        /// </summary>
        /// <param name="oxd"></param>

        private void Updateoxdsettingfile(OxdModel oxd)
        {
            string configObjString = System.IO.File.ReadAllText(Server.MapPath(@ConfigurationManager.AppSettings["oxd_config"]));
            OxdSetting oxdsettings = JsonConvert.DeserializeObject<OxdSetting>(configObjString);
            oxdsettings.AuthUrl = oxd.RedirectUrl; 
            oxdsettings.PostLogoutRedirectUrl = oxd.PostLogoutRedirectUrl;
            oxdsettings.OpHost = oxd.OpHost;
            oxdsettings.ConnectionType = oxd.ConnectionType; 
            oxdsettings.ClientId = oxd.ClientId;
            oxdsettings.ClientSecret = oxd.ClientSecret;  
            oxdsettings.ClientName = oxd.ClientName;
            oxdsettings.OxdPort = oxd.OxdPort;
            oxdsettings.HttpRestUrl = oxd.HttpRestUrl;
            string updatejsonstring = JsonConvert.SerializeObject(oxdsettings, Newtonsoft.Json.Formatting.None);
            System.IO.File.WriteAllText(Server.MapPath(@ConfigurationManager.AppSettings["oxd_config"]), updatejsonstring);
        }
        #endregion
    }
}