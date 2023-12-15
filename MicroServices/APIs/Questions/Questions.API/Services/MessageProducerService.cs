using System.Text;
using System.Text.Json;
using Domain.Common.RabbitMq;
using RabbitMQ.Client;

namespace Questions.API.Services;

public class MessageProducerService(RabbitMqConfiguration config) : IMessageProducerService
{
    private readonly ConnectionFactory _connectionFactory = new()
    {
        HostName = config.HostName,
        UserName = config.Username, 
        Password = config.Password,
        VirtualHost = "/"
    };

    public async Task SendMessageAsync<T>(T message)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare("questionsQueue", durable: true, exclusive: false);

        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish("", "questionsQueue", body: body);
    }
}