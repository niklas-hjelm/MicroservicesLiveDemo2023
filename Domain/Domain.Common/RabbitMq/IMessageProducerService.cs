namespace Domain.Common.RabbitMq;

public interface IMessageProducerService
{
    Task SendMessageAsync<T>(T message);
}