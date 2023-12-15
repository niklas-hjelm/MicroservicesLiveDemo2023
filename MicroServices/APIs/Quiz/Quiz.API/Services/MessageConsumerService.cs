using System.Text;
using Domain.Common.RabbitMq;
using Quiz.API.Services.Interfaces;
using Quiz.DataAccess;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Quiz.API.Services;

public class MessageConsumerService : IMessageConsumerService, IDisposable
{
    private const string QueueName = "questionsQueue";
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageConsumerService(IRabbitMqService rabbitMqService)
    {
        _connection = rabbitMqService.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(QueueName, durable: true, exclusive: false);
        _channel.ExchangeDeclare("saffran", ExchangeType.Fanout, durable: true);
        _channel.QueueBind(QueueName, "saffran", string.Empty);
    }

    public async Task ReadMessagesAsync()
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (sender, e) =>
        {
            var body = e.Body.ToArray();
            var text = Encoding.UTF8.GetString(body);
            Console.WriteLine(text);
            await Task.CompletedTask;
            _channel.BasicAck(e.DeliveryTag, false);
        };

        _channel.BasicConsume(QueueName, false, consumer);
        await Task.CompletedTask;
    }

    public void Dispose()
    {
        if (_channel.IsOpen) _channel.Close();
        if (_connection.IsOpen) _connection.Close();
    }
}