#oxd-Csharp

oxd-Csharp is a .Net library (DLL) to interact with Gluu's OXD Server. The OXD Server must be running in your machine to make use of this library. 

#Deployment

You can get the DLL of _oxd-Csharp_ from this repository. Add this DLL as a reference in your .Net web application.

#OXD Server APIs

The Gluu's OXD Server provides six API's for OpenID Connect authentication. The all APIs are listed below.

- Register Site
- Update Site Registration
- Get Authorization URL
- Get Tokens by Code
- Get User Info
- Get Logout URI

Click [here](https://oxd.gluu.org/docs/oxdserver/) for the complete overview of OXD Server and its API details.

#Using the _oxd-Csharp_ Library in your web application

You can call the above six APIs of OXD Server using this oxd-Csharp library without worrying about other internal details. All the C# classes invloved in this library are well documented in code so you will get enough information on all the fields and methods of these classes.

###Register Site

The following code snippet can be used to register a site.

```cs
[HttpPost]
public ActionResult RegisterSite(OxdModel oxdModel)
{
	var registerSiteInputParams = new RegisterSiteParams();
    var registerSiteClient = new RegisterSiteClient();

    //prepare input params for Register Site
    registerSiteInputParams.AuthorizationRedirectUri = oxdModel.RedirectUrl;
    registerSiteInputParams.OpHost = "https://scim-test.gluu.org";
    registerSiteInputParams.ClientName = "OxdTestingClient";
            
    //Register Site
    var registerSiteResponse = registerSiteClient.RegisterSite(oxdModel.OxdHost, oxdModel.OxdPort, registerSiteInputParams);

	//Process the response
    return Json(new { oxdId = registerSiteResponse.Data.OxdId });
}
```