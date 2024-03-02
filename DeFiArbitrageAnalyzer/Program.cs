using DeFiArbitrageAnalyzer;
using DeFiArbitrageAnalyzer.Abstraction;
using DeFiArbitrageAnalyzer.Exchange;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddSingleton<ArbitrageService>()
                .AddSingleton<AbstractExchangeGraphClient, PancakeswapClient>()
                .AddSingleton<AbstractExchangeGraphClient, UniswapClient>()
                .AddSingleton<AbstractExchangeGraphClient, SushiswapClient>()
                .AddHostedService<ArbitrageHostedService>());
static async Task Run(IServiceProvider services, string scope)
{
    Console.WriteLine($"{scope}...");

    using IServiceScope serviceScope = services.CreateScope();
}

using var host = builder.Build();

await Run(host.Services, "Analyzer is running...");

Console.WriteLine();

await host.RunAsync();

Console.ReadLine();