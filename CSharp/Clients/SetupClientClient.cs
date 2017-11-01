using Newtonsoft.Json;
using System;
using CSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;

namespace oxdCSharp.Clients
{
    public class SetupClientClient
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// oxd-local Setup a new Client using the params
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="setupClientParams">Input parameters for Setup Client command</param>
        /// <returns></returns>
        public SetupClientResponse SetupClient(String host, int port, SetupClientParams setupClientParams)
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

            if (setupClientParams == null)
            {
                throw new ArgumentNullException("The Setup Client command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(setupClientParams.AuthorizationRedirectUri))
            {
                throw new MissingFieldException("Authorization Redirect Uri is required.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                setupClientParams.ProgrammingLanguage = "csharp";
                var cmdSetupClient = new Command { CommandType = CommandType.setup_client, CommandParams = setupClientParams };
                var commandClient = new CommandClient(host, port);
                string commandResponse = commandClient.send(cmdSetupClient);

                var response = JsonConvert.DeserializeObject<SetupClientResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and Oxd ID is {1}", response.Status, response.Data.OxdId));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when Setting up Client.");
                return null;
            }
        }
        /// <summary>
        /// oxd-web Setup a new Client via http using the params 
        /// </summary>
        /// <param name="oxdweburl">Oxd to http REST service URL</param>
        /// <param name="setupClientParams">Input parameters for Setup  Client via http</param>
        /// <returns></returns>

        public SetupClientResponse SetupClient(string oxdweburl, SetupClientParams setupClientParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdweburl))
                throw new ArgumentNullException("Oxd Rest Service URL should not be NULL.");
            try
            {
                var cmdSetupClient = new Command { CommandType = RestCommandType.setup_client, CommandParams = setupClientParams };
                var commandClient = new CommandClient(oxdweburl);
                string commandResponse = commandClient.send(cmdSetupClient);
                var response = JsonConvert.DeserializeObject<SetupClientResponse>(commandResponse);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when Setting up Client");
                return null;

            }

        }
    }
}
