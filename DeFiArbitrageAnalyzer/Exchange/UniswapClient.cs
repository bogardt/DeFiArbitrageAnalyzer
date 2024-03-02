using DeFiArbitrageAnalyzer.Abstraction;
using Microsoft.Extensions.Logging;

namespace DeFiArbitrageAnalyzer.Exchange;

public class UniswapClient : AbstractExchangeGraphClient
{
    public override string Name { get; } = "Uniswap";

    private readonly ILogger<UniswapClient> _logger;

    public UniswapClient(ILogger<UniswapClient> logger)
    {
        _logger = logger;
    }
}
