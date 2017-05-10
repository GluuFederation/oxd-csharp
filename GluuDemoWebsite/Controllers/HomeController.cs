using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GluuDemoWebsite.Models;
using CSharp.CommonClasses;
using oxdCSharp.CommandResponses;
using oxdCSharp.Clients;
using oxdCSharp.CommandParameters;
using System.Configuration;

namespace GluuDemoWebsite.Controllers
{
    public class HomeController : Controller
    {
        string httpresturl = ConfigurationManager.AppSettings["oxdtohttp"];

        public ActionResult Index()
        {
            return View();
        }

        #region Main OXD Actions

        [HttpPost]
        public ActionResult Register(OxdModel oxd)
        {
            var registerSiteInputParams = new RegisterSiteParams();
            var registerSiteClient = new RegisterSiteClient();

            //prepare input params for Register Site
            registerSiteInputParams.AuthorizationRedirectUri = oxd.RedirectUrl;
            registerSiteInputParams.OpHost = ConfigurationManager.AppSettings["GluuServerUrl"]; 
            registerSiteInputParams.ClientName = ConfigurationManager.AppSettings["OxdClientName"];
            registerSiteInputParams.Scope = new List<string> { "openid", "profile", "email", "uma_protection", "uma_authorization" };

            oxd.OxdHost = ConfigurationManager.AppSettings["oxdhost"];//OXD-Server host IP addresss
            oxd.OxdPort = Convert.ToInt32(ConfigurationManager.AppSettings["oxdport"]) ;//OXD-Server host listening port number

            if (Session["oxdId"]==null)
            Session["oxdId"]= ConfigurationManager.AppSettings["oxdid"];
            oxd.OxdId = Session["oxdId"].ToString();

            if (string.IsNullOrEmpty(oxd.OxdId))
            {
               var registerSiteResponse = registerSiteClient.RegisterSite(oxd.OxdHost, oxd.OxdPort, registerSiteInputParams);
               // var registerSiteResponse = registerSiteClient.RegisterSite(httpresturl, registerSiteInputParams);
                Session["oxdId"] = registerSiteResponse.Data.OxdId;
                return Json(new { oxdId = Session["oxdId"] });
            }
            else {
                return Json(new { oxdId = oxd.OxdId });
            }


            //Process the response
           
        }

        [HttpPost]
        public ActionResult Update(OxdModel oxd)
        {
            var updateSiteInputParams = new UpdateSiteParams();
            var updateSiteClient = new UpdateSiteRegistrationClient();

            //prepare input params for Update Site Registration
            updateSiteInputParams.OxdId = oxd.OxdId;
            updateSiteInputParams.Contacts = new List<string> { oxd.OxdEmail };
            updateSiteInputParams.PostLogoutRedirectUri = oxd.PostLogoutRedirectUrl;

            oxd.OxdHost = ConfigurationManager.AppSettings["oxdhost"];//OXD-Server host IP addresss
            oxd.OxdPort = Convert.ToInt32(ConfigurationManager.AppSettings["oxdport"]);//OXD-Server host listening port number

            //Update Site Registration
            var updateSiteResponse = updateSiteClient.UpdateSiteRegistration(oxd.OxdHost, oxd.OxdPort, updateSiteInputParams);
           // var updateSiteResponse = updateSiteClient.UpdateSiteRegistration(httpresturl, updateSiteInputParams);

            //Process the response
            return Json(new { status = updateSiteResponse.Status });
        }

        [HttpPost]
        public ActionResult AuthUrl(OxdModel oxd)
        {
            var getAuthUrlInputParams = new GetAuthorizationUrlParams();
            var getAuthUrlClient = new GetAuthorizationUrlClient();

            //prepare input params for Getting Auth URL from a site
            getAuthUrlInputParams.OxdId = oxd.OxdId;

            oxd.OxdHost = ConfigurationManager.AppSettings["oxdhost"];//OXD-Server host IP addresss
            oxd.OxdPort = Convert.ToInt32(ConfigurationManager.AppSettings["oxdport"]);//OXD-Server host listening port number

            //Get Auth URL
            // var getAuthUrlResponse = getAuthUrlClient.GetAuthorizationURL(oxd.OxdHost, oxd.OxdPort, getAuthUrlInputParams);
            var getAuthUrlResponse = getAuthUrlClient.GetAuthorizationURL(httpresturl, getAuthUrlInputParams);
            //Process Response
            return Json(new { authUrl = getAuthUrlResponse.Data.AuthorizationUrl });
        }

