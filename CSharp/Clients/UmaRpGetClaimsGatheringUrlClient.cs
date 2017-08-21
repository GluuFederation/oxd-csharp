using System;
using oxdCSharp.UMA.CommandParameters;
using oxdCSharp.CommonClasses;
using Newtonsoft.Json;
using oxdCSharp.UMA.CommandResponses;

namespace oxdCSharp.UMA.UMA.Clients
{
    public class UmaRpGetClaimsGatheringUrlClient
    {


        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// oxd-local Get Claims Gathering Url
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="umaRpGetClaimsGatheringUrlParams">Input params for UMA Claims Gathering URL command</param>
        /// <returns></returns>
        public UmaRpGetClaimsGatheringUrlResponse GetClaimsGatheringUrl(string host, int port, UmaRpGetClaimsGatheringUrlParams umaRpGetClaimsGatheringUrlParams)
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

            if (umaRpGetClaimsGatheringUrlParams == null)
            {
                throw new ArgumentNullException("The UMA RS Check Access command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(umaRpGetClaimsGatheringUrlParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for checking access of UMA resources.");
            }


            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdUmaRpGetClaimsGatheringUrl = new Command { CommandType = CommandType.uma_rp_get_claims_gathering_url, CommandParams = umaRpGetClaimsGatheringUrlParams };
                var commandClient = new CommandClient(host, port);
                string commandResponse = commandClient.send(cmdUmaRpGetClaimsGatheringUrl);

                var response = JsonConvert.DeserializeObject<UmaRpGetClaimsGatheringUrlResponse>(commandResponse);

                if (response.Status.ToLower().Equals("error"))
                {
                    Logger.Info(string.Format("Got response status as {0}. The error is {1} with description {2}",
                        response.Status, response.Data.Error, response.Data.ErrorDescription));
                }
                else
                {
                    Logger.Info(string.Format("Got response status as {0} ", response.Status));
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when Getting UMA Claims Gathering URL.");
                return null;
            }
        }



        /// <summary>
        /// oxd-web Get Claims Gathering Url
        /// </summary>
        /// <param name="oxdweburl">Oxd Web url</param>
        /// <param name="umaRpGetClaimsGatheringUrlParams">Input params for UMA Claims Gathering URL command</param>
        /// <returns></returns>
        public UmaRpGetClaimsGatheringUrlResponse GetClaimsGatheringUrl(string oxdweburl, UmaRpGetClaimsGatheringUrlParams umaRpGetClaimsGatheringUrlParams)
        {
            Logger.Info("Verifying input parameters.");


            if (umaRpGetClaimsGatheringUrlParams == null)
            {
                throw new ArgumentNullException("The UMA RS Get Claims Gathering Url params should not be NULL.");
            }

            if (string.IsNullOrEmpty(umaRpGetClaimsGatheringUrlParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for  Get Claims Gathering Url ");
            }

          

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdUmaRpGetClaimsGatheringUrl = new Command { CommandType = CommandType.uma_rp_get_claims_gathering_url, CommandParams = umaRpGetClaimsGatheringUrlParams };
                var commandClient = new CommandClient(oxdweburl);
                string commandResponse = commandClient.send(cmdUmaRpGetClaimsGatheringUrl);

                var response = JsonConvert.DeserializeObject<UmaRpGetClaimsGatheringUrlResponse>(commandResponse);

                if (response.Status.ToLower().Equals("error"))
                {
                    Logger.Info(string.Format("Got response status as {0}. The error is {1} with description {2}",
                        response.Status, response.Data.Error, response.Data.ErrorDescription));
                }
                else
                {
                    Logger.Info(string.Format("Got response status as {0} ", response.Status));
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when Getting UMA Claims Gathering URL.");
                return null;
            }
        }
    }
}
