using RabbitMQ.Client;
using System.Text;

namespace Messaging
{
    public class Producer
    {
        private string HostName { get; }

        public Producer(string hostName)
        {
            HostName = hostName;
        }

        public void Send(string message)
        {
            var factory = new ConnectionFactory() { HostName = HostName };
            using var Connection = factory.CreateConnection();
            using var Channel = Connection.CreateModel();

            Channel.ExchangeDeclare("broadcast_exchange", ExchangeType.Fanout);

            var body = Encoding.UTF8.GetBytes(message);

            Channel.BasicPublish("broadcast_exchange", "", null, body);
        }
    }
}
