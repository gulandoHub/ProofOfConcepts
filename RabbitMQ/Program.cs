using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ;

internal abstract class Program
{
    private static void Main()
    {
        const string message = "Hello, RabbitMQ!";
        const string queueName = "testQueue";

        var factory = new ConnectionFactory
        { 
            HostName = "rabbit.gulando.com",
            UserName = "lilandx",
            Password = "mZ3ZxWbu"
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        // Send a message
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        Console.WriteLine("Sent message: {0}", message);

        // Receive a message
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (_, ea) =>
        {
            var receivedMessage = Encoding.UTF8.GetString(ea.Body.Span);
            Console.WriteLine("Received message: {0}", receivedMessage);
        };
        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        channel.QueueDelete(queueName);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }
}