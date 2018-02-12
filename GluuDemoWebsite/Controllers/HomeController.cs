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


            string displayjsonstring = JsonConvert.SerializeObject(oxdsettings, Formatting.None);


            return Json(displayjsonstring, JsonRequestBehavior.AllowGet);

        }

        #region Main OXD Actions

        public ActionResult Setting()
        {
            return View();
        }

        /// <summary>
        /// Setup a new client for those OPs which allow dynamic client registration.
        /// </summary>
        /// <param name="oxd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetupClient(OxdModel oxd)
        {
            Loadoxdsettings();
            var setupClientInputParams = new SetupClientParams();//Test
            var setupClientClient = new SetupClientClient();

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
                    setupClientResponse = setupClientClient.SetupClient(oxdHost, oxd.OxdPort, setupClientInputParams);


                //Setup Client using OXD Web
                if (oxd.ConnectionType == "web")
                    setupClientResponse = setupClientClient.SetupClient(oxd.HttpRestUrl, setupClientInputParams);


                SetConfigValues(oxd, setupClientResponse.Data.OxdId, setupClientResponse.Data.clientId, setupClientResponse.Data.clientSecret);

                return Json(new { oxdId = setupClientResponse.Data.OxdId, clientId = setupClientResponse.Data.clientId, clientSecret = setupClientResponse.Data.clientSecret });

            }
            else
            {
                return Json(new { oxdId = oxd.OxdId, clientId = clientid, clientSecret = clientsecret });
            }
        }


        /// <summary>
        /// Register a client. This is not required when SetupClient is used.
        /// ClientId and ClientSecret are required for OPs which don't support dynamic client registration (e.g. Google). For these OPs Register_Site is used instead of Setup_Client.
        /// </summary>
        /// <param name="oxd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegisterSite(OxdModel oxd)
        {
            Loadoxdsettings();
            var registerSiteInputParams = new RegisterSiteParams();//Test
            var registerSiteClient = new RegisterSiteClient();

            //prepare input params for Setup client
            registerSiteInputParams.AuthorizationRedirectUri = oxd.RedirectUrl;
            registerSiteInputParams.OpHost = oxd.OpHost;
            registerSiteInputParams.PostLogoutRedirectUri = oxd.PostLogoutRedirectUrl;
            registerSiteInputParams.ClientName = oxd.ClientName;
            registerSiteInputParams.Scope = scope;
            registerSiteInputParams.GrantTypes = grant_types;
            registerSiteInputParams.ClientId = oxd.ClientId;
            registerSiteInputParams.ClientSecret = oxd.ClientSecret;
            registerSiteInputParams.ClaimsRedirecturi = new List<string> { ConfigurationManager.AppSettings["ClaimsRedirectURI"] };

            var registerSiteResponse = new RegisterSiteResponse();

            if (string.IsNullOrEmpty(oxd.OxdId))
            {
                if (oxd.ConnectionType == "local")
                {
                    //registerSiteInputParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxd.OxdPort);//Keep this line if protect_commands_with_access_token is set True for oxd-server
                    registerSiteResponse = registerSiteClient.RegisterSite(oxdHost, oxd.OxdPort, registerSiteInputParams);
                }


                if (oxd.ConnectionType == "web")
                {
                    registerSiteInputParams.ProtectionAccessToken = GetProtectionAccessToken(oxd.HttpRestUrl);
                    registerSiteResponse = registerSiteClient.RegisterSite(oxd.HttpRestUrl, registerSiteInputParams);
                }


                SetConfigValues(oxd, registerSiteResponse.Data.OxdId, oxd.ClientId, oxd.ClientSecret);

                return Json(new { oxdId = registerSiteResponse.Data.OxdId, clientId = oxd.ClientId, clientSecret = oxd.ClientSecret });

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
        public void SetConfigValues(OxdModel oxd, string oxd_id, string client_id, string client_secret)
        {

            string configObjString = System.IO.File.ReadAllText(Server.MapPath(@ConfigurationManager.AppSettings["oxd_config"]));
            OxdSetting oxdsettings = JsonConvert.DeserializeObject<OxdSetting>(configObjString);
            oxdsettings.AuthUrl = oxd.RedirectUrl;
            oxdsettings.OpHost = oxd.OpHost;
            oxdsettings.PostLogoutRedirectUrl = oxd.PostLogoutRedirectUrl;
            oxdsettings.ClientName = oxd.ClientName;
            oxdsettings.ConnectionType = oxd.ConnectionType;
            oxdsettings.OxdId = oxd_id;
            oxdsettings.ClientId = client_id;
            oxdsettings.ClientSecret = client_secret;

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

                if (oxd.ConnectionType == "local")
                {
                    //updateSiteInputParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxd.OxdPort);//Keep this line if protect_commands_with_access_token is set True for oxd-server
                    updateSiteResponse = updateSiteClient.UpdateSiteRegistration(oxdHost, oxd.OxdPort, updateSiteInputParams);
                }

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

        [HttpPost]
        public ActionResult IntrospectAccessToken(OxdModel oxd)
        {
            Loadoxdsettings();
            
            var introspectAccessTokenParams = new IntrospectAccessTokenParams();
            var introspectAccessTokenClient = new IntrospectAccessTokenClient();

            introspectAccessTokenParams.OxdId = oxd.OxdId;
            
            var introspectAccessTokenResponse = new IntrospectAccessTokenResponse();

            if (oxd.ConnectionType == "local")
            {
                introspectAccessTokenParams.AccessToken = GetProtectionAccessToken(oxdHost, oxd.OxdPort);
                introspectAccessTokenResponse = introspectAccessTokenClient.IntrospectAccessToken(oxdHost, oxd.OxdPort, introspectAccessTokenParams);
            }

            if (oxd.ConnectionType == "web")
            {
                introspectAccessTokenParams.AccessToken = GetProtectionAccessToken(oxd.HttpRestUrl);
                introspectAccessTokenResponse = introspectAccessTokenClient.IntrospectAccessToken(oxd.HttpRestUrl, introspectAccessTokenParams);
            }

            
            //Process Response
            return Json(new { status = introspectAccessTokenResponse.Status });

        }

        /// <summary>
        /// delete client from oxd server and settings json file
        /// </summary>
        /// <param name="oxd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(OxdModel oxd)
        {
            Loadoxdsettings();

            string deleteStatus = "ok";

            if (dynamic_registration)
            {
                var removeSiteInputParams = new RemoveSiteParams();
                var removeSiteClient = new RemoveSiteClient();

                removeSiteInputParams.OxdId = oxd.OxdId;

                var removeSiteResponse = new RemoveSiteResponse();

                if (oxd.ConnectionType == "local")
                {
                    //removeSiteInputParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxd.OxdPort);//Keep this line if protect_commands_with_access_token is set True for oxd-server
                    removeSiteResponse = removeSiteClient.RemoveSite(oxdHost, oxd.OxdPort, removeSiteInputParams);
                }

                if (oxd.ConnectionType == "web")
                {
                    removeSiteInputParams.ProtectionAccessToken = GetProtectionAccessToken(oxd.HttpRestUrl);
                    removeSiteResponse = removeSiteClient.RemoveSite(oxd.HttpRestUrl, removeSiteInputParams);
                }


                deleteStatus = removeSiteResponse.Status;
            }

            Deleteoxdsettings();


            //Process Response
            return Json(new { status = deleteStatus });

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

            if (OXDType == "local")
            {
                //Keep this if protect_commands_with_access_token is set True for oxd-server
                //if (dynamic_registration)
                //    getAuthUrlInputParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);
                
                getAuthUrlResponse = getAuthUrlClient.GetAuthorizationURL(oxdHost, oxdPort, getAuthUrlInputParams);
            }

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
            if (code == null && state == null)
            {
                return RedirectToAction("Index");
            }

            OxdModel oxd = new OxdModel();

            #region GetProtectionAccessToken
            string protectionAccessToken = "";

            if (dynamic_registration)
            {
                if (OXDType == "local")
                {
                    protectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);
                }

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

            var getTokensByCodeResponse = new GetTokensByCodeResponse();

            if (OXDType == "local")
            {
                //getTokenByCodeInputParams.ProtectionAccessToken = protectionAccessToken;//Keep this line if protect_commands_with_access_token is set True for oxd-server
                getTokensByCodeResponse = getTokenByCodeClient.GetTokensByCode(oxdHost, oxdPort, getTokenByCodeInputParams);
            }

            if (OXDType == "web")
            {
                getTokenByCodeInputParams.ProtectionAccessToken = protectionAccessToken;
                getTokensByCodeResponse = getTokenByCodeClient.GetTokensByCode(httpresturl, getTokenByCodeInputParams);
            }


            var accessToken = getTokensByCodeResponse.Data.AccessToken;
            var refreshToken = getTokensByCodeResponse.Data.RefreshToken;
            #endregion


            #region Get Access Token By Refresh Token
            if (dynamic_registration)
            {
                #region Introspect
                // Introspect the access token which will return the token status (Active=True/False)

                var introspectAccessTokenParams = new IntrospectAccessTokenParams();
                var introspectAccessTokenClient = new IntrospectAccessTokenClient();

                introspectAccessTokenParams.OxdId = oxd_id;
                introspectAccessTokenParams.AccessToken = accessToken;

                var introspectAccessTokenResponse = new IntrospectAccessTokenResponse();

                if (OXDType == "local")
                {
                    introspectAccessTokenResponse = introspectAccessTokenClient.IntrospectAccessToken(oxdHost, oxdPort, introspectAccessTokenParams);
                }

                if (OXDType == "web")
                {
                    introspectAccessTokenResponse = introspectAccessTokenClient.IntrospectAccessToken(httpresturl, introspectAccessTokenParams);
                }
                #endregion

                // If the access_token is not active get a fresh access_token by using the refresh_token
                if (!introspectAccessTokenResponse.Data.Active)
                {

                    var getAccessTokenByRefreshTokenInputParams = new GetAccessTokenByRefreshTokenParams();
                    var getAccessTokenByRefreshTokenClient = new GetAccessTokenByRefreshTokenClient();

                    //prepare input params for Getting Tokens from a site
                    getAccessTokenByRefreshTokenInputParams.OxdId = oxd_id;
                    getAccessTokenByRefreshTokenInputParams.RefreshToken = refreshToken;
                    
                    var getAccessTokenByRefreshTokenResponse = new GetAccessTokenByRefreshTokenResponse();

                    if (OXDType == "local")
                    {
                        //getAccessTokenByRefreshTokenInputParams.ProtectionAccessToken = protectionAccessToken;//Keep this line if protect_commands_with_access_token is set True for oxd-server
                        getAccessTokenByRefreshTokenResponse = getAccessTokenByRefreshTokenClient.GetAccessTokenByRefreshToken(oxdHost, oxdPort, getAccessTokenByRefreshTokenInputParams);
                    }

                    if (OXDType == "web")
                    {
                        getAccessTokenByRefreshTokenInputParams.ProtectionAccessToken = protectionAccessToken;
                        getAccessTokenByRefreshTokenResponse = getAccessTokenByRefreshTokenClient.GetAccessTokenByRefreshToken(httpresturl, getAccessTokenByRefreshTokenInputParams);
                    }


                    accessToken = getAccessTokenByRefreshTokenResponse.Data.AccessToken;
                    refreshToken = getAccessTokenByRefreshTokenResponse.Data.RefreshToken;
                }
            }
            #endregion


            #region Get User Info

            var getUserInfoInputParams = new GetUserInfoParams();
            var getUserInfoClient = new GetUserInfoClient();

            //prepare input params for Getting User Info from a site
            getUserInfoInputParams.OxdId = oxd_id;
            getUserInfoInputParams.AccessToken = accessToken;
            
            var getUserInfoResponse = new GetUserInfoResponse();

            if (OXDType == "local")
            {
                //getUserInfoInputParams.ProtectionAccessToken = protectionAccessToken;//Keep this line if protect_commands_with_access_token is set True for oxd-server
                getUserInfoResponse = getUserInfoClient.GetUserInfo(oxdHost, oxdPort, getUserInfoInputParams);
            }

            if (OXDType == "web")
            {
                getUserInfoInputParams.ProtectionAccessToken = protectionAccessToken;
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

            if (OXDType == "local")
            {
                //Keep this if protect_commands_with_access_token is set True for oxd-server
                //if (dynamic_registration)
                //    getLogoutUriInputParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);
                
                getLogoutUriResponse = getLogoutUriClient.GetLogoutURL(oxdHost, oxdPort, getLogoutUriInputParams);
            }

            if (OXDType == "web")
            {
                if (dynamic_registration)
                    getLogoutUriInputParams.ProtectionAccessToken = GetProtectionAccessToken(httpresturl);
                
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


        public ActionResult UMA()
        {
            Loadoxdsettings();
            return View();
        }


        #region UMA methods for functionality Test

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
                    Path = "/GetAll",
                    ProtectConditions = new List<ProtectCondition>
                    {
                        new ProtectCondition
                        {
                            HttpMethods = new List<string> { "GET" },
                            //Scopes = new List<string>{ "https://client.example.com:44300/api" },
                            //TicketScopes = new List<string>{ "https://client.example.com:44300/api" },
                            ScopeExpressions = new ScopeExpression
                            {
                                Rule = JsonConvert.DeserializeObject("{'and':[{'or':[{'var':0},{'var':1}]},{'var':2}]}"),
                                Data = new List<string>{"http://photoz.example.com/dev/actions/a1","http://photoz.example.com/dev/actions/a2","http://photoz.example.com/dev/actions/a3" }
                            }
                        }
                    }
                }
            };

            var protectResponse = new UmaRsProtectResponse();

            if (OXDType == "local")
            {
                //protectParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);//Keep this line if protect_commands_with_access_token is set True for oxd-server
                protectResponse = protectClient.ProtectResources(oxdHost, oxdPort, protectParams);
            }

            if (OXDType == "web")
            {
                protectParams.ProtectionAccessToken = GetProtectionAccessToken(httpresturl);
                protectResponse = protectClient.ProtectResources(httpresturl, protectParams);
            }

            //process response
            if (protectResponse.Status.ToLower().Equals("ok"))
            {
                return Json(new { Response = protectResponse.Status });
            }

            throw new Exception("Protecting Resource is not successful. Check OXD Server log for error details.");
        }

        [HttpPost]
        public ActionResult CheckAccess()
        {
            var checkAccessParams = new UmaRsCheckAccessParams();
            var checkAccessClient = new UmaRsCheckAccessClient();

            string rpt = "";
            string path = "/GetAll";
            string httpMethod = "GET";

            if (Session["rpt"] != null)
                rpt = Session["rpt"].ToString();

            //prepare input params for Check Access
            checkAccessParams.OxdId = oxd_id;
            checkAccessParams.RPT = rpt;
            checkAccessParams.Path = path;
            checkAccessParams.HttpMethod = httpMethod;


            var checkAccessResponse = new UmaRsCheckAccessResponse();

            if (OXDType == "local")
            {
                //Get protection access token
                //checkAccessParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);//Keep this line if protect_commands_with_access_token is set True for oxd-server
                //Check Access
                checkAccessResponse = checkAccessClient.CheckAccess(oxdHost, oxdPort, checkAccessParams);
            }

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

            string state = "";
            if (Session["uma_state"] != null)
                state = Session["uma_state"].ToString();

            //prepare input params for Protect Resource
            getRptParams.OxdId = oxd_id;
            getRptParams.ticket = ticket;
            getRptParams.state = state;


            var getRptResponse = new GetRPTResponse();

            if (OXDType == "local")
            {
                //Get protection access token
                //getRptParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);//Keep this line if protect_commands_with_access_token is set True for oxd-server
                //Get RPT
                getRptResponse = getRptClient.GetRPT(oxdHost, oxdPort, getRptParams);
            }

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

            if (getRptResponse.Status.ToLower().Equals("error") && getRptResponse.Data.Error.ToLower().Equals("need_info"))
            {
                Session["uma_ticket"] = getRptResponse.Data.Details.Ticket;
                return Json(new { Response = "error: need_info" });
            }

            throw new Exception("Obtaining RPT is not successful. Check OXD Server log for error details.");
        }

        [HttpPost]
        public ActionResult IntrospectRPT()
        {
            var umaIntrospectRptParams = new UmaIntrospectRptParams();
            var umaIntrospectRptClient = new UmaIntrospectRptClient();

            string rpt = "";
            if (Session["rpt"] != null)
            {
                rpt = Session["rpt"].ToString();

                //prepare input params for Protect Resource
                umaIntrospectRptParams.OxdId = oxd_id;
                umaIntrospectRptParams.RPT = rpt;

                var umaIntrospectRptResponse = new UmaIntrospectRptResponse();

                if (OXDType == "local")
                {
                    umaIntrospectRptResponse = umaIntrospectRptClient.IntrospectRpt(oxdHost, oxdPort, umaIntrospectRptParams);
                }

                if (OXDType == "web")
                {
                    umaIntrospectRptResponse = umaIntrospectRptClient.IntrospectRpt(httpresturl, umaIntrospectRptParams);
                }

                return Json(new { Response = JsonConvert.SerializeObject(umaIntrospectRptResponse.Data) });
            }
            else
            {
                return Json(new { Response = "RPT is null. Pass valid RPT and try again" });
            }
            

            throw new Exception("Introspecting RPT is not successful. Check oxd Server log for error details.");
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
            getClaimsGatheringUrlParams.ClaimsRedirectURI = ConfigurationManager.AppSettings["ClaimsRedirectURI"];


            var getClaimsGatheringUrlResponse = new UmaRpGetClaimsGatheringUrlResponse();

            if (OXDType == "local")
            {
                //Get protection access token
                //getClaimsGatheringUrlParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);//Keep this line if protect_commands_with_access_token is set True for oxd-server
                //Authorize RPT
                getClaimsGatheringUrlResponse = getClaimsGatheringUrlClient.GetClaimsGatheringUrl(oxdHost, oxdPort, getClaimsGatheringUrlParams);
            }

            if (OXDType == "web")
            {
                //Get protection access token
                getClaimsGatheringUrlParams.ProtectionAccessToken = GetProtectionAccessToken(httpresturl);
                //Authorize RPT
                getClaimsGatheringUrlResponse = getClaimsGatheringUrlClient.GetClaimsGatheringUrl(httpresturl, getClaimsGatheringUrlParams);
            }

            //process response
            return Json(new { Response = getClaimsGatheringUrlResponse.Data.url });

        }

        #endregion


        #region UMA Implementation

        public ActionResult GetUMAClaims()
        {
            Session["uma_state"] = Request.Params["state"];
            Session["uma_ticket"] = Request.Params["ticket"];

            return RedirectToAction("Resource");
            //Response.Redirect("/UMA");
        }

        public ActionResult Resource()
        {
            if (Session["uma_state"] != null && Session["uma_ticket"] != null)
            {
                string rptStatus = GetRpt();

                if (rptStatus == "error")
                {
                    ViewBag.Resource = "Error while accessing the Resource.";
                    return View();
                }

                var checkAccessParams = new UmaRsCheckAccessParams();
                string endpoint = ConfigurationManager.AppSettings["ResourceEndpoint"];
                var method = HttpVerb.GET;
                var request = new MakeWebRequest<UmaRsCheckAccessParams>();

                var response = string.Empty;
                if (rptStatus == "got_ticket")
                {
                    response = GetClaims();
                }
                else if (rptStatus == "got_rpt")
                {
                    if (IsRptActive(Session["rpt"].ToString()))
                        response = request.MakeRequest(endpoint, method, Session["rpt"].ToString(), checkAccessParams);
                    else
                        response = "Inactive RPT";
                }

                ViewBag.Resource = response;
                Session.Clear();
            }
            else
            {
                ViewBag.Resource = "No Resource fetched.";
            }

            return View();
        }

        [HttpPost]
        public ActionResult GetResource()
        {

            //RS Check Access----------
            var checkAccessParams = new UmaRsCheckAccessParams();

            string endpoint = ConfigurationManager.AppSettings["ResourceEndpoint"];
            var method = HttpVerb.GET;

            var request = new MakeWebRequest<UmaRsCheckAccessParams>();

            var response = request.MakeRequest(endpoint, method, "", checkAccessParams);

            string ticketResponse = GetTicket(response);
            Session["uma_ticket"] = ticketResponse;
            //--------RS Check Access


            if (ticketResponse == "Authorized Resource")
                return Json(new { Response = response, action = "" });
            else if (ticketResponse == "Error")
                return Json(new { Response = "Error occurred", action = "" });


            //RP Get RPT--------------
            string rptStatus = GetRpt();

            if (rptStatus == "got_ticket")
            {
                response = GetClaims();
            }
            else if (rptStatus == "got_rpt")
            {
                if (IsRptActive(Session["rpt"].ToString()))
                {
                    response = request.MakeRequest(endpoint, method, Session["rpt"].ToString(), checkAccessParams);
                    return Json(new { Response = response, action = "" });
                }
                else
                {
                    return Json(new { Response = "Inactive RPT", action = "" });
                }
            }

            return Json(new { Response = response, action = "redirect" });

        }

        public bool IsRptActive(string rpt)
        {
            var umaIntrospectRptParams = new UmaIntrospectRptParams();
            var umaIntrospectRptClient = new UmaIntrospectRptClient();

            umaIntrospectRptParams.OxdId = oxd_id;
            umaIntrospectRptParams.RPT = rpt;

            var umaIntrospectRptResponse = new UmaIntrospectRptResponse();

            if (OXDType == "local")
            {
                umaIntrospectRptResponse = umaIntrospectRptClient.IntrospectRpt(oxdHost, oxdPort, umaIntrospectRptParams);
            }

            if (OXDType == "web")
            {
                umaIntrospectRptResponse = umaIntrospectRptClient.IntrospectRpt(httpresturl, umaIntrospectRptParams);
            }

            return umaIntrospectRptResponse.Data.Active;

        }

        private string GetTicket(string response)
        {
            if (response.Contains("Unauthorized"))
            {
                var respArray = response.Split(';');

                foreach (var ticketString in respArray)
                {
                    if (ticketString.Contains("ticket"))
                    {
                        var ticketArray = ticketString.Split(':');
                        return ticketArray[1];
                    }
                }
            }
            else if (response.Contains("Error occurred"))
            {
                return "Error";
            }

            return "Authorized Resource";
        }

        public string GetRpt()
        {
            var getRptParams = new UmaRpGetRptParams();
            var getRptClient = new UmaRpGetRptClient();

            string ticket = "";
            if (Session["uma_ticket"] != null)
            {
                ticket = Session["uma_ticket"].ToString();
                getRptParams.ticket = ticket;
            }

            string state = "";
            if (Session["uma_state"] != null)
            {
                state = Session["uma_state"].ToString();
                getRptParams.state = state;
            }


            //prepare input params for Protect Resource
            getRptParams.OxdId = oxd_id;
            //getRptParams.ticket = ticket;
            //getRptParams.state = state;


            var getRptResponse = new GetRPTResponse();

            if (OXDType == "local")
            {
                //getRptParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);//Keep this line if protect_commands_with_access_token is set True for oxd-server
                
                getRptResponse = getRptClient.GetRPT(oxdHost, oxdPort, getRptParams);
            }

            if (OXDType == "web")
            {
                getRptParams.ProtectionAccessToken = GetProtectionAccessToken(httpresturl);
                
                getRptResponse = getRptClient.GetRPT(httpresturl, getRptParams);
            }
            string response = "error";
            //process response
            if (getRptResponse.Status.ToLower().Equals("ok"))
            {
                Session["rpt"] = getRptResponse.Data.Rpt;
                response = "got_rpt";
            }

            if (getRptResponse.Status.ToLower().Equals("error") && getRptResponse.Data.Error.ToLower().Equals("need_info"))
            {
                Session["uma_ticket"] = getRptResponse.Data.Details.Ticket;
                response = "got_ticket";
            }

            return response;
        }

        public string GetClaims()
        {
            var getClaimsGatheringUrlParams = new UmaRpGetClaimsGatheringUrlParams();
            var getClaimsGatheringUrlClient = new UmaRpGetClaimsGatheringUrlClient();

            string ticket = "";
            if (Session["uma_ticket"] != null)
                ticket = Session["uma_ticket"].ToString();

            //prepare input params for Check Access
            getClaimsGatheringUrlParams.OxdId = oxd_id;
            getClaimsGatheringUrlParams.Ticket = ticket;
            getClaimsGatheringUrlParams.ClaimsRedirectURI = ConfigurationManager.AppSettings["ClaimsRedirectURI"];


            var getClaimsGatheringUrlResponse = new UmaRpGetClaimsGatheringUrlResponse();

            if (OXDType == "local")
            {
                //getClaimsGatheringUrlParams.ProtectionAccessToken = GetProtectionAccessToken(oxdHost, oxdPort);//Keep this line if protect_commands_with_access_token is set True for oxd-server
                getClaimsGatheringUrlResponse = getClaimsGatheringUrlClient.GetClaimsGatheringUrl(oxdHost, oxdPort, getClaimsGatheringUrlParams);
            }

            if (OXDType == "web")
            {
                getClaimsGatheringUrlParams.ProtectionAccessToken = GetProtectionAccessToken(httpresturl);
                getClaimsGatheringUrlResponse = getClaimsGatheringUrlClient.GetClaimsGatheringUrl(httpresturl, getClaimsGatheringUrlParams);
            }

            return getClaimsGatheringUrlResponse.Data.url;
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
