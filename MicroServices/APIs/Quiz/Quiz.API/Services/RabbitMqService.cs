using Domain.Common.RabbitMq;
using Quiz.API.Services.Interfaces;
using RabbitMQ.Client;

namespace Quiz.API.Services;

public class RabbitMqService(RabbitMqConfiguration config) : IRabbitMqService
{
    public IConnection CreateConnection()
    {
        var connectionFactory = new ConnectionFactory()
        {
            HostName = config.HostName,
            UserName = config.Username,
            Password = config.Password,
            VirtualHost = "/",
            DispatchConsumersAsync = true
        };

        var connection = connectionFactory.CreateConnection();
        return connection;
    }
}