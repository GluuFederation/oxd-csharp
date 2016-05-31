using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.Classes;
using TCP.CommonClasses;

namespace TCP.client
{
    class get_tokens_by_code
    { 
        public string GetTokenByCode()
        {
            CommandClient client = new CommandClient("127.0.0.1", "8099");

            GetTokensByCodeParams param = new GetTokensByCodeParams();
            param.SetOxdId("4e7f34e6-805a-4499-9284-bf206f50167a");
            param.SetCode("43293640-abac-4c38-b54f-02952e4781bd");
            param.SetScopes(Lists.newArrayList(new string[] { "openid", "profile" }));
            //param.SetState("af0ifjsldkj");

            Command cmd = new Command(CommandType.get_tokens_by_code);
            cmd.setParamsObject(param);
            return client.send(cmd);
        }
    }
}
