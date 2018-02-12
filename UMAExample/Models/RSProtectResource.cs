using Newtonsoft.Json;
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

            ////Without scope_expression
            //protectParams.ProtectResources = new List<ProtectResource>
            //{
            //    new ProtectResource
            //    {
            //        Path = "/values",
            //        ProtectConditions = new List<ProtectCondition>
            //        {
            //            new ProtectCondition
            //            {
            //                HttpMethods = new List<string> { "GET" },
            //                Scopes = new List<string> { "https://client.example.com:44300/api" },
            //                TicketScopes = new List<string> { "https://client.example.com:44300/api" }
            //            }
            //        }
            //    }
            //};

            ////With scope_expression
            protectParams.ProtectResources = new List<ProtectResource>
            {
                new ProtectResource
                {
                    Path = "/values",
                    ProtectConditions = new List<ProtectCondition>
                    {
                        new ProtectCondition
                        {
                            HttpMethods = new List<string> { "GET" },
                            //Scopes = new List<string>{ "https://client.example.com:44300/api", "https://client.example.com:44300/api1", "https://client.example.com:44300/api2" },
                            //TicketScopes = new List<string>{ "https://client.example.com:44300/api", "https://client.example.com:44300/api1", "https://client.example.com:44300/api2" },
                            ScopeExpressions = new ScopeExpression
                            {
                                Rule = JsonConvert.DeserializeObject("{'and':[{'or':[{'var':0},{'var':1}]},{'var':2}]}"),
                                Data = new List<string>{ "https://client.example.com:44300/api", "https://client.example.com:44300/api1", "https://client.example.com:44300/api2" }
                            }
                        }
                    }
                }
            };


            var protectResponse = new UmaRsProtectResponse();

            if (OXDType == "local")
            {
                //protectParams.ProtectionAccessToken = pat.GetProtectionAccessToken(oxdHost, oxdPort);//Keep this line if protect_commands_with_access_token is set True for oxd-server
                protectResponse = protectClient.ProtectResources(oxdHost, oxdPort, protectParams);
            }

            if (OXDType == "web")
            {
                protectParams.ProtectionAccessToken = pat.GetProtectionAccessToken(httpresturl);
                protectResponse = protectClient.ProtectResources(httpresturl, protectParams);
            }

            return protectResponse.Status;

            throw new Exception("Procteting Resource is not successful. Check OXD Server log for error details.");
        }
    }
}