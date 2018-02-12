using CSharp.CommonClasses;
using Newtonsoft.Json;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A class which is used to remove a site
    /// </summary>
    public class RemoveSiteClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Remove a site using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="removeSiteParams">Input parameters for Remove Site command</param>
        /// <returns>RemoveSiteResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        ///     "status":"ok",
        ///     "data":{
        ///         "oxd_id":"c73134c8-c4ca-4bab-9baa-2e0ca20cc433"
        ///     }
        /// }
        /// </example>
        public RemoveSiteResponse RemoveSite(String oxdHost, int oxdPort, RemoveSiteParams removeSiteParams)
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

            if (removeSiteParams == null)
            {
                throw new ArgumentNullException("The remove site command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(removeSiteParams.OxdId))
            {
                throw new MissingFieldException("OxdId is required.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdRemoveSite = new Command { CommandType = CommandType.remove_site, CommandParams = removeSiteParams };
                var commandClient = new CommandClient(oxdHost, oxdPort);
                string commandResponse = commandClient.send(cmdRemoveSite);

                var response = JsonConvert.DeserializeObject<RemoveSiteResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and Oxd ID is {1}", response.Status, response.Data.OxdId));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when removing site.");
                return null;
            }
        }


        /// <summary>
        /// Remove a site using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="removeSiteParams">Input parameters for Remove Site via http</param>
        /// <returns>RemoveSiteResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        ///     "status":"ok",
        ///     "data":{
        ///         "oxd_id":"c73134c8-c4ca-4bab-9baa-2e0ca20cc433"
        ///     }
        /// }
        /// </example>
        public RemoveSiteResponse RemoveSite(string oxdHttpsExtensionUrl, RemoveSiteParams removeSiteParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdHttpsExtensionUrl))
                throw new ArgumentNullException("Oxd Web URL should not be NULL.");
            try
            {
                var cmdRemoveSite = new Command { CommandType = RestCommandType.remove_site, CommandParams = removeSiteParams };
                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
                string commandResponse = commandClient.send(cmdRemoveSite);
                var response = JsonConvert.DeserializeObject<RemoveSiteResponse>(commandResponse);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when removing site.");
                return null;

            }



        }
    }
}
