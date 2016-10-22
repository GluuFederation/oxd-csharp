#oxd-Csharp

oxd-Csharp is a .Net library (DLL) to interact with Gluu's OXD Server. The OXD Server must be running in your machine to make use of this library. 

#Deployment

The **Oxd-Csharp** library can be installed using Nuget Package Manager Console in any .Net project. The Gluu's official Nuget Package page can be found [here](https://www.nuget.org/packages/Gluu.Oxd.OxdCSharp/). Go to the Gluu's official Nuget page and follow the instructions to install the package.

#OXD Server APIs

The Gluu's OXD Server provides basic six API's for OpenID Connect authentication. The all APIs are listed below.

- Register Site
- Update Site Registration
- Get Authorization URL
- Get Tokens by Code
- Get User Info
- Get Logout URI

The Gluu's OXD Server provides two UMA Resource Server API's. The APIs are listed below

- UMA RS Protect resources
- UMA RS Check Access

The Gluu's OXD Server provides two UMA Client API's. The APIs are listed below

- UMA RP - Get RPT
- UMA RP - Authorize RPT

The OXD Server also provides one Gluu OAuth2 Access Management API's. The API is

- UMA RP Get GAT

Click [here](https://oxd.gluu.org/docs/oxdserver/) for the complete overview of OXD Server and its API details.

#Using the _oxd-Csharp_ Library in your web application

You can call the above six APIs of OXD Server using this oxd-Csharp library without worrying about other internal details. All the C# classes invloved in this library are well documented in code so you will get enough information on all the fields and methods of these classes.

##OpenID Connect Authentication APIs

###Register Site

The following are the required information for registering a Site: 

- *OxdHost* - Oxd Server's Host address
- *OxdPort* - Oxd Server's Port number
- *AuthorizationRedirectUri* - A URL which the OP is authorized to redirect the user after authorization.

The following code snippet can be used to register a site.

```csharp
[HttpPost]
public ActionResult RegisterSite(OxdModel oxdModel)
{
	//prepare input params for Register Site
    var registerSiteInputParams = new RegisterSiteParams();
    registerSiteInputParams.AuthorizationRedirectUri = oxdModel.RedirectUrl;
    registerSiteInputParams.OpHost = "https://scim-test.gluu.org";
    registerSiteInputParams.ClientName = "OxdTestingClient";

    //Register Site
    var registerSiteClient = new RegisterSiteClient();
    var registerSiteResponse = registerSiteClient.RegisterSite(oxdModel.OxdHost, oxdModel.OxdPort, registerSiteInputParams);

    //Process the response
    return Json(new { oxdId = registerSiteResponse.Data.OxdId });
}
```

###Update Site Registration

The following are the required information for updating a registered Site: 

- *OxdHost* - Oxd Server's Host address
- *OxdPort* - Oxd Server's Port number
- *OxdId* - The _OXD ID_ of registered site

The following code snippet can be used to update a site.

```csharp
[HttpPost]
public ActionResult UpdateSiteRegistration(OxdModel oxdModel)
{
	//prepare input params for Update Site Registration
    var updateSiteInputParams = new UpdateSiteParams();
    updateSiteInputParams.OxdId = oxdModel.OxdId;
    updateSiteInputParams.Contacts = new List<string> { oxdModel.OxdEmail };
    updateSiteInputParams.PostLogoutRedirectUri = oxdModel.PostLogoutRedirectUrl;

    //Update Site Registration
    var updateSiteClient = new UpdateSiteRegistrationClient();
    var updateSiteResponse = updateSiteClient.UpdateSiteRegistration(oxdModel.OxdHost, oxdModel.OxdPort, updateSiteInputParams);

    //Process the response
    return Json(new { status = updateSiteResponse.Status });
}
```

###Get Authorization URL

The following are the required information for Getting Authorization URL: 

- *OxdHost* - Oxd Server's Host address
- *OxdPort* - Oxd Server's Port number
- *OxdId* - The _OXD ID_ of registered site

The following code snippet can be used to Get Authorization URL.

```csharp
[HttpPost]
public ActionResult GetAuthorizationUrl(OxdModel oxdModel)
{
	//prepare input params for Getting Auth URL from a site
    var getAuthUrlInputParams = new GetAuthorizationUrlParams();
    getAuthUrlInputParams.OxdId = oxdModel.OxdId;

    //Get Auth URL
    var getAuthUrlClient = new GetAuthorizationUrlClient();
    var getAuthUrlResponse = getAuthUrlClient.GetAuthorizationURL(oxdModel.OxdHost, oxdModel.OxdPort, getAuthUrlInputParams);

    //Process Response
    return Json(new { authUrl = getAuthUrlResponse.Data.AuthorizationUrl });
}
```

###Get Tokens by Code

The following are the required information for Getting Tokens by Code: 

- *OxdHost* - Oxd Server's Host address
- *OxdPort* - Oxd Server's Port number
- *OxdId* - The _OXD ID_ of registered site
- *Code* - The Code from OP redirect url
- *State* - The State from OP redirect url

> **Note:** Before using this Get Tokens by Code API, you must obtain the Code and State values by authenticating the user using AuthorizationRedirectUri.

The following code snippet can be used to Get Tokens by Code.

```csharp
[HttpPost]
public ActionResult GetTokensByCode(OxdModel oxdModel)
{
	//prepare input params for Getting Tokens from a site
    var getTokenByCodeInputParams = new GetTokensByCodeParams();
    getTokenByCodeInputParams.OxdId = oxdModel.OxdId;
    getTokenByCodeInputParams.Code = oxdModel.AuthCode;
    getTokenByCodeInputParams.State = oxdModel.AuthState;

    //Get Tokens by Code
    var getTokenByCodeClient = new GetTokensByCodeClient();
    var getTokensByCodeResponse = getTokenByCodeClient.GetTokensByCode(oxdModel.OxdHost, oxdModel.OxdPort, getTokenByCodeInputParams);

    //Process response
    return Json(new { accessToken = getTokensByCodeResponse.Data.AccessToken, refreshToken = getTokensByCodeResponse.Data.RefreshToken });
}
```

###Get User Info

The following are the required information for Getting User Info: 

- *OxdHost* - Oxd Server's Host address
- *OxdPort* - Oxd Server's Port number
- *OxdId* - The _OXD ID_ of registered site
- *AccessToken* - The _Access Token_ of the authenticated user

The following code snippet can be used to Get User Info.

```csharp
[HttpPost]
public ActionResult GetUserInfo(OxdModel oxdModel)
{
	//prepare input params for Getting User Info from a site
    var getUserInfoInputParams = new GetUserInfoParams();
    getUserInfoInputParams.OxdId = oxdModel.OxdId;
    getUserInfoInputParams.AccessToken = oxdModel.AccessToken;

    //Get User Info
    var getUserInfoClient = new GetUserInfoClient();
    var getUserInfoResponse = getUserInfoClient.GetUserInfo(oxdModel.OxdHost, oxdModel.OxdPort, getUserInfoInputParams);

    //Process response
    var userName = getUserInfoResponse.Data.UserClaims.Name.First();
    var userEmail = getUserInfoResponse.Data.UserClaims.Email == null ? string.Empty : getUserInfoResponse.Data.UserClaims.Email.FirstOrDefault();

    return Json(new { userName = userName });
}
```

###Get Logout URI

The following are the required information for Getting Logout URI: 

- *OxdHost* - Oxd Server's Host address
- *OxdPort* - Oxd Server's Port number
- *OxdId* - The _OXD ID_ of registered site

The following code snippet can be used to Get Logout URI.

```csharp
[HttpPost]
public ActionResult GetLogoutUri(OxdModel oxdModel)
{
	//prepare input params for Getting Logout URI from a site
    var getLogoutUriInputParams = new GetLogoutUrlParams();
    getLogoutUriInputParams.OxdId = oxd.OxdId;

    //Get Logout URI
    var getLogoutUriClient = new GetLogoutUriClient();
    var getLogoutUriResponse = getLogoutUriClient.GetLogoutURL(oxd.OxdHost, oxd.OxdPort, getLogoutUriInputParams);

    //Process response
    return Json(new { logoutUri = getLogoutUriResponse.Data.LogoutUri });
}
```

##UMA Resource Server API's

###UMA RS Protect Resources

The following are the required information for Protecting UMA resource in Resoruce Server: 

- *OxdId* - The _OXD ID_ of registered site

The following code snippet can be used to Protect UMA resources in Rssource Server.

```csharp
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
```

###UMA RS Check Access

The following are the required information for Checking Access of a UMA resource in Resource Server: 

- *OxdId* 		- The _OXD ID_ of registered site
- *RPT* 		- Requesting Party Token
- *Path* 		- Path of resource (e.g. http://rs.com/phones), /phones should be passed
- *HttpMethod* 	- Http method of RP request (GET, POST, PUT, DELETE)

The following code snippet can be used to Check Access of a UMA resource protected in Resource Server.

```csharp
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
```