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

namespace GluuDemoWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(OxdModel oxd)
        {
            var registerSiteInputParams = new RegisterSiteParams();
            var registerSiteClient = new RegisterSiteClient();

            //prepare input params for Register Site
            registerSiteInputParams.AuthorizationRedirectUri = oxd.RedirectUrl;
            registerSiteInputParams.OpHost = "https://scim-test.gluu.org";
            registerSiteInputParams.ClientName = "VasOxdTestingClient-CanBeRemoved";
            registerSiteInputParams.Scope = new List<string> { "openid", "uma_protection", "uma_authorization" };
            
            //Register Site
            var registerSiteResponse = registerSiteClient.RegisterSite(oxd.OxdHost, oxd.OxdPort, registerSiteInputParams);

            //Process the response
            return Json(new { oxdId = registerSiteResponse.Data.OxdId });
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

            //Update Site Registration
            var updateSiteResponse = updateSiteClient.UpdateSiteRegistration(oxd.OxdHost, oxd.OxdPort, updateSiteInputParams);

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

            //Get Auth URL
            var getAuthUrlResponse = getAuthUrlClient.GetAuthorizationURL(oxd.OxdHost, oxd.OxdPort, getAuthUrlInputParams);

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

            //Get Tokens by Code
            var getTokensByCodeResponse = getTokenByCodeClient.GetTokensByCode(oxd.OxdHost, oxd.OxdPort, getTokenByCodeInputParams);

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

            //Get User Info
            var getUserInfoResponse = getUserInfoClient.GetUserInfo(oxd.OxdHost, oxd.OxdPort, getUserInfoInputParams);

            //Process response
            var userName = getUserInfoResponse.Data.UserClaims.Name.First();
            var userEmail = getUserInfoResponse.Data.UserClaims.Email == null ? string.Empty : getUserInfoResponse.Data.UserClaims.Email.FirstOrDefault();

            return Json(new { userName = userName });
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

            //Get Logout URI
            var getLogoutUriResponse = getLogoutUriClient.GetLogoutURL(oxd.OxdHost, oxd.OxdPort, getLogoutUriInputParams);

            //Process response
            return Json(new { logoutUri = getLogoutUriResponse.Data.LogoutUri });
        }

        public ActionResult CallBack(string code, string state, string scope)
        {
            return Json(new { authCode = code, authState = state, scope = scope }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}