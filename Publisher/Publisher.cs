using RabbitMQ.Client;
using System.Text;

namespace RabbitMQDemo;

class Publisher
{
    public async static Task Main()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "admin",
            Password = "admin",
        };
        
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "hello-queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        for (int i = 1; i <= 5; i++)
        {
            string message = $"Hello RabbitMQ! Message {i}";
            var body = Encoding.UTF8.GetBytes(message);
            var props = new BasicProperties { ContentType = "text/plain", DeliveryMode = DeliveryModes.Persistent };
            props.Persistent = true;

            await channel.BasicPublishAsync(exchange: "",
                                 routingKey: "hello-queue",
                                 mandatory: false,
                                 basicProperties: props,
                                 body: body);

            Console.WriteLine($" [x] Sent: {message}");
        }

        Console.WriteLine("Press [Enter] to exit.");
        Console.ReadLine();
    }
}
