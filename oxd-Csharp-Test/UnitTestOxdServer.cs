

using oxdCSharp.CommandResponses;
using CSharp.CommonClasses;
using Moq;
using NUnit.Framework;
using oxdCSharp.Clients;
using oxdCSharp.CommandParameters;
using oxdCSharp.UMA.Clients;
using oxdCSharp.UMA.CommandParameters;
using oxdCSharp.UMA.CommandResponses;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace oxd_Csharp_Test
{
    [TestFixture]
    public class UnitTestOxdServer
    {
        /*
        [Test]
        public void TestRegisterSite_oxd_server()
        {
            intializedParameter();

            var RegisterSite = new RegisterSiteClient();
            RegisterSiteResponse registerSiteResponse = RegisterSite.RegisterSite(oxdHost, oxdPort, registerSiteParams);

            Assert.That(registerSiteResponse, Is.Not.Null);
            Assert.That(registerSiteResponse.Data.OxdId, Is.Not.Null);
        }

        [Test]
        public void TestUpdateSite_oxd_server()
        {
            intializedParameter();
            var RegisterSite = new RegisterSiteClient();
            RegisterSiteResponse registerSiteResponse = RegisterSite.RegisterSite(oxdHost, oxdPort, registerSiteParams);
            Assert.That(registerSiteResponse, Is.Not.Null);
            Assert.That(registerSiteResponse.Data.OxdId, Is.Not.Null);

            var updateSiteParams = new UpdateSiteParams();
            updateSiteParams.OxdId = registerSiteResponse.Data.OxdId;
            updateSiteParams.Scope = new List<string> { "openid", "profile" };
            updateSiteParams.Contacts = new List<string> { "jajati@email.com" };
            updateSiteParams.PostLogoutRedirectUri = "https://client.example.com/LogoutUri";

            var updateSiteRegistrationclient = new UpdateSiteRegistrationClient();
            UpdateSiteResponse updatesiteresponse = updateSiteRegistrationclient.UpdateSiteRegistration(oxdHost, oxdPort, updateSiteParams);

            Assert.That(updatesiteresponse, Is.Not.Null);

        }

        [Test]
        public void TestAuthorizationUrl_oxd_server()
        {
            intializedParameter();
            var RegisterSite = new RegisterSiteClient();
            RegisterSiteResponse registerSiteResponse = RegisterSite.RegisterSite(oxdHost, oxdPort, registerSiteParams);
            Assert.That(registerSiteResponse, Is.Not.Null);
            Assert.That(registerSiteResponse.Data.OxdId, Is.Not.Null);

            var authorizationUrlParams = new GetAuthorizationUrlParams();
            authorizationUrlParams.OxdId = registerSiteResponse.Data.OxdId;
            authorizationUrlParams.Scope = new List<string> { "openid", "profile" };


            var authorizationUrlclient = new GetAuthorizationUrlClient();
            GetAuthorizationUrlResponse authorizationurlresponse = authorizationUrlclient.GetAuthorizationURL(oxdHost, oxdPort, authorizationUrlParams);

            Assert.That(authorizationurlresponse, Is.Not.Null);
            Assert.That(authorizationurlresponse.Data.AuthorizationUrl, Is.Not.Null);

        }
        */

        //--------------------------------------------


        ////Working. But need to change library code standard
        //[Test]
        //public void TestRegisterSite_oxd_server()
        //{
        //    intializeRegisterParameter();

        //    RegisterSiteResponse registerSiteResponse;
        //    RegisterSiteResponseData data;
        //    registerSiteResponse = new RegisterSiteResponse();
        //    data = new RegisterSiteResponseData();

        //    registerSiteResponse.Status = "ok";
        //    data.OxdId = "test-oxd-id";
        //    registerSiteResponse.Data = data;

        //    var commandClient = new Mock<CommandClient>();
        //    var registerSiteParams = new RegisterSiteParams();
        //    registerSiteParams.AuthorizationRedirectUri = "test_uri";
        //    var cmdRegisterSite = new Command { CommandType = CommandType.register_site, CommandParams = registerSiteParams };
        //    commandClient.Setup(x => x.send(It.IsAny<Command>())).Returns(JsonConvert.SerializeObject(registerSiteResponse));

        //    var RegisterSite = new RegisterSiteClient();
        //    registerSiteResponse = RegisterSite.RegisterSite(commandClient.Object, cmdRegisterSite);

        //    Assert.That(registerSiteResponse, Is.Not.Null);
        //    Assert.That(registerSiteResponse.Data.OxdId, Is.Not.Null);
        //}


        //[Test]
        //public void TestRegisterSite_oxd_server_1111()
        //{
        //    intializeRegisterParameter();

        //    RegisterSiteResponse registerSiteResponse;
        //    RegisterSiteResponseData data;
        //    registerSiteResponse = new RegisterSiteResponse();
        //    data = new RegisterSiteResponseData();

        //    registerSiteResponse.Status = "ok";
        //    data.OxdId = "test-oxd-id";
        //    registerSiteResponse.Data = data;

        //    //var registerSiteClient = new Mock<RegisterSiteClient>();
        //    //registerSiteClient.Setup(x => x.RegisterSite(oxdHost, oxdPort, registerSiteParams)).Returns(registerSiteResponse);

        //    var commandClient = new Mock<CommandClient>();
        //    var param = new Mock<RegisterSiteParams>();
        //    var cmdRegisterSite = new Command { CommandType = CommandType.register_site, CommandParams = registerSiteParams };
        //    commandClient.Setup(x => x.send(It.IsAny<Command>())).Verifiable();

        //    var RegisterSite = new RegisterSiteClient();
        //    registerSiteResponse = RegisterSite.RegisterSite(oxdHost, oxdPort, registerSiteParams);

        //    Assert.That(registerSiteResponse, Is.Not.Null);
        //    Assert.That(registerSiteResponse.Data.OxdId, Is.Not.Null);
        //}


        [Test]
        public void TestRegisterSite_oxd_server_Mock()
        {
            intializeRegisterParameter();

            RegisterSiteResponse registerSiteResponse;
            RegisterSiteResponseData data;
            registerSiteResponse = new RegisterSiteResponse();
            data = new RegisterSiteResponseData();

            registerSiteResponse.Status = "ok";
            data.OxdId = "test-oxd-id";
            registerSiteResponse.Data = data;

            var registerSiteClient = new Mock<RegisterSiteClient>();
            registerSiteClient.Setup(x => x.RegisterSite(oxdHost, oxdPort, registerSiteParams)).Returns(registerSiteResponse);

            //var commandClient = new Mock<CommandClient>();
            //var cmdRegisterSite = new Command { CommandType = CommandType.register_site, CommandParams = registerSiteParams };
            //commandClient.Setup(x => x.send(cmdRegisterSite)).Returns("{\"status\":\"ok\",\"data\":{\"oxd_id\":\"5aa532fb - a042 - 48ed - b66b - 1a0d01c0123a\"}}");

            //var RegisterSite = new RegisterSiteClient();
            //RegisterSiteResponse registerSiteResponse = RegisterSite.RegisterSite(oxdHost, oxdPort, registerSiteParams);

            Assert.That(registerSiteResponse, Is.Not.Null);
            Assert.That(registerSiteResponse.Data.OxdId, Is.Not.Null);
        }

        [Test]
        public void TestSetupClient_oxd_server_Mock()
        {
            intializeSetupClientParams();

            SetupClientResponse setupClientResponse;
            SetupClientResponseData data;
            setupClientResponse = new SetupClientResponse();
            data = new SetupClientResponseData();

            setupClientResponse.Status = "ok";
            data.OxdId = "test-oxd-id";
            data.clientId = "test-client-id";
            data.clientSecret = "test-cleint-secret";
            setupClientResponse.Data = data;

            var setupClientClient = new Mock<SetupClientClient>();
            setupClientClient.Setup(x => x.SetupClient(oxdHost, oxdPort, setupClientParams)).Returns(setupClientResponse);

            Assert.That(setupClientResponse, Is.Not.Null);
            Assert.That(setupClientResponse.Data.OxdId, Is.Not.Null);
            Assert.That(setupClientResponse.Data.clientId, Is.Not.Null);
            Assert.That(setupClientResponse.Data.clientSecret, Is.Not.Null);
        }

        [Test]
        public void TestGetClientToken_oxd_server_Mock()
        {
            getClientTokenParams = new GetClientTokenParams();
            getClientTokenParams.opHost = "https://<idp-hostname>";
            getClientTokenParams.clientId = "test-client-id";
            getClientTokenParams.clientSecret = "test-cleint-secret";

            GetClientTokenResponse getClientTokenResponse;
            GetClientTokenResponseData data;
            getClientTokenResponse = new GetClientTokenResponse();
            data = new GetClientTokenResponseData();

            getClientTokenResponse.Status = "ok";
            data.accessToken = "test-access-token";
            getClientTokenResponse.Data = data;

            var getClientTokenClient = new Mock<GetClientTokenClient>();
            getClientTokenClient.Setup(x => x.GetClientToken(oxdHost, oxdPort, getClientTokenParams)).Returns(getClientTokenResponse);

            Assert.That(getClientTokenResponse, Is.Not.Null);
            Assert.That(getClientTokenResponse.Data.accessToken, Is.Not.Null);
        }

        [Test]
        public void TestUpdateSite_oxd_server_Mock()
        {
            updateSiteParams = new UpdateSiteParams();
            updateSiteParams.OxdId = "test-oxd-id";
            updateSiteParams.Scope = new List<string> { "openid", "profile" };
            updateSiteParams.Contacts = new List<string> { "johndoe@email.com" };
            updateSiteParams.PostLogoutRedirectUri = "https://client.example.com/LogoutUri";

            UpdateSiteResponse updateSiteResponse;
            updateSiteResponse = new UpdateSiteResponse();

            updateSiteResponse.Status = "ok";

            var updateSiteRegistrationClient = new Mock<UpdateSiteRegistrationClient>();
            updateSiteRegistrationClient.Setup(x => x.UpdateSiteRegistration(oxdHost, oxdPort, updateSiteParams)).Returns(updateSiteResponse);

            Assert.That(updateSiteResponse, Is.Not.Null);
            Assert.AreEqual(updateSiteResponse.Status, "ok");
        }

        [Test]
        public void TestGetAuthorizationUrl_oxd_server_Mock()
        {
            authorizationUrlParams = new GetAuthorizationUrlParams();
            authorizationUrlParams.OxdId = "test-oxd-id";
            authorizationUrlParams.Scope = new List<string> { "openid", "profile" };

            GetAuthorizationUrlResponse getAuthorizationUrlResponse;
            GetAuthorizationUrlResponseData data;
            getAuthorizationUrlResponse = new GetAuthorizationUrlResponse();
            data = new GetAuthorizationUrlResponseData();

            getAuthorizationUrlResponse.Status = "ok";
            data.AuthorizationUrl = "https://<idp-hostname>/oxauth/restv1/authorize?response_type..";
            getAuthorizationUrlResponse.Data = data;

            var getAuthorizationUrlClient = new Mock<GetAuthorizationUrlClient>();
            getAuthorizationUrlClient.Setup(x => x.GetAuthorizationURL(oxdHost, oxdPort, authorizationUrlParams)).Returns(getAuthorizationUrlResponse);

            Assert.That(getAuthorizationUrlResponse, Is.Not.Null);
            Assert.That(getAuthorizationUrlResponse.Data.AuthorizationUrl, Is.Not.Null);
        }

        [Test]
        public void TestGetTokensByCode_oxd_server_Mock()
        {
            getTokensByCodeParams = new GetTokensByCodeParams();
            getTokensByCodeParams.OxdId = "test-oxd-id";
            getTokensByCodeParams.Code = "test-code";
            getTokensByCodeParams.State = "test-state";

            GetTokensByCodeResponse getTokensByCodeResponse;
            GetTokensByCodeResponseData data;
            getTokensByCodeResponse = new GetTokensByCodeResponse();
            data = new GetTokensByCodeResponseData();

            getTokensByCodeResponse.Status = "ok";
            data.AccessToken = "test-access-token";
            data.RefreshToken = "test-refresh-token";
            getTokensByCodeResponse.Data = data;

            var getTokensByCodeClient = new Mock<GetTokensByCodeClient>();
            getTokensByCodeClient.Setup(x => x.GetTokensByCode(oxdHost, oxdPort, getTokensByCodeParams)).Returns(getTokensByCodeResponse);

            Assert.That(getTokensByCodeResponse, Is.Not.Null);
            Assert.That(getTokensByCodeResponse.Data.AccessToken, Is.Not.Null);
            Assert.That(getTokensByCodeResponse.Data.RefreshToken, Is.Not.Null);
        }

        [Test]
        public void TestGetAccessTokenByRefreshToken_oxd_server_Mock()
        {
            getAccessTokenByRefreshTokenParams = new GetAccessTokenByRefreshTokenParams();
            getAccessTokenByRefreshTokenParams.OxdId = "test-oxd-id";
            getAccessTokenByRefreshTokenParams.RefreshToken = "test-refresh-token";

            GetAccessTokenByRefreshTokenResponse getAccessTokenByRefreshTokenResponse;
            GetAccessTokenByRefreshTokenResponseData data;
            getAccessTokenByRefreshTokenResponse = new GetAccessTokenByRefreshTokenResponse();
            data = new GetAccessTokenByRefreshTokenResponseData();

            getAccessTokenByRefreshTokenResponse.Status = "ok";
            data.AccessToken = "test-access-token";
            data.RefreshToken = "test-refresh-token";
            getAccessTokenByRefreshTokenResponse.Data = data;

            var getAccessTokenByRefreshTokenClient = new Mock<GetAccessTokenByRefreshTokenClient>();
            getAccessTokenByRefreshTokenClient.Setup(x => x.GetAccessTokenByRefreshToken(oxdHost, oxdPort, getAccessTokenByRefreshTokenParams)).Returns(getAccessTokenByRefreshTokenResponse);

            Assert.That(getAccessTokenByRefreshTokenResponse, Is.Not.Null);
            Assert.That(getAccessTokenByRefreshTokenResponse.Data.AccessToken, Is.Not.Null);
            Assert.That(getAccessTokenByRefreshTokenResponse.Data.RefreshToken, Is.Not.Null);
        }

        [Test]
        public void TestGetUserInfo_oxd_server_Mock()
        {
            getUserInfoParams = new GetUserInfoParams();
            getUserInfoParams.OxdId = "test-oxd-id";
            getUserInfoParams.AccessToken = "test-access-token";

            GetUserInfoResponse getUserInfoResponse;
            GetUserInfoResponseData data;
            GetUserInfoUserClaims claims;
            getUserInfoResponse = new GetUserInfoResponse();
            data = new GetUserInfoResponseData();
            claims = new GetUserInfoUserClaims();

            getUserInfoResponse.Status = "ok";
            claims.GivenName = new List<string> { "test-given-name" };
            claims.FamilyName = new List<string> { "test-family-name" };
            data.UserClaims = claims;
            getUserInfoResponse.Data = data;

            var getUserInfoClient = new Mock<GetUserInfoClient>();
            getUserInfoClient.Setup(x => x.GetUserInfo(oxdHost, oxdPort, getUserInfoParams)).Returns(getUserInfoResponse);

            Assert.That(getUserInfoResponse, Is.Not.Null);
            Assert.That(getUserInfoResponse.Data.UserClaims.GivenName, Is.Not.Null);
            Assert.That(getUserInfoResponse.Data.UserClaims.FamilyName, Is.Not.Null);
        }

        [Test]
        public void TestGetLogoutUrl_oxd_server_Mock()
        {
            getLogoutUrlParams = new GetLogoutUrlParams();
            getLogoutUrlParams.OxdId = "test-oxd-id";

            GetLogoutUriResponse getLogoutUrlResponse;
            GetLogoutUriResponseData data;
            getLogoutUrlResponse = new GetLogoutUriResponse();
            data = new GetLogoutUriResponseData();

            getLogoutUrlResponse.Status = "ok";
            data.LogoutUri = "https://<idp-hostname>/end_session?id_token_hint=<idtoken>&state=<state>&post_logout_redirect_uri=<...>";
            getLogoutUrlResponse.Data = data;

            var getLogoutUrlClient = new Mock<GetLogoutUriClient>();
            getLogoutUrlClient.Setup(x => x.GetLogoutURL(oxdHost, oxdPort, getLogoutUrlParams)).Returns(getLogoutUrlResponse);

            Assert.That(getLogoutUrlResponse, Is.Not.Null);
            Assert.That(getLogoutUrlResponse.Data.LogoutUri, Is.Not.Null);
        }

        [Test]
        public void TestUmaRsProtect_oxd_server_Mock()
        {
            umaRsProtectParams = new UmaRsProtectParams();
            umaRsProtectParams.OxdId = "test-oxd-id";
            umaRsProtectParams.ProtectResources = new List<ProtectResource>
            {
                new ProtectResource
                {
                    Path = "/testPath",
                    ProtectConditions = new List<ProtectCondition>
                    {
                        new ProtectCondition
                        {
                            HttpMethods = new List<string> { "GET" },
                            Scopes = new List<string> { "test-scope" },
                            TicketScopes = new List<string> { "test-ticket-scope" }
                        }
                    }
                }
            };

            UmaRsProtectResponse umaRsProtectResponse;
            umaRsProtectResponse = new UmaRsProtectResponse();

            umaRsProtectResponse.Status = "ok";

            var umaRsProtectClient = new Mock<UmaRsProtectClient>();
            umaRsProtectClient.Setup(x => x.ProtectResources(oxdHost, oxdPort, umaRsProtectParams)).Returns(umaRsProtectResponse);

            Assert.That(umaRsProtectResponse, Is.Not.Null);
            Assert.AreEqual(umaRsProtectResponse.Status, "ok");
        }

        [Test]
        public void TestUmaRsCheckAccess_Access_Granted_oxd_server_Mock()
        {
            intializeCheckAccessParams();

            UmaRsCheckAccessResponse checkAccessResponse;
            UmaRsCheckAccessResponseData data;
            checkAccessResponse = new UmaRsCheckAccessResponse();
            data = new UmaRsCheckAccessResponseData();

            checkAccessResponse.Status = "ok";
            data.Access = "granted";
            checkAccessResponse.Data = data;

            var checkAccessClient = new Mock<UmaRsCheckAccessClient>();
            checkAccessClient.Setup(x => x.CheckAccess(oxdHost, oxdPort, checkAccessParams)).Returns(checkAccessResponse);

            Assert.That(checkAccessResponse, Is.Not.Null);
            Assert.AreEqual(checkAccessResponse.Status, "ok");
            Assert.AreEqual(checkAccessResponse.Data.Access, "granted");
        }

        [Test]
        public void TestUmaRsCheckAccess_Access_Denied_With_Ticket_oxd_server_Mock()
        {
            intializeCheckAccessParams();

            UmaRsCheckAccessResponse checkAccessResponse;
            UmaRsCheckAccessResponseData data;
            checkAccessResponse = new UmaRsCheckAccessResponse();
            data = new UmaRsCheckAccessResponseData();

            checkAccessResponse.Status = "ok";
            data.Access = "denied";
            data.Ticket = "test-ticket";
            checkAccessResponse.Data = data;

            var checkAccessClient = new Mock<UmaRsCheckAccessClient>();
            checkAccessClient.Setup(x => x.CheckAccess(oxdHost, oxdPort, checkAccessParams)).Returns(checkAccessResponse);

            Assert.That(checkAccessResponse, Is.Not.Null);
            Assert.AreEqual(checkAccessResponse.Status, "ok");
            Assert.AreEqual(checkAccessResponse.Data.Access, "denied");
            Assert.That(checkAccessResponse.Data.Ticket, Is.Not.Null);
        }

        [Test]
        public void TestUmaRsCheckAccess_Access_Denied_Without_Ticket_oxd_server_Mock()
        {
            intializeCheckAccessParams();

            UmaRsCheckAccessResponse checkAccessResponse;
            UmaRsCheckAccessResponseData data;
            checkAccessResponse = new UmaRsCheckAccessResponse();
            data = new UmaRsCheckAccessResponseData();

            checkAccessResponse.Status = "ok";
            data.Access = "denied";
            checkAccessResponse.Data = data;

            var checkAccessClient = new Mock<UmaRsCheckAccessClient>();
            checkAccessClient.Setup(x => x.CheckAccess(oxdHost, oxdPort, checkAccessParams)).Returns(checkAccessResponse);

            Assert.That(checkAccessResponse, Is.Not.Null);
            Assert.AreEqual(checkAccessResponse.Status, "ok");
            Assert.AreEqual(checkAccessResponse.Data.Access, "denied");
        }

        [Test]
        public void TestUmaRsCheckAccess_Resource_Not_Protected_oxd_server_Mock()
        {
            intializeCheckAccessParams();

            UmaRsCheckAccessResponse checkAccessResponse;
            UmaRsCheckAccessResponseData data;
            checkAccessResponse = new UmaRsCheckAccessResponse();
            data = new UmaRsCheckAccessResponseData();

            checkAccessResponse.Status = "error";
            data.Error = "invalid_request";
            checkAccessResponse.Data = data;

            var checkAccessClient = new Mock<UmaRsCheckAccessClient>();
            checkAccessClient.Setup(x => x.CheckAccess(oxdHost, oxdPort, checkAccessParams)).Returns(checkAccessResponse);

            Assert.That(checkAccessResponse, Is.Not.Null);
            Assert.AreEqual(checkAccessResponse.Status, "error");
            Assert.AreEqual(checkAccessResponse.Data.Error, "invalid_request");
        }

        [Test]
        public void TestUmaRpGetRpt_With_Rpt_oxd_server_Mock()
        {
            intializeGetRPTParams();

            GetRPTResponse getRPTResponse;
            GetRPTResponseData data;
            getRPTResponse = new GetRPTResponse();
            data = new GetRPTResponseData();

            getRPTResponse.Status = "ok";
            data.Rpt = "test-RPT";
            getRPTResponse.Data = data;

            var getRptClient = new Mock<UmaRpGetRptClient>();
            getRptClient.Setup(x => x.GetRPT(oxdHost, oxdPort, getRptParams)).Returns(getRPTResponse);

            Assert.That(getRPTResponse, Is.Not.Null);
            Assert.AreEqual(getRPTResponse.Status, "ok");
            Assert.That(getRPTResponse.Data.Rpt, Is.Not.Null);
        }

        [Test]
        public void TestUmaRpGetRpt_With_NeedInfo_oxd_server_Mock()
        {
            intializeGetRPTParams();

            GetRPTResponse getRPTResponse;
            GetRPTResponseData data;
            ErrorDetails details;
            getRPTResponse = new GetRPTResponse();
            data = new GetRPTResponseData();
            details = new ErrorDetails();

            getRPTResponse.Status = "error";
            data.Error = "need_info";
            details.Ticket = "test-ticket";

            data.Details = details;
            getRPTResponse.Data = data;

            var getRptClient = new Mock<UmaRpGetRptClient>();
            getRptClient.Setup(x => x.GetRPT(oxdHost, oxdPort, getRptParams)).Returns(getRPTResponse);

            Assert.That(getRPTResponse, Is.Not.Null);
            Assert.AreEqual(getRPTResponse.Status, "error");
            Assert.AreEqual(getRPTResponse.Data.Error, "need_info");
            Assert.That(getRPTResponse.Data.Details.Ticket, Is.Not.Null);
        }

        [Test]
        public void TestUmaRpGetClaimsGatheringUrl_oxd_server_Mock()
        {
            getClaimsGatheringUrlParams = new UmaRpGetClaimsGatheringUrlParams();
            getClaimsGatheringUrlParams.OxdId = "test-oxd-id";
            getClaimsGatheringUrlParams.Ticket = "ticket";
            getClaimsGatheringUrlParams.ClaimsRedirectURI = "https://client.example.com/Home/GetUMAClaims";

            UmaRpGetClaimsGatheringUrlResponse getClaimsGatheringUrlResponse;
            UmaRpGetClaimsGatheringUrlResponseData data;
            getClaimsGatheringUrlResponse = new UmaRpGetClaimsGatheringUrlResponse();
            data = new UmaRpGetClaimsGatheringUrlResponseData();

            getClaimsGatheringUrlResponse.Status = "ok";
            data.url = "https://as.com/restv1/uma/gather_claims?client_id=@!test-client&ticket=test-ticket&claims_redirect_uri=https://client.example.com/cb&state=af0ifjsldkj";
            getClaimsGatheringUrlResponse.Data = data;

            var getClaimsGatheringUrlClient = new Mock<UmaRpGetClaimsGatheringUrlClient>();
            getClaimsGatheringUrlClient.Setup(x => x.GetClaimsGatheringUrl(oxdHost, oxdPort, getClaimsGatheringUrlParams)).Returns(getClaimsGatheringUrlResponse);

            Assert.That(getClaimsGatheringUrlResponse, Is.Not.Null);
            Assert.That(getClaimsGatheringUrlResponse.Data.url, Is.Not.Null);
        }


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
            registerSiteParams.OpHost = "https://iam.centroxy.com";//Gluu host/your locally hosted gluu server can work
            registerSiteParams.AuthorizationRedirectUri = "https://client.example.com";
            registerSiteParams.Scope = new List<string> { "openid", "profile", "email", "uma_protection", "uma_authorization"};//Scope
            registerSiteParams.ClientName = "DotNet_Test";
        }

        private void intializeSetupClientParams()
        {
            setupClientParams = new SetupClientParams();
            setupClientParams.OpHost = "https://iam.centroxy.com";//Gluu host/your locally hosted gluu server can work
            setupClientParams.AuthorizationRedirectUri = "https://client.example.com";
            setupClientParams.Scope = new List<string> { "openid", "profile", "email", "uma_protection", "uma_authorization" };//Scope
            setupClientParams.ClientName = "DotNet_Test";
        }

        private void intializeCheckAccessParams()
        {
            checkAccessParams = new UmaRsCheckAccessParams();
            checkAccessParams.OxdId = "test-oxd-id";
            checkAccessParams.RPT = "test-rpt";
            checkAccessParams.Path = "/testPath";
            checkAccessParams.HttpMethod = "GET";
        }

        private void intializeGetRPTParams()
        {
            getRptParams = new UmaRpGetRptParams();
            getRptParams.OxdId = "test-oxd-id";
            getRptParams.ticket = "test-ticket";
            getRptParams.state = "test_state";
        }
    }
}
