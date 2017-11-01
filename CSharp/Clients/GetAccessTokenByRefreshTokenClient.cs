using System;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using CSharp.CommonClasses;
using Newtonsoft.Json;


namespace oxdCSharp.Clients
{
    public class GetAccessTokenByRefreshTokenClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// oxd-local Get Access Token By Refresh Token
        /// </summary>
        /// <param name="host">Oxd Host</param>
        /// <param name="port">Oxd Port</param>
        /// <param name="getAccessTokenByRefreshTokenParams">Input params for Get Access Token By Refresh Token command</param>
        /// <returns></returns>
        public GetAccessTokenByRefreshTokenResponse GetAccessTokenByRefreshToken(string host, int port, GetAccessTokenByRefreshTokenParams getAccessTokenByRefreshTokenParams)
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

            if (getAccessTokenByRefreshTokenParams == null)
            {
                throw new ArgumentNullException("The UMA RS Check Access command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getAccessTokenByRefreshTokenParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for checking access of UMA resources.");
            }


            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdgetAccessTokenByRefreshToken = new Command { CommandType = CommandType.get_access_token_by_refresh_token, CommandParams = getAccessTokenByRefreshTokenParams };
                var commandClient = new CommandClient(host, port);
                string commandResponse = commandClient.send(cmdgetAccessTokenByRefreshToken);

                var response = JsonConvert.DeserializeObject<GetAccessTokenByRefreshTokenResponse>(commandResponse);

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when Getting UMA Claims Gathering URL.");
                return null;
            }
        }



        /// <summary>
        /// oxd-web Get Access Token By Refresh Token
        /// </summary>
        /// <param name="oxdweburl">Oxd Web url</param>
        /// <param name="getAccessTokenByRefreshTokenParams">Input params for Get Access Token By Refresh Token command</param>
        /// <returns></returns>
        public GetAccessTokenByRefreshTokenResponse GetAccessTokenByRefreshToken(string oxdweburl, GetAccessTokenByRefreshTokenParams getAccessTokenByRefreshTokenParams)
        {
            Logger.Info("Verifying input parameters.");


            if (getAccessTokenByRefreshTokenParams == null)
            {
                throw new ArgumentNullException("The UMA RS Get Claims Gathering Url params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getAccessTokenByRefreshTokenParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for  Get Claims Gathering Url ");
            }



            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdgetAccessTokenByRefreshToken = new Command { CommandType = CommandType.get_access_token_by_refresh_token, CommandParams = getAccessTokenByRefreshTokenParams };
                var commandClient = new CommandClient(oxdweburl);
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
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when Getting UMA Claims Gathering URL.");
                return null;
            }
        }

    }
}
