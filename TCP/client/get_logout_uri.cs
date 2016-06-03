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
    class get_logout_uri
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public LogoutResponse GetLogoutURL(string host, int port, string oxd_id)
        {
            try
            {
                CommandClient client = new CommandClient(host, port);

                GetLogoutUrlParams param = new GetLogoutUrlParams();
                param.setOxdId(oxd_id);
                param.setIdTokenHint("dummy_token");
                //param.setPostLogoutRedirectUri("");

                param.setState(Guid.NewGuid().ToString());
                //param.setSessionState(UUID.randomUUID().toString()); 


                Command cmd = new Command(CommandType.get_logout_uri);
                cmd.setParamsObject(param);

                string response = client.send(cmd);
                LogoutResponse res = new LogoutResponse(JsonConvert.DeserializeObject<dynamic>(response).data);
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
