using Newtonsoft.Json;
using System;
using CSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A class which is used to setup a new client
    /// </summary>
    public class SetupClientClient
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Setup a new Client using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="setupClientParams">Input parameters for Setup Client command</param>
        /// <returns>SetupClientResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"oxd_id": "c73134c8-c4ca-4bab-9baa-2e0ca20cc433",
        /// 		"op_host": "https://idp-hostname",
        /// 		"client_id": "@!4116.DF7C.62D4.D0CF!0001!D420.A5E5!0008!616C.398A.1380.1F45",
        /// 		"client_secret": "f996649f-b027-4537-abe5-71b7cb71ebae",
        /// 		"client_registration_access_token": "67e957b8-823e-412d-8e89-616c45b2db62",
        /// 		"client_registration_client_uri": "https://idp-hostname/oxauth/restv1/register?client_id=@!4116.DF7C.62D4.D0CF!0001!D420.A5E5!0008!616C.398A.1380.1F45",
        /// 		"client_id_issued_at": 1513857463,
        /// 		"client_secret_expires_at": 1513943863
        /// 	}
        /// }
        /// </example>
        public SetupClientResponse SetupClient(String oxdHost, int oxdPort, SetupClientParams setupClientParams)
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

            if (setupClientParams == null)
            {
                throw new ArgumentNullException("The Setup Client command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(setupClientParams.AuthorizationRedirectUri))
            {
                throw new MissingFieldException("Authorization Redirect Uri is required.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                setupClientParams.ProgrammingLanguage = "csharp";
                var cmdSetupClient = new Command { CommandType = CommandType.setup_client, CommandParams = setupClientParams };
                var commandClient = new CommandClient(oxdHost, oxdPort);
                string commandResponse = commandClient.send(cmdSetupClient);

                var response = JsonConvert.DeserializeObject<SetupClientResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and Oxd ID is {1}", response.Status, response.Data.OxdId));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when Setting up Client.");
                return null;
            }
        }


        /// <summary>
        /// Setup a new Client using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="setupClientParams">Input parameters for Setup  Client via http</param>
        /// <returns>SetupClientResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"oxd_id": "c73134c8-c4ca-4bab-9baa-2e0ca20cc433",
        /// 		"op_host": "https://idp-hostname",
        /// 		"client_id": "@!4116.DF7C.62D4.D0CF!0001!D420.A5E5!0008!616C.398A.1380.1F45",
        /// 		"client_secret": "f996649f-b027-4537-abe5-71b7cb71ebae",
        /// 		"client_registration_access_token": "67e957b8-823e-412d-8e89-616c45b2db62",
        /// 		"client_registration_client_uri": "https://idp-hostname/oxauth/restv1/register?client_id=@!4116.DF7C.62D4.D0CF!0001!D420.A5E5!0008!616C.398A.1380.1F45",
        /// 		"client_id_issued_at": 1513857463,
        /// 		"client_secret_expires_at": 1513943863
        /// 	}
        /// }
        /// </example>
        public SetupClientResponse SetupClient(string oxdHttpsExtensionUrl, SetupClientParams setupClientParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdHttpsExtensionUrl))
                throw new ArgumentNullException("Oxd Rest Service URL should not be NULL.");
            try
            {
                var cmdSetupClient = new Command { CommandType = RestCommandType.setup_client, CommandParams = setupClientParams };
                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
                string commandResponse = commandClient.send(cmdSetupClient);
                var response = JsonConvert.DeserializeObject<SetupClientResponse>(commandResponse);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when Setting up Client");
                return null;

            }

        }
    }
}
