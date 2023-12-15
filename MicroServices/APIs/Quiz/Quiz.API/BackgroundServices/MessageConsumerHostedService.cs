using Domain.Common.RabbitMq;

namespace Quiz.API.BackgroundServices;

public class MessageConsumerHostedService(IMessageConsumerService consumerService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await consumerService.ReadMessagesAsync();
    }
}