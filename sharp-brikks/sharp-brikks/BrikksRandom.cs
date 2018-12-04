using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace sharpbrikks
{
    public class BrikksRandom
    {
        public string RandomOrgApiKey { get; set; }
        public BrikksRandomJsonUsageResponse.BrikksRandomJsonResult Usage { get; set; }

        public BrikksRandom()
        {
        }
        public BrikksRandom(string apiKey)
        {
            this.RandomOrgApiKey = apiKey;
            this.Usage = this.GetUsage();
        }

        /*
public async Task<string> GetAsync(string uri)
{
    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

    using(HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
    using(Stream stream = response.GetResponseStream())
    using(StreamReader reader = new StreamReader(stream))
    {
        return await reader.ReadToEndAsync();
    }
}
         * */

        public List<int> GenerateIntegers(int num, int min, int max, int col = 10)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.random.org/integers/?num={num}&min={min}&max={max}&col={col}&base=10&format=plain&rnd=new");
            string result = string.Empty;
            List<int> numberList = new List<int>();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            foreach (var line in result.Split('\n'))
            {
                if(!string.IsNullOrEmpty(line))
                {
                    foreach (var number in line.Split('\t'))
                    {
                        numberList.Add(int.Parse(number));
                    }
                }
            }

            return numberList;


            // delay?

            // check quota

            // time waited


            // 
        }

        /// <summary>
        /// Generates a single positive true random integer
        /// </summary>
        /// <param name="min">min random value</param>
        /// <param name="max">max random value</param>
        /// <returns>single int (-1 of error)</returns>
        public int GenerateSingleInteger(int min, int max)
        {
            if(this.Usage.Status == "running")
            {
                // Random.org API documentation
                // https://api.random.org/json-rpc/1/

                // generate random id to identify request
                int requestID = ThreadSafeRandom.ThisThreadsRandom.Next(999999999);

                try
                {
                    using (var webClient = new WebClient())
                    {
                        var json = "{\"jsonrpc\":\"2.0\",\"method\":\"generateIntegers\",\"params\":{\"apiKey\":\"" + RandomOrgApiKey + "\",\"n\":1,\"min\":" + min + ",\"max\":" + max + ",\"replacement\":true,\"base\":10},\"id\":" + requestID + "}";
                        string response = webClient.UploadString($"https://api.random.org/json-rpc/1/invoke", "POST", json);

                        var jsonObject = JObject.Parse(response);
                        var resultobject = jsonObject.ToObject<BrikksRandomJsonIntegerResponse>();

                        if (resultobject.ID == requestID)
                        {
                            return resultobject.Result.Random.Data[0];
                        }
                        else
                        {
                            // Fallback -> use .Net Random
                            return ThreadSafeRandom.ThisThreadsRandom.Next(min, max);
                        }
                    }
                }
                catch (Exception)
                {
                    // Fallback -> use .Net Random
                    return ThreadSafeRandom.ThisThreadsRandom.Next(min, max);
                }
            }
            else
            {
                return ThreadSafeRandom.ThisThreadsRandom.Next(min, max);
            }
        }

        public BrikksRandomJsonUsageResponse.BrikksRandomJsonResult GetUsage()
        {
            // Random.org API documentation
            // https://api.random.org/json-rpc/1/

            // generate random id to identify request
            int requestID = ThreadSafeRandom.ThisThreadsRandom.Next(999999999);

            try
            {
                using (var webClient = new WebClient())
                {
                    var json = "{\"jsonrpc\":\"2.0\",\"method\":\"getUsage\",\"params\":{\"apiKey\":\"" + RandomOrgApiKey + "\"},\"id\":" + requestID + "}";
                    string response = webClient.UploadString($"https://api.random.org/json-rpc/1/invoke", "POST", json);

                    var jsonObject = JObject.Parse(response);
                    var resultobject = jsonObject.ToObject<BrikksRandomJsonUsageResponse>();

                    if (resultobject.ID == requestID)
                    {
                        return resultobject.Result;
                    }
                }
            }
            catch (Exception)
            {
                // TODO error handling
                // Fallback -> use .Net Random
                //return ThreadSafeRandom.ThisThreadsRandom.Next(min, max);
            }
            finally
            {
                
            }

            return null;
        }
    }
}
