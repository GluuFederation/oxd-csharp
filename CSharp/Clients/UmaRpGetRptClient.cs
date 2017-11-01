using CSharp.CommonClasses;
using Newtonsoft.Json;
using oxdCSharp.UMA.CommandParameters;
using oxdCSharp.UMA.CommandResponses;
using System;

namespace oxdCSharp.UMA.Clients
{
    /// <summary>
    /// A client class which is used to get RPT from UMA RP
    /// </summary>
    public class UmaRpGetRptClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets RPT token from UMA RP
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="getRptParams">Input params for Get RPT command</param>
        /// <returns></returns>
        public GetRPTResponse GetRPT(string host, int port, UmaRpGetRptParams getRptParams)
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

            if (getRptParams == null)
            {
                throw new ArgumentNullException("The get RPT command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getRptParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for getting RPT from UMA RP.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdGetRPT = new Command { CommandType = CommandType.uma_rp_get_rpt, CommandParams = getRptParams };
                var commandClient = new CommandClient(host, port);
                string commandResponse = commandClient.send(cmdGetRPT);

                var response = JsonConvert.DeserializeObject<GetRPTResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and RPT is {1}", response.Status, response.Data.Rpt));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting RPT token.");
                return null;
            }
        }






        /// <summary>
        /// Gets RPT token from UMA RP
        /// </summary>
        /// <param name="oxdweburl">oxdweburl</param>
        /// <param name="getRptParams">Input params for Get RPT command</param>
        /// <returns></returns>
        public GetRPTResponse GetRPT(string oxdweburl, UmaRpGetRptParams getRptParams)
        {

            if (getRptParams == null)
            {
                throw new ArgumentNullException("The get RPT command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getRptParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for getting RPT from UMA RP.");
            }

            try
            {

                var commandClient = new CommandClient(oxdweburl);
                return GetRPTResponse(getRptParams, commandClient);
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting RPT token.");
                return null;
            }
        }



        private GetRPTResponse GetRPTResponse(UmaRpGetRptParams getRptParams,CommandClient oxdcommand)
        {
            var cmdGetRPT = new Command { CommandType = CommandType.uma_rp_get_rpt, CommandParams = getRptParams };            
            string commandResponse = oxdcommand.send(cmdGetRPT);
            var response = JsonConvert.DeserializeObject<GetRPTResponse>(commandResponse);
            Logger.Info(string.Format("Got response status as {0} and RPT is {1}", response.Status, response.Data.Rpt));
            return response;
        }



    }
}