        [HttpPost]
        public ActionResult GetTokens(OxdModel oxd)
        {
            var getTokenByCodeInputParams = new GetTokensByCodeParams();
            var getTokenByCodeClient = new GetTokensByCodeClient();

            //prepare input params for Getting Tokens from a site
            getTokenByCodeInputParams.OxdId = oxd.OxdId;
            getTokenByCodeInputParams.Code = oxd.AuthCode;
            getTokenByCodeInputParams.State = oxd.AuthState;

            //SET OXD Server host and Port 
            oxd.OxdHost = ConfigurationManager.AppSettings["oxdhost"];//OXD-Server host IP addresss
            oxd.OxdPort = Convert.ToInt32(ConfigurationManager.AppSettings["oxdport"]);//OXD-Server host listening port number

            //Get Tokens by Code
            //var getTokensByCodeResponse = getTokenByCodeClient.GetTokensByCode(oxd.OxdHost, oxd.OxdPort, getTokenByCodeInputParams);
            var getTokensByCodeResponse = getTokenByCodeClient.GetTokensByCode(httpresturl, getTokenByCodeInputParams);

            //Process response
            return Json(new { accessToken = getTokensByCodeResponse.Data.AccessToken, refreshToken = getTokensByCodeResponse.Data.RefreshToken });
        }

        [HttpPost]
        public ActionResult GetUserInfo(OxdModel oxd)
        {
            var getUserInfoInputParams = new GetUserInfoParams();
            var getUserInfoClient = new GetUserInfoClient();

            //prepare input params for Getting User Info from a site
            getUserInfoInputParams.OxdId = oxd.OxdId;
            getUserInfoInputParams.AccessToken = oxd.AccessToken;

            //SET OXD Server host and Port 
            oxd.OxdHost = ConfigurationManager.AppSettings["oxdhost"];//OXD-Server host IP addresss
            oxd.OxdPort = Convert.ToInt32(ConfigurationManager.AppSettings["oxdport"]);//OXD-Server host listening port number

            //Get User Info
            //var getUserInfoResponse = getUserInfoClient.GetUserInfo(oxd.OxdHost, oxd.OxdPort, getUserInfoInputParams);
            var getUserInfoResponse = getUserInfoClient.GetUserInfo(httpresturl, getUserInfoInputParams);

            //Process response
            var userName = getUserInfoResponse.Data.UserClaims.Name.First();
            var userEmail = getUserInfoResponse.Data.UserClaims.Email == null ? string.Empty : getUserInfoResponse.Data.UserClaims.Email.FirstOrDefault();

            return Json(new { userName = userName, userEmail= userEmail });
        }

        [HttpPost]
        public ActionResult FullUmaTest(OxdModel oxd)
        {
            #region Protecing Resources

            //1. Protect resources
            var protectResponse = ProtectResources(oxd);

            #endregion

            #region Checking access with empty RPT. Expected => Access Denied with valid Ticket

            //2. Check Access with empty RPT. Expect access denied and a valid ticket
            var checkAccessResponse = CheckAccess(string.Empty, "/scim", "GET", oxd);

            if(!checkAccessResponse.Data.Access.ToLower().Equals("denied"))
            {
                throw new Exception(string.Format("Access Denied expected. But something else {0} is coming.", checkAccessResponse.Data.Access));
            }

            var ticket = checkAccessResponse.Data.Ticket;

            if(string.IsNullOrEmpty(ticket))
            {
                throw new Exception("Expected valid ticket as part of check access. But Null or Empty returned.");
            }

            #endregion

            #region Obtaining valid RPT. Expected => A valid RPT

            //3. Obtain RPT. Expect a valid RPT is returned.
            var getRptResponse = ObtainRpt(oxd);

            var rpt = getRptResponse.Data.Rpt;

            if(string.IsNullOrEmpty(rpt))
            {
                throw new Exception("Tryting to obtain RPT. But Null or Empty returned.");
            }

            #endregion  

            #region Checking access with valid RPT. Expected => Access Denied

            //4. Check Access again with valid RPT. Still the access should be granted. Expect access denied
            checkAccessResponse = null;
            checkAccessResponse = CheckAccess(rpt, "/scim", "GET", oxd);

            if (!checkAccessResponse.Data.Access.ToLower().Equals("denied"))
            {
                throw new Exception(string.Format("Access Denied expected. But something else {0} is coming.", checkAccessResponse.Data.Access));
            }
            
            #endregion

            #region Authorizing RPT. Expected => Status Ok

            //5. Authorize RPT. Expect status should be ok
            var authorizeRptResponse = AuthorizeRpt(rpt, ticket, oxd);

            if(!authorizeRptResponse.Status.ToLower().Equals("ok"))
            {
                throw new Exception(string.Format("Trying to authorize rpt. But got the error as {0} and description is {1}", 
                    authorizeRptResponse.Data.AuthorizeErrorCode, authorizeRptResponse.Data.AuthorizeErrorDescription));
            }

            #endregion

            #region Check Access again after authorizing RPT. Exepcted => Access Granted.

            //6. Authorized RPT. Check Access again. Expect access granted.
            checkAccessResponse = null;
            checkAccessResponse = CheckAccess(rpt, "/scim", "GET", oxd);

            if(!checkAccessResponse.Data.Access.ToLower().Equals("granted"))
            {
                throw new Exception("Access is not granted even after authorizing the RPT.");
            }

            #endregion

            return Json(new { fullTestStatus = "success" });
        }

