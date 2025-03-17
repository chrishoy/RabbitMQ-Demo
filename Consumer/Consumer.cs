using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQDemo;

class Consumer
{
    public static async Task Main()
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

        await channel.QueueDeclareAsync(queue: "hello-queue",
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received: {message}");
            await channel.BasicAckAsync(ea.DeliveryTag, false);
        };

        await channel.BasicConsumeAsync(queue: "hello-queue",
                             autoAck: true,
                             consumer: consumer);

        Console.WriteLine(" [*] Waiting for messages. Press [Enter] to exit.");
        Console.ReadLine();
    }
}
