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
    /// A client class which is used to Get Authorization URL using Oxd Server
    /// </summary>
    public class GetAuthorizationUrlClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets Authorization URL of a site using input params
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="getAuthUrlParams">Input params for Get Authorization URL command</param>
        /// <returns></returns>
        public GetAuthorizationUrlResponse GetAuthorizationURL(string host, int port, GetAuthorizationUrlParams getAuthUrlParams)
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

            if (getAuthUrlParams == null)
            {
                throw new ArgumentNullException("The get auth url command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getAuthUrlParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for getting auth url of site.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdGetAuthUrl = new Command { CommandType = CommandType.get_authorization_url, CommandParams = getAuthUrlParams };
                var commandClient = new CommandClient(host, port);
                string commandResponse = commandClient.send(cmdGetAuthUrl);

                var response = JsonConvert.DeserializeObject<GetAuthorizationUrlResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and auth url is {1}", response.Status, response.Data.AuthorizationUrl));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting auth url of site.");
                return null;
            }
        }

        /// <summary>
        /// Gets Authorization URL via http using the params 
        /// </summary>
        /// <param name="oxdWebUrl">Oxd to http REST service URL</param>
        /// <param name="getAuthUrlParams">Input params for Get Authorization URL command via http</param>
        /// <returns></returns>

        public GetAuthorizationUrlResponse GetAuthorizationURL(string oxdWebUrl, GetAuthorizationUrlParams getAuthUrlParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdWebUrl))
                throw new ArgumentNullException("Oxd Rest Service URL should not be NULL.");

            try
            {
                var cmdGetAuthUrl = new Command { CommandType = RestCommandType.get_authorization_url, CommandParams = getAuthUrlParams };
                var commandClient = new CommandClient(oxdWebUrl);
                string commandResponse = commandClient.send(cmdGetAuthUrl);
                var response = JsonConvert.DeserializeObject<GetAuthorizationUrlResponse>(commandResponse);
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
