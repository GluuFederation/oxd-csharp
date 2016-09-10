using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp.CommonClasses;
using oxdCSharp.Responses;

namespace CSharp.client
{
    public class get_tokens_by_code_test
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
        public GetTokensByCodeResponse GetTokenByCode(string host, int port, string oxdId, string authCode)
        {
            try
            {
                CommandClient client = new CommandClient(host, port);
                GetTokensByCodeParams param = new GetTokensByCodeParams();
                param.SetOxdId(string.IsNullOrEmpty(oxdId)? StoredValues._oxd_id : oxdId);
                param.SetCode(authCode);
                param.SetScopes(Lists.newArrayList(new string[] { "openid", "profile" }));
                Command cmd = new Command(CommandType.get_tokens_by_code);
                cmd.setParamsObject(param);
                string commandresponse = client.send(cmd);
                GetTokensByCodeResponse response = JsonConvert.DeserializeObject<GetTokensByCodeResponse>(commandresponse);
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