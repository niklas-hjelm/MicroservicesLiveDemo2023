namespace Domain.Common.RabbitMq;

public interface IMessageConsumerService
{
    Task ReadMessagesAsync();
}