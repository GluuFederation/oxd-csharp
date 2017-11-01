using Newtonsoft.Json;
using System;
using CSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System.Diagnostics.Contracts;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A client class which is used to register a site using Oxd Server
    /// </summary>
    public class RegisterSiteClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Registers a new site using the params
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="registerSiteParams">Input parameters for Register Site command</param>
        /// <returns></returns>
        public RegisterSiteResponse RegisterSite(String host, int port, RegisterSiteParams registerSiteParams)
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
                var commandClient = new CommandClient(host, port);
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
        /// Registers a new site via http using the params 
        /// </summary>
        /// <param name="oxdWebUrl">Oxd to http REST service URL</param>
        /// <param name="registerSiteParams">Input parameters for Register Site via http</param>
        /// <returns></returns>

        public RegisterSiteResponse RegisterSite(string oxdWebUrl, RegisterSiteParams registerSiteParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdWebUrl))
                throw new ArgumentNullException("Oxd Web URL should not be NULL.");
            try
            {
                var cmdRegisterSite = new Command { CommandType = RestCommandType.register_site, CommandParams = registerSiteParams };
                var commandClient = new CommandClient(oxdWebUrl);
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