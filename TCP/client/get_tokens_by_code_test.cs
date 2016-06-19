using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.CommonClasses;
using TCP.ResponseClasses;

namespace TCP.client
{
    class get_tokens_by_code_test
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Method to get token
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="oxd_id"></param>
        /// <param name="userId"></param>
        /// <param name="userSecret"></param>
        /// <returns></returns>
        public GetTokensByCodeResponse GetTokenByCode(string host, int port, string userId, string userSecret)
        {
            try
            {
                CommandClient client = new CommandClient(host, port);
                GetTokensByCodeParams param = new GetTokensByCodeParams();
                param.SetOxdId(StoredValues._oxd_id);
                param.SetCode(get_authorization_code.GetAuthorizationCode(host, port, userId, userSecret));
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
                Console.WriteLine(ex.Message);
                Logger.Debug(ex.Message);
                return null;
            }
        }
    }
}