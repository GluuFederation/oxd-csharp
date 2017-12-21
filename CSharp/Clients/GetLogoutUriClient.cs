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
    /// A class which is used to get logout URI of a site
    /// </summary>
    public class GetLogoutUriClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets logout URI of a registered site using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="getLogoutUriParams">Input params for Get Logout URI command</param>
        /// <returns>GetLogoutUriResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"uri": "https://idp-hostname/oxauth/restv1/end_session?id_token_hint=eyJraWQiOgt6yxMMltA"
        /// 	}
        /// }
        /// </example>
        public GetLogoutUriResponse GetLogoutURL(string oxdHost, int oxdPort, GetLogoutUrlParams getLogoutUriParams)
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
                var commandClient = new CommandClient(oxdHost, oxdPort);
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
        /// Gets logout URI of a registered site using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="getLogoutUriParams">Input params for Get Logout URI command</param>
        /// <returns>GetLogoutUriResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"uri": "https://idp-hostname/oxauth/restv1/end_session?id_token_hint=eyJraWQiOgt6yxMMltA"
        /// 	}
        /// }
        /// </example>
        public GetLogoutUriResponse GetLogoutURL(string oxdHttpsExtensionUrl, GetLogoutUrlParams getLogoutUriParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdHttpsExtensionUrl))
                throw new ArgumentNullException("Oxd Rest Service URL should not be NULL.");

            try
            {
                var cmdGetLogoutUri = new Command { CommandType = RestCommandType.get_logout_uri, CommandParams = getLogoutUriParams };
                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
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
