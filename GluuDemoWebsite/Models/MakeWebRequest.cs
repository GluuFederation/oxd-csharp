using CSharp.CommonClasses;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace GluuDemoWebsite.Models
{
    public class MakeWebRequest<T>
    {
        string contentType = "application/x-www-form-urlencoded";

        public string MakeRequest(string endpoint, HttpVerb method, string rpt, T postData)
        {
            var request = (HttpWebRequest)WebRequest.Create(endpoint);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            // allows for validation of SSL conversations
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            request.Method = method.ToString();
            //request.ContentLength = 0;
            request.ContentType = contentType;

            if (!string.IsNullOrEmpty(rpt))
                request.Headers.Add("RPT", rpt);

            if (postData != null && method == HttpVerb.POST)
            {
                var encoding = new UTF8Encoding();
                string jsonObj = JsonConvert.SerializeObject((object)postData);
                byte[] bytes = Encoding.ASCII.GetBytes(jsonObj);
                request.ContentLength = bytes.Length;
                request.AllowWriteStreamBuffering = true;
                try
                {
                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            var responseValue = string.Empty;
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (response.Headers.GetValues("ticket") != null)
                        {
                            responseValue = "Unauthorized;ticket:" + response.Headers.GetValues("ticket").First();
                            return responseValue;
                        }
                        

                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();

                            }
                    }

                    return responseValue;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {

                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        responseValue = httpResponse.StatusCode.ToString() + ";ticket:" + httpResponse.Headers.GetValues("ticket").First();
                    }
                    else
                    {
                        responseValue = "Error occurred";
                    }

                    return responseValue;
                }
            }
        }
        
    }
}