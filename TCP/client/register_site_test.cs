using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.Classes;

namespace TCP.client
{
    class register_site_test
    {
        public string register()
        {
            CommandClient client = new CommandClient("127.0.0.1", "8099");
             
            RegisterSiteParams param = new RegisterSiteParams();
            param.SetAuthorizationRedirectUri("https://www.omsttech.com/wp-login.php?option=oxdOpenId");
            param.SetPostLogoutRedirectUri("https://www.omsttech.com/wp-login.php?action=logout&_wpnonce=a3c70643e9");            
            param.SetApplicationType("web");
            param.SetRedirectUris(Lists.newArrayList(new string[] { "https://www.omsttech.com/wp-login.php?option=oxdOpenId" }));            
            param.SetAcrValues(new List<string>());
            param.SetScope(Lists.newArrayList(new string[] { "openid", "profile", "email", "address", "clientinfo", "mobile_phone", "phone" }));
            param.SetContacts(Lists.newArrayList(new string[] { "vlad.karapetyan.1988@mail.ru" }));
            param.SetGrantType(Lists.newArrayList(new string[] { "authorization_code" }));
            param.SetResponseTypes(Lists.newArrayList(new string[] { "code" }));
            param.SetClientLogoutUri(Lists.newArrayList(new string[] { "https://www.omsttech.com/wp-login.php?action=logout&_wpnonce=a3c70643e9" }));            
            
            //param.Op_host("https://ce-dev2.gluu.org");

            Command cmd = new Command(CommandType.register_site);
            cmd.setParamsObject(param);

            return client.send(cmd);
        }
        public string update()
        {

            return "";
        }
    }
}
