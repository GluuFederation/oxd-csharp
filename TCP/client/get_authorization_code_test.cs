using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.Classes;
using TCP.CommonClasses;
using TCP.ResponseClasses;

namespace TCP.client
{
    public static class get_authorization_code
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static string GetAuthorizationCode(string host, int port, string oxd_id, string userId, string userSecret)
        {
            try
            {
                CommandClient client = new CommandClient(host, port);

                GetAuthorizationCodeParams param = new GetAuthorizationCodeParams();
                param.SetOxdId(oxd_id);
                param.SetUserName(userId);
                param.SetPassword(userSecret);
                param.SetAcrValues(new List<string>());
                Command cmd = new Command(CommandType.get_authorization_code);
                cmd.setParamsObject(param);
                string response = client.send(cmd);
                GetAuthorizationCodeResponse res = new GetAuthorizationCodeResponse(JsonConvert.DeserializeObject<dynamic>(response).data);
                Assert.IsNotNull(res);
                Assert.IsTrue(String.IsNullOrEmpty(res.getCode()));
                return res.getCode();
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);
                return ex.Message;
            }
        }
    }
}
