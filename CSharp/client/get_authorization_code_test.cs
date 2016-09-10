using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using CSharp.CommonClasses;
using oxdCSharp.CommandParameters;
using oxdCSharp.CommandResponses;

namespace CSharp.client
{
    public static class get_authorization_code
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Get the Authorization code for getting token
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="userId">Username</param>
        /// <param name="userSecret">Password</param>
        /// <returns></returns>
        public static string GetAuthorizationCode(string host, int port, string userId, string userSecret)
        {
            try
            {
                CommandClient client = new CommandClient(host, port);
                GetAuthorizationCodeParams param = new GetAuthorizationCodeParams();
                param.SetOxdId(StoredValues._oxd_id);
                param.SetUserName(userId);
                param.SetPassword(userSecret);
                param.SetAcrValues(new List<string>());
                Command cmd = new Command(CommandType.get_authorization_code);
                cmd.setParamsObject(param);
                string response = client.send(cmd);
                GetAuthorizationCodeResponse res = new GetAuthorizationCodeResponse(JsonConvert.DeserializeObject<dynamic>(response).data);
                Assert.IsNotNull(res);
                Assert.IsTrue(!String.IsNullOrEmpty(res.getCode()));
                return res.getCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.Debug(ex.Message);
                return ex.Message;
            }
        }
    }
}
