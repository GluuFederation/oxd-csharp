using CSharp.CommonClasses;
using Newtonsoft.Json;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A client class which is used to get GAT token
    /// </summary>
    public class GetGATClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets GAT (Gluu Access Token) from the Resource Provider
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="getGatParams">Input params for Get GAT command</param>
        /// <returns></returns>
        public GetGATResponse GetGat(string host, int port, GetGATParams getGatParams)
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

            if (getGatParams == null)
            {
                throw new ArgumentNullException("The get GAT command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getGatParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for getting GAT.");
            }

            if (getGatParams.Scopes == null || getGatParams.Scopes.Count == 0)
            {
                throw new MissingFieldException("At least one scope is required for getting GAT.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdGetGAT = new Command { CommandType = CommandType.uma_rp_get_gat, CommandParams = getGatParams };
                var commandClient = new CommandClient(host, port);
                string commandResponse = commandClient.send(cmdGetGAT);

                var response = JsonConvert.DeserializeObject<GetGATResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and RPT is {1}", response.Status, response.Data.Rpt));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting GAT token.");
                return null;
            }
        }
    }
}