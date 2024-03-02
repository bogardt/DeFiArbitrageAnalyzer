using DeFiArbitrageAnalyzer.Abstraction;
using Microsoft.Extensions.Logging;

namespace DeFiArbitrageAnalyzer.Exchange;

public class SushiswapClient : AbstractExchangeGraphClient
{
    public override string Name { get; } = "Sushiswap";

    private readonly ILogger<SushiswapClient> _logger;

    public SushiswapClient(ILogger<SushiswapClient> logger)
    {
        _logger = logger;
    }
}
