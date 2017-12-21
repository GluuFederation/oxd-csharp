using System;
using Newtonsoft.Json;
using CSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A class which is used to get the client token which can be used for protection
    /// </summary>
    public class GetClientTokenClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets Client Access Token using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="getClientTokenParams">Input params to Get  Client Acccess Token  command</param>
        /// <returns>GetClientTokenResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"access_token": "7ec389c5-64f8-49a3-a80c-e3e16d134bcb",
        /// 		"expires_in": 299,
        /// 		"refresh_token": null,
        /// 		"scope": "openid"
        /// 	}
        /// }
        /// </example>
        public GetClientTokenResponse GetClientToken(string oxdHost, int oxdPort, GetClientTokenParams getClientTokenParams)
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

            if (getClientTokenParams == null)
            {
                throw new ArgumentNullException("The get auth url command params should not be NULL.");
            }
            

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdGetClientAccessToken = new Command { CommandType = CommandType.get_client_token, CommandParams = getClientTokenParams };
                var commandClient = new CommandClient(oxdHost, oxdPort);
                string commandResponse = commandClient.send(cmdGetClientAccessToken);

                var response = JsonConvert.DeserializeObject<GetClientTokenResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and auth url is {1}", response.Status, response.Data.accessToken));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting auth url of site.");
                return null;
            }
        }


        /// <summary>
        /// Gets Client Access Token using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="getClientTokenParams">Input params to Get  Client Acccess Token  command</param>
        /// <returns>GetClientTokenResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"access_token": "7ec389c5-64f8-49a3-a80c-e3e16d134bcb",
        /// 		"expires_in": 299,
        /// 		"refresh_token": null,
        /// 		"scope": "openid"
        /// 	}
        /// }
        /// </example>
        public GetClientTokenResponse GetClientToken(string oxdHttpsExtensionUrl, GetClientTokenParams getClientTokenParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdHttpsExtensionUrl))
                throw new ArgumentNullException("Oxd Rest Service URL should not be NULL.");

            try
            {
                var cmdGetClientAccessToken = new Command { CommandType = RestCommandType.get_client_token, CommandParams = getClientTokenParams };
                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
                string commandResponse = commandClient.send(cmdGetClientAccessToken);
                var response = JsonConvert.DeserializeObject<GetClientTokenResponse>(commandResponse);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting Authorization url");
                return null;

            }

        }
    }
}
