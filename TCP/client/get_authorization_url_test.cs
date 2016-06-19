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
    class get_authorization_url_test
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Method to get Authorization URL
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public string GetAuthorizationURL(string host, int port)
        {
            try
            {
                CommandClient client = new CommandClient(host, port);

                GetAuthorizationUrlParams param = new GetAuthorizationUrlParams();
                param.SetOxdId(StoredValues._oxd_id);
                param.SetAcrValues(new List<string>());

                Command cmd = new Command(CommandType.get_authorization_url);
                cmd.setParamsObject(param);

                string response = client.send(cmd);
                GetAuthorizationUrlResponse res = new GetAuthorizationUrlResponse(JsonConvert.DeserializeObject<dynamic>(response).data);

                Assert.IsNotNull(res);
                Assert.IsTrue(!String.IsNullOrEmpty(res.getAuthorizationUrl()));
                return res.getAuthorizationUrl();
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
