using CSharp.CommonClasses;
using Newtonsoft.Json;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A client class which is used to authorize RPT
    /// </summary>
    public class UmaRpAuthorizeRptClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Authorizes RPT with the given RPT and Ticket
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="authorizeRptParams">Input Params for UMA RP Authorize RPT command</param>
        /// <returns></returns>
        public UmaRpAuthorizeRptResponse AuthorizeRpt(string host, int port, UmaRpAuthorizeRptParams authorizeRptParams)
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

            if (authorizeRptParams == null)
            {
                throw new ArgumentNullException("The authorize RPT command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(authorizeRptParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for authorizing RPT.");
            }

            if (string.IsNullOrEmpty(authorizeRptParams.RPT))
            {
                throw new MissingFieldException("Valid RPT is required for authorizing RPT.");
            }

            if (string.IsNullOrEmpty(authorizeRptParams.Ticket))
            {
                throw new MissingFieldException("Valid ticket is required for authorizing RPT.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdAuthorizeRpt = new Command { CommandType = CommandType.uma_rp_authorize_rpt, CommandParams = authorizeRptParams };
                var commandClient = new CommandClient(host, port);
                string commandResponse = commandClient.send(cmdAuthorizeRpt);

                var response = JsonConvert.DeserializeObject<UmaRpAuthorizeRptResponse>(commandResponse);
                if(response.Status.ToLower().Equals("error"))
                {
                    Logger.Info(string.Format("Got response status as {0}. The error code is {1} and error description is {2}", 
                        response.Status, response.Data.AuthorizeErrorCode, response.Data.AuthorizeErrorDescription));
                }
                else
                {
                    Logger.Info(string.Format("Got response status as {0}", response.Status));
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when authorizing RPT token.");
                return null;
            }
        }
    }
}