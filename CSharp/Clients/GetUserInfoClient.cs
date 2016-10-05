using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using CSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System.Diagnostics.Contracts;
using System.Linq;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A client class which is used to Get User Info by access token using Oxd Server
    /// </summary>
    public class GetUserInfoClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets User Info by access token using Oxd Server
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="getUserInfoParams">Input params for Get User Info command.</param>
        /// <returns></returns>
        public GetUserInfoResponse GetUserInfo(string host, int port, GetUserInfoParams getUserInfoParams)
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

            if(getUserInfoParams == null)
            {
                throw new ArgumentNullException("The get user info command params should not be NULL.");
            }

            if(string.IsNullOrEmpty(getUserInfoParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for getting user info.");
            }

            if(string.IsNullOrEmpty(getUserInfoParams.AccessToken))
            {
                throw new MissingFieldException("Access Token is required for getting user info.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdGetUserInfo = new Command { CommandType = CommandType.get_user_info, CommandParams = getUserInfoParams };
                var commandClient = new CommandClient(host, port);
                string commandResponse = commandClient.send(cmdGetUserInfo);

                var response = JsonConvert.DeserializeObject<GetUserInfoResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and name is {1}", response.Status, response.Data.UserClaims.Name.First()));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting user info of site.");
                return null;
            }
        }
    }
}
