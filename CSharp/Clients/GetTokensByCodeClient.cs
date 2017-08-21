using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using oxdCSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System.Diagnostics.Contracts;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A client class which is used to Get Tokens by Auth Code and Auth State of a site using Oxd Server
    /// </summary>
    public class GetTokensByCodeClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets different type of tokens by Code using Oxd Server
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="getTokensByCodeParams">Input params for Get Tokens by Code command</param>
        /// <returns></returns>
        public GetTokensByCodeResponse GetTokensByCode(string host, int port, GetTokensByCodeParams getTokensByCodeParams)
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

            if (getTokensByCodeParams == null)
            {
                throw new ArgumentNullException("The get tokens by code command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getTokensByCodeParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for getting tokens.");
            }

            if (string.IsNullOrEmpty(getTokensByCodeParams.Code))
            {
                throw new MissingFieldException("Auth Code is required for getting tokens.");
            }

            if (string.IsNullOrEmpty(getTokensByCodeParams.State))
            {
                throw new MissingFieldException("Auth State is required for getting tokens.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdGetTokensByCode = new Command { CommandType = CommandType.get_tokens_by_code, CommandParams = getTokensByCodeParams };
                var commandClient = new CommandClient(host, port);
                string commandResponse = commandClient.send(cmdGetTokensByCode);

                var response = JsonConvert.DeserializeObject<GetTokensByCodeResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and access token is {1}", response.Status, response.Data.AccessToken));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting tokens of site.");
                return null;
            }
        }

        /// <summary>
        /// Gets different type of tokens by Code using http
        /// </summary>
        /// <param name="oxdWebUrl">Oxd to http REST service URL</param>
        /// <param name="getTokensByCodeParams">Input params for Get Tokens by Code command</param>
        /// <returns></returns>

        public GetTokensByCodeResponse GetTokensByCode(string oxdWebUrl, GetTokensByCodeParams getTokensByCodeParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdWebUrl))
                throw new ArgumentNullException("Oxd Rest Service URL should not be NULL.");

            try
            {
                var cmdGetTokensByCode = new Command { CommandType = RestCommandType.get_tokens_by_code, CommandParams = getTokensByCodeParams };
                var commandClient = new CommandClient(oxdWebUrl);
                string commandResponse = commandClient.send(cmdGetTokensByCode);
                var response = JsonConvert.DeserializeObject<GetTokensByCodeResponse>(commandResponse);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting Token Code.");
                return null;

            }

        }

    }
}