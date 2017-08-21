using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using oxdCSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System.Diagnostics.Contracts;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A client class which is used to get logout URI of a site using Oxd Server
    /// </summary>
    public class GetLogoutUriClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets logout URI of a registered site using Oxd Server
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="getLogoutUriParams">Input params for Get Logout URI command</param>
        /// <returns>Logout Url</returns>
        public GetLogoutUriResponse GetLogoutURL(string host, int port, GetLogoutUrlParams getLogoutUriParams)
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

            if(getLogoutUriParams == null)
            {
                throw new ArgumentNullException("The get logout uri command params should not be NULL.");
            }

            if(string.IsNullOrEmpty(getLogoutUriParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for getting user info.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdGetLogoutUri = new Command { CommandType = CommandType.get_logout_uri, CommandParams = getLogoutUriParams };
                var commandClient = new CommandClient(host, port);
                string commandResponse = commandClient.send(cmdGetLogoutUri);

                var response = JsonConvert.DeserializeObject<GetLogoutUriResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and logout uri is {1}", response.Status, response.Data.LogoutUri));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting logout uri of site.");
                return null;
            }
        }


        /// <summary>
        /// Gets logout URI of a registered site using http
        /// </summary>
        /// <param name="oxdWebUrl">Oxd to http REST service URL</param>
        /// <param name="getLogoutUriParams">Input params for Get Logout URI command</param>
        /// <returns>Logout Url</returns>

        public GetLogoutUriResponse GetLogoutURL(string oxdWebUrl, GetLogoutUrlParams getLogoutUriParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdWebUrl))
                throw new ArgumentNullException("Oxd Rest Service URL should not be NULL.");

            try
            {
                var cmdGetLogoutUri = new Command { CommandType = RestCommandType.get_logout_uri, CommandParams = getLogoutUriParams };
                var commandClient = new CommandClient(oxdWebUrl);
                string commandResponse = commandClient.send(cmdGetLogoutUri);
                var response = JsonConvert.DeserializeObject<GetLogoutUriResponse>(commandResponse);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting logout uri of site.");
                return null;

            }

        }

    }
}
