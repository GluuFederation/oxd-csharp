using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.Classes;
using TCP.ResponseClasses;

namespace TCP.client
{
    class get_user_info
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public GetUserInfoResponse GetUserInfo(string host, int port, string oxd_id, string accessToken)
        {
            try
            {
                CommandClient client = new CommandClient(host, port);

                GetUserInfoParams param = new GetUserInfoParams();
                param.setOxdId(oxd_id);
                param.setAccessToken(accessToken);

                Command cmd = new Command(CommandType.get_user_info);
                cmd.setParamsObject(param);

                string response = client.send(cmd);
                GetUserInfoResponse res = new GetUserInfoResponse(JsonConvert.DeserializeObject<dynamic>(response).data);
                Assert.IsNotNull(res); 
                return res;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);
                return null;
            }
        }
    }
}
