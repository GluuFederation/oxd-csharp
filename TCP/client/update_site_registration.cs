using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.Classes;

namespace TCP.client
{
    class update_site_registration
    {
        public string UpdateSiteRegisteration()
        {
            CommandClient client = new CommandClient("127.0.0.1", "8099");

            UpdateSiteParams param = new UpdateSiteParams();
            param.SetOxdId("14c7ed41-cb45-4bcc-b5c6-d311a44c8a91");
            param.SetAuthorizationRedirectUri("http://www.omsttech.com/wp-login.php");
            param.SetPostLogoutRedirectUri("http://www.omsttech.com/wp-login.php?action=logout&_wpnonce=a3c70643e9");
            param.SetApplicationType("web");
            param.SetRedirectUris(Lists.newArrayList(new string[] { "http://www.omsttech.com/wp-login.php" }));
            param.SetAcrValues(new List<string>());
            param.SetClientJwksUri("");
            param.SetContacts(Lists.newArrayList(new string[] { "hellochopra1@gmail.com" }));
            param.SetGrantType(Lists.newArrayList(new string[] { "authorization_code" }));
            param.SetClientTokenEndpointAuthMethod("");
            param.SetClientLogoutUri(Lists.newArrayList(new string[] { "http://www.omsttech.com/wp-login.php?action=logout&_wpnonce=a3c70643e9" }));

            //param.Op_host("https://ce-dev2.gluu.org");

            Command cmd = new Command(CommandType.update_site_registration);
            cmd.setParamsObject(param);
            return client.send(cmd);
        }
    }
}
