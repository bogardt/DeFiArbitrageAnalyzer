using DeFiArbitrageAnalyzer.Abstraction;
using Microsoft.Extensions.Logging;

namespace DeFiArbitrageAnalyzer.Exchange;

public class PancakeswapClient : AbstractExchangeGraphClient
{
    public override string Name { get; } = "Pancakeswap";

    private readonly ILogger<PancakeswapClient> _logger;

    public PancakeswapClient(ILogger<PancakeswapClient> logger)
    {
        _logger = logger;
    }

}
