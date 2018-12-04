using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpbrikks
{
    public class BrikksRandomJsonIntegerResponse
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpc { get; set; }

        [JsonProperty("result")]
        public BrikksRandomJsonResult Result { get; set; }

        [JsonProperty("id")]
        public int ID { get; set; }

        public class BrikksRandomJsonResult
        {
            [JsonProperty("random")]
            public BrikksRandomJsonData Random { get; set; }

            [JsonProperty("bitsUsed")]
            public int BitsUsed { get; set; }

            [JsonProperty("bitsLeft")]
            public int BitsLeft { get; set; }

            [JsonProperty("requestsLeft")]
            public int RequestsLeft { get; set; }

            [JsonProperty("advisoryDelay")]
            public int AdvisoryDelay { get; set; }

            public class BrikksRandomJsonData
            {
                [JsonProperty("data")]
                public List<int> Data { get; set; }

                [JsonProperty("completionTime")]
                public DateTimeOffset CompletionTime { get; set; }
            }
        }
    }
}