        [HttpPost]
        public ActionResult GetGat(OxdModel oxd)
        {
            var getGatInputParams = new GetGATParams();
            var getGatClient = new GetGATClient();

            //prepare input params for Getting GAT
            getGatInputParams.OxdId = oxd.OxdId;
            getGatInputParams.Scopes = new List<string> {
                                            "https://scim-test.gluu.org/identity/seam/resource/restv1/scim/vas1",
                                            "https://scim-test.gluu.org/identity/seam/resource/restv1/scim/vas2" };

            //Get GAT
            var getGatResponse = getGatClient.GetGat(oxd.OxdHost, oxd.OxdPort, getGatInputParams);

            //Process response
            return Json(new { getGatResponse = getGatResponse.Data.Rpt });
        }

        [HttpPost]
        public ActionResult GetLogoutUri(OxdModel oxd)
        {
            var getLogoutUriInputParams = new GetLogoutUrlParams();
            var getLogoutUriClient = new GetLogoutUriClient();

            //prepare input params for Getting Logout URI from a site
            getLogoutUriInputParams.OxdId = oxd.OxdId;


            //SET OXD Server host and Port 
            oxd.OxdHost = ConfigurationManager.AppSettings["oxdhost"];//OXD-Server host IP addresss
            oxd.OxdPort = Convert.ToInt32(ConfigurationManager.AppSettings["oxdport"]);//OXD-Server host listening port number

            //Get Logout URI
            //var getLogoutUriResponse = getLogoutUriClient.GetLogoutURL(oxd.OxdHost, oxd.OxdPort, getLogoutUriInputParams);
            var getLogoutUriResponse = getLogoutUriClient.GetLogoutURL(httpresturl, getLogoutUriInputParams);

            //Process response
            return Json(new { logoutUri = getLogoutUriResponse.Data.LogoutUri });
        }

        public ActionResult CallBack(string code, string state, string scope)
        {
            return Json(new { authCode = code, authState = state, scope = scope }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region UMA related methods

        private UmaRsProtectResponse ProtectResources(OxdModel oxdModel)
        {
            var protectParams = new UmaRsProtectParams();
            var protectClient = new UmaRsProtectClient();

            //prepare input params for Protect Resource
            protectParams.OxdId = oxdModel.OxdId;
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

            //Protect Resources
            var protectResponse = protectClient.ProtectResources(oxdModel.OxdHost, oxdModel.OxdPort, protectParams);

            //process response
            if(protectResponse.Status.ToLower().Equals("ok"))
            {
                return protectResponse;
            }

            throw new Exception("Procteting Resource is not successful. Check OXD Server log for error details.");
        }

        private UmaRsCheckAccessResponse CheckAccess(string rpt, string path, string httpMethod, OxdModel oxdModel)
        {
            var checkAccessParams = new UmaRsCheckAccessParams();
            var checkAccessClient = new UmaRsCheckAccessClient();

            //prepare input params for Check Access
            checkAccessParams.OxdId = oxdModel.OxdId;
            checkAccessParams.RPT = rpt;
            checkAccessParams.Path = path;
            checkAccessParams.HttpMethod = httpMethod;

            //Check Access
            var checkAccessResponse = checkAccessClient.CheckAccess(oxdModel.OxdHost, oxdModel.OxdPort, checkAccessParams);

            //process response
            return checkAccessResponse;
        }

        private GetRPTResponse ObtainRpt(OxdModel oxdModel)
        {
            var getRptParams = new UmaRpGetRptParams();
            var getRptClient = new UmaRpGetRptClient();

            //prepare input params for Protect Resource
            getRptParams.OxdId = oxdModel.OxdId;
            getRptParams.ForceNew = false;

            //Get RPT
            var getRptResponse = getRptClient.GetRPT(oxdModel.OxdHost, oxdModel.OxdPort, getRptParams);

            //process response
            if (getRptResponse.Status.ToLower().Equals("ok"))
            {
                return getRptResponse;
            }

            throw new Exception("Obtaining RPT is not successful. Check OXD Server log for error details.");
        }

        private UmaRpAuthorizeRptResponse AuthorizeRpt(string rpt, string ticket, OxdModel oxdModel)
        {
            var authorizeRptParams = new UmaRpAuthorizeRptParams();
            var authorizeRptClient = new UmaRpAuthorizeRptClient();

            //prepare input params for Check Access
            authorizeRptParams.OxdId = oxdModel.OxdId;
            authorizeRptParams.RPT = rpt;
            authorizeRptParams.Ticket = ticket;

            //Authorize RPT
            var authorizeRptResponse = authorizeRptClient.AuthorizeRpt(oxdModel.OxdHost, oxdModel.OxdPort, authorizeRptParams);

            //process response
            return authorizeRptResponse;
        }

        #endregion
    }
}