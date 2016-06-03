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
    class get_tokens_by_code
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public GetTokensByCodeResponse GetTokenByCode(string host, int port, string oxd_id, string userId, string userSecret)
        {
            try
            {
                CommandClient client = new CommandClient(host, port);
                GetTokensByCodeParams param = new GetTokensByCodeParams();
                param.SetOxdId(oxd_id);
                param.SetCode(get_authorization_code.GetAuthorizationCode(host, port, oxd_id, userId, userSecret));
                param.SetScopes(Lists.newArrayList(new string[] { "openid", "profile" }));
                Command cmd = new Command(CommandType.get_tokens_by_code);
                cmd.setParamsObject(param);
                string commandresponse = client.send(cmd);
                GetTokensByCodeResponse response = new GetTokensByCodeResponse(JsonConvert.DeserializeObject<dynamic>(commandresponse).data);
                Assert.IsNotNull(response); 
                return response;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);
                return null;
            }
        }
    }
}
