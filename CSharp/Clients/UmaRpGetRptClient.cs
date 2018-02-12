using CSharp.CommonClasses;
using Newtonsoft.Json;
using oxdCSharp.UMA.CommandParameters;
using oxdCSharp.UMA.CommandResponses;
using System;

namespace oxdCSharp.UMA.Clients
{
    /// <summary>
    /// A class which is used to get RPT from UMA RP
    /// </summary>
    public class UmaRpGetRptClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets RPT token from UMA RP using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="getRptParams">Input params for Get RPT command</param>
        /// <returns>GetRPTResponse</returns>
        /// <example>
        /// <b>Success Response:</b>
        /// {
        ///      "status":"ok",
        ///      "data":{
        ///          "access_token":"SSJHBSUSSJHVhjsgvhsgvshgsv",
        ///          "token_type":"Bearer",
        ///          "pct":"c2F2ZWRjb25zZW50",
        ///          "upgraded":true
        ///      }
        /// }
        /// </example>
        /// <example>
        /// <b>Needs Info Error Response:</b>
        /// {
        /// 	"status": "error",
        /// 	"data": {
        /// 		"error": "need_info",
        /// 		"error_description": "The authorization server needs additional information in order to determine whether the client is authorized to have these permissions.",
        /// 		"details": {
        /// 			"error": "need_info",
        /// 			"ticket": "ZXJyb3JfZGV0YWlscw==",
        /// 			"required_claims": [{
        /// 				"claim_token_format": [
        /// 					"http://openid.net/specs/openid-connect-core-1_0.html#IDToken"
        /// 				],
        /// 				"claim_type": "urn:oid:0.9.2342.19200300.100.1.3",
        /// 				"friendly_name": "email",
        /// 				"issuer": ["https://example.com/idp"],
        /// 				"name": "email23423453ou453"
        /// 			}],
        /// 			"redirect_user": "https://as.example.com/rqp_claims?id=2346576421"
        /// 		}
        /// 	}
        /// }
        /// </example>
        /// <example>
        /// <b>Invalid Ticket Error Response:</b>
        /// {
        ///     "status":"error",
        ///     "data":{
        ///             "error":"invalid_ticket",
        ///             "error_description":"Ticket is not valid (outdated or not present on Authorization Server)."
        ///            }
        /// }
        /// </example>
        public GetRPTResponse GetRPT(string oxdHost, int oxdPort, UmaRpGetRptParams getRptParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdHost))
            {
                throw new ArgumentNullException("Oxd Host should not be NULL.");
            }

            if (oxdPort <= 0)
            {
                throw new ArgumentNullException("Oxd Port should be a valid port number.");
            }

            if (getRptParams == null)
            {
                throw new ArgumentNullException("The get RPT command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getRptParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for getting RPT from UMA RP.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdGetRPT = new Command { CommandType = CommandType.uma_rp_get_rpt, CommandParams = getRptParams };
                var commandClient = new CommandClient(oxdHost, oxdPort);
                string commandResponse = commandClient.send(cmdGetRPT);

                var response = JsonConvert.DeserializeObject<GetRPTResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and RPT is {1}", response.Status, response.Data.Rpt));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting RPT token.");
                return null;
            }
        }


        /// <summary>
        /// Gets RPT token from UMA RP using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="getRptParams">Input params for Get RPT command</param>
        /// <returns>GetRPTResponse</returns>
        /// <example>
        /// <b>Success Response:</b>
        /// {
        ///      "status":"ok",
        ///      "data":{
        ///          "access_token":"SSJHBSUSSJHVhjsgvhsgvshgsv",
        ///          "token_type":"Bearer",
        ///          "pct":"c2F2ZWRjb25zZW50",
        ///          "upgraded":true
        ///      }
        /// }
        /// </example>
        /// <example>
        /// <b>Needs Info Error Response:</b>
        /// {
        /// 	"status": "error",
        /// 	"data": {
        /// 		"error": "need_info",
        /// 		"error_description": "The authorization server needs additional information in order to determine whether the client is authorized to have these permissions.",
        /// 		"details": {
        /// 			"error": "need_info",
        /// 			"ticket": "ZXJyb3JfZGV0YWlscw==",
        /// 			"required_claims": [{
        /// 				"claim_token_format": [
        /// 					"http://openid.net/specs/openid-connect-core-1_0.html#IDToken"
        /// 				],
        /// 				"claim_type": "urn:oid:0.9.2342.19200300.100.1.3",
        /// 				"friendly_name": "email",
        /// 				"issuer": ["https://example.com/idp"],
        /// 				"name": "email23423453ou453"
        /// 			}],
        /// 			"redirect_user": "https://as.example.com/rqp_claims?id=2346576421"
        /// 		}
        /// 	}
        /// }
        /// </example>
        /// <example>
        /// <b>Invalid Ticket Error Response:</b>
        /// {
        ///     "status":"error",
        ///     "data":{
        ///             "error":"invalid_ticket",
        ///             "error_description":"Ticket is not valid (outdated or not present on Authorization Server)."
        ///            }
        /// }
        /// </example>
        public GetRPTResponse GetRPT(string oxdHttpsExtensionUrl, UmaRpGetRptParams getRptParams)
        {

            if (getRptParams == null)
            {
                throw new ArgumentNullException("The get RPT command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getRptParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for getting RPT from UMA RP.");
            }

            try
            {

                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
                return GetRPTResponse(getRptParams, commandClient);
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting RPT token.");
                return null;
            }
        }



        private GetRPTResponse GetRPTResponse(UmaRpGetRptParams getRptParams, CommandClient oxdcommand)
        {
            var cmdGetRPT = new Command { CommandType = CommandType.uma_rp_get_rpt, CommandParams = getRptParams };            
            string commandResponse = oxdcommand.send(cmdGetRPT);
            var response = JsonConvert.DeserializeObject<GetRPTResponse>(commandResponse);
            Logger.Info(string.Format("Got response status as {0} and RPT is {1}", response.Status, response.Data.Rpt));
            return response;
        }



    }
}
