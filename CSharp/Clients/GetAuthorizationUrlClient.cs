using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using CSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;
using System.Diagnostics.Contracts;

namespace oxdCSharp.Clients
{
    /// <summary>
    /// A class which is used to Get Authorization URL
    /// </summary>
    public class GetAuthorizationUrlClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets Authorization URL of a site using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="getAuthUrlParams">Input params for Get Authorization URL command</param>
        /// <returns>GetAuthorizationUrlResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"authorization_url": "https://idp-hostname/oxauth/restv1/authorize?response_type=code&client_id=@!4116.DF7C.62D4.D0CF!0001!D420.A5E5!0008!6156.5BD4.5F9B.D172&redirect_uri=https://client.example.com:44383/Home/GetUserInfo&scope=openid+profile+email+uma_protection+uma_authorization&state=cim3uintftqoqckqhgd1vbs6iv&nonce=4pn3vgisdg4em0ups2ud79iig5&custom_response_headers=%5B%7B%22param1%22%3A%22value1%22%7D%2C%7B%22param2%22%3A%22value2%22%7D%5D"
        /// 	}
        /// }
        /// </example>
        public GetAuthorizationUrlResponse GetAuthorizationURL(string oxdHost, int oxdPort, GetAuthorizationUrlParams getAuthUrlParams)
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

            if (getAuthUrlParams == null)
            {
                throw new ArgumentNullException("The get auth url command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(getAuthUrlParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for getting auth url of site.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdGetAuthUrl = new Command { CommandType = CommandType.get_authorization_url, CommandParams = getAuthUrlParams };
                var commandClient = new CommandClient(oxdHost, oxdPort);
                string commandResponse = commandClient.send(cmdGetAuthUrl);

                var response = JsonConvert.DeserializeObject<GetAuthorizationUrlResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and auth url is {1}", response.Status, response.Data.AuthorizationUrl));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting auth url of site.");
                return null;
            }
        }


        /// <summary>
        /// Gets Authorization URL of a site using oxd-htts-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="getAuthUrlParams">Input params for Get Authorization URL command via http</param>
        /// <returns>GetAuthorizationUrlResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"authorization_url": "https://idp-hostname/oxauth/restv1/authorize?response_type=code&client_id=@!4116.DF7C.62D4.D0CF!0001!D420.A5E5!0008!6156.5BD4.5F9B.D172&redirect_uri=https://client.example.com:44383/Home/GetUserInfo&scope=openid+profile+email+uma_protection+uma_authorization&state=cim3uintftqoqckqhgd1vbs6iv&nonce=4pn3vgisdg4em0ups2ud79iig5&custom_response_headers=%5B%7B%22param1%22%3A%22value1%22%7D%2C%7B%22param2%22%3A%22value2%22%7D%5D"
        /// 	}
        /// }
        /// </example>
        public GetAuthorizationUrlResponse GetAuthorizationURL(string oxdHttpsExtensionUrl, GetAuthorizationUrlParams getAuthUrlParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdHttpsExtensionUrl))
                throw new ArgumentNullException("Oxd Rest Service URL should not be NULL.");

            try
            {
                var cmdGetAuthUrl = new Command { CommandType = RestCommandType.get_authorization_url, CommandParams = getAuthUrlParams };
                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
                string commandResponse = commandClient.send(cmdGetAuthUrl);
                var response = JsonConvert.DeserializeObject<GetAuthorizationUrlResponse>(commandResponse);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when getting Authorization url");
                return null;

            }

        }
    }
}
