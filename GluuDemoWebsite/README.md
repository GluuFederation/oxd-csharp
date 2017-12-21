# oxd-CSharp Demo Site

This is a demo site for oxd-csharp written using C#.Net to demonstrate how to use oxd-csharp to perform authorization with an OpenID Provider and fetch information.

## Deployment

### Prerequisites

1. **Client Server**

    - .Net Framework 4.5 or higher
    - oxd-server running in the background. [Install oxd server](https://gluu.org/docs/oxd/install/)

2. **OpenID Provider**

     - An OpenID provider like Gluu Server. [Install Gluu Server](https://gluu.org/docs/ce/3.1.1/)

### Testing OpenID Connect with the demo site


- Open the downloaded [Sample Project](https://github.com/GluuFederation/oxd-csharp/archive/3.1.1.zip) specific to this oxd-csharp library in Visual Studio.

- Enable SSL using the following instructions:

    - Open the client application in Visual Studio.
    - Go to client application properties.
    - Navigate to `Development Server` and set `SSL Enabled` to `True`.

- Change the `hostname` in the project using the following instructions:

     - Make hidden folders visible in the windows explorer. If this has already been done then ignore then skip this step.
     - Navigate to `vs/config` folder in the root of the project in the windows explorer.
     - Open the `applicationhost.config` file.
     - Add the following lines to `bindings` section of the project:

```code
<binding protocol="https" bindingInformation="*:<portno>:client.example.com" />
```

- After adding the aforementioned lines the binding section will look like this:
     
```code
<site name="GluuDemoWebsite" id="2">
    <application path="/" applicationPool="Clr4IntegratedAppPool">
        <virtualDirectory path="/" physicalPath="<path of the project>" />
    </application>
    <bindings>
        <binding protocol="https" bindingInformation="*:<portno>:client.example.com" />
    </bindings>
</site>
```
      
- With the oxd-server running, navigate to the URL's below to run the sample client application. To register a client in the oxd-server use the Setup Client URL. Upon successful registration of the client application, an oxd ID will be displayed in the UI. Next, navigate to the Login URL for authentication.

    - Setup Client URL: https://client.example.com:portno/Home/Setting
    - Login URL: https://client.example.com:portno
    - UMA URL: https://client.example.com:portno/Home/UMA

- The input values used during Setup Client are stored in the configuration file (oxd_config.json). Therefore, the configuration file needs to be writable by the client application.
