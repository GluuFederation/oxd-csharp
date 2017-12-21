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
    /// A class which is used to Get User Info by access token
    /// </summary>
    public class GetUserInfoClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets User Info by access token using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="getUserInfoParams">Input params for Get User Info command.</param>
        /// <returns>GetUserInfoResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"claims": {
        /// 			"sub": ["SGUMcFlAj3QlkOQVgwYpSozbjvynk4B2VNpr-mDnuVw"],
        /// 			"name": ["Jane Doe"],
        /// 			"given_name": ["Jane"],
        /// 			"family_name": ["Doe"],
        /// 			"preferred_username": ["j.doe"],
        /// 			"email": ["janedoe@example.com"],
        /// 			"picture": null
        /// 		}
        /// 	}
        /// }
        /// </example>
        public GetUserInfoResponse GetUserInfo(string oxdHost, int oxdPort, GetUserInfoParams getUserInfoParams)
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

            if (getUserInfoParams == null)
            {
                throw new ArgumentNullException("The get user info command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getUserInfoParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for getting user info.");
            }

            if (string.IsNullOrEmpty(getUserInfoParams.AccessToken))
            {
                throw new MissingFieldException("Access Token is required for getting user info.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdGetUserInfo = new Command { CommandType = CommandType.get_user_info, CommandParams = getUserInfoParams };
                var commandClient = new CommandClient(oxdHost, oxdPort);
                string commandResponse = commandClient.send(cmdGetUserInfo);
                var response = JsonConvert.DeserializeObject<GetUserInfoResponse>(commandResponse);
                
                Logger.Info(string.Format("Got response status as {0} and name is {1}", response.Status, response.Data.UserClaims.Name.FirstOrDefault()));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting user info of site.");
                return null;
            }
        }


        /// <summary>
        /// Gets User Info by access token using oxd-https-extension 
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="getUserInfoParams">Input params for Get User Info command via http</param>
        /// <returns>GetUserInfoResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"claims": {
        /// 			"sub": ["SGUMcFlAj3QlkOQVgwYpSozbjvynk4B2VNpr-mDnuVw"],
        /// 			"name": ["Jane Doe"],
        /// 			"given_name": ["Jane"],
        /// 			"family_name": ["Doe"],
        /// 			"preferred_username": ["j.doe"],
        /// 			"email": ["janedoe@example.com"],
        /// 			"picture": null
        /// 		}
        /// 	}
        /// }
        /// </example>

        public GetUserInfoResponse GetUserInfo(string oxdHttpsExtensionUrl, GetUserInfoParams getUserInfoParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdHttpsExtensionUrl))
                throw new ArgumentNullException("Oxd Rest Service URL should not be NULL.");


            try
            {
                var cmdGetUserInfo = new Command { CommandType = RestCommandType.get_user_info, CommandParams = getUserInfoParams };
                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
                string commandResponse = commandClient.send(cmdGetUserInfo);
                var response = JsonConvert.DeserializeObject<GetUserInfoResponse>(commandResponse);
                return response;


            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting User Info site.");
                return null;

            }

        }
    }
}
