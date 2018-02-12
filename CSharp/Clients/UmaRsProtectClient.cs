using CSharp.CommonClasses;
using Newtonsoft.Json;
using oxdCSharp.UMA.CommandParameters;
using oxdCSharp.UMA.CommandResponses;
using System;

namespace oxdCSharp.UMA.Clients
{
    /// <summary>
    /// A class which is used to protect UMA Resource in Resource Server
    /// </summary>
    public class UmaRsProtectClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Protects set of UMA resources in Resource Server using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="umaRsProtectParams">Input params for UMA RS Protect command</param>
        /// <returns>UmaRsProtectResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok"
        /// }
        /// </example>
        public UmaRsProtectResponse ProtectResources(string oxdHost, int oxdPort, UmaRsProtectParams umaRsProtectParams)
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
                var commandClient = new CommandClient(oxdHost, oxdPort);
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
        /// Protects set of UMA resources in Resource Server using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="umaRsProtectParams">Input params for UMA RS Protect command</param>
        /// <returns>UmaRsProtectResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok"
        /// }
        /// </example>
        public UmaRsProtectResponse ProtectResources(string oxdHttpsExtensionUrl, UmaRsProtectParams umaRsProtectParams)
        {
           
            if (umaRsProtectParams == null)
            {
                throw new ArgumentNullException("The UMA RS Protect command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(umaRsProtectParams.OxdId))
            {
                throw new MissingFieldException("oxd Id is required for protecting UMA resources.");
            }

            if (umaRsProtectParams.ProtectResources == null || umaRsProtectParams.ProtectResources.Count == 0)
            {
                throw new MissingFieldException("Valid resources are required for protecting UMA resource in RS.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdUmaRsProtect = new Command { CommandType = CommandType.uma_rs_protect, CommandParams = umaRsProtectParams };


                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
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
    }
}
