using DeFiArbitrageAnalyzer.Abstraction;
using DeFiArbitrageAnalyzer.GraphQL.Model;
using System.Collections.Concurrent;

namespace DeFiArbitrageAnalyzer;

public class ArbitrageService
{
    public static string Symbol { get; set; } = "ETH";

    public List<ExchangeMarket> _exchangeMarketConfigurations = new List<ExchangeMarket>
    {
            new ExchangeMarket()
            {
                UrlEndPoint = "https://api.thegraph.com/subgraphs/name/pancakeswap/exhange-eth",
                Symbol = Symbol,
                Market = "Pancakeswap",
                Token0 = "0xc02aaa39b223fe8d0a0e5c4f27ead9083c756cc2",
                Token1 = "0xdac17f958d2ee523a2206206994597c13d831ec7"
            },
            new ExchangeMarket()
            {
                UrlEndPoint = "https://api.thegraph.com/subgraphs/name/sushiswap/exchange",
                Symbol = Symbol,
                Market = "Sushiswap",
                Token0 = "0xc02aaa39b223fe8d0a0e5c4f27ead9083c756cc2",
                Token1 = "0xdac17f958d2ee523a2206206994597c13d831ec7"
            },
            new ExchangeMarket()
            {
                UrlEndPoint = "https://api.thegraph.com/subgraphs/name/uniswap/uniswap-v2",
                Symbol = Symbol,
                Market = "Uniswap",
                Token0 = "0xc02aaa39b223fe8d0a0e5c4f27ead9083c756cc2",
                Token1 = "0xdac17f958d2ee523a2206206994597c13d831ec7"
            }
    };

    public async Task AnalyzeOpportunitiesAsync(IEnumerable<AbstractExchangeGraphClient> exchangeClients)
    {
        var marketExchangesArbitrageBook = new ConcurrentDictionary<string, ConcurrentBag<ExchangeMarket>>();

        foreach (var exchange in _exchangeMarketConfigurations)
            marketExchangesArbitrageBook[exchange.Market] = [exchange];


        await Parallel.ForEachAsync(exchangeClients,
            new ParallelOptions { MaxDegreeOfParallelism = 10 },
            async (client, cancellationToken) =>
        {
            var isSuccess = marketExchangesArbitrageBook[client.Name].TryPeek(out var data);
            if (!isSuccess) ;
            //
            await client.GetPairsPricesAsync(data!);

        });

        SeekOpportunity(marketExchangesArbitrageBook);
    }

    public void SeekOpportunity(ConcurrentDictionary<string, ConcurrentBag<ExchangeMarket>> marketExchangesArbitrageBook)
    {
        foreach (var symbolGroup in marketExchangesArbitrageBook
            .SelectMany(d => d.Value)
            .GroupBy(m => m.Symbol))
        {
            var bestBuy = symbolGroup.MinBy(m => m.Price);
            var bestSell = symbolGroup.MaxBy(m => m.Price);

            if (bestBuy.Price < bestSell.Price)
            {
                Console.WriteLine($"Arbitrage opportunity for {Symbol}: Buy on {bestBuy.Market}" +
                    $" at {bestBuy.Price}, sell on {bestSell.Market} at {bestSell.Price}.");

            }
        }
    }
}
