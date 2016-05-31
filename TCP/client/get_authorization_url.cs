using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.Classes;

namespace TCP.client
{
    class get_authorization_url
    {
        public string GetAuthorizationURL()
        {
            CommandClient client = new CommandClient("127.0.0.1", "8099");

            GetAuthorizationUrlParams param = new GetAuthorizationUrlParams();
            param.SetOxdId("4e7f34e6-805a-4499-9284-bf206f50167a");
            param.SetAcrValues(new List<string>());
              
            Command cmd = new Command(CommandType.get_authorization_url);
            cmd.setParamsObject(param);
            return client.send(cmd);
        }
    }
}
