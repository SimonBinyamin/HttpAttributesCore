using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HttpAttributesCore.Service
{
    public class JsonUri<T>
    {
    
        public static T GetSingleFromJson(string url)
        {
            HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(url);
            GETRequest.Method = "GET";
            GETRequest.Headers.Add("Accept", "application/json");
            GETRequest.Headers.Add("user", "pass");
            var response = GETRequest.GetResponse() as HttpWebResponse;
            var dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(responseFromServer);
        }


        public static T GetSingleFromLocalJson(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }



        public static int ServerStatus(string url)
        {

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest
                                .Create(url);
                webRequest.Method = "GET";
                webRequest.Headers.Add("user", "pass");
                webRequest.AllowAutoRedirect = false;
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                var wRespStatusCode = (int)response.StatusCode;

                return wRespStatusCode;
            }
            catch (Exception)
            {

                return 400;
            }


        }
    }
}
