using NotificationService.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace NotificationService.Messaging
{
    public class RabbitMQSubscriber : IDisposable
    {
        private readonly INotificationService _service;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQSubscriber(INotificationService service)
        {
            _service = service;
        }

        public void Start()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "user_exchange", type: ExchangeType.Fanout);
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName, exchange: "user_exchange", routingKey: "");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var user = JsonSerializer.Deserialize<UserMessage>(message);

                if (user != null)
                {
                    _service.CreateNotification(user.Id, $"Welcome {user.Name}!");
                    Console.WriteLine($"[NotificationService] Notification created for {user.Name}");
                }
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            Console.WriteLine("[NotificationService] Listening for UserCreatedEvent...");
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }

        private class UserMessage
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
