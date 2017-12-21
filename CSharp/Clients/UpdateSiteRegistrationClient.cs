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
    /// A class which is used to update a registered site
    /// </summary>
    public class UpdateSiteRegistrationClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Updates already registered site with input params using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="updateSiteParams">Input params for Update Site Registration command</param>
        /// <returns>UpdateSiteResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok"
        /// }
        public UpdateSiteResponse UpdateSiteRegistration(string oxdHost, int oxdPort, UpdateSiteParams updateSiteParams)
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

            if (updateSiteParams == null)
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
                var commandClient = new CommandClient(oxdHost, oxdPort);
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


        /// <summary>
        /// Updates already registered site with input params using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="registerSiteParams">Input parameters for Register Site via http</param>
        /// <returns>UpdateSiteResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok"
        /// }
        public UpdateSiteResponse UpdateSiteRegistration(string oxdHttpsExtensionUrl, UpdateSiteParams registerSiteParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdHttpsExtensionUrl))
                throw new ArgumentNullException("Oxd Rest Service URL should not be NULL.");


            try
            {
                var cmdUpdateSite = new Command { CommandType = RestCommandType.update_site_registration, CommandParams = registerSiteParams };
                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
                string commandResponse = commandClient.send(cmdUpdateSite);
                var response = JsonConvert.DeserializeObject<UpdateSiteResponse>(commandResponse);
                return response;

            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when updating Client Info.");
                return null;

            }



        }
    }
}
