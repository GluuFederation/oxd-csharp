using oxdCSharp.UMA.Clients;
using oxdCSharp.UMA.CommandParameters;
using oxdCSharp.UMA.CommandResponses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;


namespace UMATestApi.Models
{
    public class RSProtectResource
    {
        public static string oxd_id = "";

        public RSProtectResource(string oxdId)
        {
            oxd_id = oxdId;
        }

        public string ProtectResources(string oxdHost, int oxdPort, string httpresturl, string OXDType)
        {
            var protectParams = new UmaRsProtectParams();
            var protectClient = new UmaRsProtectClient();
            var pat = new ProtectionAccessToken();

            //prepare input params for Protect Resource
            protectParams.OxdId = oxd_id;
            protectParams.ProtectResources = new List<ProtectResource>
            {
                new ProtectResource
                {
                    Path = "/values",
                    //Path = "/GetAll",
                    ProtectConditions = new List<ProtectCondition>
                    {
                        new ProtectCondition
                        {
                            HttpMethods = new List<string> { "GET" },
                            Scopes = new List<string> { "https://client.example.com:44300/api" },
                            TicketScopes = new List<string> { "https://client.example.com:44300/api" },
                            //Scopes = new List<string> { "https://client.example.com:44300/home" },
                            //TicketScopes = new List<string> { "https://client.example.com:44300/home" }
                        }
                    }
                }
            };

            var protectResponse = new UmaRsProtectResponse();

            //For OXD Local
            if (OXDType == "local")
            {
                //Get protection access token
                protectParams.ProtectionAccessToken = pat.GetProtectionAccessToken(oxdHost, oxdPort);
                protectResponse = protectClient.ProtectResources(oxdHost, oxdPort, protectParams);
            }

            //For OXD Web
            if (OXDType == "web")
            {
                //Get protection access token
                protectParams.ProtectionAccessToken = pat.GetProtectionAccessToken(httpresturl);
                protectResponse = protectClient.ProtectResources(httpresturl, protectParams);
            }

            return protectResponse.Status;

            throw new Exception("Procteting Resource is not successful. Check OXD Server log for error details.");
        }
    }
}