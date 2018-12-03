using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBrikks
{
    /*
        {
            "jsonrpc": "2.0",
            "result": {
                "status": "running",
                "creationTime": "2013-02-01 17:53:40Z",
                "bitsLeft": 1681187,
                "requestsLeft": 388982,
                "totalBits": 288501073,
                "totalRequests": 5595390
            },
            "id": 26713
        }
    */
    public class BrikksRandomJsonUsageResponse
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpc { get; set; }

        [JsonProperty("result")]
        public BrikksRandomJsonResult Result { get; set; }

        [JsonProperty("id")]
        public int ID { get; set; }

        public class BrikksRandomJsonResult
        {
            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("creationTime")]
            public DateTimeOffset CreationTime { get; set; }

            [JsonProperty("bitsLeft")]
            public int BitsLeft { get; set; }

            [JsonProperty("requestsLeft")]
            public int RequestsLeft { get; set; }

            [JsonProperty("totalBits")]
            public int TotalBits { get; set; }

            [JsonProperty("totalRequests")]
            public int TotalRequests { get; set; }
        }
    }
}
