using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System.Configuration;
using System.Diagnostics.Contracts;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A client class which is used to update a registered site using Oxd Server
    /// </summary>
    public class UpdateSiteRegistrationClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Updates already registered site with input params.
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="updateSiteParams">Input params for Update Site Registration command</param>
        /// <returns></returns>
        public UpdateSiteResponse UpdateSiteRegistration(string host, int port, UpdateSiteParams updateSiteParams)
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

            if(updateSiteParams == null)
            {
                throw new ArgumentNullException("The update site command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(updateSiteParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for updating site.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdUpdateSite = new Command { CommandType = CommandType.update_site_registration, CommandParams = updateSiteParams };
                var commandClient = new CommandClient(host, port);
                string commandResponse = commandClient.send(cmdUpdateSite);

                var response = JsonConvert.DeserializeObject<UpdateSiteResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0}", response.Status));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when updating site.");
                return null;
            }
        }
    }
}
