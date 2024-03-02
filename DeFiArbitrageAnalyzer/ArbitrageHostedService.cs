using DeFiArbitrageAnalyzer;
using DeFiArbitrageAnalyzer.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class ArbitrageHostedService : IHostedService
{
    private readonly ArbitrageService _arbitrageService;
    private readonly IServiceProvider _provider;

    public ArbitrageHostedService(ArbitrageService arbitrageService,
        IServiceProvider provider)
    {
        _arbitrageService = arbitrageService;
        _provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var exchanges = _provider.GetServices<AbstractExchangeGraphClient>();

        for (var i = 0; i < 10; i++)
        {
            await _arbitrageService.AnalyzeOpportunitiesAsync(exchanges);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
