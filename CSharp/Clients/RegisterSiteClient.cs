using Newtonsoft.Json;
using System;
using CSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System.Diagnostics.Contracts;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A class which is used to register a site
    /// </summary>
    public class RegisterSiteClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Registers a new site using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="registerSiteParams">Input parameters for Register Site command</param>
        /// <returns>RegisterSiteResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        ///     "status":"ok",
        ///     "data":{
        ///         "oxd_id":"c73134c8-c4ca-4bab-9baa-2e0ca20cc433"
        ///     }
        /// }
        /// </example>
        public RegisterSiteResponse RegisterSite(String oxdHost, int oxdPort, RegisterSiteParams registerSiteParams)
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

            if (registerSiteParams == null)
            {
                throw new ArgumentNullException("The register site command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(registerSiteParams.AuthorizationRedirectUri))
            {
                throw new MissingFieldException("Authorization Redirect Uri is required.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdRegisterSite = new Command { CommandType = CommandType.register_site, CommandParams = registerSiteParams };
                var commandClient = new CommandClient(oxdHost, oxdPort);
                string commandResponse = commandClient.send(cmdRegisterSite);

                var response = JsonConvert.DeserializeObject<RegisterSiteResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and Oxd ID is {1}", response.Status, response.Data.OxdId));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when registering site.");
                return null;
            }
        }


        /// <summary>
        /// Registers a new site using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="registerSiteParams">Input parameters for Register Site via http</param>
        /// <returns>RegisterSiteResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        ///     "status":"ok",
        ///     "data":{
        ///         "oxd_id":"c73134c8-c4ca-4bab-9baa-2e0ca20cc433"
        ///     }
        /// }
        /// </example>
        public RegisterSiteResponse RegisterSite(string oxdHttpsExtensionUrl, RegisterSiteParams registerSiteParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdHttpsExtensionUrl))
                throw new ArgumentNullException("Oxd Web URL should not be NULL.");
            try
            {
                var cmdRegisterSite = new Command { CommandType = RestCommandType.register_site, CommandParams = registerSiteParams };
                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
                string commandResponse = commandClient.send(cmdRegisterSite);
                var response = JsonConvert.DeserializeObject<RegisterSiteResponse>(commandResponse);
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
