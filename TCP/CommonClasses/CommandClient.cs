using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TCP.ResponseClasses;
namespace TCP.Classes
{
    class CommandClient
    {
        IPEndPoint ipEndPoint = null;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public CommandClient(string host, int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(host), port);
        }

        public string send(Command command)
        {
            try
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
                    byte[] buffer = new byte[10000];
                    int lengthOfReturnedBuffer = sender.Receive(buffer);
                    char[] chars = new char[lengthOfReturnedBuffer];
                    Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                    int charLen = d.GetChars(buffer, 0, lengthOfReturnedBuffer, chars, 0);
                    String returnedJson = new String(chars);
                    Console.WriteLine("The Json:{0}", returnedJson);

                    returnedJson = returnedJson.Remove(0, 4);

                    /////solution 1
                    //Dictionary<string, object> values = deserializeToDictionary(returnedJson);


                    /////solution 2
                    //var results = JsonConvert.DeserializeObject<dynamic>(returnedJson).status.Value;


                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    return returnedJson;
                }
                else
                    throw new ArgumentNullException("It's not allowed to send blank/empty command.");
            }
            catch (Exception ex)
            { 
                Logger.Debug(ex.Message);
                return ex.Message;
            }
        }

        private Dictionary<string, object> deserializeToDictionary(string jo)
        {
            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(jo);
            var values2 = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> d in values)
            {
                // if (d.Value.GetType().FullName.Contains("Newtonsoft.Json.Linq.JObject"))
                if (d.Value is JObject)
                {
                    values2.Add(d.Key, deserializeToDictionary(d.Value.ToString()));
                }
                else
                {
                    values2.Add(d.Key, d.Value);
                }
            }
            return values2;
        }
    }
}
