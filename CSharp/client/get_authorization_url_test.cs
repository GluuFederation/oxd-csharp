﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class get_authorization_url_test
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Method to get Authorization URL
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public GetAuthorizationUrlResponse GetAuthorizationURL(string host, int port, string oxdId = "")
        {
            try
            {
                CommandClient client = new CommandClient(host, port);

                GetAuthorizationUrlParams param = new GetAuthorizationUrlParams();
                param.OxdId = oxdId;

                var cmd = new Command { CommandType = CommandType.get_authorization_url, CommandParams = param };

                string response = client.send(cmd);
                GetAuthorizationUrlResponse res = JsonConvert.DeserializeObject<GetAuthorizationUrlResponse>(response);

                Assert.IsNotNull(res);
                Assert.IsTrue(!String.IsNullOrEmpty(res.Data.AuthorizationUrl));
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.Debug(ex.Message);
                throw ex;
            }
        }
    }
}
