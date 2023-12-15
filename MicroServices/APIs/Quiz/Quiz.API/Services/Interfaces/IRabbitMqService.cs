using RabbitMQ.Client;

namespace Quiz.API.Services.Interfaces;

public interface IRabbitMqService
{
    IConnection CreateConnection();
}