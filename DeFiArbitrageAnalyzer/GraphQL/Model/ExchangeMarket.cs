namespace DeFiArbitrageAnalyzer.GraphQL.Model;

public class ExchangeMarket
{
    public string UrlEndPoint { get; set; }
    public string Market { get; set; }
    public string Symbol { get; set; }
    public decimal Price { get; set; }
    public string Token0 { get; set; }
    public string Token1 { get; set; }
}
