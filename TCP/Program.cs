    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
using Newtonsoft.Json;
using TCP.Classes;
using System.Collections;
using TCP.client;

namespace TCP
{
    class Program
    {

        static void Main(string[] args)
        {

            try
            {
                //register_site_test testing = new register_site_test();
                //testing.register();

                //update_site_registration test = new update_site_registration();
                //test.UpdateSiteRegisteration();

                //get_authorization_url test = new get_authorization_url();
                //test.GetAuthorizationURL();

                get_authorization_code test = new get_authorization_code();
                test.GetAuthorizationCode();

                //get_tokens_by_code test = new get_tokens_by_code();
                //test.GetTokenByCode();


            }
            catch (SocketException ex)
            {

            }
        }
    }
}
