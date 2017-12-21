using CSharp.CommonClasses;
using Newtonsoft.Json;
using oxdCSharp.UMA.CommandParameters;
using oxdCSharp.UMA.CommandResponses;
using System;

namespace oxdCSharp.UMA.Clients
{
    /// <summary>
    /// A class which is used to check access of a UMA resource in Resource Server
    /// </summary>
    public class UmaRsCheckAccessClient
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Checks Access for UMA RS resource using oxd-server
        /// </summary>
        /// <param name="oxdHost">The IP address of the oxd-server</param>
        /// <param name="oxdPort">The port of the oxd-server</param>
        /// <param name="umaRsCheckAccessParams">Input params for UMA RS Check Access command</param>
        /// <returns>UmaRsCheckAccessResponse</returns>
        /// <example>
        /// <b>Access Granted Response:</b>
        /// {
        ///     "status":"ok",
        ///     "data":{
        ///         "access":"granted"
        ///     }
        /// }
        /// </example>
        /// <example>
        /// <b>Access Denied with Ticket Response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"access": "denied",
        /// 		"www-authenticate_header": "UMA realm=\"rs\",as_uri=\"https://as.example.com\",error=\"insufficient_scope\",ticket=\"d26c30fd-eb94-40da-9f61-0c424acedf0e\"",
        /// 		"ticket": "d26c30fd-eb94-40da-9f61-0c424acedf0e",
        /// 		"error": null,
        /// 		"error_description": null
        /// 	}
        /// }
        /// </example>
        /// <example>
        /// <b>Access Denied without Ticket Response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"access": "denied",
        /// 	}
        /// }
        /// </example>
        /// <example>
        /// <b>Resource is not Protected:</b>
        /// {
        ///     "status":"error",
        ///     "data":{
        ///         "error":"invalid_request",
        ///         "error_description":"Resource is not protected. Please protect your resource first with uma_rs_protect command."
        ///     }
        /// }
        /// </example>
        public UmaRsCheckAccessResponse CheckAccess(string oxdHost, int oxdPort, UmaRsCheckAccessParams umaRsCheckAccessParams)
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

            if (umaRsCheckAccessParams == null)
            {
                throw new ArgumentNullException("The UMA RS Check Access command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(umaRsCheckAccessParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for checking access of UMA resources.");
            }

            if (string.IsNullOrEmpty(umaRsCheckAccessParams.Path))
            {
                throw new MissingFieldException("Valid Path is required for checking access of UMA resource in RS.");
            }

            if (string.IsNullOrEmpty(umaRsCheckAccessParams.HttpMethod))
            {
                throw new MissingFieldException("Valid HTTP method is required for checking access of UMA resource in RS.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdUmaRsCheckAccess = new Command { CommandType = CommandType.uma_rs_check_access, CommandParams = umaRsCheckAccessParams };
                var commandClient = new CommandClient(oxdHost, oxdPort);
                string commandResponse = commandClient.send(cmdUmaRsCheckAccess);

                var response = JsonConvert.DeserializeObject<UmaRsCheckAccessResponse>(commandResponse);

                if(response.Status.ToLower().Equals("error"))
                {
                    Logger.Info(string.Format("Got response status as {0}. The error is {1} with description {2}", 
                        response.Status, response.Data.Error, response.Data.ErrorDescription));
                }
                else
                {
                    Logger.Info(string.Format("Got response status as {0} and the access is {1}", response.Status, response.Data.Access));
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when checking access of UMA resource.");
                return null;
            }
        }


        /// <summary>
        /// Checks Access for UMA RS resource using oxd-https-extension
        /// </summary>
        /// <param name="oxdHttpsExtensionUrl">oxd-https-extension REST service URL</param>
        /// <param name="umaRsCheckAccessParams">Input params for UMA RS Check Access command</param>
        /// <returns>UmaRsCheckAccessResponse</returns>
        /// <example>
        /// <b>Access Granted Response:</b>
        /// {
        ///     "status":"ok",
        ///     "data":{
        ///         "access":"granted"
        ///     }
        /// }
        /// </example>
        /// <example>
        /// <b>Access Denied with Ticket Response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"access": "denied",
        /// 		"www-authenticate_header": "UMA realm=\"rs\",as_uri=\"https://as.example.com\",error=\"insufficient_scope\",ticket=\"d26c30fd-eb94-40da-9f61-0c424acedf0e\"",
        /// 		"ticket": "d26c30fd-eb94-40da-9f61-0c424acedf0e",
        /// 		"error": null,
        /// 		"error_description": null
        /// 	}
        /// }
        /// </example>
        /// <example>
        /// <b>Access Denied without Ticket Response:</b>
        /// {
        /// 	"status": "ok",
        /// 	"data": {
        /// 		"access": "denied",
        /// 	}
        /// }
        /// </example>
        /// <example>
        /// <b>Resource is not Protected:</b>
        /// {
        ///     "status":"error",
        ///     "data":{
        ///         "error":"invalid_request",
        ///         "error_description":"Resource is not protected. Please protect your resource first with uma_rs_protect command."
        ///     }
        /// }
        /// </example>
        public UmaRsCheckAccessResponse CheckAccess(string oxdHttpsExtensionUrl, UmaRsCheckAccessParams umaRsCheckAccessParams)
        {
            Logger.Info("Verifying input parameters.");
           

            if (umaRsCheckAccessParams == null)
            {
                throw new ArgumentNullException("The UMA RS Check Access command params should not be NULL.");
            }

            if (string.IsNullOrEmpty(umaRsCheckAccessParams.OxdId))
            {
                throw new MissingFieldException("Oxd ID is required for checking access of UMA resources.");
            }

            if (string.IsNullOrEmpty(umaRsCheckAccessParams.Path))
            {
                throw new MissingFieldException("Valid Path is required for checking access of UMA resource in RS.");
            }

            if (string.IsNullOrEmpty(umaRsCheckAccessParams.HttpMethod))
            {
                throw new MissingFieldException("Valid HTTP method is required for checking access of UMA resource in RS.");
            }

            try
            {
                Logger.Info("Preparing and sending command.");
                var cmdUmaRsCheckAccess = new Command { CommandType = CommandType.uma_rs_check_access, CommandParams = umaRsCheckAccessParams };
                var commandClient = new CommandClient(oxdHttpsExtensionUrl);
                string commandResponse = commandClient.send(cmdUmaRsCheckAccess);

                var response = JsonConvert.DeserializeObject<UmaRsCheckAccessResponse>(commandResponse);

                if (response.Status.ToLower().Equals("error"))
                {
                    Logger.Info(string.Format("Got response status as {0}. The error is {1} with description {2}",
                        response.Status, response.Data.Error, response.Data.ErrorDescription));
                }
                else
                {
                    Logger.Info(string.Format("Got response status as {0} and the access is {1}", response.Status, response.Data.Access));
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log(NLog.LogLevel.Error, ex, "Exception when checking access of UMA resource.");
                return null;
            }
        }

    }
}
