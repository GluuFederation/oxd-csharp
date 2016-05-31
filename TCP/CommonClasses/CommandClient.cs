using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
namespace TCP.Classes
{
    class CommandClient
    {
        IPEndPoint ipEndPoint = null;
        public CommandClient(string host, string port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(host), 8099);
        }

        public string send(Command command)
        {
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(ipEndPoint);
            Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());
                        
            var json = JsonConvert.SerializeObject(command);
            if (!String.IsNullOrEmpty(json))
            {
                int le = json.Length.ToString().Length;
                int length = le.ToString().Length;
                if (le < 4)
                    json = "0" + json.Length + json;
                byte[] message = Encoding.ASCII.GetBytes(json);
                sender.Send(message);
                byte[] buffer = new byte[1024];
                int lengthOfReturnedBuffer = sender.Receive(buffer);
                char[] chars = new char[lengthOfReturnedBuffer];
                Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(buffer, 0, lengthOfReturnedBuffer, chars, 0);
                String returnedJson = new String(chars);
                Console.WriteLine("The Json:{0}", returnedJson);

                

                //var objects = JArray.Parse(returnedJson); // parse as array  
                //foreach (JObject root in objects)
                //{
                //    foreach (KeyValuePair<String, JToken> app in root)
                //    {
                //        var appName = app.Key;
                //        var vals = app.Value;
                //        //var description = (String)app.Value["Description"];
                //        //var value = (String)app.Value["Value"];

                //        Console.WriteLine(appName);
                //        //Console.WriteLine(description);
                //        Console.WriteLine(vals);
                //        Console.WriteLine("\n");
                //    }
                //}

                var jsonObject = JsonConvert.DeserializeObject<GetTokensByCodeParams>("[" + returnedJson + "]");


                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                return returnedJson;
            }
            else
                throw new ArgumentNullException("It's not allowed to send blank/empty command."); 
        }
    }
}
