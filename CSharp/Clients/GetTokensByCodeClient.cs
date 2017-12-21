using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using CSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System.Diagnostics.Contracts;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A class which is used to Get Tokens by Auth Code and Auth State of a site
    /// </summary>
    public class GetTokensByCodeClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets different type of tokens by Code and state using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="getTokensByCodeParams">Input params for Get Tokens by Code command</param>
        /// <returns>GetTokensByCodeResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"access_token": "cfa5b25f-09d4-47bf-8762-98719406ace2",
        /// 		"expires_in": 299,
        /// 		"refresh_token": "51798a79-5be4-47d1-a898-4a65fe207b5c",
        /// 		"id_token": "eyJraWQiOiIdfgfdvcdyhMltA",
        /// 		"id_token_claims": {
        /// 			"iss": ["https://idp-hostname"],
        /// 			"sub": ["SGUMcFlAj3QlkOQVgwYpSozbjvynk4B2VNpr-mDnuVw"],
        /// 			"aud": ["@!4116.DF7C.62D4.D0CF!0001!D420.A5E5!0008!6156.5BD4.5F9B.D172"],
        /// 			"nonce": ["4pn3vgisdg4em0ups2ud79iig5"],
        /// 			"exp": [1513861298],
        /// 			"iat": [1513857698],
        /// 			"at_hash": ["8tCEomWN_VySJwkJ4lxYfA"]
        /// 		}
        /// 	}
        /// }
        /// </example>
        public GetTokensByCodeResponse GetTokensByCode(string oxdHost, int oxdPort, GetTokensByCodeParams getTokensByCodeParams)
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

            if (getTokensByCodeParams == null)
            {
                throw new ArgumentNullException("The get tokens by code command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getTokensByCodeParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for getting tokens.");
            }

            if (string.IsNullOrEmpty(getTokensByCodeParams.Code))
            {
                throw new MissingFieldException("Auth Code is required for getting tokens.");
            }

            if (string.IsNullOrEmpty(getTokensByCodeParams.State))
            {
                throw new MissingFieldException("Auth State is required for getting tokens.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdGetTokensByCode = new Command { CommandType = CommandType.get_tokens_by_code, CommandParams = getTokensByCodeParams };
                var commandClient = new CommandClient(oxdHost, oxdPort);
                string commandResponse = commandClient.send(cmdGetTokensByCode);

                var response = JsonConvert.DeserializeObject<GetTokensByCodeResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and access token is {1}", response.Status, response.Data.AccessToken));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting tokens of site.");
                return null;
            }
        }


        /// <summary>
        /// Gets different type of tokens by Code and state using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="getTokensByCodeParams">Input params for Get Tokens by Code command</param>
        /// <returns>GetTokensByCodeResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"access_token": "cfa5b25f-09d4-47bf-8762-98719406ace2",
        /// 		"expires_in": 299,
        /// 		"refresh_token": "51798a79-5be4-47d1-a898-4a65fe207b5c",
        /// 		"id_token": "eyJraWQiOiIdfgfdvcdyhMltA",
        /// 		"id_token_claims": {
        /// 			"iss": ["https://idp-hostname"],
        /// 			"sub": ["SGUMcFlAj3QlkOQVgwYpSozbjvynk4B2VNpr-mDnuVw"],
        /// 			"aud": ["@!4116.DF7C.62D4.D0CF!0001!D420.A5E5!0008!6156.5BD4.5F9B.D172"],
        /// 			"nonce": ["4pn3vgisdg4em0ups2ud79iig5"],
        /// 			"exp": [1513861298],
        /// 			"iat": [1513857698],
        /// 			"at_hash": ["8tCEomWN_VySJwkJ4lxYfA"]
        /// 		}
        /// 	}
        /// }
        /// </example>
        public GetTokensByCodeResponse GetTokensByCode(string oxdHttpsExtensionUrl, GetTokensByCodeParams getTokensByCodeParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdHttpsExtensionUrl))
                throw new ArgumentNullException("Oxd Rest Service URL should not be NULL.");

            try
            {
                var cmdGetTokensByCode = new Command { CommandType = RestCommandType.get_tokens_by_code, CommandParams = getTokensByCodeParams };
                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
                string commandResponse = commandClient.send(cmdGetTokensByCode);
                var response = JsonConvert.DeserializeObject<GetTokensByCodeResponse>(commandResponse);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting Token Code.");
                return null;

            }

        }

    }
}
