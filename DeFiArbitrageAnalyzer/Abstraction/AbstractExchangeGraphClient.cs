using DeFiArbitrageAnalyzer.GraphQL.Model;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;

namespace DeFiArbitrageAnalyzer.Abstraction;
public abstract class AbstractExchangeGraphClient
{
    public abstract string Name { get; }

    public virtual async Task<Pairs> GetPairsPricesAsync(ExchangeMarket data)
    {
        using var graphQLClient = new GraphQLHttpClient(data.UrlEndPoint, new NewtonsoftJsonSerializer());

        var request = new GraphQLHttpRequest
        {
            Query = @"
                {
                    pairs(where: {token0: """ + data.Token0 + @""", token1: """ + data.Token1 + @"""}) {
                        token0Price
                        token1Price
                    }
                }"
        };

        var response = await graphQLClient.SendQueryAsync<Pairs>(request);
        if (response.Errors is not null)
        {
            Console.WriteLine(response.Errors.ToString());
        }

        var pairs = response.Data;
        //Console.WriteLine($"{Name} " + JsonConvert.SerializeObject(pairs));

        var amount = decimal.Parse(pairs._Pairs[0]["token1Price"].ToString()!);
        data.Price = amount;
        
        return pairs;
    }

}
