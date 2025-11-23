using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using UserService.Models;

public class RabbitMQPublisher : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQPublisher()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: "user_exchange", type: ExchangeType.Fanout);
    }

    public void PublishUserCreated(User user)
    {
        var message = JsonSerializer.Serialize(user);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "user_exchange", routingKey: "", body: body);
        Console.WriteLine($"[UserService] Published UserCreatedEvent for {user.Name}");
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}
