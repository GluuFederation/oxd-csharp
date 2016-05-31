using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.Classes;
using TCP.CommonClasses;

namespace TCP.client
{
    class get_authorization_code
    {
        public string GetAuthorizationCode()
        {
            CommandClient client = new CommandClient("127.0.0.1", "8099");

            GetAuthorizationCodeParams param = new GetAuthorizationCodeParams(); 
            param.SetOxdId("4e7f34e6-805a-4499-9284-bf206f50167a");
            param.SetUserName("vikas1980");
            param.SetPassword("vikas1980");
            param.SetAcrValues(new List<string>());

            Command cmd = new Command(CommandType.get_authorization_code);
            cmd.setParamsObject(param);
            return client.send(cmd);
        }
    }
}
