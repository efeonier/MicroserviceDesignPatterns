using MassTransit;
using Shared.SagaOrchestration.Messages;
using Stock.API.Repositories.Interface;

namespace Stock.API.Consumers;

public class StockRollBackMessageConsumer : IConsumer<IStockRollBackMessage>
{
    private readonly IStockRepository _stockRepository;
    private readonly ILogger<StockRollBackMessageConsumer> _logger;
    public StockRollBackMessageConsumer(IStockRepository stockRepository, ILogger<StockRollBackMessageConsumer> logger)
    {
        _stockRepository = stockRepository;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<IStockRollBackMessage> context)
    {
        foreach (var item in context.Message.OrderItems)
        {
            var stock = await _stockRepository.GetByIdAsync(item.ProductId);
            if (stock != null)
            {
                stock.Quantity += item.Count;
                await _stockRepository.UpdateAsync(stock.Id, stock);
            }
        }
        _logger.LogInformation($"Stock was released");
    }
}