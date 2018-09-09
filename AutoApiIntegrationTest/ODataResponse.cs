using Newtonsoft.Json;
using System.Collections.Generic;

namespace AutoApiIntegrationTest
{
    public class ODataResponse<T>
    {
        [JsonProperty("@odata.count")]
        public int Count { get; set; }
        public List<T> Value { get; set; }
    }
}
