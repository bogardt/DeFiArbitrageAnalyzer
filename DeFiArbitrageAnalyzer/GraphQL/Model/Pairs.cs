using Newtonsoft.Json;

namespace DeFiArbitrageAnalyzer.GraphQL.Model
{
    public class Pairs
    {
        [JsonProperty("pairs")]
        public List<Dictionary<string, object>> _Pairs { get;set; }
    }
}