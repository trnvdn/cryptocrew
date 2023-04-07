using Newtonsoft.Json;

namespace TestTask
{
    public class Currency
    {
        public string id { get; set; }
        public int rank { get; set; }
        public string symbol { get; set; }
        public decimal supply { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? maxSupply { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal marketCapUsd { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal volumeUsd24Hr { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal priceUsd { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal changePercent24Hr { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal vwap24Hr { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string explorer { get; set; }
    }
}
