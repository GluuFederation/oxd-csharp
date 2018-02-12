
using CSharp.CommonClasses;
using Newtonsoft.Json;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A class which is used to Introspect Access Token
    /// </summary>
    public class IntrospectAccessTokenClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Introspect Access Token using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="introspectAccessTokenParams">Input parameters for Introspect Access Token command</param>
        /// <returns>IntrospectAccessTokenResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"active": true,
        /// 		"scopes": ["openid"],
        /// 		"client_id": "@!DEEA.B5F2.074F.4295!0001!5BE0.4886!0008!C314.0AC7.C908.77D0",
        /// 		"username": null,
        /// 		"token_type": "bearer",
        /// 		"exp": 1517391098,
        /// 		"iat": 1517390798,
        /// 		"sub": null,
        /// 		"aud": "@!DEEA.B5F2.074F.4295!0001!5BE0.4886!0008!C314.0AC7.C908.77D0",
        /// 		"iss": "https://idp.gluu.org",
        /// 		"jti": null,
        /// 		"acr_values": null
        /// 	}
        /// }
        /// </example>
        public IntrospectAccessTokenResponse IntrospectAccessToken(String oxdHost, int oxdPort, IntrospectAccessTokenParams introspectAccessTokenParams)
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

            if (introspectAccessTokenParams == null)
            {
                throw new ArgumentNullException("The introspect access token command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(introspectAccessTokenParams.OxdId))
            {
                throw new MissingFieldException("OxdId is required.");
            }

            if (string.IsNullOrEmpty(introspectAccessTokenParams.AccessToken))
            {
                throw new MissingFieldException("AccessToken is required.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdIntrospectAccessToken = new Command { CommandType = CommandType.introspect_access_token, CommandParams = introspectAccessTokenParams };
                var commandClient = new CommandClient(oxdHost, oxdPort);
                string commandResponse = commandClient.send(cmdIntrospectAccessToken);

                var response = JsonConvert.DeserializeObject<IntrospectAccessTokenResponse>(commandResponse);
                
                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when Introspecting Access Token.");
                return null;
            }
        }


        /// <summary>
        /// Introspect Access Token using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="introspectAccessTokenParams">Input parameters for Introspect Access Token via https</param>
        /// <returns>IntrospectAccessTokenResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"active": true,
        /// 		"scopes": ["openid"],
        /// 		"client_id": "@!DEEA.B5F2.074F.4295!0001!5BE0.4886!0008!C314.0AC7.C908.77D0",
        /// 		"username": null,
        /// 		"token_type": "bearer",
        /// 		"exp": 1517391098,
        /// 		"iat": 1517390798,
        /// 		"sub": null,
        /// 		"aud": "@!DEEA.B5F2.074F.4295!0001!5BE0.4886!0008!C314.0AC7.C908.77D0",
        /// 		"iss": "https://idp.gluu.org",
        /// 		"jti": null,
        /// 		"acr_values": null
        /// 	}
        /// }
        /// </example>
        public IntrospectAccessTokenResponse IntrospectAccessToken(string oxdHttpsExtensionUrl, IntrospectAccessTokenParams introspectAccessTokenParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdHttpsExtensionUrl))
                throw new ArgumentNullException("oxd-https-extension URL should not be NULL.");

            if (introspectAccessTokenParams == null)
                throw new ArgumentNullException("The introspect access token command params should not be NULL.");

            try
            {
                var cmdIntrospectAccessToken = new Command { CommandType = RestCommandType.introspect_access_token, CommandParams = introspectAccessTokenParams };
                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
                string commandResponse = commandClient.send(cmdIntrospectAccessToken);
                var response = JsonConvert.DeserializeObject<IntrospectAccessTokenResponse>(commandResponse);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when Introspecting Access Token.");
                return null;

            }



        }
    }
}