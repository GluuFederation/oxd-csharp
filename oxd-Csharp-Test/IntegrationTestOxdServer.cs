using NUnit.Framework;
using oxdCSharp.Clients;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using oxdCSharp.UMA.CommandParameters;
using System.Collections.Generic;

namespace oxd_Csharp_Test
{
    [TestFixture]
    public class IntegrationTestOxdServer
    {

        //[Test]
        //public void TestRegisterSite_oxd_server()
        //{
        //    intializeRegisterParameter();

        //    var RegisterSite = new RegisterSiteClient();
        //    RegisterSiteResponse registerSiteResponse = RegisterSite.RegisterSite(oxdHost, oxdPort, registerSiteParams);
        //    Assert.That(registerSiteResponse, Is.Not.Null);
        //    Assert.That(registerSiteResponse.Data.OxdId, Is.Not.Null);

        //    var authorizationUrlParams = new GetAuthorizationUrlParams();
        //    authorizationUrlParams.OxdId = registerSiteResponse.Data.OxdId;
        //    authorizationUrlParams.Scope = new List<string> { "openid", "profile" };


        //    var authorizationUrlclient = new GetAuthorizationUrlClient();
        //    GetAuthorizationUrlResponse authorizationurlresponse = authorizationUrlclient.GetAuthorizationURL(oxdHost, oxdPort, authorizationUrlParams);

        //    Assert.That(authorizationurlresponse, Is.Not.Null);
        //    Assert.That(authorizationurlresponse.Data.AuthorizationUrl, Is.Not.Null);
        //}

        private static SetupClientParams setupClientParams;
        private static GetClientTokenParams getClientTokenParams;
        private static RegisterSiteParams registerSiteParams;
        private static UpdateSiteParams updateSiteParams;
        private static GetAuthorizationUrlParams authorizationUrlParams;
        private static GetTokensByCodeParams getTokensByCodeParams;
        private static GetAccessTokenByRefreshTokenParams getAccessTokenByRefreshTokenParams;
        private static GetUserInfoParams getUserInfoParams;
        private static GetLogoutUrlParams getLogoutUrlParams;
        private static UmaRsProtectParams umaRsProtectParams;
        private static UmaRsCheckAccessParams checkAccessParams;
        private static UmaRpGetRptParams getRptParams;
        private static UmaRpGetClaimsGatheringUrlParams getClaimsGatheringUrlParams;

        private static string oxdHost = "127.0.0.1";
        private static int oxdPort = 8099;

        private void intializeRegisterParameter()
        {
            registerSiteParams = new RegisterSiteParams();
            registerSiteParams.OpHost = "https://<idp-hostname>";//Gluu host/your locally hosted gluu server can work
            registerSiteParams.AuthorizationRedirectUri = "https://client.example.com";
            registerSiteParams.Scope = new List<string> { "openid", "profile", "email", "uma_protection", "uma_authorization" };//Scope
            registerSiteParams.ClientName = "DotNet_Test";
        }
    }
}
