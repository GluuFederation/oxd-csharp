using CSharp.CommonClasses;
using Newtonsoft.Json;
using oxdCSharp.UMA.CommandParameters;
using oxdCSharp.UMA.CommandResponses;
using System;

namespace oxdCSharp.UMA.Clients
{
    /// <summary>
    /// A client class which is used to check access of a UMA resource in Resource Server
    /// </summary>
    public class UmaRsCheckAccessClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// oxd-local Checks Access for UMA RS resource
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="umaRsCheckAccessParams">Input params for UMA RS Check Access command</param>
        /// <returns></returns>
        public UmaRsCheckAccessResponse CheckAccess(string host, int port, UmaRsCheckAccessParams umaRsCheckAccessParams)
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

            if (umaRsCheckAccessParams == null)
            {
                throw new ArgumentNullException("The UMA RS Check Access command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(umaRsCheckAccessParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for checking access of UMA resources.");
            }

            if (string.IsNullOrEmpty(umaRsCheckAccessParams.Path))
            {
                throw new MissingFieldException("Valid Path is required for checking access of UMA resource in RS.");
            }

            if (string.IsNullOrEmpty(umaRsCheckAccessParams.HttpMethod))
            {
                throw new MissingFieldException("Valid HTTP method is required for checking access of UMA resource in RS.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdUmaRsCheckAccess = new Command { CommandType = CommandType.uma_rs_check_access, CommandParams = umaRsCheckAccessParams };
                var commandClient = new CommandClient(host, port);
                string commandResponse = commandClient.send(cmdUmaRsCheckAccess);

                var response = JsonConvert.DeserializeObject<UmaRsCheckAccessResponse>(commandResponse);

                if(response.Status.ToLower().Equals("error"))
                {
                    Logger.Info(string.Format("Got response status as {0}. The error is {1} with description {2}", 
                        response.Status, response.Data.Error, response.Data.ErrorDescription));
                }
                else
                {
                    Logger.Info(string.Format("Got response status as {0} and the access is {1}", response.Status, response.Data.Access));
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when checking access of UMA resource.");
                return null;
            }
        }



        /// <summary>
        /// oxd-web Checks Access for UMA RS resource
        /// </summary>
        /// <param name="oxdweburl">Oxd Web url</param>
        /// <param name="umaRsCheckAccessParams">Input params for UMA RS Check Access command</param>
        /// <returns></returns>
        public UmaRsCheckAccessResponse CheckAccess(string oxdweburl, UmaRsCheckAccessParams umaRsCheckAccessParams)
        {
            Logger.Info("Verifying input parameters.");
           

            if (umaRsCheckAccessParams == null)
            {
                throw new ArgumentNullException("The UMA RS Check Access command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(umaRsCheckAccessParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for checking access of UMA resources.");
            }

            if (string.IsNullOrEmpty(umaRsCheckAccessParams.Path))
            {
                throw new MissingFieldException("Valid Path is required for checking access of UMA resource in RS.");
            }

            if (string.IsNullOrEmpty(umaRsCheckAccessParams.HttpMethod))
            {
                throw new MissingFieldException("Valid HTTP method is required for checking access of UMA resource in RS.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdUmaRsCheckAccess = new Command { CommandType = CommandType.uma_rs_check_access, CommandParams = umaRsCheckAccessParams };
                var commandClient = new CommandClient(oxdweburl);
                string commandResponse = commandClient.send(cmdUmaRsCheckAccess);

                var response = JsonConvert.DeserializeObject<UmaRsCheckAccessResponse>(commandResponse);

                if (response.Status.ToLower().Equals("error"))
                {
                    Logger.Info(string.Format("Got response status as {0}. The error is {1} with description {2}",
                        response.Status, response.Data.Error, response.Data.ErrorDescription));
                }
                else
                {
                    Logger.Info(string.Format("Got response status as {0} and the access is {1}", response.Status, response.Data.Access));
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when checking access of UMA resource.");
                return null;
            }
        }

    }
}