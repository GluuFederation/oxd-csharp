using System;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using CSharp.CommonClasses;
using Newtonsoft.Json;


namespace oxdCSharp.Clients
{
    /// <summary>
    /// A class which is used to get a new access token using refresh token
    /// </summary>
    public class GetAccessTokenByRefreshTokenClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get Access Token By Refresh Token using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="getAccessTokenByRefreshTokenParams">Input params for Get Access Token By Refresh Token command</param>
        /// <returns>GetAccessTokenByRefreshTokenResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"access_token": "fb5b52be-fd99-46b7-b87a-6167f86ae4bb",
        /// 		"expires_in": "299",
        /// 		"refresh_token": "49850248-ee70-4d21-85b2-949476c26e1b"
        /// 	}
        /// }
        /// </example>
        public GetAccessTokenByRefreshTokenResponse GetAccessTokenByRefreshToken(string oxdHost, int oxdPort, GetAccessTokenByRefreshTokenParams getAccessTokenByRefreshTokenParams)
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

            if (getAccessTokenByRefreshTokenParams == null)
            {
                throw new ArgumentNullException("The get access_token by refresh_token command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getAccessTokenByRefreshTokenParams.OxdId))
            {
                throw new MissingFieldException("oxd Id is required for getting access_token by refresh_token.");
            }


            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdgetAccessTokenByRefreshToken = new Command { CommandType = CommandType.get_access_token_by_refresh_token, CommandParams = getAccessTokenByRefreshTokenParams };
                var commandClient = new CommandClient(oxdHost, oxdPort);
                string commandResponse = commandClient.send(cmdgetAccessTokenByRefreshToken);

                var response = JsonConvert.DeserializeObject<GetAccessTokenByRefreshTokenResponse>(commandResponse);

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting access_token by refresh_token.");
                return null;
            }
        }


        /// <summary>
        /// Get Access Token By Refresh Token using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="getAccessTokenByRefreshTokenParams">Input params for Get Access Token By Refresh Token command</param>
        /// <returns>GetAccessTokenByRefreshTokenResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"access_token": "fb5b52be-fd99-46b7-b87a-6167f86ae4bb",
        /// 		"expires_in": "299",
        /// 		"refresh_token": "49850248-ee70-4d21-85b2-949476c26e1b"
        /// 	}
        /// }
        /// </example>
        public GetAccessTokenByRefreshTokenResponse GetAccessTokenByRefreshToken(string oxdHttpsExtensionUrl, GetAccessTokenByRefreshTokenParams getAccessTokenByRefreshTokenParams)
        {
            Logger.Info("Verifying input parameters.");


            if (getAccessTokenByRefreshTokenParams == null)
            {
                throw new ArgumentNullException("The UMA RS Get Claims Gathering Url params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getAccessTokenByRefreshTokenParams.OxdId))
            {
                throw new MissingFieldException("oxd Id is required for getting access_token by refresh_token");
            }


            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdgetAccessTokenByRefreshToken = new Command { CommandType = CommandType.get_access_token_by_refresh_token, CommandParams = getAccessTokenByRefreshTokenParams };
                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
                string commandResponse = commandClient.send(cmdgetAccessTokenByRefreshToken);

                var response = JsonConvert.DeserializeObject<GetAccessTokenByRefreshTokenResponse>(commandResponse);

                if (response.Status.ToLower().Equals("error"))
                {
              
                    Logger.Info(string.Format("Got response status as {0} ", response.Status));
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting access_token by refresh_token.");
                return null;
            }
        }

    }
}
