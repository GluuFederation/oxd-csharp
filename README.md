# oxd-Csharp

oxd-Csharp is a .Net library (DLL) to interact with Gluu's oxd Server. The oxd Server must be running in your machine to make use of this library. 

# Deployment

The **oxd-Csharp** library can be installed using Nuget Package Manager Console in any .Net project. The Gluu's official Nuget Package page can be found [here](https://www.nuget.org/packages/Gluu.Oxd.OxdCSharp/). Go to the Gluu's official Nuget page and follow the instructions to install the package.

# OXD Server APIs

The Gluu's OXD Server provides the following basic API's for OpenID Connect authentication.

- Setup Client
- Get Client Token
- Introspect Access Token
- Register Site
- Update Site
- Remove Site
- Get Authorization URL
- Get Tokens by Code
- Get Access Token By Refresh Token
- Get User Info
- Get Logout URI

The Gluu's OXD Server provides UMA Resource Server API's. The APIs are listed below

- UMA RS Protect resources
- UMA RS Check Access
- UMA Introspect RPT

The Gluu's OXD Server provides two UMA Client API's. The APIs are listed below

- UMA RP - Get RPT
- UMA RP - Get Claims Gathering Url

Click [here](https://oxd.gluu.org/docs/oxdserver/) for the complete overview of oxd Server and its API details.

# Using the _oxd-Csharp_ Library in your web application

You can call the above APIs of oxd Server using this oxd-Csharp library without worrying about other internal details. All the C# classes invloved in this library are well documented in code so you will get enough information on all the fields and methods of these classes.

## OpenID Connect Authentication APIs

### Setup Client

The following are the required information for setup a client: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *AuthorizationRedirectUri* - A URL which the OP is authorized to redirect the user after authorization.

The following code snippet can be used to setup a client.

**Setup Client using oxd-server**

```csharp
public ActionResult SetupClient(string oxdHost, int oxdPort,  string OpHost, string redirectUrl)
{
    //prepare input params for Setup client
    var setupClientInputParams = new SetupClientParams()
    {
        AuthorizationRedirectUri = redirectUrl,
        OpHost = OpHost,
        ClientName = "OxdTestingClient",
        Scope = new List<string> { "openid", "profile", "email", "uma_protection", "uma_authorization" },
        GrantTypes = new List<string> { "authorization_code", "client_credentials", "uma_ticket" }
    };

    var setupClientClient = new SetupClientClient();
    var setupClientResponse = new SetupClientResponse();
    setupClientResponse = setupClientClient.SetupClient(oxdHost, oxdPort, setupClientInputParams);

    return Json(new { oxdId = oxd.OxdId, clientId = setupClientResponse.Data.clientId, clientSecret = setupClientResponse.Data.clientSecret });
}
```

**Setup Client using oxd-https-extension**

```csharp
public ActionResult SetupClient( string oxdHttpsUrl, string OpHost, string redirectUrl)
{
    //prepare input params for Setup client
    var setupClientInputParams = new SetupClientParams()
    {
        AuthorizationRedirectUri = redirectUrl,
        OpHost = OpHost,
        ClientName = "OxdTestingClient",
        Scope = new List<string> { "openid", "profile", "email", "uma_protection", "uma_authorization" },
        GrantTypes = new List<string> { "authorization_code", "client_credentials", "uma_ticket" }
    };

    var setupClientClient = new SetupClientClient();
    var setupClientResponse = new SetupClientResponse();
    setupClientResponse = setupClientClient.SetupClient(oxdHttpsUrl, setupClientInputParams);

    return Json(new { oxdId = oxd.OxdId, clientId = setupClientResponse.Data.clientId, clientSecret = setupClientResponse.Data.clientSecret });
}
```

### Get Client Token

The following are the required information for get a client token: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *ClientId* - Client ID from OpenID Connect Provider (OP)
- *ClientSecret* - Client Secret from OpenID Connect Provider (OP)
- *OpHost* - URL of the OpenID Connect Provider (OP)

The following code snippet can be used to get client token.

**Get Client Token using oxd-server**

```csharp
public string GetProtectionAccessToken(string oxdHost, int oxdPort, string OpHost, string ClientId, string clientSecret)
{
    //prepare input params for Client Registration
    var getClientAccessTokenParams = new GetClientTokenParams()
    {
        clientId = clientid,
        clientSecret = clientsecret,
        opHost = OpHost
    };

    var getClientAccessToken = new GetClientTokenClient();
    string protectionAccessToken = getClientAccessToken.GetClientToken(oxdHost, oxdPort, getClientAccessTokenParams()).Data.accessToken;

    return protectionAccessToken;
}
```

**Get Client Token using oxd-https-extension**

```csharp
public string GetProtectionAccessToken( string oxdHttpsUrl, string OpHost, string ClientId, string clientSecret)
{
    //prepare input params for Client Registration
    var getClientAccessTokenParams = new GetClientTokenParams()
    {
        clientId = clientid,
        clientSecret = clientsecret,
        opHost = OpHost
    };

    var getClientAccessToken = new GetClientTokenClient();
    string protectionAccessToken = getClientAccessToken.GetClientToken(oxdHttpsUrl, getClientAccessTokenParams()).Data.accessToken;

    return protectionAccessToken;
}
```


### Introspect Access Token

The following are the required information for introspect access token: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *OxdId* - The _OXD ID_ of registered site
- *AccessToken* - Acccess Token

The following code snippet can be used to introspect access token.

**Introspect Access Token using oxd-server**

```csharp
public ActionResult IntrospectAccessToken(string oxdHost, int oxdPort, string oxd_id, string access_token)
    {
        var introspectAccessTokenParams = new IntrospectAccessTokenParams()
        {
            OxdId = oxd_id,
            AccessToken = access_token
        };

        var introspectAccessTokenClient = new IntrospectAccessTokenClient();
        var introspectAccessTokenResponse = new IntrospectAccessTokenResponse();

        introspectAccessTokenResponse = introspectAccessTokenClient.IntrospectAccessToken(oxdHost, oxdPort, introspectAccessTokenParams);

        return Json(new { status = introspectAccessTokenResponse.Status });

    }
```

**Introspect Access Token using oxd-https-extension**

```csharp
public ActionResult IntrospectAccessToken(string oxdHttpsUrl, string oxd_id, string access_token)
    {
        var introspectAccessTokenParams = new IntrospectAccessTokenParams()
        {
            OxdId = oxd_id,
            AccessToken = access_token
        };

        var introspectAccessTokenClient = new IntrospectAccessTokenClient();
        var introspectAccessTokenResponse = new IntrospectAccessTokenResponse();

        introspectAccessTokenResponse = introspectAccessTokenClient.IntrospectAccessToken(oxdHttpsUrl, introspectAccessTokenParams);

        return Json(new { status = introspectAccessTokenResponse.Status });

    }
```


### Register Site

The following are the required information for registering a Site: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *AuthorizationRedirectUri* - A URL which the OP is authorized to redirect the user after authorization.
- *ProtectionAccessToken* - Generated from GetClientToken method. Required for oxd-https-extension

> **Note:** The `Register Site` endpoint is not required if client is registered using `Setup Client`.

The following code snippet can be used to register a site.

**Register Site using oxd-server**

```csharp
public ActionResult RegisterSite(string oxdHost, int oxdPort, string OpHost, string redirectUrl)
{
    //prepare input params for Client Registration
    var registerSiteInputParams = new RegisterSiteParams()
    {
        AuthorizationRedirectUri = redirectUrl,
        OpHost = OpHost,
        ClientName = "<Your Client Name>",
        Scope = new List<string> { "openid", "profile", "email" }
    };

    var registerSiteClient = new RegisterSiteClient();
    var registerSiteResponse = new RegisterSiteResponse();
    registerSiteResponse = registerSiteClient.RegisterSite(oxdHost, oxdPort, registerSiteInputParams);
    
    //Response
    return Json(new { oxdId = registerSiteResponse.Data.OxdId });
}
```

**Register Site using oxd-https-extension**

```csharp
public ActionResult RegisterSite(string oxdHttpsUrl, string OpHost, string redirectUrl, string protectionAccessToken)
{
    //prepare input params for Client Registration
    var registerSiteInputParams = new RegisterSiteParams()
    {
        AuthorizationRedirectUri = redirectUrl,
        OpHost = OpHost,
        ClientName = "<Your Client Name>",
        Scope = new List<string> { "openid", "profile", "email" },
        ProtectionAccessToken = protectionAccessToken
    };

    var registerSiteClient = new RegisterSiteClient();
    var registerSiteResponse = new RegisterSiteResponse();
    registerSiteResponse = registerSiteClient.RegisterSite(oxdHttpsUrl, registerSiteInputParams);
    
    //Response
    return Json(new { oxdId = registerSiteResponse.Data.OxdId });
}
```


### Update Site

The following are the required information for updating a Site: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *OxdId* - The _OXD ID_ of registered site
- *ProtectionAccessToken* - Generated from GetClientToken method. Required for oxd-https-extension

The following code snippet can be used to update a site.

**Update Site using oxd-server**

```csharp
public ActionResult UpdateSite(string oxdHost, int oxdPort,  string oxdId, string postLogoutRedirectUrl)
{
    //prepare input params for Update Site Registration
    var updateSiteInputParams = new UpdateSiteParams()
    {
        OxdId = oxdId,
        Contacts = new List<string> { "support@email.com" },
        PostLogoutRedirectUri = postLogoutRedirectUrl
    };

    var updateSiteClient = new UpdateSiteRegistrationClient();
    var updateSiteResponse = new UpdateSiteResponse();
    updateSiteResponse = updateSiteClient.UpdateSiteRegistration(oxdHost, oxdPort, updateSiteInputParams);
    
    //Response
    return Json(new { status = updateSiteResponse.Status });
}
```

**Update Site using oxd-https-extension**

```csharp
public ActionResult UpdateSite(string oxdHttpsUrl, string oxdId, string postLogoutRedirectUrl, string protectionAccessToken)
{
    //prepare input params for Update Site Registration
    var updateSiteInputParams = new UpdateSiteParams()
    {
        OxdId = oxdId,
        Contacts = new List<string> { "support@email.com" },
        PostLogoutRedirectUri = postLogoutRedirectUrl,
        ProtectionAccessToken = protectionAccessToken
    };

    var updateSiteClient = new UpdateSiteRegistrationClient();
    var updateSiteResponse = new UpdateSiteResponse();
    updateSiteResponse = updateSiteClient.UpdateSiteRegistration(oxdHttpsUrl, updateSiteInputParams);

    //Response
    return Json(new { status = updateSiteResponse.Status });
}
```


### Remove Site

The following are the required information for removing a Site: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *OxdId* - The _OXD ID_ of registered site
- *ProtectionAccessToken* - Generated from GetClientToken method. Required for oxd-https-extension

The following code snippet can be used to remove a site.

**Remove Site using oxd-server**

```csharp
public ActionResult RemoveSite(string oxdHost, int oxdPort, string oxd_id)
    {
        var removeSiteInputParams = new RemoveSiteParams();
        {
            OxdId = oxd_id
        };

        var removeSiteClient = new RemoveSiteClient();
        var removeSiteResponse = new RemoveSiteResponse();

        removeSiteResponse = removeSiteClient.RemoveSite(oxdHost, oxdPort, removeSiteInputParams);

        return Json(new { status = removeSiteResponse.Status });

    }
```

**Remove Site using oxd-https-extension**

```csharp
public ActionResult RemoveSite(string oxdHttpsUrl, string oxd_id, string protectionAccessToken)
    {
        var removeSiteInputParams = new RemoveSiteParams();
        {
            OxdId = oxd_id,
            ProtectionAccessToken = protectionAccessToken
        };
        var removeSiteClient = new RemoveSiteClient();
        var removeSiteResponse = new RemoveSiteResponse();

        removeSiteResponse = removeSiteClient.RemoveSite(oxdHttpsUrl, removeSiteInputParams);

        return Json(new { status = removeSiteResponse.Status });

    }
```


### Get Authorization URL

The following are the required information for Getting Authorization URL: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *OxdId* - The _OXD ID_ of registered site
- *ProtectionAccessToken* - Generated from GetClientToken method. Required for oxd-https-extension

The following code snippet can be used to Get Authorization URL.

**Get Authurization URL using oxd-server**

```csharp
public ActionResult GetAuthorizationURL(string oxdHost, int oxdPort, string oxdId)
{
    //prepare input params for Getting Auth URL from a site
    var getAuthUrlInputParams = new GetAuthorizationUrlParams()
    {
        OxdId = oxdId
    };

    var getAuthUrlClient = new GetAuthorizationUrlClient();
    var getAuthUrlResponse = new GetAuthorizationUrlResponse();
    getAuthUrlResponse = getAuthUrlClient.GetAuthorizationURL(oxdHost, oxdPort, getAuthUrlInputParams);

    //Response
    return Json(new { authUrl = getAuthUrlResponse.Data.AuthorizationUrl });
}
```

**Get Authurization URL using oxd-https-extension**

```csharp
public ActionResult GetAuthorizationURL(string oxdHttpsUrl, string oxdId, string protectionAccessToken)
{
    //prepare input params for Getting Auth URL from a site
    var getAuthUrlInputParams = new GetAuthorizationUrlParams()
    {
        OxdId = oxdId,
        ProtectionAccessToken = protectionAccessToken
    };

    var getAuthUrlClient = new GetAuthorizationUrlClient();
    var getAuthUrlResponse = new GetAuthorizationUrlResponse();
    getAuthUrlResponse = getAuthUrlClient.GetAuthorizationURL(oxdHttpsUrl, getAuthUrlInputParams);

    //Response
    return Json(new { authUrl = getAuthUrlResponse.Data.AuthorizationUrl });
}
```

### Get Tokens by Code

The following are the required information for Getting Tokens by Code: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *OxdId* - The _OXD ID_ of registered site
- *Code* - The Code from OP redirect url
- *State* - The State from OP redirect url
- *ProtectionAccessToken* - Generated from GetClientToken method. Required for oxd-https-extension

> **Note:** Before using this Get Tokens by Code API, you must obtain the Code and State values by authenticating the user using AuthorizationRedirectUri.

The following code snippet can be used to Get Tokens by Code.

**Get Tokens by Code using oxd-server**

```csharp
public ActionResult GetTokenByCode(string oxdHost, int oxdPort, string oxdId, string Code, string State)
{
    //prepare input params for Getting Tokens from a site
    var getTokenByCodeInputParams = new GetTokensByCodeParams()
    {
        OxdId = oxdId,
        Code = Code,
        State = State
    };

    var getTokenByCodeClient = new GetTokensByCodeClient();
    var getTokensByCodeResponse = new GetTokensByCodeResponse();
    getTokensByCodeResponse = getTokenByCodeClient.GetTokensByCode(oxdHost, oxdPort, getTokenByCodeInputParams);

    //Response
    return Json(new { accessToken = getTokensByCodeResponse.Data.AccessToken, refreshToken = getTokensByCodeResponse.Data.RefreshToken });
}
```

**Get Tokens by Code using oxd-https-extension**

```csharp
public ActionResult GetTokenByCode( string oxdHttpsUrl, string oxdId, string Code, string State, string protectionAccessToken)
{
    //prepare input params for Getting Tokens from a site
    var getTokenByCodeInputParams = new GetTokensByCodeParams()
    {
        OxdId = oxdId,
        Code = Code,
        State = State,
        ProtectionAccessToken = protectionAccessToken
    };

    var getTokenByCodeClient = new GetTokensByCodeClient();
    var getTokensByCodeResponse = new GetTokensByCodeResponse();
    getTokensByCodeResponse = getTokenByCodeClient.GetTokensByCode(oxdHttpsUrl, getTokenByCodeInputParams);

    //Response
    return Json(new { accessToken = getTokensByCodeResponse.Data.AccessToken, refreshToken = getTokensByCodeResponse.Data.RefreshToken });
}
```


### Get Access Token By Refresh Token

The following are the required information for Getting Access Tokens by Refresh Token: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *OxdId* - The _OXD ID_ of registered site
- *RefreshToken* - Refresh Token from GetTokensByCode
- *ProtectionAccessToken* - Generated from GetClientToken method. Required for oxd-https-extension

The following code snippet can be used to Get Access Tokens by Refresh Token.

**Get Access Token by Refresh Token using oxd-server**

```csharp
public ActionResult GetAccessTokenByRefreshToken(string oxdHost, int oxdPort, string oxdId, string refreshToken)
{
    //prepare input params for Getting Tokens from a site
    var getAccessTokenByRefreshTokenInputParams = new GetAccessTokenByRefreshTokenParams()
    {
        OxdId = oxdId,
        RefreshToken = refreshToken
    };

    var getTokenByCodeClient = new GetTokensByCodeClient();
    var getAccessTokenByRefreshTokenResponse = new GetAccessTokenByRefreshTokenResponse();
    getAccessTokenByRefreshTokenResponse = getAccessTokenByRefreshTokenClient.GetAccessTokenByRefreshToken(oxdHost, oxdPort, getAccessTokenByRefreshTokenInputParams);

    //Response
    return Json(new { accessToken = getAccessTokenByRefreshTokenResponse.Data.AccessToken, refreshToken = getAccessTokenByRefreshTokenResponse.Data.RefreshToken });
}
```

**Get Access Token by Refresh Token using oxd-https-extension**

```csharp
public ActionResult GetAccessTokenByRefreshToken(string oxdHttpsUrl, string oxdId, string refreshToken, string protectionAccessToken)
{
    //prepare input params for Getting Tokens from a site
    var getAccessTokenByRefreshTokenInputParams = new GetAccessTokenByRefreshTokenParams()
    {
        OxdId = oxdId,
        RefreshToken = refreshToken,
        ProtectionAccessToken = protectionAccessToken
    };

    var getTokenByCodeClient = new GetTokensByCodeClient();
    var getAccessTokenByRefreshTokenResponse = new GetAccessTokenByRefreshTokenResponse();
    getAccessTokenByRefreshTokenResponse = getAccessTokenByRefreshTokenClient.GetAccessTokenByRefreshToken(oxdHttpsUrl, getAccessTokenByRefreshTokenInputParams);

    //Response
    return Json(new { accessToken = getAccessTokenByRefreshTokenResponse.Data.AccessToken, refreshToken = getAccessTokenByRefreshTokenResponse.Data.RefreshToken });
}
```


### Get User Info

The following are the required information for Getting User Info: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *OxdId* - The _OXD ID_ of registered site
- *AccessToken* - The _Access Token_ of the authenticated user
- *ProtectionAccessToken* - Generated from GetClientToken method. Required for oxd-https-extension

The following code snippet can be used to Get User Info.

**Get User Info using oxd-server**

```csharp
public ActionResult GetUserInfo(string oxdHost, int oxdPort,  string oxdId, string accessToken)
{
    //prepare input params for Getting User Info from a site
    var getUserInfoInputParams = new GetUserInfoParams()
    {
        OxdId = oxdId,
        AccessToken = accessToken
    };

    var getUserInfoClient = new GetUserInfoClient();
    var getUserInfoResponse = new GetUserInfoResponse();

    getUserInfoResponse = getUserInfoClient.GetUserInfo(oxdHost, oxdPort, getUserInfoInputParams);

    //Response
    var userName = getUserInfoResponse.Data.UserClaims.Name.First();
    var userEmail = getUserInfoResponse.Data.UserClaims.Email == null ? string.Empty : getUserInfoResponse.Data.UserClaims.Email.FirstOrDefault();

    return Json(new { userName = userName, userEmail = userEmail });
}
```

**Get User Info using oxd-https-extension**

```csharp
public ActionResult GetUserInfo(string oxdHttpsUrl, string oxdId, string accessToken, string protectionAccessToken)
{
    //prepare input params for Getting User Info from a site
    var getUserInfoInputParams = new GetUserInfoParams()
    {
        OxdId = oxdId,
        AccessToken = accessToken,
        ProtectionAccessToken = protectionAccessToken
    };

    var getUserInfoClient = new GetUserInfoClient();
    var getUserInfoResponse = new GetUserInfoResponse();
    
    getUserInfoResponse = getUserInfoClient.GetUserInfo(oxdHttpsUrl, getUserInfoInputParams);

    //Response
    var userName = getUserInfoResponse.Data.UserClaims.Name.First();
    var userEmail = getUserInfoResponse.Data.UserClaims.Email == null ? string.Empty : getUserInfoResponse.Data.UserClaims.Email.FirstOrDefault();

    return Json(new { userName = userName, userEmail = userEmail });
}
```


### Get Logout URI

The following are the required information for Getting Logout URI: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *OxdId* - The _OXD ID_ of registered site
- *ProtectionAccessToken* - Generated from GetClientToken method. Required for oxd-https-extension

The following code snippet can be used to Get Logout URI.

**Get Logout URI using oxd-server**

```csharp
public ActionResult GetLogoutUrl(string oxdHost, int oxdPort, string oxdId)
{
    //prepare input params for Getting Logout URI from a site
    var getLogoutUriInputParams = new GetLogoutUrlParams()
    {
        OxdId = oxdId
    };

    var getLogoutUriClient = new GetLogoutUriClient();
    var getLogoutUriResponse = new GetLogoutUriResponse();
    
    getLogoutUriResponse = getLogoutUriClient.GetLogoutURL(oxdHost, oxdPort, getLogoutUriInputParams);
    
    //Response
    return Json(new { logoutUri = getLogoutUriResponse.Data.LogoutUri });
}
```

**Get Logout URI using oxd-https-extension**

```csharp
public ActionResult GetLogoutUrl(string oxdHttpsUrl, string oxdId, string protectionAccessToken)
{
    //prepare input params for Getting Logout URI from a site
    var getLogoutUriInputParams = new GetLogoutUrlParams()
    {
        OxdId = oxdId,
        ProtectionAccessToken = protectionAccessToken
    };

    var getLogoutUriClient = new GetLogoutUriClient();
    var getLogoutUriResponse = new GetLogoutUriResponse();

    getLogoutUriResponse = getLogoutUriClient.GetLogoutURL(oxdHttpsUrl, getLogoutUriInputParams);

    //Response
    return Json(new { logoutUri = getLogoutUriResponse.Data.LogoutUri });
}
```

## UMA Resource Server API's

### UMA RS Protect Resources

The following are the required information for Protecting UMA resource in Resoruce Server: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *OxdId* - The _OXD ID_ of registered site
- *ProtectionAccessToken* - Generated from GetClientToken method. Required for oxd-https-extension

The following code snippet can be used to Protect UMA resources in Resource Server.

**RS Protect using oxd-server**

```csharp
public ActionResult ProtectResources(string oxdHost, int oxdPort, string oxdId)
{
    //prepare input params for Protect Resource
    var protectParams = new UmaRsProtectParams()
    {
        OxdId = oxdId,
        ProtectResources = new List<ProtectResource>
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
        }
    };
    var protectClient = new UmaRsProtectClient();
    var protectResponse = new UmaRsProtectResponse();
    protectResponse = protectClient.ProtectResources(oxdHost, oxdPort, protectParams);
    
    return Json(new { Response = protectResponse.Status });
}
```

***RS Protect with scope_expression using oxd-server***

```csharp
public ActionResult ProtectResources(string oxdHost, int oxdPort, string oxd_id)
    {
        var protectParams = new UmaRsProtectParams()
        {
            OxdId = oxd_id,
            ProtectResources = new List<ProtectResource>
            {
                new ProtectResource
                {
                    Path = "/photo",
                    ProtectConditions = new List<ProtectCondition>
                    {
                        new ProtectCondition
                        {
                            HttpMethods = new List<string> { "GET" },
                            ScopeExpressions = new ScopeExpression
                            {
                                Rule = JsonConvert.DeserializeObject("{'and':[{'or':[{'var':0},{'var':1}]},{'var':2}]}"),
                                Data = new List<string>{"http://photoz.example.com/dev/actions/all","http://photoz.example.com/dev/actions/add","http://photoz.example.com/dev/actions/internalClient" }
                            }
                        }
                    }
                }
            }
        };

        var protectClient = new UmaRsProtectClient();
        var protectResponse = new UmaRsProtectResponse();

        protectResponse = protectClient.ProtectResources(oxdHost, oxdPort, protectParams);

        return Json(new { Response = protectResponse.Status });
    }
```

**RS Protect using oxd-https-extension**

```csharp
public ActionResult ProtectResources(string oxdHttpsUrl, string oxdId, string protectionAccessToken)
{
    //prepare input params for Protect Resource
    var protectParams = new UmaRsProtectParams()
    {
        OxdId = oxdId,
        ProtectResources = new List<ProtectResource>
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
        },
        ProtectionAccessToken = protectionAccessToken
    };
    var protectClient = new UmaRsProtectClient();
    var protectResponse = new UmaRsProtectResponse();
    protectResponse = protectClient.ProtectResources(oxdHttpsUrl, protectParams);

    return Json(new { Response = protectResponse.Status });
}
```

***RS Protect with scope_expression using oxd-https-extension***

```csharp
public ActionResult ProtectResources(string oxdHttpsUrl, string oxd_id, string protectionAccessToken)
    {
        var protectParams = new UmaRsProtectParams()
        {
            OxdId = oxd_id,
            ProtectResources = new List<ProtectResource>
            {
                new ProtectResource
                {
                    Path = "/photo",
                    ProtectConditions = new List<ProtectCondition>
                    {
                        new ProtectCondition
                        {
                            HttpMethods = new List<string> { "GET" },
                            ScopeExpressions = new ScopeExpression
                            {
                                Rule = JsonConvert.DeserializeObject("{'and':[{'or':[{'var':0},{'var':1}]},{'var':2}]}"),
                                Data = new List<string>{"http://photoz.example.com/dev/actions/all","http://photoz.example.com/dev/actions/add","http://photoz.example.com/dev/actions/internalClient" }
                            }
                        }
                    }
                }
            },
            ProtectionAccessToken = protectionAccessToken
        };

        var protectClient = new UmaRsProtectClient();
        var protectResponse = new UmaRsProtectResponse();

        protectResponse = protectClient.ProtectResources(oxdHttpsUrl, protectParams);

        return Json(new { Response = protectResponse.Status });
    }
```

### UMA RS Check Access

The following are the required information for Checking Access of a UMA resource in Resource Server: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *OxdId* 		- The _OXD ID_ of registered site
- *RPT* 		- Requesting Party Token
- *Path* 		- Path of resource (e.g. http://rs.com/phones), /phones should be passed
- *HttpMethod* 	- Http method of RP request (GET, POST, PUT, DELETE)
- *ProtectionAccessToken* - Generated from GetClientToken method. Required for oxd-https-extension

The following code snippet can be used to Check Access of a UMA resource protected in Resource Server.

**Check Access using oxd-server**

```csharp
public ActionResult CheckAccess(string oxdHost, int oxdPort,  string oxdId, string rpt)
{
    //prepare input params for Check Access
    var checkAccessParams = new UmaRsCheckAccessParams()
    {
        OxdId = oxdId,
        RPT = rpt,
        Path = "/scim",
        HttpMethod = "GET"
    };

    var checkAccessClient = new UmaRsCheckAccessClient();
    var checkAccessResponse = new UmaRsCheckAccessResponse();
    checkAccessResponse = checkAccessClient.CheckAccess(oxdHost, oxdPort, checkAccessParams);
                
    if (checkAccessResponse.Status.ToLower().Equals("ok"))
    {
        return Json(new { Response = JsonConvert.SerializeObject(checkAccessResponse.Data) });
    }
}
```

**Check Access using oxd-https-extension**

```csharp
public ActionResult CheckAccess( string oxdHttpsUrl, string oxdId, string rpt, string protectionAccessToken)
{
    //prepare input params for Check Access
    var checkAccessParams = new UmaRsCheckAccessParams()
    {
        OxdId = oxdId,
        RPT = rpt,
        Path = "/scim",
        HttpMethod = "GET",
        ProtectionAccessToken = protectionAccessToken
    };

    var checkAccessClient = new UmaRsCheckAccessClient();
    var checkAccessResponse = new UmaRsCheckAccessResponse();

    checkAccessResponse = checkAccessClient.CheckAccess(oxdHttpsUrl, checkAccessParams);
    
    if (checkAccessResponse.Status.ToLower().Equals("ok"))
    {
        return Json(new { Response = JsonConvert.SerializeObject(checkAccessResponse.Data) });
    }
}
```


### UMA Introspect RPT

The following are the required information for introspecting RPT: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *OxdId* 		- The _OXD ID_ of registered site
- *RPT* 		- Requesting Party Token

The following code snippet can be used to introspect RPT.

**Introspect RPT using oxd-server**

```csharp
public ActionResult IntrospectRPT(string oxdHost, int oxdPort, string oxd_id, string rpt)
    {
        var umaIntrospectRptParams = new UmaIntrospectRptParams()
        {
            OxdId = oxd_id,
            RPT = rpt
        };

        var umaIntrospectRptClient = new UmaIntrospectRptClient();
        var umaIntrospectRptResponse = new UmaIntrospectRptResponse();

        umaIntrospectRptResponse = umaIntrospectRptClient.IntrospectRpt(oxdHost, oxdPort, umaIntrospectRptParams);

        return Json(new { Response = umaIntrospectRptResponse.Data });
    }
```

**Introspect RPT using oxd-https-extension**

```csharp
public ActionResult IntrospectRPT(string oxdHttpsUrl, string oxd_id, string rpt)
    {
        var umaIntrospectRptParams = new UmaIntrospectRptParams()
        {
            OxdId = oxd_id,
            RPT = rpt
        };

        var umaIntrospectRptClient = new UmaIntrospectRptClient();
        var umaIntrospectRptResponse = new UmaIntrospectRptResponse();

        umaIntrospectRptResponse = umaIntrospectRptClient.IntrospectRpt(oxdHttpsUrl, umaIntrospectRptParams);

        return Json(new { Response = umaIntrospectRptResponse.Data });
    }
```


## UMA Client API's

### UMA RP - Get RPT

The following are the required information for Getting RPT from RP: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *OxdId* - The _oxd Id_ of registered site
- *Ticket* - Ticket from CheckAccess
- *ProtectionAccessToken* - Generated from GetClientToken method. Required for oxd-https-extension

The following code snippet can be used to get RPT from RP.

**RP Get RPT using oxd-server**

```csharp
public ActionResult ObtainRpt(string oxdHost, int oxdPort,  string oxdId, string ticket, string pct, string rpt )
{
    //prepare input params for Protect Resource
    var getRptParams = new UmaRpGetRptParams()
    {
        getRptParams.OxdId = oxdId,
        getRptParams.ticket = ticket
    };
    
    var getRptClient = new UmaRpGetRptClient();
    var getRptResponse = new GetRPTResponse();
    
    getRptResponse = getRptClient.GetRPT(oxdHost, oxdPort, getRptParams);
    
    //process response
    if (getRptResponse.Status.ToLower().Equals("ok"))
    {
        return Json(new { Response = JsonConvert.SerializeObject(getRptResponse.Data) });
    }
}
```

**RP Get RPT using oxd-https-extension**

```csharp
public ActionResult ObtainRpt(string oxdHttpsUrl, string oxdId, string ticket, string protectionAccessToken, , string pct, string rpt)
{
    //prepare input params for Protect Resource
    var getRptParams = new UmaRpGetRptParams()
    {
        getRptParams.OxdId = oxdId,
        getRptParams.ticket = ticket,
        ProtectionAccessToken = protectionAccessToken
    };
    
    var getRptClient = new UmaRpGetRptClient();
    var getRptResponse = new GetRPTResponse();
    
    getRptResponse = getRptClient.GetRPT(oxdHttpsUrl, getRptParams);

    //process response
    if (getRptResponse.Status.ToLower().Equals("ok"))
    {
        return Json(new { Response = JsonConvert.SerializeObject(getRptResponse.Data) });
    }
}
```

### UMA RP - Get Claims Gathering URL

The following are the required information for Getting Claims Gathering URL: 

- *OxdHost* - Oxd Server's Host address. Required for oxd-server
- *OxdPort* - Oxd Server's Port number. Required for oxd-server
- *oxdHttpsUrl* - URL of the oxd-https-extension. Required for oxd-https-extension
- *OxdId*	- The _OXD ID_ of registered site
- *ClaimsRedirectURI* 	- Claims Redirect URI
- *Ticket* 	- Ticket from Check Access command response if the resource is protected with Ticket Scope
- *ProtectionAccessToken* - Generated from GetClientToken method. Required for oxd-https-extension

The following code snippet can be used to Get Claims Gathering URL.

**RP Get Claims Gathering URL using oxd-server**

```csharp
public ActionResult GetClaimsGatheringUrl(string oxdHost, int oxdPort, string oxdId, string ticket)
{
    //prepare input params for Check Access
    var getClaimsGatheringUrlParams = new UmaRpGetClaimsGatheringUrlParams()
    {
        OxdId = oxdId,
        Ticket = ticket,
        ClaimsRedirectURI = "https://client.example.com"
    };

    var getClaimsGatheringUrlClient = new UmaRpGetClaimsGatheringUrlClient();
    var getClaimsGatheringUrlResponse = new UmaRpGetClaimsGatheringUrlResponse();
    getClaimsGatheringUrlResponse = getClaimsGatheringUrlClient.GetClaimsGatheringUrl(oxdHost, oxdPort, getClaimsGatheringUrlParams);
    
    //process response
    return Json(new { Response = JsonConvert.SerializeObject(getClaimsGatheringUrlResponse.Data) });
}
```

**RP Get Claims Gathering URL using oxd-https-extension**

```csharp
public ActionResult GetClaimsGatheringUrl( string oxdHttpsUrl, string oxdId, string ticket, string protectionAccessToken)
{
    //prepare input params for Check Access
    var getClaimsGatheringUrlParams = new UmaRpGetClaimsGatheringUrlParams()
    {
        OxdId = oxdId,
        Ticket = ticket,
        ClaimsRedirectURI = "https://client.example.com",
        ProtectionAccessToken = protectionAccessToken
    };

    var getClaimsGatheringUrlClient = new UmaRpGetClaimsGatheringUrlClient();
    var getClaimsGatheringUrlResponse = new UmaRpGetClaimsGatheringUrlResponse();
                
    getClaimsGatheringUrlResponse = getClaimsGatheringUrlClient.GetClaimsGatheringUrl(oxdHttpsUrl, getClaimsGatheringUrlParams);
    
    //process response
    return Json(new { Response = JsonConvert.SerializeObject(getClaimsGatheringUrlResponse.Data) });
}
```
