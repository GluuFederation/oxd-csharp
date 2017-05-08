

using NUnit.Framework;
using oxdCSharp.Clients;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System.Collections.Generic;

namespace oxd_Csharp_Test
{
    [TestFixture]
    public class UnitTestOxdHttp
    {
        [Test]
        public void TestRegisterSite_oxd_http()
        {
            intializedParameter();

            var RegisterSite = new RegisterSiteClient();
            RegisterSiteResponse registerSiteResponse = RegisterSite.RegisterSite("https://127.0.0.1:8443", registerSiteParams);
            Assert.That(registerSiteResponse, Is.Not.Null);
            Assert.That(registerSiteResponse.Data.OxdId, Is.Not.Null);
        }
        [Test]
        public void TestUpdateSite_oxd_http()
        {
            intializedParameter();
            var RegisterSite = new RegisterSiteClient();
            RegisterSiteResponse registerSiteResponse = RegisterSite.RegisterSite("https://127.0.0.1:8443", registerSiteParams);
            Assert.That(registerSiteResponse, Is.Not.Null);
            Assert.That(registerSiteResponse.Data.OxdId, Is.Not.Null);

            var updateSiteParams = new UpdateSiteParams();
            updateSiteParams.OxdId = registerSiteResponse.Data.OxdId;
            updateSiteParams.Scope = new List<string> { "openid", "profile" };
            updateSiteParams.Contacts = new List<string> { "jajati@email.com" };
            updateSiteParams.PostLogoutRedirectUri = "https://client.example.com/LogoutUri";

            var updateSiteRegistrationclient = new UpdateSiteRegistrationClient();
            UpdateSiteResponse updatesiteresponse = updateSiteRegistrationclient.UpdateSiteRegistration("127.0.0.1", 8099, updateSiteParams);

            Assert.That(updatesiteresponse, Is.Not.Null);

        }

        [Test]
        public void TestAuthorizationUrl_oxd_http()
        {
            intializedParameter();
            var RegisterSite = new RegisterSiteClient();
            RegisterSiteResponse registerSiteResponse = RegisterSite.RegisterSite("https://127.0.0.1:8443", registerSiteParams);
            Assert.That(registerSiteResponse, Is.Not.Null);
            Assert.That(registerSiteResponse.Data.OxdId, Is.Not.Null);

            var authorizationUrlParams = new GetAuthorizationUrlParams();
            authorizationUrlParams.OxdId = registerSiteResponse.Data.OxdId;
            authorizationUrlParams.Scope = new List<string> { "openid", "profile" };


            var authorizationUrlclient = new GetAuthorizationUrlClient();
            GetAuthorizationUrlResponse authorizationurlresponse = authorizationUrlclient.GetAuthorizationURL("127.0.0.1", 8099, authorizationUrlParams);

            Assert.That(authorizationurlresponse, Is.Not.Null);
            Assert.That(authorizationurlresponse.Data.AuthorizationUrl, Is.Not.Null);

        }

        private static RegisterSiteParams registerSiteParams;

        private void intializedParameter()
        {
            registerSiteParams = new RegisterSiteParams();
            registerSiteParams.OpHost = "https://gluu.centroxy.com";//Gluu host/your locally hosted gluu server can work
            registerSiteParams.AuthorizationRedirectUri = "https://client.example.com";
            registerSiteParams.Scope = new List<string> { "openid", "profile", "email", "uma_protection", "uma_authorization" };//Scope
            registerSiteParams.ClientName = "DotNet_Test";
        }
    }

}
