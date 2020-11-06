using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection()){
                using (var channel = connection.CreateModel()){
                    channel.QueueDeclare(queue: "test",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    
                    string message = "Test Initial";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "test",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine("Message Sent: {0}", message);
                }
            }

            Console.WriteLine("Press a Key to exit.");
            Console.ReadKey();
        }
    }
}
