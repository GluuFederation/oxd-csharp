using oxdCSharp.CommonClasses;
using Newtonsoft.Json;
using oxdCSharp.UMA.CommandParameters;
using oxdCSharp.UMA.CommandResponses;
using System;

namespace oxdCSharp.UMA.Clients
{
    /// <summary>
    /// A client class which is used to protect UMA Resource in Resource Server
    /// </summary>
    public class UmaRsProtectClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// oxd-local Protects set of UMA resources in Resource Server
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="umaRsProtectParams">Input params for UMA RS Protect command</param>
        /// <returns></returns>
        public UmaRsProtectResponse ProtectResources(string host, int port, UmaRsProtectParams umaRsProtectParams)
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

            if (umaRsProtectParams == null)
            {
                throw new ArgumentNullException("The UMA RS Protect command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(umaRsProtectParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for protecting UMA resources.");
            }

            if (umaRsProtectParams.ProtectResources == null || umaRsProtectParams.ProtectResources.Count == 0)
            {
                throw new MissingFieldException("Valid resources are required for protecting UMA resource in RS.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdUmaRsProtect = new Command { CommandType = CommandType.uma_rs_protect, CommandParams = umaRsProtectParams };
                var commandClient = new CommandClient(host, port);
                string commandResponse = commandClient.send(cmdUmaRsProtect);

                var response = JsonConvert.DeserializeObject<UmaRsProtectResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0}", response.Status));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when protecting UMA resource.");
                return null;
            }
        }


        /// <summary>
        /// oxd-web Protects set of UMA resources in Resource Server
        /// </summary>
        /// <param name="oxdWebUrl">Oxd Web url</param>
        /// <param name="umaRsProtectParams">Input params for UMA RS Protect command</param>
        /// <returns></returns>
        public UmaRsProtectResponse ProtectResources(string oxdWebUrl, UmaRsProtectParams umaRsProtectParams)
        {
           
            if (umaRsProtectParams == null)
            {
                throw new ArgumentNullException("oxd-web The UMA RS Protect command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(umaRsProtectParams.OxdId))
            {
                throw new MissingFieldException("oxd-web Oxd ID is required for protecting UMA resources.");
            }

            if (umaRsProtectParams.ProtectResources == null || umaRsProtectParams.ProtectResources.Count == 0)
            {
                throw new MissingFieldException("oxd-web Valid resources are required for protecting UMA resource in RS.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdUmaRsProtect = new Command { CommandType = CommandType.uma_rs_protect, CommandParams = umaRsProtectParams };


                var commandClient = new CommandClient(oxdWebUrl);
                string commandResponse = commandClient.send(cmdUmaRsProtect);
                var response = JsonConvert.DeserializeObject<UmaRsProtectResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0}", response.Status));
                return response;
            
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when protecting UMA resource (OXD Web).");
                return null;
            }
        }
    }
}