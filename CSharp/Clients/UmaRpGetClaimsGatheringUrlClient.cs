using System;
using oxdCSharp.UMA.CommandParameters;
using CSharp.CommonClasses;
using Newtonsoft.Json;
using oxdCSharp.UMA.CommandResponses;

namespace oxdCSharp.UMA.Clients
{
    /// <summary>
    /// A class which is used to get claims gathering URL from UMA RP
    /// </summary>
    public class UmaRpGetClaimsGatheringUrlClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get Claims Gathering Url using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="umaRpGetClaimsGatheringUrlParams">Input params for UMA Claims Gathering URL command</param>
        /// <returns>UmaRpGetClaimsGatheringUrlResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"url": "https://idp-hostname/oxauth/restv1/uma/gather_claims?client_id=@!4116.DF7C.62D4.D0CF!0001!D420.A5E5!0008!6156.5BD4.5F9B.D172&ticket=d26c30fd-eb94-40da-9f61-0c424acedf0e&claims_redirect_uri=https://client.example.com&state=fk0vl0lvmn8imecjf67m57r772",
        /// 		"state": "fk0vl0lvmn8imecjf67m57r772",
        /// 		"error": null,
        /// 		"error_description": null
        /// 	}
        /// }
        /// </example>
        public UmaRpGetClaimsGatheringUrlResponse GetClaimsGatheringUrl(string oxdHost, int oxdPort, UmaRpGetClaimsGatheringUrlParams umaRpGetClaimsGatheringUrlParams)
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
                var commandClient = new CommandClient(oxdHost, oxdPort);
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
        /// Get Claims Gathering Url using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="umaRpGetClaimsGatheringUrlParams">Input params for UMA Claims Gathering URL command</param>
        /// <returns>UmaRpGetClaimsGatheringUrlResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"url": "https://idp-hostname/oxauth/restv1/uma/gather_claims?client_id=@!4116.DF7C.62D4.D0CF!0001!D420.A5E5!0008!6156.5BD4.5F9B.D172&ticket=d26c30fd-eb94-40da-9f61-0c424acedf0e&claims_redirect_uri=https://client.example.com&state=fk0vl0lvmn8imecjf67m57r772",
        /// 		"state": "fk0vl0lvmn8imecjf67m57r772",
        /// 		"error": null,
        /// 		"error_description": null
        /// 	}
        /// }
        /// </example>
        public UmaRpGetClaimsGatheringUrlResponse GetClaimsGatheringUrl(string oxdHttpsExtensionUrl, UmaRpGetClaimsGatheringUrlParams umaRpGetClaimsGatheringUrlParams)
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
                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
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
