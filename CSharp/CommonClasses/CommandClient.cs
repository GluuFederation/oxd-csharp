using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using oxdCSharp.CommandResponses;


namespace oxdCSharp.CommonClasses
{
    /// <summary>
    /// Command client for communicating with Server
    /// </summary>
    class CommandClient
    {
        IPEndPoint ipEndPoint = null;
        string oxdHttpUrl = null;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Setting up Command Client with Host and Port to communicate
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public CommandClient(string host, int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(host), port);

        }
        /// <summary>
        /// Setting up Command Client with REST service to communicate via http
        /// </summary>
        /// <param name="restserviceURL"></param>
     
        public CommandClient(string restserviceURL)
        {
            oxdHttpUrl = restserviceURL;

        }



        /// <summary>
        /// Sending Command to server in Json format
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Json Response</returns>
        public string send(Command command)
        {
            if (ipEndPoint != null)
                return sendviaSocket(command);
            else if (!string.IsNullOrEmpty(oxdHttpUrl))
                return sendviahttp(command);
            else
                return null;

            
        }



        private string sendviaSocket(Command command)
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
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    return returnedJson;
                }
                else
                {
                    Console.WriteLine("It's not allowed to send blank/empty command.");
                    Logger.Debug("It's not allowed to send blank/empty command.");
                    throw new ArgumentNullException("It's not allowed to send blank/empty command.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.Debug(ex.Message);
                return ex.Message;
            }


        }
        private string sendviahttp(Command command)
        {
            string endPoint = string.Format("{0}/{1}", oxdHttpUrl, command.CommandType.Replace("_","-"));
            var json = JsonConvert.SerializeObject(command.CommandParams);

            var protectionAccessToken = GetprotectionAccessTokenFromCommand(command);
           
            var client = new RestClient(endpoint: endPoint,
                             method: HttpVerb.POST,
                             postData: json);


            var response = client.MakeRequest(protectionAccessToken);
            return response;
        }

        private string GetprotectionAccessTokenFromCommand(Command command)
        {
            try
            {
                var protectionAccessToken = command.CommandParams.GetType().GetProperty("ProtectionAccessToken").GetValue(command.CommandParams, null);
                return protectionAccessToken;
            }
            catch(Exception ex)
            {

                return null;
            }
            
        }

        /// <summary>
        /// testing method to retrieve the Json object
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
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
