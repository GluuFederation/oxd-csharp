
using CSharp.CommonClasses;
using Newtonsoft.Json;
using oxdCSharp.UMA.CommandParameters;
using oxdCSharp.UMA.CommandResponses;
using System;

namespace oxdCSharp.UMA.Clients
{
    /// <summary>
    /// A class which is used to get Introspect Rpt
    /// </summary>
    public class UmaIntrospectRptClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Introspect Rpt using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="umaIntrospectRptParams">Input params for Introspect Rpt command</param>
        /// <returns>UmaIntrospectRptResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"active": true,
        /// 		"exp": 1518091492556,
        /// 		"iat": 1518091192556,
        /// 		"nbf": null,
        /// 		"permissions": [{
        /// 			"resource_id": "dd270f7b-551c-42e5-a1fa-a1811cff3f72",
        /// 			"resource_scopes": ["https://client.example.com:44300/api"],
        /// 			"exp": 1518094783755
        /// 		}],
        /// 		"client_id": null,
        /// 		"sub": null,
        /// 		"aud": null,
        /// 		"iss": null,
        /// 		"jti": null
        /// 	}
        /// }
        /// </example>
        public UmaIntrospectRptResponse IntrospectRpt(string oxdHost, int oxdPort, UmaIntrospectRptParams umaIntrospectRptParams)
        {
            Logger.Info("Verifying input parameters.");
            if (string.IsNullOrEmpty(oxdHost))
            {
                throw new ArgumentNullException("oxd Host should not be NULL.");
            }

            if (oxdPort <= 0)
            {
                throw new ArgumentNullException("oxd Port should be a valid port number.");
            }

            if (umaIntrospectRptParams == null)
            {
                throw new ArgumentNullException("The Introspect RPT command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(umaIntrospectRptParams.OxdId))
            {
                throw new MissingFieldException("oxd ID is required.");
            }

            if (string.IsNullOrEmpty(umaIntrospectRptParams.RPT))
            {
                throw new MissingFieldException("RPT is required.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdIntrospectRPT = new Command { CommandType = CommandType.introspect_rpt, CommandParams = umaIntrospectRptParams };
                var commandClient = new CommandClient(oxdHost, oxdPort);
                string commandResponse = commandClient.send(cmdIntrospectRPT);

                var response = JsonConvert.DeserializeObject<UmaIntrospectRptResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and Active status is {1}", response.Status, response.Data.Active));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when introspecting RPT.");
                return null;
            }
        }


        /// <summary>
        /// Introspect Rpt using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="umaIntrospectRptParams">Input params for Introspect Rpt command</param>
        /// <returns>UmaIntrospectRptResponse</returns>
        /// <example>
        /// <b>Example response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"active": true,
        /// 		"exp": 1518091492556,
        /// 		"iat": 1518091192556,
        /// 		"nbf": null,
        /// 		"permissions": [{
        /// 			"resource_id": "dd270f7b-551c-42e5-a1fa-a1811cff3f72",
        /// 			"resource_scopes": ["https://client.example.com:44300/api"],
        /// 			"exp": 1518094783755
        /// 		}],
        /// 		"client_id": null,
        /// 		"sub": null,
        /// 		"aud": null,
        /// 		"iss": null,
        /// 		"jti": null
        /// 	}
        /// }
        /// </example>
        public UmaIntrospectRptResponse IntrospectRpt(string oxdHttpsExtensionUrl, UmaIntrospectRptParams umaIntrospectRptParams)
        {
            Logger.Info("Verifying input parameters.");

            if (string.IsNullOrEmpty(oxdHttpsExtensionUrl))
            {
                throw new ArgumentNullException("oxd-https-extension URL should not be NULL.");
            }

            if (umaIntrospectRptParams == null)
            {
                throw new ArgumentNullException("The Introspect RPT command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(umaIntrospectRptParams.OxdId))
            {
                throw new MissingFieldException("oxd ID is required.");
            }

            if (string.IsNullOrEmpty(umaIntrospectRptParams.RPT))
            {
                throw new MissingFieldException("RPT is required.");
            }

            try
            {

                var commandClient = new CommandClient(oxdHttpsExtensionUrl);

                var cmdIntrospectRPT = new Command { CommandType = CommandType.introspect_rpt, CommandParams = umaIntrospectRptParams };
                string commandResponse = commandClient.send(cmdIntrospectRPT);
                var response = JsonConvert.DeserializeObject<UmaIntrospectRptResponse>(commandResponse);
                Logger.Info(string.Format("Got response status as {0} and Active status is {1}", response.Status, response.Data.Active));
                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when introspecting RPT.");
                return null;
            }
        }
        

    }
}