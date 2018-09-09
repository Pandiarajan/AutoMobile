using Newtonsoft.Json;
using System.Collections.Generic;

namespace AutoApiIntegrationTest
{
    internal class ODataResponse<T>
    {
        [JsonProperty("@odata.count")]
        public int Count { get; set; }
        public List<T> Value { get; set; }
    }
}
