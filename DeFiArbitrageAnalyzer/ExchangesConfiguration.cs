using DeFiArbitrageAnalyzer.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeFiArbitrageAnalyzer
{
    public static class ExchangesConfiguration
    {
        public static string Symbol { get; set; } = "ETH";

        public static List<ExchangeMarket> ExchangeMarketConfigurations =
        [
                new()
                {
                    UrlEndPoint = "https://api.thegraph.com/subgraphs/name/pancakeswap/exhange-eth",
                    Symbol = Symbol,
                    Market = "Pancakeswap",
                    Token0 = "0xc02aaa39b223fe8d0a0e5c4f27ead9083c756cc2",
                    Token1 = "0xdac17f958d2ee523a2206206994597c13d831ec7"
                },
                new()
                {
                    UrlEndPoint = "https://api.thegraph.com/subgraphs/name/sushiswap/exchange",
                    Symbol = Symbol,
                    Market = "Sushiswap",
                    Token0 = "0xc02aaa39b223fe8d0a0e5c4f27ead9083c756cc2",
                    Token1 = "0xdac17f958d2ee523a2206206994597c13d831ec7"
                },
                new()
                {
                    UrlEndPoint = "https://api.thegraph.com/subgraphs/name/uniswap/uniswap-v2",
                    Symbol = Symbol,
                    Market = "Uniswap",
                    Token0 = "0xc02aaa39b223fe8d0a0e5c4f27ead9083c756cc2",
                    Token1 = "0xdac17f958d2ee523a2206206994597c13d831ec7"
                }
        ];
    }
}
