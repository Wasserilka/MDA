using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messaging
{
    public class Consumer
    {
        private IConnection Connection { get; }
        private IModel Channel { get; }
        private string HostName { get; }

        public Consumer(string hostName)
        {
            HostName = hostName;
            var factory = new ConnectionFactory() { HostName = HostName };
            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();
        }

        public void Receive (EventHandler<BasicDeliverEventArgs> receiveCallback)
        {
            Channel.ExchangeDeclare("broadcast_exchange", ExchangeType.Fanout);
            var queueName = Channel.QueueDeclare().QueueName;
            Channel.QueueBind(queueName, "broadcast_exchange", queueName);

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += receiveCallback;

            Channel.BasicConsume(queueName, true, consumer);
        }
    }
}