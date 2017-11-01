using System;
using Newtonsoft.Json;
using CSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;

namespace oxdCSharp.Clients
{
   public class GetClientTokenClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets Authorization URL of a site using input params
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="getClientTokenParams">Input params to Get  Client Acccess Token  command</param>
        /// <returns>Client Token</returns>
        public GetClientTokenResponse GetClientToken(string host, int port, GetClientTokenParams getClientTokenParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException("Oxd Host should not be NULL.");
            }

            if (port <= 0)
            {
                throw new ArgumentNullException("Oxd Port should be a valid port number.");
            }

            if (getClientTokenParams == null)
            {
                throw new ArgumentNullException("The get auth url command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getClientTokenParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for getting auth url of site.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdGetClientAccessToken = new Command { CommandType = CommandType.get_client_token, CommandParams = getClientTokenParams };
                var commandClient = new CommandClient(host, port);
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
        /// Gets Client Access Token
        /// </summary>
        /// <param name="oxdtohttpurl">Oxd to http REST service URL</param>
        /// <param name="getClientTokenParams">Input params to Get  Client Acccess Token  command</param>
        /// <returns>Client Token</returns>
       
        public GetClientTokenResponse GetClientToken(string oxdtohttpurl, GetClientTokenParams getClientTokenParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdtohttpurl))
                throw new ArgumentNullException("Oxd Rest Service URL should not be NULL.");

            try
            {
                var cmdGetClientAccessToken = new Command { CommandType = RestCommandType.get_client_token, CommandParams = getClientTokenParams };
                var commandClient = new CommandClient(oxdtohttpurl);
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
