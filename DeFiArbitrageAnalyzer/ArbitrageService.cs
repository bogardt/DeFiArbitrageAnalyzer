using DeFiArbitrageAnalyzer.Abstraction;
using DeFiArbitrageAnalyzer.GraphQL.Model;
using System.Collections.Concurrent;

namespace DeFiArbitrageAnalyzer;

public class ArbitrageService
{
    public async Task AnalyzeOpportunitiesAsync(IEnumerable<AbstractExchangeGraphClient> exchangeClients)
    {
        var marketExchangesArbitrageBook = new ConcurrentDictionary<string, ConcurrentBag<ExchangeMarket>>();

        foreach (var exchange in ExchangesConfiguration.ExchangeMarketConfigurations)
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
                Console.WriteLine($"Arbitrage opportunity for {ExchangesConfiguration.Symbol}: Buy on {bestBuy.Market}" +
                    $" at {bestBuy.Price}, sell on {bestSell.Market} at {bestSell.Price}.");

            }
        }
    }
}
