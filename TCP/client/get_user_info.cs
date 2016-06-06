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
    class get_user_info
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get the User Info
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public GetUserInfoResponse GetUserInfo(string host, int port, string accessToken)
        {
            try
            {
                CommandClient client = new CommandClient(host, port);

                GetUserInfoParams param = new GetUserInfoParams();
                param.setOxdId(StoredValues._oxd_id);
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
                Console.WriteLine(ex.Message);
                Logger.Debug(ex.Message);
                return null;
            }
        }
    }
}
